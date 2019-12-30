//------------------------------------------------------------------------------
// <copyright file="SettingsEditDialog.Designer.cs" company="">
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
// <summary>Contains information about the SettingsEditDialog class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	partial class SettingsEditDialog
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
			this.labelName = new System.Windows.Forms.Label();
			this.textBoxFormat = new System.Windows.Forms.TextBox();
			this.numEditMinZoom = new System.Windows.Forms.NumericUpDown();
			this.numEditMaxZoom = new System.Windows.Forms.NumericUpDown();
			this.labelFormat = new System.Windows.Forms.Label();
			this.labelLocalPath = new System.Windows.Forms.Label();
			this.labelMinZoom = new System.Windows.Forms.Label();
			this.labelMaxZoom = new System.Windows.Forms.Label();
			this.textBoxLocalPath = new System.Windows.Forms.TextBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.numEditMinZoom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numEditMaxZoom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(23, 27);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(35, 13);
			this.labelName.TabIndex = 0;
			this.labelName.Text = "Name";
			// 
			// textBoxFormat
			// 
			this.textBoxFormat.Location = new System.Drawing.Point(125, 61);
			this.textBoxFormat.Name = "textBoxFormat";
			this.textBoxFormat.Size = new System.Drawing.Size(256, 20);
			this.textBoxFormat.TabIndex = 1;
			// 
			// numEditMinZoom
			// 
			this.numEditMinZoom.Location = new System.Drawing.Point(125, 135);
			this.numEditMinZoom.Name = "numEditMinZoom";
			this.numEditMinZoom.Size = new System.Drawing.Size(52, 20);
			this.numEditMinZoom.TabIndex = 2;
			// 
			// numEditMaxZoom
			// 
			this.numEditMaxZoom.Location = new System.Drawing.Point(125, 172);
			this.numEditMaxZoom.Name = "numEditMaxZoom";
			this.numEditMaxZoom.Size = new System.Drawing.Size(52, 20);
			this.numEditMaxZoom.TabIndex = 3;
			// 
			// labelFormat
			// 
			this.labelFormat.AutoSize = true;
			this.labelFormat.Location = new System.Drawing.Point(23, 64);
			this.labelFormat.Name = "labelFormat";
			this.labelFormat.Size = new System.Drawing.Size(52, 13);
			this.labelFormat.TabIndex = 4;
			this.labelFormat.Text = "Web URI";
			// 
			// labelLocalPath
			// 
			this.labelLocalPath.AutoSize = true;
			this.labelLocalPath.Location = new System.Drawing.Point(23, 101);
			this.labelLocalPath.Name = "labelLocalPath";
			this.labelLocalPath.Size = new System.Drawing.Size(57, 13);
			this.labelLocalPath.TabIndex = 5;
			this.labelLocalPath.Text = "Local path";
			// 
			// labelMinZoom
			// 
			this.labelMinZoom.AutoSize = true;
			this.labelMinZoom.Location = new System.Drawing.Point(23, 137);
			this.labelMinZoom.Name = "labelMinZoom";
			this.labelMinZoom.Size = new System.Drawing.Size(57, 13);
			this.labelMinZoom.TabIndex = 6;
			this.labelMinZoom.Text = "Min. Zoom";
			// 
			// labelMaxZoom
			// 
			this.labelMaxZoom.AutoSize = true;
			this.labelMaxZoom.Location = new System.Drawing.Point(23, 174);
			this.labelMaxZoom.Name = "labelMaxZoom";
			this.labelMaxZoom.Size = new System.Drawing.Size(60, 13);
			this.labelMaxZoom.TabIndex = 9;
			this.labelMaxZoom.Text = "Max. Zoom";
			// 
			// textBoxLocalPath
			// 
			this.textBoxLocalPath.Location = new System.Drawing.Point(125, 98);
			this.textBoxLocalPath.Name = "textBoxLocalPath";
			this.textBoxLocalPath.Size = new System.Drawing.Size(256, 20);
			this.textBoxLocalPath.TabIndex = 8;
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(125, 24);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(256, 20);
			this.textBoxName.TabIndex = 12;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(258, 121);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(123, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "keywords: {zoom} {x} {y}";
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Image = global::GpxTracker.Properties.Resources.ok;
			this.buttonOk.Location = new System.Drawing.Point(222, 226);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(78, 25);
			this.buttonOk.TabIndex = 10;
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
			this.buttonCancel.Location = new System.Drawing.Point(121, 226);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(78, 25);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// pictureBox
			// 
			this.pictureBox.Image = global::GpxTracker.Properties.Resources.Network_Internet;
			this.pictureBox.Location = new System.Drawing.Point(333, 144);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(48, 48);
			this.pictureBox.TabIndex = 7;
			this.pictureBox.TabStop = false;
			// 
			// SettingsEditDialog
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(419, 270);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.labelMaxZoom);
			this.Controls.Add(this.textBoxLocalPath);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.labelMinZoom);
			this.Controls.Add(this.labelLocalPath);
			this.Controls.Add(this.labelFormat);
			this.Controls.Add(this.numEditMaxZoom);
			this.Controls.Add(this.numEditMinZoom);
			this.Controls.Add(this.textBoxFormat);
			this.Controls.Add(this.labelName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsEditDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit connection settings";
			((System.ComponentModel.ISupportInitialize)(this.numEditMinZoom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numEditMaxZoom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.TextBox textBoxFormat;
		private System.Windows.Forms.NumericUpDown numEditMinZoom;
		private System.Windows.Forms.NumericUpDown numEditMaxZoom;
		private System.Windows.Forms.Label labelFormat;
		private System.Windows.Forms.Label labelLocalPath;
		private System.Windows.Forms.Label labelMinZoom;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Label labelMaxZoom;
		private System.Windows.Forms.TextBox textBoxLocalPath;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label1;
	}
}