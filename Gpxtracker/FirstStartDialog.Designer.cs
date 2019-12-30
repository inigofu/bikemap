//------------------------------------------------------------------------------
// <copyright file="FirstStartDialog.Designer.cs" company="">
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
// <summary>Contains information about the FirstStartDialog class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	partial class FirstStartDialog
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstStartDialog));
			this.buttonNo = new System.Windows.Forms.Button();
			this.labelWelcome = new System.Windows.Forms.Label();
			this.labelText = new System.Windows.Forms.Label();
			this.buttonYes = new System.Windows.Forms.Button();
			this.pictureBoxImage = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonNo
			// 
			this.buttonNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.buttonNo.Image = global::GpxTracker.Properties.Resources.cross;
			this.buttonNo.Location = new System.Drawing.Point(234, 316);
			this.buttonNo.Name = "buttonNo";
			this.buttonNo.Size = new System.Drawing.Size(80, 25);
			this.buttonNo.TabIndex = 14;
			this.buttonNo.Text = "No";
			this.buttonNo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.buttonNo.UseVisualStyleBackColor = true;
			// 
			// labelWelcome
			// 
			this.labelWelcome.AutoSize = true;
			this.labelWelcome.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelWelcome.Location = new System.Drawing.Point(197, 23);
			this.labelWelcome.Name = "labelWelcome";
			this.labelWelcome.Size = new System.Drawing.Size(131, 33);
			this.labelWelcome.TabIndex = 16;
			this.labelWelcome.Text = "Welcome!";
			// 
			// labelText
			// 
			this.labelText.Location = new System.Drawing.Point(200, 77);
			this.labelText.Name = "labelText";
			this.labelText.Size = new System.Drawing.Size(249, 219);
			this.labelText.TabIndex = 17;
			this.labelText.Text = resources.GetString("labelText.Text");
			// 
			// buttonYes
			// 
			this.buttonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.buttonYes.Image = global::GpxTracker.Properties.Resources.ok;
			this.buttonYes.Location = new System.Drawing.Point(358, 316);
			this.buttonYes.Name = "buttonYes";
			this.buttonYes.Size = new System.Drawing.Size(80, 25);
			this.buttonYes.TabIndex = 15;
			this.buttonYes.Text = "Yes";
			this.buttonYes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.buttonYes.UseVisualStyleBackColor = true;
			// 
			// pictureBoxImage
			// 
			this.pictureBoxImage.Image = global::GpxTracker.Properties.Resources.regensburg_small;
			this.pictureBoxImage.Location = new System.Drawing.Point(6, 6);
			this.pictureBoxImage.Name = "pictureBoxImage";
			this.pictureBoxImage.Size = new System.Drawing.Size(162, 344);
			this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxImage.TabIndex = 13;
			this.pictureBoxImage.TabStop = false;
			// 
			// FirstStartDialog
			// 
			this.AcceptButton = this.buttonYes;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonNo;
			this.ClientSize = new System.Drawing.Size(496, 356);
			this.Controls.Add(this.labelText);
			this.Controls.Add(this.labelWelcome);
			this.Controls.Add(this.buttonYes);
			this.Controls.Add(this.buttonNo);
			this.Controls.Add(this.pictureBoxImage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FirstStartDialog";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "First Start";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxImage;
		private System.Windows.Forms.Button buttonNo;
		private System.Windows.Forms.Button buttonYes;
		private System.Windows.Forms.Label labelWelcome;
		private System.Windows.Forms.Label labelText;

	}
}
