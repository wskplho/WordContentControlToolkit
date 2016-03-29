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
    partial class UiCtrlXmlTextEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UiCtrlXmlTextEditor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCheckSyntax = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReformatXml = new System.Windows.Forms.ToolStripButton();
            this.tsbWordWrap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRevert = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.rtbXml = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCheckSyntax,
            this.toolStripSeparator,
            this.tsbReformatXml,
            this.tsbWordWrap,
            this.toolStripSeparator1,
            this.tsbRevert,
            this.toolStripSeparator2,
            this.tsbImport,
            this.tsbExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(338, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCheckSyntax
            // 
            this.tsbCheckSyntax.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCheckSyntax.Image = global::dbe.Properties.Resources.CheckGrammarHS;
            this.tsbCheckSyntax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheckSyntax.Name = "tsbCheckSyntax";
            this.tsbCheckSyntax.Size = new System.Drawing.Size(23, 22);
            this.tsbCheckSyntax.Text = "&Check Syntax";
            this.tsbCheckSyntax.Click += new System.EventHandler(this.tsbCheckSyntax_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbReformatXml
            // 
            this.tsbReformatXml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbReformatXml.Image = global::dbe.Properties.Resources.reformat;
            this.tsbReformatXml.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbReformatXml.Name = "tsbReformatXml";
            this.tsbReformatXml.Size = new System.Drawing.Size(23, 22);
            this.tsbReformatXml.Text = "&Format XML";
            this.tsbReformatXml.ToolTipText = "Formats (\"Pretty Prints\") the entire XML document";
            this.tsbReformatXml.Click += new System.EventHandler(this.tsbReformatXml_Click);
            // 
            // tsbWordWrap
            // 
            this.tsbWordWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbWordWrap.Image = global::dbe.Properties.Resources.HtmlBalanceBracesHS;
            this.tsbWordWrap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWordWrap.Name = "tsbWordWrap";
            this.tsbWordWrap.Size = new System.Drawing.Size(23, 22);
            this.tsbWordWrap.Text = "&Word Wrap";
            this.tsbWordWrap.ToolTipText = "Toggle Word Wrap";
            this.tsbWordWrap.Click += new System.EventHandler(this.tsbWordWrap_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRevert
            // 
            this.tsbRevert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRevert.Image = global::dbe.Properties.Resources.revert;
            this.tsbRevert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRevert.Name = "tsbRevert";
            this.tsbRevert.Size = new System.Drawing.Size(23, 22);
            this.tsbRevert.Text = "Revert";
            this.tsbRevert.ToolTipText = "Revert changes since last commited edit";
            this.tsbRevert.Click += new System.EventHandler(this.tsbRevert_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbImport
            // 
            this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(23, 22);
            this.tsbImport.Text = "&Open";
            this.tsbImport.ToolTipText = "Open an XML document into the editor";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExport.Image = global::dbe.Properties.Resources.VSProject_generatedfile;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(23, 22);
            this.tsbExport.Text = "&Save";
            this.tsbExport.ToolTipText = "Export this XML document to disk";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // rtbXml
            // 
            this.rtbXml.AcceptsTab = true;
            this.rtbXml.DetectUrls = false;
            this.rtbXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbXml.Location = new System.Drawing.Point(0, 25);
            this.rtbXml.Name = "rtbXml";
            this.rtbXml.Size = new System.Drawing.Size(338, 422);
            this.rtbXml.TabIndex = 8;
            this.rtbXml.Text = "";
            this.rtbXml.WordWrap = false;
            this.rtbXml.ModifiedChanged += new System.EventHandler(this.rtbXml_ModifiedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            this.saveFileDialog1.Title = "Save As";
            // 
            // UiCtrlXmlTextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbXml);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UiCtrlXmlTextEditor";
            this.Size = new System.Drawing.Size(338, 447);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbCheckSyntax;
        private System.Windows.Forms.ToolStripButton tsbReformatXml;
        private System.Windows.Forms.RichTextBox rtbXml;
        private System.Windows.Forms.ToolStripButton tsbWordWrap;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton tsbRevert;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
