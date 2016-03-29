namespace dbe
{
    partial class UiFmCheckForUpdates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UiFmCheckForUpdates));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lMsg = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.llDownload = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(27, 65);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(239, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // lMsg
            // 
            this.lMsg.Location = new System.Drawing.Point(12, 12);
            this.lMsg.Name = "lMsg";
            this.lMsg.Size = new System.Drawing.Size(285, 52);
            this.lMsg.TabIndex = 1;
            this.lMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(214, 103);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // llDownload
            // 
            this.llDownload.AutoSize = true;
            this.llDownload.Location = new System.Drawing.Point(64, 73);
            this.llDownload.Name = "llDownload";
            this.llDownload.Size = new System.Drawing.Size(166, 13);
            this.llDownload.TabIndex = 3;
            this.llDownload.TabStop = true;
            this.llDownload.Text = "Click here to get the latest update";
            this.llDownload.Visible = false;
            this.llDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDownload_LinkClicked);
            // 
            // UiFmCheckForUpdates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(301, 138);
            this.Controls.Add(this.llDownload);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UiFmCheckForUpdates";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check for Updates";
            this.Load += new System.EventHandler(this.UiFmCheckForUpdates_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lMsg;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel llDownload;
    }
}