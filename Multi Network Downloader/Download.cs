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
        const int PIECE_SIZE = 4000000;

        private int threadCount;
        private readonly List<IPAddress> adapters;
        private readonly string url;

        private DownloadFile file;
        private int partsCount;

        public Download(int threadCount, List<IPAddress> adapters, string url)
        {
            this.threadCount = threadCount;
            this.adapters = adapters;
            this.url = url;
        }

        public void startDownload()
        {
            long range = getRange(url);
            Console.WriteLine(range);
            partsCount = Decimal.ToInt32(Math.Ceiling(range / (Decimal)PIECE_SIZE));

            file = new DownloadFile(partsCount);

            //Create threads
            Task[] tasks = new Task[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                int currenti = i;

                tasks[currenti] = Task.Factory.StartNew(() => downloadWorker(adapters[currenti % adapters.Count]));
            }

            Thread saveThread = new Thread(() => file.startSaving(url));
            saveThread.Name = "File saver";
            saveThread.Start();

            Task.WaitAll(tasks);

            Console.WriteLine("Download Complete");
        }

        private void downloadWorker(IPAddress adapter)
        {
            int? partPosition = file.getPart();

            while (partPosition != null)
            {
                try
                {
                    file.setPart(downloadPart(adapter, (int)partPosition), (int)partPosition);
                }
                catch
                {
                    Console.WriteLine("Error downloading part " + partPosition + "/" + (partsCount - 1));
                }

                partPosition = file.getPart();
            }
        }

        private byte[] downloadPart(IPAddress ipAddress, long partPosition)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ServicePoint.BindIPEndPointDelegate = (servicePoint, remoteEndPoint, retryCount) => new IPEndPoint(ipAddress, 0);
            request.AddRange(partPosition * PIECE_SIZE, partPosition * PIECE_SIZE + (PIECE_SIZE - 1));

            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:103.0) Gecko/20100101 Firefox/103.0";

            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;

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

            Console.WriteLine("Downloaded part " + partPosition + "/" + (partsCount - 1));

            return bytes;
        }

        private long getRange(String url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:103.0) Gecko/20100101 Firefox/103.0";
            //TODO create timeout.
            WebResponse res = request.GetResponse();

            WebHeaderCollection headers = res.Headers;

            return long.Parse(headers.Get("Content-Length"));
        }
    }
}
