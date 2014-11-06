namespace Epicoil.Appl.Presentations.Sales
{
    partial class OrderLineDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderLineDialog));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butSelect = new System.Windows.Forms.Button();
            this.butClear = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.ordernum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.norno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.possession = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Commodity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bussinessType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineweight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvList);
            this.splitContainer1.Size = new System.Drawing.Size(916, 302);
            this.splitContainer1.SplitterDistance = 55;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butSelect);
            this.groupBox1.Controls.Add(this.butClear);
            this.groupBox1.Controls.Add(this.butSearch);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(903, 37);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // butSelect
            // 
            this.butSelect.Location = new System.Drawing.Point(818, 9);
            this.butSelect.Name = "butSelect";
            this.butSelect.Size = new System.Drawing.Size(80, 25);
            this.butSelect.TabIndex = 3;
            this.butSelect.Text = "Select";
            this.butSelect.UseVisualStyleBackColor = true;
            this.butSelect.Click += new System.EventHandler(this.butSelect_Click);
            // 
            // butClear
            // 
            this.butClear.Location = new System.Drawing.Point(736, 9);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(80, 25);
            this.butClear.TabIndex = 2;
            this.butClear.Text = "Clear";
            this.butClear.UseVisualStyleBackColor = true;
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(654, 9);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(80, 25);
            this.butSearch.TabIndex = 1;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = true;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvList.BackgroundColor = System.Drawing.Color.White;
            this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvList.ColumnHeadersHeight = 25;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ordernum,
            this.orderline,
            this.norno,
            this.possession,
            this.Commodity,
            this.spec,
            this.coating,
            this.bussinessType,
            this.thick,
            this.width,
            this.length,
            this.lineweight,
            this.lineamount});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(916, 243);
            this.dgvList.TabIndex = 18;
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            // 
            // ordernum
            // 
            this.ordernum.HeaderText = "OrderNum";
            this.ordernum.Name = "ordernum";
            this.ordernum.ReadOnly = true;
            this.ordernum.Visible = false;
            this.ordernum.Width = 80;
            // 
            // orderline
            // 
            this.orderline.HeaderText = "Line No.";
            this.orderline.Name = "orderline";
            this.orderline.ReadOnly = true;
            this.orderline.Width = 77;
            // 
            // norno
            // 
            this.norno.HeaderText = "NOR No.";
            this.norno.Name = "norno";
            this.norno.ReadOnly = true;
            this.norno.Width = 80;
            // 
            // possession
            // 
            this.possession.HeaderText = "Possession";
            this.possession.Name = "possession";
            this.possession.ReadOnly = true;
            this.possession.Width = 94;
            // 
            // Commodity
            // 
            this.Commodity.HeaderText = "Commodity";
            this.Commodity.Name = "Commodity";
            this.Commodity.ReadOnly = true;
            this.Commodity.Width = 93;
            // 
            // spec
            // 
            this.spec.HeaderText = "Spec";
            this.spec.Name = "spec";
            this.spec.ReadOnly = true;
            this.spec.Width = 59;
            // 
            // coating
            // 
            this.coating.HeaderText = "Coating";
            this.coating.Name = "coating";
            this.coating.ReadOnly = true;
            this.coating.Width = 73;
            // 
            // bussinessType
            // 
            this.bussinessType.HeaderText = "BussinessType";
            this.bussinessType.Name = "bussinessType";
            this.bussinessType.ReadOnly = true;
            this.bussinessType.Width = 113;
            // 
            // thick
            // 
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.thick.DefaultCellStyle = dataGridViewCellStyle1;
            this.thick.HeaderText = "Thick";
            this.thick.Name = "thick";
            this.thick.ReadOnly = true;
            this.thick.Width = 60;
            // 
            // width
            // 
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.width.DefaultCellStyle = dataGridViewCellStyle2;
            this.width.HeaderText = "Width";
            this.width.Name = "width";
            this.width.ReadOnly = true;
            this.width.Width = 62;
            // 
            // length
            // 
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.length.DefaultCellStyle = dataGridViewCellStyle3;
            this.length.HeaderText = "Length";
            this.length.Name = "length";
            this.length.ReadOnly = true;
            this.length.Width = 69;
            // 
            // lineweight
            // 
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.lineweight.DefaultCellStyle = dataGridViewCellStyle4;
            this.lineweight.HeaderText = "Line Weight";
            this.lineweight.Name = "lineweight";
            this.lineweight.ReadOnly = true;
            this.lineweight.Width = 96;
            // 
            // lineamount
            // 
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0";
            this.lineamount.DefaultCellStyle = dataGridViewCellStyle5;
            this.lineamount.HeaderText = "Line Amount";
            this.lineamount.Name = "lineamount";
            this.lineamount.ReadOnly = true;
            // 
            // OrderLineDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 302);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderLineDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order Line Dialog";
            this.Load += new System.EventHandler(this.OrderLineDialog_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butSelect;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordernum;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderline;
        private System.Windows.Forms.DataGridViewTextBoxColumn norno;
        private System.Windows.Forms.DataGridViewTextBoxColumn possession;
        private System.Windows.Forms.DataGridViewTextBoxColumn Commodity;
        private System.Windows.Forms.DataGridViewTextBoxColumn spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn coating;
        private System.Windows.Forms.DataGridViewTextBoxColumn bussinessType;
        private System.Windows.Forms.DataGridViewTextBoxColumn thick;
        private System.Windows.Forms.DataGridViewTextBoxColumn width;
        private System.Windows.Forms.DataGridViewTextBoxColumn length;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineweight;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineamount;
    }
}