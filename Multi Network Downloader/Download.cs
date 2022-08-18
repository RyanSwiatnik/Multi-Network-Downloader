using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Multi_Network_Downloader
{
    class Download
    {
        /// <summary>
        /// Size of file pieces in bytes.
        /// </summary>
        const int PIECE_SIZE = 4000000;

        private readonly int threadCount;
        private readonly List<IPEndPoint> adapters;
        private readonly string url;
        private readonly string saveLocation;

        private DownloadFile file;
        private int partsCount;

        private readonly IProgress<int> downloadProgress;
        private readonly IProgress<int> saveProgress;
        private readonly IProgress<string> downloadStatus;

        public Download(int threadCount, List<IPEndPoint> adapters, string url, string saveLocation, IProgress<int> downloadProgress, IProgress<int> saveProgress, IProgress<string> downloadStatus)
        {
            this.threadCount = threadCount;
            this.adapters = adapters;
            this.url = url;
            this.saveLocation = saveLocation;
            this.downloadProgress = downloadProgress;
            this.saveProgress = saveProgress;
            this.downloadStatus = downloadStatus;
        }

        public void startDownload()
        {
            try
            {
                if (adapters.Count == 0)
                {
                    throw new Exception("Network adapter not selected.");
                }

                long range = getRange(url);
                Console.WriteLine(range);
                partsCount = Decimal.ToInt32(Math.Ceiling(range / (Decimal)PIECE_SIZE));

                downloadStatus.Report("Downloading...");
                file = new DownloadFile(partsCount, saveLocation, downloadProgress, saveProgress);

                //Create threads
                Task[] tasks = new Task[threadCount];
                for (int i = 0; i < threadCount; i++)
                {
                    int currenti = i;

                    tasks[currenti] = Task.Factory.StartNew(() => downloadWorker(adapters[currenti % adapters.Count]));
                }

                Thread saveThread = new Thread(() => file.startSaving(url))
                {
                    Name = "File saver",
                    IsBackground = true
                };
                saveThread.Start();

                Task.WaitAll(tasks);

                Console.WriteLine("Download Complete");

                if (saveThread.Join(10000))
                {
                    downloadStatus.Report("Download Complete!");
                }
                else
                {
                    downloadStatus.Report("Download Timeout.");
                    saveThread.Abort();
                }
            }
            catch (Exception ex)
            {
                downloadStatus.Report($"Download Error: {ex.Message}");
                Console.WriteLine(ex.ToString());
            }
        }

        private void downloadWorker(IPEndPoint adapter)
        {
            int? partPosition = file.getPart();
            int failCount = 0;

            while (partPosition != null)
            {
                try
                {
                    file.setPart(downloadPart(adapter, (int)partPosition), (int)partPosition);
                }
                catch
                {
                    file.failPart((int)partPosition);
                    failCount++;
                    Console.WriteLine($"Error downloaded part {partPosition}/{(partsCount - 1)} using {adapter}");
                }

                if (failCount < 3)
                {
                    partPosition = file.getPart();
                }
                else
                {
                    partPosition = null;
                }
            }
        }

        private byte[] downloadPart(IPEndPoint ipEndPoint, long partPosition)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ServicePoint.BindIPEndPointDelegate = (servicePoint, remoteEndPoint, retryCount) =>
            {
                if (retryCount < 3)
                {
                    return ipEndPoint;
                }
                else
                {
                    Console.WriteLine($"Failed binding to {ipEndPoint.Address}");
                    return null;
                }
            };
            request.AddRange(partPosition * PIECE_SIZE, partPosition * PIECE_SIZE + (PIECE_SIZE - 1));

            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:103.0) Gecko/20100101 Firefox/103.0";

            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            request.ContentLength = 0;
            request.KeepAlive = false;
            request.Timeout = 10000;

            WebResponse res = request.GetResponse();

            Stream streamResponse = res.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);


            var bytes = default(byte[]);
            using (var memstream = new MemoryStream())
            {
                streamRead.BaseStream.CopyTo(memstream);
                bytes = memstream.ToArray();
            }

            streamRead.Close();
            streamResponse.Close();
            res.Close();

            Console.WriteLine($"Downloaded part {partPosition}/{(partsCount - 1)} using {ipEndPoint}");

            return bytes;
        }

        private long getRange(String url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:103.0) Gecko/20100101 Firefox/103.0";
            request.Timeout = 10000;

            WebResponse res = request.GetResponse();

            WebHeaderCollection headers = res.Headers;

            return long.Parse(headers.Get("Content-Length"));
        }
    }
}
