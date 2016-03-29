namespace dbe
{
    partial class UiFmDocGen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UiFmDocGen));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnInputFolderOpenDlg = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbInputFolder = new System.Windows.Forms.TextBox();
            this.tbOutputFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOuputFolderOpenDlg = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.llLearnMore = new System.Windows.Forms.LinkLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorkerDocGen = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerStatusPinger = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Treat this Word document as a template and generate Word documents from XML data." +
                "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::dbe.Properties.Resources.infoicon_better;
            this.pictureBox1.Location = new System.Drawing.Point(8, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 26);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnInputFolderOpenDlg
            // 
            this.btnInputFolderOpenDlg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInputFolderOpenDlg.Image = global::dbe.Properties.Resources.open;
            this.btnInputFolderOpenDlg.Location = new System.Drawing.Point(476, 67);
            this.btnInputFolderOpenDlg.Name = "btnInputFolderOpenDlg";
            this.btnInputFolderOpenDlg.Size = new System.Drawing.Size(33, 23);
            this.btnInputFolderOpenDlg.TabIndex = 2;
            this.btnInputFolderOpenDlg.UseVisualStyleBackColor = true;
            this.btnInputFolderOpenDlg.Click += new System.EventHandler(this.btnInputFolderOpenDlg_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Input Folder";
            // 
            // tbInputFolder
            // 
            this.tbInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbInputFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbInputFolder.Location = new System.Drawing.Point(8, 69);
            this.tbInputFolder.Name = "tbInputFolder";
            this.tbInputFolder.Size = new System.Drawing.Size(462, 20);
            this.tbInputFolder.TabIndex = 4;
            // 
            // tbOutputFolder
            // 
            this.tbOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbOutputFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbOutputFolder.Location = new System.Drawing.Point(8, 118);
            this.tbOutputFolder.Name = "tbOutputFolder";
            this.tbOutputFolder.Size = new System.Drawing.Size(462, 20);
            this.tbOutputFolder.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Output Folder";
            // 
            // btnOuputFolderOpenDlg
            // 
            this.btnOuputFolderOpenDlg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOuputFolderOpenDlg.Image = global::dbe.Properties.Resources.open;
            this.btnOuputFolderOpenDlg.Location = new System.Drawing.Point(476, 116);
            this.btnOuputFolderOpenDlg.Name = "btnOuputFolderOpenDlg";
            this.btnOuputFolderOpenDlg.Size = new System.Drawing.Size(33, 23);
            this.btnOuputFolderOpenDlg.TabIndex = 8;
            this.btnOuputFolderOpenDlg.UseVisualStyleBackColor = true;
            this.btnOuputFolderOpenDlg.Click += new System.EventHandler(this.btnOuputFolderOpenDlg_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Image = global::dbe.Properties.Resources.DataContainer_NewRecordHS;
            this.btnGenerate.Location = new System.Drawing.Point(343, 155);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 9;
            this.btnGenerate.Text = "&Generate";
            this.btnGenerate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(434, 155);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // llLearnMore
            // 
            this.llLearnMore.AutoSize = true;
            this.llLearnMore.Location = new System.Drawing.Point(448, 16);
            this.llLearnMore.Name = "llLearnMore";
            this.llLearnMore.Size = new System.Drawing.Size(61, 13);
            this.llLearnMore.TabIndex = 12;
            this.llLearnMore.TabStop = true;
            this.llLearnMore.Text = "Learn More";
            this.llLearnMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLearnMore_LinkClicked);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "of XML Data Files:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(87, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "for Generated Word Documents:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus,
            this.toolStripProgressBar1});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 192);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(521, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabelStatus.Text = "Ready.";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar1.Visible = false;
            // 
            // UiFmDocGen
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(521, 214);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.llLearnMore);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tbOutputFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOuputFolderOpenDlg);
            this.Controls.Add(this.tbInputFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnInputFolderOpenDlg);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UiFmDocGen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Document Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UiFmDocGen_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnInputFolderOpenDlg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbInputFolder;
        private System.Windows.Forms.TextBox tbOutputFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOuputFolderOpenDlg;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel llLearnMore;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDocGen;
        private System.ComponentModel.BackgroundWorker backgroundWorkerStatusPinger;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}