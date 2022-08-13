using System;
using System.Linq;
using System.Threading;
using System.IO;

namespace Multi_Network_Downloader
{
    class DownloadFile
    {
        private FilePiece[] pieces;
        private readonly string saveLocation;

        private int downloadedPieces = 0;
        private int savedPieces = 0;
        private readonly IProgress<int> downloadProgress;
        private readonly IProgress<int> saveProgress;

        public DownloadFile(int partsCount, string saveLocation, IProgress<int> downloadProgress, IProgress<int> saveProgress)
        {
            pieces = new FilePiece[partsCount];
            this.saveLocation = saveLocation;
            this.downloadProgress = downloadProgress;
            this.saveProgress = saveProgress;
        }

        public int? getPart()
        {
            //TODO Stop returning parts when out of memory.
            lock (pieces)
            {
                for (int i = 0; i < pieces.Length; i++)
                {
                    if (pieces[i] == null)
                    {
                        pieces[i] = new FilePiece();
                        return i;
                    }
                }
            }

            return null;
        }

        public void setPart(byte[] bytes, int partPosition)
        {
            pieces[partPosition].setData(bytes);

            downloadedPieces++;
            double downloadPercentage = (double)downloadedPieces / (double)pieces.Length * 1000;
            downloadProgress.Report((int)Math.Round(downloadPercentage));
        }

        public void failPart(int partPosition)
        {
            pieces[partPosition] = null;
        }

        public void startSaving(string url)
        {
            bool partsRemaining = true;
            int i = 0;

            //Check if file with identical name already exists.
            string fileName = saveLocation + "\\" + url.Split('/').Last();
            string newFileName = fileName;
            int fileSuffix = 1;
            while (File.Exists(newFileName))
            {
                int suffixPosition = fileName.LastIndexOf('.');

                if (suffixPosition != -1)
                {
                    newFileName = fileName.Insert(suffixPosition, $"({fileSuffix})");
                }
                else
                {
                    newFileName = fileName + $"({fileSuffix})";
                }

                fileSuffix++;
            }

            FileStream file = new FileStream(newFileName, FileMode.Append);

            while (partsRemaining)
            {
                while (i < pieces.Length && pieces[i] != null && pieces[i].state == PieceState.Downloaded)
                {
                    //Save
                    byte[] data = pieces[i].getData();
                    file.Write(data, 0, data.Length);

                    pieces[i].setSaved();

                    savedPieces++;
                    double savePercentage = (double)savedPieces / (double)pieces.Length * 1000;
                    saveProgress.Report((int)Math.Round(savePercentage));

                    Console.WriteLine("Saved part " + i + "/" + (pieces.Length - 1));

                    if (i == pieces.Length - 1)
                    {
                        partsRemaining = false;
                    }
                    i++;
                }

                Thread.Sleep(100);
            }
            file.Close();

            Console.WriteLine("Save Complete");
        }
    }
}