using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;

namespace Multi_Network_Downloader
{
    public partial class MainForm : Form
    {
        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

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
        }

        private void download_Click(object sender, EventArgs e)
        {
            List<IPAddress> selectedAdapters = new List<IPAddress>();
            for (int i = 0; i < interfaceList.SelectedIndices.Count; i++)
            {
                foreach (UnicastIPAddressInformation ip in adapters[interfaceList.SelectedIndices[i]].GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        selectedAdapters.Add(ip.Address);
                    }
                }
            }

            downloadProgressBar.Value = 0;
            Progress<int> downloadProgress = new Progress<int>(value =>
            {
                downloadProgressBar.Value = value;
            });

            saveProgressBar.Value = 0;
            Progress<int> saveProgress = new Progress<int>(value =>
            {
                saveProgressBar.Value = value;
            });

            Download download = new Download((int)threadCount.Value, selectedAdapters, url.Text, saveLocation.Text, downloadProgress, saveProgress);
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
