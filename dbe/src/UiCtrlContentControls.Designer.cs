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
    partial class UiCtrlContentControls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UiCtrlContentControls));
            this.ilcc = new System.Windows.Forms.ImageList(this.components);
            this.lvcc = new System.Windows.Forms.ListView();
            this.chIcon = new System.Windows.Forms.ColumnHeader();
            this.chId = new System.Windows.Forms.ColumnHeader();
            this.chTag = new System.Windows.Forms.ColumnHeader();
            this.chType = new System.Windows.Forms.ColumnHeader();
            this.chXpath = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbGroupBy = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilcc
            // 
            this.ilcc.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilcc.ImageStream")));
            this.ilcc.TransparentColor = System.Drawing.Color.Transparent;
            this.ilcc.Images.SetKeyName(0, "PlainText");
            this.ilcc.Images.SetKeyName(1, "RichText");
            this.ilcc.Images.SetKeyName(2, "Picture");
            this.ilcc.Images.SetKeyName(3, "Group");
            this.ilcc.Images.SetKeyName(4, "DropDownList");
            this.ilcc.Images.SetKeyName(5, "DatePicker");
            this.ilcc.Images.SetKeyName(6, "ComboBox");
            this.ilcc.Images.SetKeyName(7, "BuildingBlocksGallery");
            // 
            // lvcc
            // 
            this.lvcc.AllowColumnReorder = true;
            this.lvcc.AllowDrop = true;
            this.lvcc.BackColor = System.Drawing.Color.White;
            this.lvcc.BackgroundImageTiled = true;
            this.lvcc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIcon,
            this.chId,
            this.chTag,
            this.chType,
            this.chXpath});
            this.lvcc.ContextMenuStrip = this.contextMenuStrip1;
            this.lvcc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvcc.FullRowSelect = true;
            this.lvcc.Location = new System.Drawing.Point(0, 25);
            this.lvcc.MultiSelect = false;
            this.lvcc.Name = "lvcc";
            this.lvcc.ShowGroups = false;
            this.lvcc.Size = new System.Drawing.Size(881, 792);
            this.lvcc.SmallImageList = this.ilcc;
            this.lvcc.TabIndex = 13;
            this.lvcc.UseCompatibleStateImageBehavior = false;
            this.lvcc.View = System.Windows.Forms.View.Details;
            this.lvcc.DoubleClick += new System.EventHandler(this.lvcc_DoubleClick);
            this.lvcc.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvcc_DragDrop);
            this.lvcc.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvcc_ColumnClick);
            this.lvcc.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvcc_DragEnter);
            // 
            // chIcon
            // 
            this.chIcon.Text = "";
            this.chIcon.Width = 35;
            // 
            // chId
            // 
            this.chId.Text = "ID";
            this.chId.Width = 67;
            // 
            // chTag
            // 
            this.chTag.Text = "Tag";
            this.chTag.Width = 73;
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 80;
            // 
            // chXpath
            // 
            this.chXpath.Text = "XPath";
            this.chXpath.Width = 611;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 26);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.propertiesToolStripMenuItem.Text = "&Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.lvcc_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsbGroupBy,
            this.toolStripSeparator1,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(881, 25);
            this.toolStrip1.TabIndex = 20;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton1.Text = "  Group By: ";
            // 
            // tsbGroupBy
            // 
            this.tsbGroupBy.DropDownHeight = 120;
            this.tsbGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbGroupBy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tsbGroupBy.IntegralHeight = false;
            this.tsbGroupBy.Items.AddRange(new object[] {
            "dfg",
            "dfg",
            "dfg",
            "dfgdfgdfg"});
            this.tsbGroupBy.Name = "tsbGroupBy";
            this.tsbGroupBy.Size = new System.Drawing.Size(98, 25);
            this.tsbGroupBy.SelectedIndexChanged += new System.EventHandler(this.cbGroupBy_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::dbe.Properties.Resources.wordlocator;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.ToolTipText = "Locate the content controls in the actual Word document";
            this.toolStripButton4.Click += new System.EventHandler(this.btnLocator_Click);
            // 
            // UiCtrlContentControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.lvcc);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UiCtrlContentControls";
            this.Size = new System.Drawing.Size(881, 817);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvcc;
        private System.Windows.Forms.ColumnHeader chTag;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chXpath;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.ImageList ilcc;
        private System.Windows.Forms.ColumnHeader chIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripButton1;
        private System.Windows.Forms.ToolStripComboBox tsbGroupBy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
    }
}
