//------------------------------------------------------------------------------
// <copyright file="CopyForm.Designer.cs" company="">
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
// <summary>Contains information about the CopyForm class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	partial class CopyForm
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
			this.animation = new GpxTracker.MyAnimation();
			this.labelStatus0 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.labelStatus1 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// animation
			// 
			this.animation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.animation.FileName = null;
			this.animation.FileType = GpxTracker.MyAnimation.AviFileType.CopyFile;
			this.animation.Location = new System.Drawing.Point(46, 12);
			this.animation.Name = "animation";
			this.animation.Size = new System.Drawing.Size(272, 60);
			this.animation.TabIndex = 0;
			// 
			// labelStatus0
			// 
			this.labelStatus0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelStatus0.AutoSize = true;
			this.labelStatus0.Location = new System.Drawing.Point(12, 137);
			this.labelStatus0.Name = "labelStatus0";
			this.labelStatus0.Size = new System.Drawing.Size(65, 13);
			this.labelStatus0.TabIndex = 13;
			this.labelStatus0.Text = "labelStatus0";
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(15, 201);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(89, 25);
			this.buttonCancel.TabIndex = 12;
			this.buttonCancel.Text = "Cancel (inv)";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Visible = false;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(15, 103);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(338, 14);
			this.progressBar.Step = 1;
			this.progressBar.TabIndex = 11;
			// 
			// labelStatus1
			// 
			this.labelStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelStatus1.AutoSize = true;
			this.labelStatus1.Location = new System.Drawing.Point(12, 160);
			this.labelStatus1.Name = "labelStatus1";
			this.labelStatus1.Size = new System.Drawing.Size(65, 13);
			this.labelStatus1.TabIndex = 14;
			this.labelStatus1.Text = "labelStatus1";
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Enabled = false;
			this.buttonOK.Location = new System.Drawing.Point(138, 201);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(89, 25);
			this.buttonOK.TabIndex = 15;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			// 
			// CopyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(365, 240);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.labelStatus1);
			this.Controls.Add(this.labelStatus0);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.animation);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CopyForm";
			this.Text = "Copy Files ...";
			this.Load += new System.EventHandler(this.CopyForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MyAnimation animation;
		private System.Windows.Forms.Label labelStatus0;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label labelStatus1;
		private System.Windows.Forms.Button buttonOK;
	}
}