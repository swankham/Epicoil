namespace Epicoil.Appl.Presentations.Planning
{
    partial class WorkEntryDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkEntryDialog));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvMaterial = new System.Windows.Forms.DataGridView();
            this.workordernumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvMaterial);
            this.splitContainer1.Size = new System.Drawing.Size(997, 483);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvMaterial
            // 
            this.dgvMaterial.AllowUserToAddRows = false;
            this.dgvMaterial.AllowUserToDeleteRows = false;
            this.dgvMaterial.AllowUserToResizeRows = false;
            this.dgvMaterial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMaterial.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMaterial.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMaterial.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvMaterial.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvMaterial.ColumnHeadersHeight = 25;
            this.dgvMaterial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.workordernumber});
            this.dgvMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterial.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvMaterial.EnableHeadersVisualStyles = false;
            this.dgvMaterial.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvMaterial.Location = new System.Drawing.Point(0, 0);
            this.dgvMaterial.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMaterial.MultiSelect = false;
            this.dgvMaterial.Name = "dgvMaterial";
            this.dgvMaterial.RowHeadersVisible = false;
            this.dgvMaterial.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterial.Size = new System.Drawing.Size(997, 339);
            this.dgvMaterial.TabIndex = 18;
            // 
            // workordernumber
            // 
            this.workordernumber.HeaderText = "Work No.";
            this.workordernumber.Name = "workordernumber";
            this.workordernumber.ReadOnly = true;
            this.workordernumber.Width = 81;
            // 
            // WorkEntryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 483);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkEntryDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Work Entry Dialog";
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn workordernumber;
    }
}