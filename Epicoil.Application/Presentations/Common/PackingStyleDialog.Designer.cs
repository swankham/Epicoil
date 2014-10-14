namespace Epicoil.Appl.Presentations
{
    partial class PackingStyleDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackingStyleDialog));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbStyleType = new Epicoil.Library.Frameworks.CustomComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCoating = new System.Windows.Forms.TextBox();
            this.butSearch = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.stylecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coileskid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coilwrapping = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coilstrapping = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sheetskid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sheetwrapping = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sheetstrapping = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvList);
            this.splitContainer1.Size = new System.Drawing.Size(806, 484);
            this.splitContainer1.SplitterDistance = 66;
            this.splitContainer1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbStyleType);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtCoating);
            this.panel2.Controls.Add(this.butSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(456, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 66);
            this.panel2.TabIndex = 0;
            // 
            // cmbStyleType
            // 
            this.cmbStyleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyleType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbStyleType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbStyleType.FormattingEnabled = true;
            this.cmbStyleType.Items.AddRange(new object[] {
            "",
            "Sheet",
            "Eye Up",
            "Eye Side"});
            this.cmbStyleType.Location = new System.Drawing.Point(110, 32);
            this.cmbStyleType.Name = "cmbStyleType";
            this.cmbStyleType.Size = new System.Drawing.Size(137, 23);
            this.cmbStyleType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Packing Style Type :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Style Code :";
            // 
            // txtCoating
            // 
            this.txtCoating.Location = new System.Drawing.Point(110, 9);
            this.txtCoating.Name = "txtCoating";
            this.txtCoating.Size = new System.Drawing.Size(137, 20);
            this.txtCoating.TabIndex = 1;
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(253, 9);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(91, 23);
            this.butSearch.TabIndex = 0;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvList.BackgroundColor = System.Drawing.Color.White;
            this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvList.ColumnHeadersHeight = 25;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stylecode,
            this.type,
            this.coileskid,
            this.coilwrapping,
            this.coilstrapping,
            this.sheetskid,
            this.sheetwrapping,
            this.sheetstrapping});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(806, 414);
            this.dgvList.TabIndex = 15;
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            // 
            // stylecode
            // 
            this.stylecode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.stylecode.HeaderText = "Style Code";
            this.stylecode.Name = "stylecode";
            this.stylecode.ReadOnly = true;
            this.stylecode.Width = 83;
            // 
            // type
            // 
            this.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.type.HeaderText = "Type";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 56;
            // 
            // coileskid
            // 
            this.coileskid.HeaderText = "Coil Skid";
            this.coileskid.Name = "coileskid";
            this.coileskid.ReadOnly = true;
            // 
            // coilwrapping
            // 
            this.coilwrapping.HeaderText = "Coil Wrapping";
            this.coilwrapping.Name = "coilwrapping";
            this.coilwrapping.ReadOnly = true;
            // 
            // coilstrapping
            // 
            this.coilstrapping.HeaderText = "Coil Strapping";
            this.coilstrapping.Name = "coilstrapping";
            this.coilstrapping.ReadOnly = true;
            // 
            // sheetskid
            // 
            this.sheetskid.HeaderText = "Sheet Skid";
            this.sheetskid.Name = "sheetskid";
            this.sheetskid.ReadOnly = true;
            // 
            // sheetwrapping
            // 
            this.sheetwrapping.HeaderText = "Sheet Wrapping";
            this.sheetwrapping.Name = "sheetwrapping";
            this.sheetwrapping.ReadOnly = true;
            // 
            // sheetstrapping
            // 
            this.sheetstrapping.HeaderText = "Sheet Strapping";
            this.sheetstrapping.Name = "sheetstrapping";
            this.sheetstrapping.ReadOnly = true;
            // 
            // PackingStyleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(220)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(806, 484);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PackingStyleDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PackingStyle Dialog";
            this.Load += new System.EventHandler(this.PackingStyleDialog_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCoating;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.DataGridView dgvList;
        private Epicoil.Library.Frameworks.CustomComboBox cmbStyleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn stylecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn coileskid;
        private System.Windows.Forms.DataGridViewTextBoxColumn coilwrapping;
        private System.Windows.Forms.DataGridViewTextBoxColumn coilstrapping;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheetskid;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheetwrapping;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheetstrapping;
    }
}