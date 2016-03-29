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

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.Diagnostics;
using dbe.Properties;
using System.Threading;

namespace dbe
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class UiCtrlStoreTree : System.Windows.Forms.UserControl
    {
        #region UI Members
        private Panel panel;
        private Label lblXpath;
        private Label lblPrefixMappings;
        public TextBox tbXPath;
        private TreeView tvDOMTree;
        private ImageList imageList;
        public TextBox tbPrefixMappings;
        private IContainer components;
        #endregion

        // Business Logic Members
        private TreeNode m_tnRoot;
        private List<string> m_rgns;
		private Hashtable m_hshXnTn;
        private int m_lTotal;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;                
        private string m_sStoreId;

		public UiCtrlStoreTree()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            ClearState();
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code       

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UiCtrlStoreTree));
            this.panel = new System.Windows.Forms.Panel();
            this.tbPrefixMappings = new System.Windows.Forms.TextBox();
            this.lblPrefixMappings = new System.Windows.Forms.Label();
            this.tbXPath = new System.Windows.Forms.TextBox();
            this.lblXpath = new System.Windows.Forms.Label();
            this.tvDOMTree = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.tbPrefixMappings);
            this.panel.Controls.Add(this.lblPrefixMappings);
            this.panel.Controls.Add(this.tbXPath);
            this.panel.Controls.Add(this.lblXpath);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(0, 591);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(392, 57);
            this.panel.TabIndex = 1;
            // 
            // tbPrefixMappings
            // 
            this.tbPrefixMappings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPrefixMappings.Location = new System.Drawing.Point(89, 31);
            this.tbPrefixMappings.Name = "tbPrefixMappings";
            this.tbPrefixMappings.ReadOnly = true;
            this.tbPrefixMappings.Size = new System.Drawing.Size(298, 20);
            this.tbPrefixMappings.TabIndex = 13;
            // 
            // lblPrefixMappings
            // 
            this.lblPrefixMappings.AutoSize = true;
            this.lblPrefixMappings.Location = new System.Drawing.Point(3, 34);
            this.lblPrefixMappings.Name = "lblPrefixMappings";
            this.lblPrefixMappings.Size = new System.Drawing.Size(85, 13);
            this.lblPrefixMappings.TabIndex = 12;
            this.lblPrefixMappings.Text = "Prefix Mappings:";
            // 
            // tbXPath
            // 
            this.tbXPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbXPath.Location = new System.Drawing.Point(89, 6);
            this.tbXPath.Name = "tbXPath";
            this.tbXPath.ReadOnly = true;
            this.tbXPath.Size = new System.Drawing.Size(298, 20);
            this.tbXPath.TabIndex = 3;
            // 
            // lblXpath
            // 
            this.lblXpath.Location = new System.Drawing.Point(46, 7);
            this.lblXpath.Name = "lblXpath";
            this.lblXpath.Size = new System.Drawing.Size(40, 16);
            this.lblXpath.TabIndex = 2;
            this.lblXpath.Text = "XPath:";
            this.lblXpath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tvDOMTree
            // 
            this.tvDOMTree.AllowDrop = true;
            this.tvDOMTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvDOMTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDOMTree.ImageIndex = 0;
            this.tvDOMTree.ImageList = this.imageList;
            this.tvDOMTree.LabelEdit = true;
            this.tvDOMTree.Location = new System.Drawing.Point(0, 25);
            this.tvDOMTree.Name = "tvDOMTree";
            this.tvDOMTree.SelectedImageIndex = 0;
            this.tvDOMTree.ShowLines = false;
            this.tvDOMTree.Size = new System.Drawing.Size(392, 566);
            this.tvDOMTree.TabIndex = 7;
            this.tvDOMTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDOMTree_AfterSelect);
            this.tvDOMTree.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.trvDOMTree_BeforeLabelEdit);
            this.tvDOMTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvDOMTree_ItemDrag);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "attribute");
            this.imageList.Images.SetKeyName(1, "leafelement");
            this.imageList.Images.SetKeyName(2, "element");
            this.imageList.Images.SetKeyName(3, "empty");
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(392, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::dbe.Properties.Resources.plus;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Expand One Level";
            this.toolStripButton1.Click += new System.EventHandler(this.btnExpandOneLevel_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::dbe.Properties.Resources.minus;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Collapse One Level";
            this.toolStripButton2.Click += new System.EventHandler(this.btnCollapseOne_Click);
            // 
            // UiCtrlStoreTree
            // 
            this.Controls.Add(this.tvDOMTree);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel);
            this.Name = "UiCtrlStoreTree";
            this.Size = new System.Drawing.Size(392, 648);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        public void ClearState()
        {
            m_tnRoot = null;
            m_rgns = null;
		    m_hshXnTn = null;
		    m_lTotal = 0;                
            m_sStoreId = null;
        }

		/// <summary>
		/// The main entry point for the control.
		/// </summary>
		public void LoadTreeViewFromXmlDOM(XmlDocument xd, string sStoreId)
		{
            ClearState();

            m_hshXnTn = new Hashtable();
            m_rgns = new List<string>();
            m_sStoreId = sStoreId;			
            m_lTotal = 0;

            m_tnRoot = TnFromXn(xd as XmlNode);
			XmlNode xn = null;

			try 
			{
				xn = xd as XmlNode;
			}
			catch (Exception ex)
			{
                Debug.Fail("Cannot find the root element.", ex.ToString());
                return;
			}       
            
            Program.Dbe.Ui.SetStatusBarMsg("Populating XML Bind View...");
            Program.Dbe.Ui.Cursor = Cursors.WaitCursor;
            System.Windows.Forms.Application.DoEvents();
            tvDOMTree.Nodes.Clear();
            tvDOMTree.Nodes.Add(m_tnRoot);
            m_tnRoot.Tag = xn;
            PopulateTreeNode(xn, m_tnRoot);            
            // Expand only first 2 levels for perf. TODO: Create a settings dialog for a user to customize this.
            ExpandOneLevel(m_tnRoot);
            ExpandOneLevel(m_tnRoot);
            Program.Dbe.Ui.Cursor = Cursors.Default;
            Program.Dbe.Ui.SetStatusBarMsg(Resources.StatusBarReady);

            // Scroll to the top
            tvDOMTree.Nodes[0].EnsureVisible();            
		}

        private bool FXnLeaf(XmlNode xn)
        {
            foreach (XmlNode ixn in xn.ChildNodes)
                if (ixn.NodeType == XmlNodeType.Element)
                    return false;

            return true;
        }

		/// <summary>
		/// This function builds a treenode out of an xml node. The idea is to decorate the 
		/// name and show some thing that would at least give a decent representation
		/// </summary>
		private TreeNode TnFromXn(XmlNode xn)
		{
            const string sImgKeyAttribute = "attribute";
            const string sImgKeyLeafElement = "leafelement";
            const string sImgKeyElement = "element";
            const string sImgKeyEmpty = "empty";

			TreeNode tnResult = null;	

			switch (xn.NodeType)
			{
				case XmlNodeType.Attribute: 
                    tnResult = new TreeNode(xn.LocalName);
                    tnResult.ImageKey = sImgKeyAttribute;
                    tnResult.SelectedImageKey = sImgKeyAttribute;
					break;

				case XmlNodeType.Element:
				{
                    tnResult = new TreeNode(xn.LocalName);

                    if (FXnLeaf(xn))
                    {
                        tnResult.ImageKey = sImgKeyLeafElement;
                        tnResult.SelectedImageKey = sImgKeyLeafElement;
                    }
                    else
                    {
                        tnResult.ImageKey = sImgKeyElement;
                        tnResult.SelectedImageKey = sImgKeyElement;
                    }
					break;
				}
				
				case XmlNodeType.Comment:
                    tnResult = new TreeNode("<!--" + xn.Value + "-->");
                    tnResult.ForeColor = Color.DarkGreen;
                    tnResult.ImageKey = sImgKeyEmpty;
                    tnResult.SelectedImageKey = sImgKeyEmpty;
					break;

				case XmlNodeType.Document:
                    tnResult = new TreeNode("/");
                    tnResult.ImageKey = sImgKeyElement;
                    tnResult.SelectedImageKey = sImgKeyElement;
					break;

                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					break;
			}

            if (tnResult != null)
			{
				m_hshXnTn.Add(xn,tnResult);
				tnResult.Tag = xn;              
			}

			return tnResult;
		}

		private string XpathFromTn(TreeNode tn)
		{
			if (tn == null) return string.Empty;
            XmlNode xn = tn.Tag as XmlNode;
            if (xn == null) return string.Empty;
            return XpathFromXn(xn);
		}

		private string XpathFromXn(XmlNode xn)
		{
            if (xn == null)
            {
                Debug.Fail("Cannot create an xpath from a null node");
                return string.Empty;
            }

			string strThis = null;
			string strThisName = null; 
			string ns = string.Empty;
			XmlNode xnParent = xn.ParentNode;
			bool fCheckParent = true;

			switch (xn.NodeType)
			{
				case XmlNodeType.Element:
					strThisName = xn.LocalName;

					//add a namespace if one exists
					if (xn.NamespaceURI != string.Empty) 
						ns = "ns" + this.m_rgns.IndexOf(xn.NamespaceURI) + ":";						

					strThis = "/" + ns + strThisName + XpathPosFromXn(xn);
					break;

				case XmlNodeType.Attribute:
					strThisName = xn.Name;					
					strThis = "/@" + strThisName;
					
					if (strThisName != "xmlns") //do not get parent if xmlns
						xnParent = xn.SelectSingleNode("..", null);
					else
						fCheckParent = false;
					break;

				case XmlNodeType.ProcessingInstruction:
				{
					XmlProcessingInstruction xpi = xn as XmlProcessingInstruction;
					strThis = "/processing-instruction(";

					strThis = strThis + ")" + XpathPosFromXn(xn);
					break;
				}

				case XmlNodeType.Text:
					strThis = "/text()" + XpathPosFromXn(xn);
					break;

				case XmlNodeType.Comment:
					strThis = "/comment()" + XpathPosFromXn(xn);
					break;

				case XmlNodeType.Document:
					strThis = string.Empty;
					fCheckParent = false;
					break;

				case XmlNodeType.EntityReference:
				case XmlNodeType.CDATA:
					strThis = "/text()" + XpathPosFromXn(xn);
					break;
								
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					break;
			}

			return strThis.Insert(0, fCheckParent ? XpathFromXn(xnParent) : "");
		}

		private bool CollapseOneLevel(TreeNode tn)
		{
			int cExpanded = CExpandedChildFromTn(tn);
			if (cExpanded > 0)
			{
				foreach (TreeNode tnChild in tn.Nodes)
					CollapseOneLevel(tnChild);

				return true;
			}
			else
			{
				tn.Collapse();
				return false;
			}
		}

		private int CExpandedChildFromTn(TreeNode tn)
		{
			int count = 0;
			foreach (TreeNode tnChild in tn.Nodes)
				count += (tnChild.IsExpanded ? 1 : 0);

			return count;
		}

		private bool ExpandOneLevel(TreeNode tn)
		{
			if (tn.IsExpanded)
			{
				foreach (TreeNode tnChild in tn.Nodes)
					ExpandOneLevel(tnChild);

				return false;
			}
			else
				tn.Expand();
				return true;
		}

		private int CCollapsedChildFromTn(TreeNode tn)
		{
			int count = 0;
			foreach (TreeNode tnChild in tn.Nodes)
				count += (!tnChild.IsExpanded ? 1 : 0);

			return count;
		}

		private string XpathPosFromXn(XmlNode xn)
		{
			if (xn == null)
				throw new Exception("cannot create an xpath from a null node");

			long lSib = 0;
			long lTotal = 0;
			XmlNode xnPrevSib = null;
			string name = xn.LocalName;
			string nsUri = xn.NamespaceURI;
			XmlNodeType nt = xn.NodeType;

			if (xn.ParentNode != null)
				lTotal = xn.ParentNode.ChildNodes.Count;

			while ((xnPrevSib = xn.PreviousSibling) != null)
			{
				if (xnPrevSib.NodeType == nt &&
					xnPrevSib.LocalName.Equals(name) &&
					xnPrevSib.NamespaceURI.Equals(nsUri))
					lSib++;
				
				xn = xnPrevSib;
			}

			return lTotal > 0 ? "[" + (lSib + 1).ToString() + "]" : string.Empty;
		}

		private string XpathPosFromXnNoNames(XmlNode xn)
		{
			if (xn == null)
				throw new Exception("cannot create an xpath from a null node");

			long lSib = 0;
			XmlNode xnPrevSib = null;

			if (xn.ParentNode != null)
				m_lTotal = xn.ParentNode.ChildNodes.Count;

			while ((xnPrevSib = xn.PreviousSibling) != null)
			{
				lSib++;
				xn = xnPrevSib;
			}

			return m_lTotal > 0 ? "[" + (lSib + 1).ToString() + "]" : string.Empty;
		}

		private void PopulateTreeNode(XmlNode xn, TreeNode tn)
		{
			foreach (XmlNode xnChild in xn.ChildNodes)
			{
				TreeNode tnChild = TnFromXn(xnChild);
				if (tnChild != null)
					tn.Nodes.Add(tnChild);

				PopulateTreeNode(xnChild, tnChild);
				PopulateNamespaceList(xnChild.NamespaceURI);
			}

			if (xn.Attributes != null)
				foreach (XmlNode xnChild in xn.Attributes)
				{
					TreeNode tnChild = TnFromXn(xnChild);
					if (tnChild != null)
						tn.Nodes.Add (tnChild);

					PopulateTreeNode (xnChild, tnChild);
				}
		}

		private void PopulateNamespaceList(string ns)
		{			
			if (this.m_rgns.IndexOf (ns) == -1) //add only unique ns's
				if (!string.IsNullOrEmpty(ns))
				    this.m_rgns.Insert (0, ns);
		}

		/// <summary>
        /// Nothing special here, just recalc the xpath 
		/// </summary>
		private void CheckedChanged(object sender, System.EventArgs e)
		{
			CalcXPath();
		}

		private void CalcXPath()
		{
			TreeNode tnSelected = tvDOMTree.SelectedNode;
			if (tnSelected != null)
			{
				tbXPath.Text = XpathFromTn(tnSelected);
                tbPrefixMappings.Text = GetPrefixMappings();
			}
		}

		private void trvDOMTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (e.Node.Text != "@xmlns")			
				CalcXPath();
			else 
				ClearPropFields();
			
		}
		private void ClearPropFields()
		{				
			this.tbXPath.Text = string.Empty;
		}

		
		private void btnCollapseOne_Click(object sender, System.EventArgs e)
		{
			CollapseOneLevel(m_tnRoot);

            // Scroll to the top
            tvDOMTree.Nodes[0].EnsureVisible();            
		}

		private void btnExpandOneLevel_Click(object sender, System.EventArgs e)
		{
			ExpandOneLevel(m_tnRoot);

            // Scroll to the top
            tvDOMTree.Nodes[0].EnsureVisible();            
		}

		private void trvDOMTree_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{			
			TreeNode tnSelected = tvDOMTree.SelectedNode;
			if (tnSelected == null) 
				return;

            XmlNode xnSelected = (XmlNode)tnSelected.Tag;

            // Restrict dragging of invalid nodes
            if ((xnSelected.NodeType != XmlNodeType.Element && xnSelected.NodeType != XmlNodeType.Attribute) ||
                (xnSelected.NodeType == XmlNodeType.Element && !FXnLeaf(xnSelected)))
            {
                Mbox.ShowSimpleMsgBoxWarning(Resources.IllegalXmlTreeDrag);
                return;
            }
            
            string sXpath = XpathFromTn(tnSelected);
			DataObject dobj = new DataObject();
            dobj.SetData(new BindInfo(sXpath, GetPrefixMappings(), m_sStoreId));
			this.DoDragDrop(dobj, DragDropEffects.Move);
		}

        public class BindInfo
        {
            public string XPath;
            public string PrefixMappings;
            public string StoreID;

            public BindInfo(string sXpath, string sPrefixMappings, string sStoreId)
            {
                XPath = sXpath;
                PrefixMappings = sPrefixMappings;
                StoreID = sStoreId;
            }
        }
     
		private string GetPrefixMappings()
		{
			string s = string.Empty;
			for (int i = 0 ; i < this.m_rgns.Count; i++)
				s += "xmlns:ns"+i+"='"+this.m_rgns[i]+"'"+(i < this.m_rgns.Count - 1 ? " " : string.Empty); 

			return s;
		}

		private void btnGetNS_Click(object sender, System.EventArgs e)
		{		
			MessageBox.Show (GetPrefixMappings());
		}
        		
		private void trvDOMTree_BeforeLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			if ( ((XmlNode)e.Node.Tag).NodeType != XmlNodeType.Text )
			{
				e.CancelEdit = true;				
			}
		}

        public void Clear()
        {
            ClearState();
            tvDOMTree.Nodes.Clear();            
        }     
    }
}
