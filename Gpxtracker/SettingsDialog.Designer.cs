//------------------------------------------------------------------------------
// <copyright file="SettingsDialog.Designer.cs" company="">
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
// <summary>Contains information about the SettingsDialog class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	partial class SettingsDialog
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
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.comboBoxMapSource = new System.Windows.Forms.ComboBox();
			this.labelMapDataSource = new System.Windows.Forms.Label();
			this.labelKeepMapFiles = new System.Windows.Forms.Label();
			this.comboBoxCacheHistory = new System.Windows.Forms.ComboBox();
			this.buttonEditSource = new System.Windows.Forms.Button();
			this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.textBoxFolder = new System.Windows.Forms.TextBox();
			this.labelLocalPath = new System.Windows.Forms.Label();
			this.comboBoxSystem = new System.Windows.Forms.ComboBox();
			this.labelSystem = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Image = global::GpxTracker.Properties.Resources.ok;
			this.buttonOk.Location = new System.Drawing.Point(230, 215);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(78, 25);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "OK";
			this.buttonOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Image = global::GpxTracker.Properties.Resources.cross;
			this.buttonCancel.Location = new System.Drawing.Point(129, 215);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(78, 25);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// comboBoxMapSource
			// 
			this.comboBoxMapSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMapSource.FormattingEnabled = true;
			this.comboBoxMapSource.Location = new System.Drawing.Point(146, 98);
			this.comboBoxMapSource.Name = "comboBoxMapSource";
			this.comboBoxMapSource.Size = new System.Drawing.Size(192, 21);
			this.comboBoxMapSource.TabIndex = 2;
			// 
			// labelMapDataSource
			// 
			this.labelMapDataSource.AutoSize = true;
			this.labelMapDataSource.Location = new System.Drawing.Point(24, 101);
			this.labelMapDataSource.Name = "labelMapDataSource";
			this.labelMapDataSource.Size = new System.Drawing.Size(87, 13);
			this.labelMapDataSource.TabIndex = 3;
			this.labelMapDataSource.Text = "Map data source";
			// 
			// labelKeepMapFiles
			// 
			this.labelKeepMapFiles.AutoSize = true;
			this.labelKeepMapFiles.Location = new System.Drawing.Point(24, 29);
			this.labelKeepMapFiles.Name = "labelKeepMapFiles";
			this.labelKeepMapFiles.Size = new System.Drawing.Size(97, 13);
			this.labelKeepMapFiles.TabIndex = 4;
			this.labelKeepMapFiles.Text = "Cache map files for";
			// 
			// comboBoxCacheHistory
			// 
			this.comboBoxCacheHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCacheHistory.FormattingEnabled = true;
			this.comboBoxCacheHistory.Location = new System.Drawing.Point(146, 26);
			this.comboBoxCacheHistory.Name = "comboBoxCacheHistory";
			this.comboBoxCacheHistory.Size = new System.Drawing.Size(192, 21);
			this.comboBoxCacheHistory.TabIndex = 5;
			// 
			// buttonEditSource
			// 
			this.buttonEditSource.Image = global::GpxTracker.Properties.Resources.NewCardHS2;
			this.buttonEditSource.Location = new System.Drawing.Point(344, 95);
			this.buttonEditSource.Name = "buttonEditSource";
			this.buttonEditSource.Size = new System.Drawing.Size(29, 27);
			this.buttonEditSource.TabIndex = 7;
			this.buttonEditSource.UseVisualStyleBackColor = true;
			this.buttonEditSource.Click += new System.EventHandler(this.buttonEditSource_Click);
			// 
			// pictureBoxIcon
			// 
			this.pictureBoxIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxIcon.Image = global::GpxTracker.Properties.Resources.gears;
			this.pictureBoxIcon.Location = new System.Drawing.Point(388, 12);
			this.pictureBoxIcon.Name = "pictureBoxIcon";
			this.pictureBoxIcon.Size = new System.Drawing.Size(37, 33);
			this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxIcon.TabIndex = 6;
			this.pictureBoxIcon.TabStop = false;
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowse.Image = global::GpxTracker.Properties.Resources.openfolderHS;
			this.buttonBrowse.Location = new System.Drawing.Point(344, 133);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(29, 27);
			this.buttonBrowse.TabIndex = 9;
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// textBoxFolder
			// 
			this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFolder.BackColor = System.Drawing.SystemColors.Window;
			this.textBoxFolder.Location = new System.Drawing.Point(146, 137);
			this.textBoxFolder.Name = "textBoxFolder";
			this.textBoxFolder.ReadOnly = true;
			this.textBoxFolder.Size = new System.Drawing.Size(192, 20);
			this.textBoxFolder.TabIndex = 8;
			// 
			// labelLocalPath
			// 
			this.labelLocalPath.AutoSize = true;
			this.labelLocalPath.Location = new System.Drawing.Point(24, 140);
			this.labelLocalPath.Name = "labelLocalPath";
			this.labelLocalPath.Size = new System.Drawing.Size(57, 13);
			this.labelLocalPath.TabIndex = 10;
			this.labelLocalPath.Text = "Local path";
			// 
			// comboBoxSystem
			// 
			this.comboBoxSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSystem.FormattingEnabled = true;
			this.comboBoxSystem.Items.AddRange(new object[] {
            "Metric",
            "Imperial"});
			this.comboBoxSystem.Location = new System.Drawing.Point(146, 62);
			this.comboBoxSystem.Name = "comboBoxSystem";
			this.comboBoxSystem.Size = new System.Drawing.Size(192, 21);
			this.comboBoxSystem.TabIndex = 12;
			// 
			// labelSystem
			// 
			this.labelSystem.AutoSize = true;
			this.labelSystem.Location = new System.Drawing.Point(24, 65);
			this.labelSystem.Name = "labelSystem";
			this.labelSystem.Size = new System.Drawing.Size(41, 13);
			this.labelSystem.TabIndex = 11;
			this.labelSystem.Text = "System";
			// 
			// SettingsDialog
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(437, 264);
			this.Controls.Add(this.comboBoxSystem);
			this.Controls.Add(this.labelSystem);
			this.Controls.Add(this.labelLocalPath);
			this.Controls.Add(this.buttonBrowse);
			this.Controls.Add(this.textBoxFolder);
			this.Controls.Add(this.buttonEditSource);
			this.Controls.Add(this.pictureBoxIcon);
			this.Controls.Add(this.comboBoxCacheHistory);
			this.Controls.Add(this.labelKeepMapFiles);
			this.Controls.Add(this.labelMapDataSource);
			this.Controls.Add(this.comboBoxMapSource);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ComboBox comboBoxMapSource;
		private System.Windows.Forms.Label labelMapDataSource;
		private System.Windows.Forms.Label labelKeepMapFiles;
		private System.Windows.Forms.ComboBox comboBoxCacheHistory;
		private System.Windows.Forms.PictureBox pictureBoxIcon;
		private System.Windows.Forms.Button buttonEditSource;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.Label labelLocalPath;
		private System.Windows.Forms.ComboBox comboBoxSystem;
		private System.Windows.Forms.Label labelSystem;
	}
}