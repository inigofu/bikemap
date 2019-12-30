//------------------------------------------------------------------------------
// <copyright file="SyncForm.Designer.cs" company="">
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
// <summary>Contains information about the SyncForm class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	partial class SyncForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncForm));
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.dgvColCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dgvColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvExistsLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvColSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.textBoxFolder = new System.Windows.Forms.TextBox();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
			this.labelSelectedFiles = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(246, 480);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(89, 28);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(361, 480);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(89, 28);
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
			this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView.BackgroundColor = System.Drawing.Color.White;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColCheck,
            this.dgvColDescription,
            this.dgvExistsLocal,
            this.dgvColSize});
			this.dataGridView.Location = new System.Drawing.Point(12, 60);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(438, 400);
			this.dataGridView.StandardTab = true;
			this.dataGridView.TabIndex = 3;
			this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
			// 
			// dgvColCheck
			// 
			this.dgvColCheck.FillWeight = 1F;
			this.dgvColCheck.HeaderText = "Copy";
			this.dgvColCheck.MinimumWidth = 40;
			this.dgvColCheck.Name = "dgvColCheck";
			this.dgvColCheck.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// dgvColDescription
			// 
			dataGridViewCellStyle2.Format = "G";
			dataGridViewCellStyle2.NullValue = null;
			this.dgvColDescription.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvColDescription.FillWeight = 79F;
			this.dgvColDescription.HeaderText = "Description";
			this.dgvColDescription.MinimumWidth = 50;
			this.dgvColDescription.Name = "dgvColDescription";
			this.dgvColDescription.ReadOnly = true;
			// 
			// dgvExistsLocal
			// 
			this.dgvExistsLocal.FillWeight = 10F;
			this.dgvExistsLocal.HeaderText = "Local";
			this.dgvExistsLocal.MinimumWidth = 55;
			this.dgvExistsLocal.Name = "dgvExistsLocal";
			this.dgvExistsLocal.ReadOnly = true;
			// 
			// dgvColSize
			// 
			this.dgvColSize.FillWeight = 10F;
			this.dgvColSize.HeaderText = "Size";
			this.dgvColSize.MinimumWidth = 65;
			this.dgvColSize.Name = "dgvColSize";
			this.dgvColSize.ReadOnly = true;
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowse.Image = global::GpxTracker.Properties.Resources.openfolderHS;
			this.buttonBrowse.Location = new System.Drawing.Point(421, 18);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(29, 27);
			this.buttonBrowse.TabIndex = 2;
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// textBoxFolder
			// 
			this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFolder.BackColor = System.Drawing.SystemColors.Window;
			this.textBoxFolder.Location = new System.Drawing.Point(12, 22);
			this.textBoxFolder.Name = "textBoxFolder";
			this.textBoxFolder.ReadOnly = true;
			this.textBoxFolder.Size = new System.Drawing.Size(403, 20);
			this.textBoxFolder.TabIndex = 1;
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.ShowNewFolderButton = false;
			// 
			// checkBoxSelectAll
			// 
			this.checkBoxSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxSelectAll.AutoSize = true;
			this.checkBoxSelectAll.Location = new System.Drawing.Point(12, 466);
			this.checkBoxSelectAll.Name = "checkBoxSelectAll";
			this.checkBoxSelectAll.Size = new System.Drawing.Size(120, 17);
			this.checkBoxSelectAll.TabIndex = 6;
			this.checkBoxSelectAll.Text = "Select / deselect all";
			this.checkBoxSelectAll.UseVisualStyleBackColor = true;
			this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectAll_CheckedChanged);
			// 
			// labelSelectedFiles
			// 
			this.labelSelectedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelSelectedFiles.AutoSize = true;
			this.labelSelectedFiles.Location = new System.Drawing.Point(9, 495);
			this.labelSelectedFiles.Name = "labelSelectedFiles";
			this.labelSelectedFiles.Size = new System.Drawing.Size(88, 13);
			this.labelSelectedFiles.TabIndex = 7;
			this.labelSelectedFiles.Text = "{0} Files selected";
			// 
			// SyncForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(462, 520);
			this.Controls.Add(this.labelSelectedFiles);
			this.Controls.Add(this.checkBoxSelectAll);
			this.Controls.Add(this.buttonBrowse);
			this.Controls.Add(this.textBoxFolder);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(470, 480);
			this.Name = "SyncForm";
			this.Text = "File overview";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.CheckBox checkBoxSelectAll;
		private System.Windows.Forms.Label labelSelectedFiles;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColCheck;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColDescription;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvExistsLocal;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSize;
	}
}