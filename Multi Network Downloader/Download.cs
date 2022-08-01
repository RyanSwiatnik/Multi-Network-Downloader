using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Microsoft.Win32;

namespace Multi_Network_Downloader
{
    class Download
    {
        private readonly int partSize;
        private readonly List<IPAddress> adapters;
        private readonly string url;

        private int partsCount;

        private byte[] parts;

        public Download(int partSize, List<IPAddress> adapters, string url)
        {
            this.partSize = partSize;
            this.adapters = adapters;
            this.url = url;
        }

        public void startDownload()
        {
            int range = getRange(url);
            Console.WriteLine(range);
            partsCount = Decimal.ToInt32(Math.Ceiling(range / (Decimal)partSize));

            parts = new byte[range];

            //Create threads
            Task[] tasks = new Task[partsCount];
            for (int i = 0; i < partsCount; i++)
            {
                int currenti = i;

                IPAddress adapter = adapters[0];
                if (currenti % 10 == 0)
                {
                    adapter = adapters[1];
                }
                tasks[currenti] = Task.Factory.StartNew(() => downloadPart(adapter, currenti));
            }
            Task.WaitAll(tasks);
            

            File.WriteAllBytes(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString() + "\\" + url.Split('/').Last(), parts);
            Console.WriteLine("Download Complete");
        }

        private void downloadPart(IPAddress ipAddress, int partPosition)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ServicePoint.BindIPEndPointDelegate = (servicePoint, remoteEndPoint, retryCount) => new IPEndPoint(ipAddress, 0);
            request.AddRange(partPosition * partSize, partPosition * partSize + (partSize - 1));

            try
            {
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

                Buffer.BlockCopy(bytes, 0, parts, partPosition * partSize, bytes.Length);
                Console.WriteLine("Downloaded part " + partPosition + "/" + (partsCount - 1));
            } catch
            {
                Console.WriteLine("Error downloading part " + partPosition + "/" + (partsCount - 1));
                downloadPart(ipAddress, partPosition);
            }
        }

        private int getRange(String url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse res = request.GetResponse();

            WebHeaderCollection headers = res.Headers;

            for (int i = 0; i < headers.Count; i++)
            {
                if (headers.Keys[i] == "Content-Length")
                {
                    return int.Parse(headers[i]);
                }
            }

            return 0;
        }
    }
}
