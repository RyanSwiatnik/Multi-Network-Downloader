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
            foreach (NetworkInterface adapter in adapters) {
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
            
            Download download = new Download(4000000, selectedAdapters, url.Text);
            new Thread(download.startDownload).Start();
        }
    }
}
