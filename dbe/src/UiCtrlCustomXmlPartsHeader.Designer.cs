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
    partial class UiCtrlCustomXmlPartsHeader
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
            this.label2 = new System.Windows.Forms.Label();
            this.lClose = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(4, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Custom XML Parts";
            // 
            // lClose
            // 
            this.lClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lClose.AutoSize = true;
            this.lClose.BackColor = System.Drawing.Color.Transparent;
            this.lClose.Font = new System.Drawing.Font("Lucida Console", 10.25F, System.Drawing.FontStyle.Bold);
            this.lClose.ForeColor = System.Drawing.Color.LightGray;
            this.lClose.Location = new System.Drawing.Point(853, 2);
            this.lClose.Name = "lClose";
            this.lClose.Size = new System.Drawing.Size(16, 14);
            this.lClose.TabIndex = 26;
            this.lClose.Text = "x";
            this.lClose.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            this.lClose.Click += new System.EventHandler(this.label1_Click);
            this.lClose.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // UCXmlDataStoreHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.lClose);
            this.Controls.Add(this.label2);
            this.Name = "UCXmlDataStoreHeader";
            this.Size = new System.Drawing.Size(872, 19);
            this.Enter += new System.EventHandler(this.UCXmlDataStoreHeader_Enter);
            this.DoubleClick += new System.EventHandler(this.UCXmlDataStoreHeader_DoubleClick);
            this.Leave += new System.EventHandler(this.UCXmlDataStoreHeader_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lClose;
    }
}
