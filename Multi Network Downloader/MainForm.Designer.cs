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
            this.SuspendLayout();
            // 
            // interfaceList
            // 
            this.interfaceList.FormattingEnabled = true;
            this.interfaceList.Location = new System.Drawing.Point(12, 39);
            this.interfaceList.Name = "interfaceList";
            this.interfaceList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.interfaceList.Size = new System.Drawing.Size(234, 160);
            this.interfaceList.TabIndex = 0;
            // 
            // download
            // 
            this.download.Location = new System.Drawing.Point(252, 39);
            this.download.Name = "download";
            this.download.Size = new System.Drawing.Size(75, 23);
            this.download.TabIndex = 1;
            this.download.Text = "Download";
            this.download.UseVisualStyleBackColor = true;
            this.download.Click += new System.EventHandler(this.download_Click);
            // 
            // url
            // 
            this.url.Location = new System.Drawing.Point(12, 13);
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(500, 20);
            this.url.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 215);
            this.Controls.Add(this.url);
            this.Controls.Add(this.download);
            this.Controls.Add(this.interfaceList);
            this.Name = "MainForm";
            this.Text = "Multi Network Downloader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox interfaceList;
        private System.Windows.Forms.Button download;
        private System.Windows.Forms.TextBox url;
    }
}

