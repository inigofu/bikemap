//------------------------------------------------------------------------------
// <copyright file="MainForm.Designer.cs" company="">
// Copyright (c) 2009 GPS Track Viewer development team
// http://GpxTracker.codeplex.com/
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, 
// MA 02110-1301, USA.
// </copyright>
// <author></author>
// <date>22.09.2010</date>
// <summary>Contains information about the MainForm class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainerBig = new System.Windows.Forms.SplitContainer();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelAccDesc = new System.Windows.Forms.Label();
            this.labelAccAsc = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSpeedAvg = new System.Windows.Forms.Label();
            this.labelAlti = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelDist = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelSpeedCurr = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainerRight = new System.Windows.Forms.SplitContainer();
            this.toolStripContainer3 = new System.Windows.Forms.ToolStripContainer();
            this.mapView = new GpxTracker.MapView();
            this.toolStripView = new System.Windows.Forms.ToolStrip();
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tslZoom = new System.Windows.Forms.ToolStripLabel();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsbScreenshot = new System.Windows.Forms.ToolStripButton();
            this.tsbMeasure = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.mapChart = new GpxTracker.MapChart();
            this.toolStripChart = new System.Windows.Forms.ToolStrip();
            this.tsbChartDistance = new System.Windows.Forms.ToolStripButton();
            this.tsbChartTime = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbChartHeight = new System.Windows.Forms.ToolStripButton();
            this.tsbChartSpeed = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemYardstick = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMapGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMapNumbers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExtras = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemImport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemContent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBig)).BeginInit();
            this.splitContainerBig.Panel1.SuspendLayout();
            this.splitContainerBig.Panel2.SuspendLayout();
            this.splitContainerBig.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).BeginInit();
            this.splitContainerRight.Panel1.SuspendLayout();
            this.splitContainerRight.Panel2.SuspendLayout();
            this.splitContainerRight.SuspendLayout();
            this.toolStripContainer3.ContentPanel.SuspendLayout();
            this.toolStripContainer3.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapView)).BeginInit();
            this.toolStripView.SuspendLayout();
            this.toolStripContainer2.ContentPanel.SuspendLayout();
            this.toolStripContainer2.LeftToolStripPanel.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            this.toolStripChart.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerBig
            // 
            resources.ApplyResources(this.splitContainerBig, "splitContainerBig");
            this.splitContainerBig.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerBig.Name = "splitContainerBig";
            // 
            // splitContainerBig.Panel1
            // 
            this.splitContainerBig.Panel1.Controls.Add(this.panelInfo);
            this.splitContainerBig.Panel1.Controls.Add(this.dataGridView);
            // 
            // splitContainerBig.Panel2
            // 
            this.splitContainerBig.Panel2.Controls.Add(this.splitContainerRight);
            resources.ApplyResources(this.splitContainerBig.Panel2, "splitContainerBig.Panel2");
            // 
            // panelInfo
            // 
            resources.ApplyResources(this.panelInfo, "panelInfo");
            this.panelInfo.BackColor = System.Drawing.Color.OldLace;
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelInfo.Controls.Add(this.tableLayoutPanel);
            this.panelInfo.Name = "panelInfo";
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.labelAccDesc, 1, 7);
            this.tableLayoutPanel.Controls.Add(this.labelAccAsc, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.label6, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.labelSpeedAvg, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelAlti, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.labelDist, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.labelSpeedCurr, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelTime, 1, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // labelAccDesc
            // 
            resources.ApplyResources(this.labelAccDesc, "labelAccDesc");
            this.labelAccDesc.Name = "labelAccDesc";
            // 
            // labelAccAsc
            // 
            resources.ApplyResources(this.labelAccAsc, "labelAccAsc");
            this.labelAccAsc.Name = "labelAccAsc";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // labelSpeedAvg
            // 
            resources.ApplyResources(this.labelSpeedAvg, "labelSpeedAvg");
            this.labelSpeedAvg.Name = "labelSpeedAvg";
            // 
            // labelAlti
            // 
            resources.ApplyResources(this.labelAlti, "labelAlti");
            this.labelAlti.Name = "labelAlti";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // labelDist
            // 
            resources.ApplyResources(this.labelDist, "labelDist");
            this.labelDist.Name = "labelDist";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // labelSpeedCurr
            // 
            resources.ApplyResources(this.labelSpeedCurr, "labelSpeedCurr");
            this.labelSpeedCurr.Name = "labelSpeedCurr";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelTime
            // 
            resources.ApplyResources(this.labelTime, "labelTime");
            this.labelTime.Name = "labelTime";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dataGridView, "dataGridView");
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colDistance,
            this.colTime});
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
            this.dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_ColumnHeaderMouseClick);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            resources.ApplyResources(this.colName, "colName");
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colDistance
            // 
            this.colDistance.DataPropertyName = "TotalDistance";
            resources.ApplyResources(this.colDistance, "colDistance");
            this.colDistance.Name = "colDistance";
            this.colDistance.ReadOnly = true;
            // 
            // colTime
            // 
            this.colTime.DataPropertyName = "TotalTime";
            resources.ApplyResources(this.colTime, "colTime");
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // splitContainerRight
            // 
            resources.ApplyResources(this.splitContainerRight, "splitContainerRight");
            this.splitContainerRight.Name = "splitContainerRight";
            // 
            // splitContainerRight.Panel1
            // 
            this.splitContainerRight.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerRight.Panel1.Controls.Add(this.toolStripContainer3);
            // 
            // splitContainerRight.Panel2
            // 
            this.splitContainerRight.Panel2.Controls.Add(this.toolStripContainer2);
            // 
            // toolStripContainer3
            // 
            this.toolStripContainer3.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer3.ContentPanel
            // 
            this.toolStripContainer3.ContentPanel.Controls.Add(this.mapView);
            resources.ApplyResources(this.toolStripContainer3.ContentPanel, "toolStripContainer3.ContentPanel");
            resources.ApplyResources(this.toolStripContainer3, "toolStripContainer3");
            this.toolStripContainer3.LeftToolStripPanelVisible = false;
            this.toolStripContainer3.Name = "toolStripContainer3";
            this.toolStripContainer3.RightToolStripPanelVisible = false;
            // 
            // toolStripContainer3.TopToolStripPanel
            // 
            this.toolStripContainer3.TopToolStripPanel.Controls.Add(this.toolStripView);
            // 
            // mapView
            // 
            this.mapView.BackColor = System.Drawing.Color.Salmon;
            this.mapView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.mapView, "mapView");
            this.mapView.MyRenderFlags = GpxTracker.MapView.RenderFlags.AntiAlias;
            this.mapView.Name = "mapView";
            this.mapView.TabStop = false;
            this.mapView.SelectedIndexChanged += new System.EventHandler<GpxTracker.IndexChangedEventArgs>(this.mapView_SelIndexChanged);
            this.mapView.ControlIndexChanged += new System.EventHandler<GpxTracker.IndexChangedEventArgs>(this.mapView_ControlIndexChanged);
            this.mapView.ZoomChanged += new System.EventHandler<GpxTracker.ZoomChangedEventArgs>(this.mapView_ZoomChanged);
            this.mapView.Click += new System.EventHandler(this.MapView_Click);
            // 
            // toolStripView
            // 
            resources.ApplyResources(this.toolStripView, "toolStripView");
            this.toolStripView.CanOverflow = false;
            this.toolStripView.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbZoomIn,
            this.tslZoom,
            this.tsbZoomOut,
            this.tsbScreenshot,
            this.tsbMeasure});
            this.toolStripView.Name = "toolStripView";
            this.toolStripView.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripView.Stretch = true;
            // 
            // tsbZoomIn
            // 
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomIn.Image = global::GpxTracker.Properties.Resources.magni_plus_big;
            resources.ApplyResources(this.tsbZoomIn, "tsbZoomIn");
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Click += new System.EventHandler(this.tsbZoomIn_Click);
            // 
            // tslZoom
            // 
            resources.ApplyResources(this.tslZoom, "tslZoom");
            this.tslZoom.Margin = new System.Windows.Forms.Padding(0);
            this.tslZoom.Name = "tslZoom";
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomOut.Image = global::GpxTracker.Properties.Resources.magni_minus_big;
            resources.ApplyResources(this.tsbZoomOut, "tsbZoomOut");
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Click += new System.EventHandler(this.tsbZoomOut_Click);
            // 
            // tsbScreenshot
            // 
            this.tsbScreenshot.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbScreenshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbScreenshot.Image = global::GpxTracker.Properties.Resources.camera2;
            resources.ApplyResources(this.tsbScreenshot, "tsbScreenshot");
            this.tsbScreenshot.Name = "tsbScreenshot";
            this.tsbScreenshot.Click += new System.EventHandler(this.tsbScreenshot_Click);
            // 
            // tsbMeasure
            // 
            this.tsbMeasure.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMeasure.Image = global::GpxTracker.Properties.Resources.measure;
            resources.ApplyResources(this.tsbMeasure, "tsbMeasure");
            this.tsbMeasure.Name = "tsbMeasure";
            this.tsbMeasure.Click += new System.EventHandler(this.tsbMeasure_Click);
            // 
            // toolStripContainer2
            // 
            this.toolStripContainer2.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Controls.Add(this.mapChart);
            resources.ApplyResources(this.toolStripContainer2.ContentPanel, "toolStripContainer2.ContentPanel");
            resources.ApplyResources(this.toolStripContainer2, "toolStripContainer2");
            // 
            // toolStripContainer2.LeftToolStripPanel
            // 
            this.toolStripContainer2.LeftToolStripPanel.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripContainer2.LeftToolStripPanel.Controls.Add(this.toolStripChart);
            this.toolStripContainer2.LeftToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.RightToolStripPanelVisible = false;
            this.toolStripContainer2.TopToolStripPanelVisible = false;
            // 
            // mapChart
            // 
            this.mapChart.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mapChart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.mapChart, "mapChart");
            this.mapChart.EnableAntiAliasing = true;
            this.mapChart.HorzDesc = null;
            this.mapChart.Name = "mapChart";
            this.mapChart.VertDesc = null;
            this.mapChart.SelectedIndexChanged += new System.EventHandler<GpxTracker.IndexChangedEventArgs>(this.mapChart_SelIndexChanged);
            this.mapChart.ControlIndexChanged += new System.EventHandler<GpxTracker.IndexChangedEventArgs>(this.mapChart_ControlIndexChanged);
            // 
            // toolStripChart
            // 
            resources.ApplyResources(this.toolStripChart, "toolStripChart");
            this.toolStripChart.CanOverflow = false;
            this.toolStripChart.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbChartDistance,
            this.tsbChartTime,
            this.toolStripSeparator6,
            this.tsbChartHeight,
            this.tsbChartSpeed});
            this.toolStripChart.Name = "toolStripChart";
            this.toolStripChart.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripChart.Stretch = true;
            // 
            // tsbChartDistance
            // 
            this.tsbChartDistance.CheckOnClick = true;
            this.tsbChartDistance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChartDistance.Image = global::GpxTracker.Properties.Resources.distance;
            resources.ApplyResources(this.tsbChartDistance, "tsbChartDistance");
            this.tsbChartDistance.Name = "tsbChartDistance";
            this.tsbChartDistance.Click += new System.EventHandler(this.tsbChartDistance_Click);
            // 
            // tsbChartTime
            // 
            this.tsbChartTime.CheckOnClick = true;
            this.tsbChartTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChartTime.Image = global::GpxTracker.Properties.Resources.time;
            resources.ApplyResources(this.tsbChartTime, "tsbChartTime");
            this.tsbChartTime.Name = "tsbChartTime";
            this.tsbChartTime.Click += new System.EventHandler(this.tsbChartTime_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // tsbChartHeight
            // 
            this.tsbChartHeight.CheckOnClick = true;
            this.tsbChartHeight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChartHeight.Image = global::GpxTracker.Properties.Resources.height;
            resources.ApplyResources(this.tsbChartHeight, "tsbChartHeight");
            this.tsbChartHeight.Name = "tsbChartHeight";
            this.tsbChartHeight.Click += new System.EventHandler(this.tsbChartHeight_Click);
            // 
            // tsbChartSpeed
            // 
            this.tsbChartSpeed.CheckOnClick = true;
            this.tsbChartSpeed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChartSpeed.Image = global::GpxTracker.Properties.Resources.speed;
            resources.ApplyResources(this.tsbChartSpeed, "tsbChartSpeed");
            this.tsbChartSpeed.Name = "tsbChartSpeed";
            this.tsbChartSpeed.Click += new System.EventHandler(this.tsbChartSpeed_Click);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerBig);
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemView,
            this.menuItemExtras,
            this.menuItemHelp});
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExit});
            this.menuItemFile.Name = "menuItemFile";
            resources.ApplyResources(this.menuItemFile, "menuItemFile");
            // 
            // menuItemExit
            // 
            this.menuItemExit.Image = global::GpxTracker.Properties.Resources.close;
            this.menuItemExit.Name = "menuItemExit";
            resources.ApplyResources(this.menuItemExit, "menuItemExit");
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemView
            // 
            this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemYardstick,
            this.menuItemMapGrid,
            this.menuItemMapNumbers});
            this.menuItemView.Name = "menuItemView";
            resources.ApplyResources(this.menuItemView, "menuItemView");
            // 
            // menuItemYardstick
            // 
            this.menuItemYardstick.CheckOnClick = true;
            this.menuItemYardstick.Name = "menuItemYardstick";
            resources.ApplyResources(this.menuItemYardstick, "menuItemYardstick");
            this.menuItemYardstick.CheckedChanged += new System.EventHandler(this.yardstickMenuItem_CheckedChanged);
            // 
            // menuItemMapGrid
            // 
            this.menuItemMapGrid.CheckOnClick = true;
            this.menuItemMapGrid.Name = "menuItemMapGrid";
            resources.ApplyResources(this.menuItemMapGrid, "menuItemMapGrid");
            this.menuItemMapGrid.CheckedChanged += new System.EventHandler(this.mapGridMenuItem_CheckedChanged);
            // 
            // menuItemMapNumbers
            // 
            this.menuItemMapNumbers.CheckOnClick = true;
            this.menuItemMapNumbers.Name = "menuItemMapNumbers";
            resources.ApplyResources(this.menuItemMapNumbers, "menuItemMapNumbers");
            this.menuItemMapNumbers.CheckedChanged += new System.EventHandler(this.mapNumbersMenuItem_CheckedChanged);
            // 
            // menuItemExtras
            // 
            this.menuItemExtras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSettings,
            this.menuItemImport});
            this.menuItemExtras.Name = "menuItemExtras";
            resources.ApplyResources(this.menuItemExtras, "menuItemExtras");
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Image = global::GpxTracker.Properties.Resources.gears;
            this.menuItemSettings.Name = "menuItemSettings";
            resources.ApplyResources(this.menuItemSettings, "menuItemSettings");
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // menuItemImport
            // 
            this.menuItemImport.Image = global::GpxTracker.Properties.Resources.import;
            this.menuItemImport.Name = "menuItemImport";
            resources.ApplyResources(this.menuItemImport, "menuItemImport");
            this.menuItemImport.Click += new System.EventHandler(this.menuItemImport_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemContent,
            this.toolStripSeparator5,
            this.menuItemAbout});
            this.menuItemHelp.Name = "menuItemHelp";
            resources.ApplyResources(this.menuItemHelp, "menuItemHelp");
            // 
            // menuItemContent
            // 
            this.menuItemContent.Name = "menuItemContent";
            resources.ApplyResources(this.menuItemContent, "menuItemContent");
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Image = global::GpxTracker.Properties.Resources.info_small;
            resources.ApplyResources(this.menuItemAbout, "menuItemAbout");
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.splitContainerBig.Panel1.ResumeLayout(false);
            this.splitContainerBig.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBig)).EndInit();
            this.splitContainerBig.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.splitContainerRight.Panel1.ResumeLayout(false);
            this.splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).EndInit();
            this.splitContainerRight.ResumeLayout(false);
            this.toolStripContainer3.ContentPanel.ResumeLayout(false);
            this.toolStripContainer3.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer3.ResumeLayout(false);
            this.toolStripContainer3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapView)).EndInit();
            this.toolStripView.ResumeLayout(false);
            this.toolStripView.PerformLayout();
            this.toolStripContainer2.ContentPanel.ResumeLayout(false);
            this.toolStripContainer2.LeftToolStripPanel.ResumeLayout(false);
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.toolStripChart.ResumeLayout(false);
            this.toolStripChart.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

		private GpxTracker.MapChart mapChart;
		private GpxTracker.MapView mapView;
		private System.Windows.Forms.SplitContainer splitContainerBig;
		private System.Windows.Forms.SplitContainer splitContainerRight;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem menuItemFile;
		private System.Windows.Forms.ToolStripMenuItem menuItemExit;
		private System.Windows.Forms.ToolStripMenuItem menuItemExtras;
		private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
		private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
		private System.Windows.Forms.ToolStripMenuItem menuItemContent;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer2;
		private System.Windows.Forms.ToolStrip toolStripChart;
		private System.Windows.Forms.ToolStripButton tsbChartDistance;
		private System.Windows.Forms.ToolStripButton tsbChartTime;
		private System.Windows.Forms.ToolStripButton tsbChartHeight;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton tsbChartSpeed;
		private System.Windows.Forms.ToolStripContainer toolStripContainer3;
		private System.Windows.Forms.ToolStrip toolStripView;
		private System.Windows.Forms.ToolStripButton tsbZoomIn;
		private System.Windows.Forms.ToolStripButton tsbZoomOut;
		private System.Windows.Forms.ToolStripLabel tslZoom;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.ToolStripMenuItem menuItemImport;
		private System.Windows.Forms.ToolStripButton tsbScreenshot;
		private System.Windows.Forms.Panel panelInfo;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelAccDesc;
		private System.Windows.Forms.Label labelAccAsc;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelSpeedAvg;
		private System.Windows.Forms.Label labelAlti;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelDist;
		private System.Windows.Forms.Label labelTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDistance;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
		private System.Windows.Forms.ToolStripMenuItem menuItemView;
		private System.Windows.Forms.ToolStripMenuItem menuItemYardstick;
		private System.Windows.Forms.ToolStripMenuItem menuItemMapGrid;
		private System.Windows.Forms.ToolStripMenuItem menuItemMapNumbers;
		private System.Windows.Forms.ToolStripButton tsbMeasure;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label labelSpeedCurr;
	}
}

