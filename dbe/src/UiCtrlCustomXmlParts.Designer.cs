//-------------------------------------------------------------------------------------------------
// <copyright company="Microsoft">
//    Author: Matt Scott (mrscott). Copyright (c) Microsoft Corporation.  All rights reserved.
//
//    The use and distribution terms for this software are covered by the
//    Microsoft Limited Permissive License: 
//    http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedpermissivelicense.mspx
//    which can be found in the file license_mslpl.txt at the root of this distribution.
//    By using this software in any fashion, you are agreeing to be bound by
//    the terms of this license.
//
//    You must not remove this notice, or any other, from this software.
//
//    Project homepage can be found here: http://www.codeplex.com/dbe  
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace dbe
{
    partial class UiCtrlCustomXmlParts
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UiCtrlCustomXmlParts));
            this.cbParts = new System.Windows.Forms.ComboBox();
            this.tcXmlViews = new System.Windows.Forms.TabControl();
            this.tpBindView = new System.Windows.Forms.TabPage();
            this.tpEditView = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lCreateNewXmlPart = new System.Windows.Forms.LinkLabel();
            this.lDeleteXmlPart = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tsbLocator = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tmrNsPinger = new System.Windows.Forms.Timer(this.components);
            this.pnInfoBarNoXml = new System.Windows.Forms.Panel();
            this.lCreateNewXmlPartInfoBar = new System.Windows.Forms.LinkLabel();
            this.lNoCustomXmlPartsMsg = new System.Windows.Forms.Label();
            this.pbInfoIcon = new System.Windows.Forms.PictureBox();
            this.storeTree = new dbe.UiCtrlStoreTree();
            this.xmlTextEditor1 = new dbe.UiCtrlXmlTextEditor();
            this.tcXmlViews.SuspendLayout();
            this.tpBindView.SuspendLayout();
            this.tpEditView.SuspendLayout();
            this.pnInfoBarNoXml.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfoIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // cbParts
            // 
            this.cbParts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbParts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParts.FormattingEnabled = true;
            this.cbParts.Location = new System.Drawing.Point(6, 21);
            this.cbParts.Name = "cbParts";
            this.cbParts.Size = new System.Drawing.Size(417, 21);
            this.cbParts.TabIndex = 11;
            this.cbParts.SelectionChangeCommitted += new System.EventHandler(this.cbParts_SelectionChangeCommitted);
            this.cbParts.Click += new System.EventHandler(this.cbParts_Click);
            // 
            // tcXmlViews
            // 
            this.tcXmlViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcXmlViews.Controls.Add(this.tpBindView);
            this.tcXmlViews.Controls.Add(this.tpEditView);
            this.tcXmlViews.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcXmlViews.Location = new System.Drawing.Point(4, 48);
            this.tcXmlViews.Name = "tcXmlViews";
            this.tcXmlViews.SelectedIndex = 0;
            this.tcXmlViews.Size = new System.Drawing.Size(417, 596);
            this.tcXmlViews.TabIndex = 10;
            this.tcXmlViews.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcXmlViews_Selecting);
            // 
            // tpBindView
            // 
            this.tpBindView.Controls.Add(this.storeTree);
            this.tpBindView.Location = new System.Drawing.Point(4, 22);
            this.tpBindView.Name = "tpBindView";
            this.tpBindView.Padding = new System.Windows.Forms.Padding(3);
            this.tpBindView.Size = new System.Drawing.Size(409, 570);
            this.tpBindView.TabIndex = 0;
            this.tpBindView.Text = "Bind View";
            this.tpBindView.UseVisualStyleBackColor = true;
            // 
            // tpEditView
            // 
            this.tpEditView.Controls.Add(this.xmlTextEditor1);
            this.tpEditView.Location = new System.Drawing.Point(4, 22);
            this.tpEditView.Name = "tpEditView";
            this.tpEditView.Padding = new System.Windows.Forms.Padding(3);
            this.tpEditView.Size = new System.Drawing.Size(409, 570);
            this.tpEditView.TabIndex = 1;
            this.tpEditView.Text = "Edit View";
            this.tpEditView.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Namespace:";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lCreateNewXmlPart
            // 
            this.lCreateNewXmlPart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lCreateNewXmlPart.AutoSize = true;
            this.lCreateNewXmlPart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCreateNewXmlPart.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lCreateNewXmlPart.Location = new System.Drawing.Point(14, 674);
            this.lCreateNewXmlPart.Name = "lCreateNewXmlPart";
            this.lCreateNewXmlPart.Size = new System.Drawing.Size(156, 13);
            this.lCreateNewXmlPart.TabIndex = 1;
            this.lCreateNewXmlPart.TabStop = true;
            this.lCreateNewXmlPart.Text = "&Create a new Custom XML Part";
            this.lCreateNewXmlPart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lCreateNewXmlPart_LinkClicked);
            // 
            // lDeleteXmlPart
            // 
            this.lDeleteXmlPart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lDeleteXmlPart.AutoSize = true;
            this.lDeleteXmlPart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDeleteXmlPart.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lDeleteXmlPart.Location = new System.Drawing.Point(14, 692);
            this.lDeleteXmlPart.Name = "lDeleteXmlPart";
            this.lDeleteXmlPart.Size = new System.Drawing.Size(122, 13);
            this.lDeleteXmlPart.TabIndex = 20;
            this.lDeleteXmlPart.TabStop = true;
            this.lDeleteXmlPart.Text = "&Delete Custom XML Part";
            this.lDeleteXmlPart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lDeleteXmlPart_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 653);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Actions";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(6, 667);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(417, 1);
            this.label4.TabIndex = 22;
            // 
            // tsbLocator
            // 
            this.tsbLocator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLocator.Image = ((System.Drawing.Image)(resources.GetObject("tsbLocator.Image")));
            this.tsbLocator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLocator.Name = "tsbLocator";
            this.tsbLocator.Size = new System.Drawing.Size(47, 22);
            this.tsbLocator.Text = "Locator";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(49, 22);
            this.tsbRefresh.Text = "Refresh";
            // 
            // tmrNsPinger
            // 
            this.tmrNsPinger.Enabled = true;
            this.tmrNsPinger.Interval = 1500;
            this.tmrNsPinger.Tick += new System.EventHandler(this.tmrNsPinger_Tick);
            // 
            // pnInfoBarNoXml
            // 
            this.pnInfoBarNoXml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnInfoBarNoXml.BackColor = System.Drawing.SystemColors.Info;
            this.pnInfoBarNoXml.Controls.Add(this.lCreateNewXmlPartInfoBar);
            this.pnInfoBarNoXml.Controls.Add(this.lNoCustomXmlPartsMsg);
            this.pnInfoBarNoXml.Controls.Add(this.pbInfoIcon);
            this.pnInfoBarNoXml.Location = new System.Drawing.Point(12, 99);
            this.pnInfoBarNoXml.Name = "pnInfoBarNoXml";
            this.pnInfoBarNoXml.Size = new System.Drawing.Size(401, 48);
            this.pnInfoBarNoXml.TabIndex = 1;
            // 
            // lCreateNewXmlPartInfoBar
            // 
            this.lCreateNewXmlPartInfoBar.AutoSize = true;
            this.lCreateNewXmlPartInfoBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCreateNewXmlPartInfoBar.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lCreateNewXmlPartInfoBar.Location = new System.Drawing.Point(33, 25);
            this.lCreateNewXmlPartInfoBar.Name = "lCreateNewXmlPartInfoBar";
            this.lCreateNewXmlPartInfoBar.Size = new System.Drawing.Size(157, 13);
            this.lCreateNewXmlPartInfoBar.TabIndex = 29;
            this.lCreateNewXmlPartInfoBar.TabStop = true;
            this.lCreateNewXmlPartInfoBar.Text = "Click here to create a new one.";
            this.lCreateNewXmlPartInfoBar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lCreateNewXmlPart_LinkClicked);
            // 
            // lNoCustomXmlPartsMsg
            // 
            this.lNoCustomXmlPartsMsg.AutoSize = true;
            this.lNoCustomXmlPartsMsg.Location = new System.Drawing.Point(31, 8);
            this.lNoCustomXmlPartsMsg.Name = "lNoCustomXmlPartsMsg";
            this.lNoCustomXmlPartsMsg.Size = new System.Drawing.Size(240, 13);
            this.lNoCustomXmlPartsMsg.TabIndex = 28;
            this.lNoCustomXmlPartsMsg.Text = "There are no Custom XML parts in this document.";
            // 
            // pbInfoIcon
            // 
            this.pbInfoIcon.Image = global::dbe.Properties.Resources.infoicon_better;
            this.pbInfoIcon.Location = new System.Drawing.Point(6, 4);
            this.pbInfoIcon.Name = "pbInfoIcon";
            this.pbInfoIcon.Size = new System.Drawing.Size(23, 23);
            this.pbInfoIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbInfoIcon.TabIndex = 27;
            this.pbInfoIcon.TabStop = false;
            // 
            // storeTree
            // 
            this.storeTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storeTree.Location = new System.Drawing.Point(3, 3);
            this.storeTree.Name = "storeTree";
            this.storeTree.Size = new System.Drawing.Size(403, 564);
            this.storeTree.TabIndex = 0;
            // 
            // xmlTextEditor1
            // 
            this.xmlTextEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlTextEditor1.Location = new System.Drawing.Point(3, 3);
            this.xmlTextEditor1.Name = "xmlTextEditor1";
            this.xmlTextEditor1.Size = new System.Drawing.Size(403, 564);
            this.xmlTextEditor1.TabIndex = 0;
            // 
            // UiCtrlCustomXmlParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnInfoBarNoXml);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lDeleteXmlPart);
            this.Controls.Add(this.lCreateNewXmlPart);
            this.Controls.Add(this.cbParts);
            this.Controls.Add(this.tcXmlViews);
            this.Controls.Add(this.label1);
            this.Name = "UiCtrlCustomXmlParts";
            this.Size = new System.Drawing.Size(430, 724);
            this.tcXmlViews.ResumeLayout(false);
            this.tpBindView.ResumeLayout(false);
            this.tpEditView.ResumeLayout(false);
            this.pnInfoBarNoXml.ResumeLayout(false);
            this.pnInfoBarNoXml.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfoIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbParts;
        private System.Windows.Forms.TabControl tcXmlViews;
        private System.Windows.Forms.TabPage tpBindView;
        private dbe.UiCtrlStoreTree storeTree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpEditView;
        private UiCtrlXmlTextEditor xmlTextEditor1;
        private System.Windows.Forms.ToolStripButton tsbLocator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.LinkLabel lCreateNewXmlPart;
        private System.Windows.Forms.LinkLabel lDeleteXmlPart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer tmrNsPinger;
        private System.Windows.Forms.Panel pnInfoBarNoXml;
        private System.Windows.Forms.LinkLabel lCreateNewXmlPartInfoBar;
        private System.Windows.Forms.Label lNoCustomXmlPartsMsg;
        private System.Windows.Forms.PictureBox pbInfoIcon;
    }
}
