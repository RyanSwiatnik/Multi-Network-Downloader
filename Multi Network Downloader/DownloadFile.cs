using System;
using System.Linq;
using System.Threading;
using System.IO;
using Microsoft.Win32;

namespace Multi_Network_Downloader
{
    class DownloadFile
    {
        private FilePiece[] pieces;

        public DownloadFile(int partsCount)
        {
            pieces = new FilePiece[partsCount];
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

        }

        public void startSaving(string url)
        {
            bool partsRemaining = true;
            int i = 0;

            //TODO Add existing file check.
            FileStream file = new FileStream(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString() + "\\" + url.Split('/').Last(), FileMode.Append);

            while (partsRemaining)
            {
                while (i < pieces.Length && pieces[i] != null && pieces[i].state == PieceState.Downloaded)
                {
                    //Save
                    byte[] data = pieces[i].getData();
                    file.Write(data, 0, data.Length);

                    pieces[i].setSaved();
                    Console.WriteLine("Saved part " + i);

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