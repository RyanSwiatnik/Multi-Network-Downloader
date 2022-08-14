namespace Multi_Network_Downloader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.interfaceList = new System.Windows.Forms.ListBox();
            this.download = new System.Windows.Forms.Button();
            this.url = new System.Windows.Forms.TextBox();
            this.downloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.saveProgressBar = new System.Windows.Forms.ProgressBar();
            this.threadCount = new System.Windows.Forms.NumericUpDown();
            this.selectSaveLocation = new System.Windows.Forms.Button();
            this.saveLocation = new System.Windows.Forms.TextBox();
            this.threadsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.threadCount)).BeginInit();
            this.SuspendLayout();
            // 
            // interfaceList
            // 
            this.interfaceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.interfaceList.FormattingEnabled = true;
            this.interfaceList.Location = new System.Drawing.Point(12, 128);
            this.interfaceList.Name = "interfaceList";
            this.interfaceList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.interfaceList.Size = new System.Drawing.Size(234, 173);
            this.interfaceList.TabIndex = 6;
            // 
            // download
            // 
            this.download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.download.Location = new System.Drawing.Point(429, 40);
            this.download.Name = "download";
            this.download.Size = new System.Drawing.Size(84, 23);
            this.download.TabIndex = 3;
            this.download.Text = "Download";
            this.download.UseVisualStyleBackColor = true;
            this.download.Click += new System.EventHandler(this.download_Click);
            // 
            // url
            // 
            this.url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.url.Location = new System.Drawing.Point(12, 41);
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(411, 20);
            this.url.TabIndex = 2;
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadProgressBar.Location = new System.Drawing.Point(12, 70);
            this.downloadProgressBar.Maximum = 1000;
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(500, 23);
            this.downloadProgressBar.TabIndex = 4;
            // 
            // saveProgressBar
            // 
            this.saveProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveProgressBar.Location = new System.Drawing.Point(12, 99);
            this.saveProgressBar.Maximum = 1000;
            this.saveProgressBar.Name = "saveProgressBar";
            this.saveProgressBar.Size = new System.Drawing.Size(500, 23);
            this.saveProgressBar.TabIndex = 5;
            // 
            // threadCount
            // 
            this.threadCount.Location = new System.Drawing.Point(307, 128);
            this.threadCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.threadCount.Name = "threadCount";
            this.threadCount.Size = new System.Drawing.Size(71, 20);
            this.threadCount.TabIndex = 8;
            this.threadCount.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // selectSaveLocation
            // 
            this.selectSaveLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectSaveLocation.Location = new System.Drawing.Point(429, 11);
            this.selectSaveLocation.Name = "selectSaveLocation";
            this.selectSaveLocation.Size = new System.Drawing.Size(84, 23);
            this.selectSaveLocation.TabIndex = 1;
            this.selectSaveLocation.Text = "Save Location";
            this.selectSaveLocation.UseVisualStyleBackColor = true;
            this.selectSaveLocation.Click += new System.EventHandler(this.selectSaveLocation_Click);
            // 
            // saveLocation
            // 
            this.saveLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveLocation.Location = new System.Drawing.Point(13, 12);
            this.saveLocation.Name = "saveLocation";
            this.saveLocation.Size = new System.Drawing.Size(410, 20);
            this.saveLocation.TabIndex = 0;
            // 
            // threadsLabel
            // 
            this.threadsLabel.AutoSize = true;
            this.threadsLabel.Location = new System.Drawing.Point(252, 130);
            this.threadsLabel.Name = "threadsLabel";
            this.threadsLabel.Size = new System.Drawing.Size(49, 13);
            this.threadsLabel.TabIndex = 7;
            this.threadsLabel.Text = "Threads:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 314);
            this.Controls.Add(this.threadsLabel);
            this.Controls.Add(this.saveLocation);
            this.Controls.Add(this.selectSaveLocation);
            this.Controls.Add(this.threadCount);
            this.Controls.Add(this.saveProgressBar);
            this.Controls.Add(this.downloadProgressBar);
            this.Controls.Add(this.url);
            this.Controls.Add(this.download);
            this.Controls.Add(this.interfaceList);
            this.Name = "MainForm";
            this.Text = "Multi Network Downloader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.threadCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox interfaceList;
        private System.Windows.Forms.Button download;
        private System.Windows.Forms.TextBox url;
        private System.Windows.Forms.ProgressBar downloadProgressBar;
        private System.Windows.Forms.ProgressBar saveProgressBar;
        private System.Windows.Forms.NumericUpDown threadCount;
        private System.Windows.Forms.Button selectSaveLocation;
        private System.Windows.Forms.TextBox saveLocation;
        private System.Windows.Forms.Label threadsLabel;
    }
}

