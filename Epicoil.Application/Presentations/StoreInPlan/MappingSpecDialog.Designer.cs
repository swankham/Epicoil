namespace Epicoil.Appl.Presentations.StoreInPlan
{
    partial class MappingSpecDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingSpecDialog));
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.butDelete = new System.Windows.Forms.Button();
            this.butAdd = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMatSpec2 = new System.Windows.Forms.TextBox();
            this.txtMatSpec1 = new System.Windows.Forms.TextBox();
            this.butSpec = new System.Windows.Forms.Button();
            this.txtCommodityName = new System.Windows.Forms.TextBox();
            this.txtCommodityCode = new System.Windows.Forms.TextBox();
            this.butCommodity = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LookupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvList.ColumnHeadersHeight = 25;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LookupID,
            this.TypeCode,
            this.Source,
            this.cmdt,
            this.spec});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(376, 308);
            this.dgvList.TabIndex = 15;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
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
            this.splitContainer1.Panel1.Controls.Add(this.butDelete);
            this.splitContainer1.Panel1.Controls.Add(this.butAdd);
            this.splitContainer1.Panel1.Controls.Add(this.txtSource);
            this.splitContainer1.Panel1.Controls.Add(this.txtSupplierName);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvList);
            this.splitContainer1.Size = new System.Drawing.Size(376, 462);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 5;
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(294, 32);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(75, 23);
            this.butDelete.TabIndex = 6;
            this.butDelete.Text = "Delete";
            this.butDelete.UseVisualStyleBackColor = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butAdd
            // 
            this.butAdd.Location = new System.Drawing.Point(213, 32);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(75, 23);
            this.butAdd.TabIndex = 5;
            this.butAdd.Text = "Add";
            this.butAdd.UseVisualStyleBackColor = true;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(69, 34);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(138, 20);
            this.txtSource.TabIndex = 4;
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(69, 6);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.ReadOnly = true;
            this.txtSupplierName.Size = new System.Drawing.Size(300, 20);
            this.txtSupplierName.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMatSpec2);
            this.groupBox1.Controls.Add(this.txtMatSpec1);
            this.groupBox1.Controls.Add(this.butSpec);
            this.groupBox1.Controls.Add(this.txtCommodityName);
            this.groupBox1.Controls.Add(this.txtCommodityCode);
            this.groupBox1.Controls.Add(this.butCommodity);
            this.groupBox1.Location = new System.Drawing.Point(12, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 77);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result";
            // 
            // txtMatSpec2
            // 
            this.txtMatSpec2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatSpec2.Location = new System.Drawing.Point(162, 49);
            this.txtMatSpec2.Name = "txtMatSpec2";
            this.txtMatSpec2.ReadOnly = true;
            this.txtMatSpec2.Size = new System.Drawing.Size(181, 20);
            this.txtMatSpec2.TabIndex = 20;
            // 
            // txtMatSpec1
            // 
            this.txtMatSpec1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatSpec1.Location = new System.Drawing.Point(115, 48);
            this.txtMatSpec1.Name = "txtMatSpec1";
            this.txtMatSpec1.Size = new System.Drawing.Size(41, 20);
            this.txtMatSpec1.TabIndex = 19;
            this.txtMatSpec1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // butSpec
            // 
            this.butSpec.Location = new System.Drawing.Point(21, 45);
            this.butSpec.Name = "butSpec";
            this.butSpec.Size = new System.Drawing.Size(88, 23);
            this.butSpec.TabIndex = 18;
            this.butSpec.Text = "Spec";
            this.butSpec.UseVisualStyleBackColor = true;
            this.butSpec.Click += new System.EventHandler(this.butSpec_Click);
            // 
            // txtCommodityName
            // 
            this.txtCommodityName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCommodityName.Location = new System.Drawing.Point(162, 22);
            this.txtCommodityName.Name = "txtCommodityName";
            this.txtCommodityName.ReadOnly = true;
            this.txtCommodityName.Size = new System.Drawing.Size(181, 20);
            this.txtCommodityName.TabIndex = 17;
            // 
            // txtCommodityCode
            // 
            this.txtCommodityCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCommodityCode.Location = new System.Drawing.Point(115, 22);
            this.txtCommodityCode.Name = "txtCommodityCode";
            this.txtCommodityCode.Size = new System.Drawing.Size(41, 20);
            this.txtCommodityCode.TabIndex = 16;
            this.txtCommodityCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // butCommodity
            // 
            this.butCommodity.Location = new System.Drawing.Point(21, 19);
            this.butCommodity.Name = "butCommodity";
            this.butCommodity.Size = new System.Drawing.Size(88, 23);
            this.butCommodity.TabIndex = 15;
            this.butCommodity.Text = "Commodity";
            this.butCommodity.UseVisualStyleBackColor = true;
            this.butCommodity.Click += new System.EventHandler(this.butCommodity_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Source :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Supplier :";
            // 
            // LookupID
            // 
            this.LookupID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LookupID.HeaderText = "LookupID";
            this.LookupID.Name = "LookupID";
            this.LookupID.Visible = false;
            this.LookupID.Width = 78;
            // 
            // TypeCode
            // 
            this.TypeCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TypeCode.HeaderText = "TypeCode";
            this.TypeCode.Name = "TypeCode";
            this.TypeCode.ReadOnly = true;
            this.TypeCode.Visible = false;
            this.TypeCode.Width = 80;
            // 
            // Source
            // 
            this.Source.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Source.HeaderText = "Source";
            this.Source.Name = "Source";
            this.Source.ReadOnly = true;
            // 
            // cmdt
            // 
            this.cmdt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmdt.HeaderText = "Commodity";
            this.cmdt.Name = "cmdt";
            this.cmdt.ReadOnly = true;
            // 
            // spec
            // 
            this.spec.HeaderText = "Spec Code";
            this.spec.Name = "spec";
            this.spec.ReadOnly = true;
            // 
            // MappingSpecDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(220)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(376, 462);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MappingSpecDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Specification mapping lookup";
            this.Load += new System.EventHandler(this.MappingSpecDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button butDelete;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCommodityName;
        private System.Windows.Forms.TextBox txtCommodityCode;
        private System.Windows.Forms.Button butCommodity;
        private System.Windows.Forms.TextBox txtMatSpec2;
        private System.Windows.Forms.TextBox txtMatSpec1;
        private System.Windows.Forms.Button butSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn LookupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdt;
        private System.Windows.Forms.DataGridViewTextBoxColumn spec;
    }
}