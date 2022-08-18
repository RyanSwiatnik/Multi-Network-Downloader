using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using Microsoft.Win32;

namespace Multi_Network_Downloader
{
    public partial class MainForm : Form
    {
        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

        Progress<int> downloadProgress;
        Progress<int> saveProgress;
        Progress<string> downloadStatus;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (NetworkInterface adapter in adapters)
            {
                interfaceList.Items.Add(adapter.Name);
            }

            interfaceList.SetSelected(0, true);

            saveLocation.Text = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();

            saveProgress = new Progress<int>(value => saveProgressBar.Value = value);
            downloadProgress = new Progress<int>(value => downloadProgressBar.Value = value);

            downloadStatus = new Progress<string>(value => downloadStatusLabel.Text = value);
        }

        private void download_Click(object sender, EventArgs e)
        {
            List<IPEndPoint> selectedAdapters = new List<IPEndPoint>();
            for (int i = 0; i < interfaceList.SelectedIndices.Count; i++)
            {
                foreach (UnicastIPAddressInformation ip in adapters[interfaceList.SelectedIndices[i]].GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        selectedAdapters.Add(new IPEndPoint(ip.Address, 0));
                    }
                }
            }

            downloadProgressBar.Value = 0;
            saveProgressBar.Value = 0;

            Download download = new Download((int)threadCount.Value, selectedAdapters, url.Text, saveLocation.Text, downloadProgress, saveProgress, downloadStatus);
            Thread downloadThread = new Thread(download.startDownload);
            downloadThread.Name = "Download Manager";
            downloadThread.Start();
        }

        private void selectSaveLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
            {
                saveLocation.Text = folderDialog.SelectedPath;
            }
        }
    }
}
