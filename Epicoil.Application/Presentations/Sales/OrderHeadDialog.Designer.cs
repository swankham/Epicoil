namespace Epicoil.Appl.Presentations.Sales
{
    partial class OrderHeadDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderHeadDialog));
            this.butSelect = new System.Windows.Forms.Button();
            this.butClear = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.txtBT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.txtCustID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.orderid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordernum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shipto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.custpo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.socode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.term = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordertype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalweight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butSelect
            // 
            this.butSelect.Location = new System.Drawing.Point(627, 65);
            this.butSelect.Name = "butSelect";
            this.butSelect.Size = new System.Drawing.Size(80, 25);
            this.butSelect.TabIndex = 3;
            this.butSelect.Text = "Select";
            this.butSelect.UseVisualStyleBackColor = true;
            this.butSelect.Click += new System.EventHandler(this.butSelect_Click);
            // 
            // butClear
            // 
            this.butClear.Location = new System.Drawing.Point(545, 65);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(80, 25);
            this.butClear.TabIndex = 2;
            this.butClear.Text = "Clear";
            this.butClear.UseVisualStyleBackColor = true;
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(463, 65);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(80, 25);
            this.butSearch.TabIndex = 1;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = true;
            // 
            // txtBT
            // 
            this.txtBT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBT.Location = new System.Drawing.Point(583, 11);
            this.txtBT.Name = "txtBT";
            this.txtBT.Size = new System.Drawing.Size(124, 21);
            this.txtBT.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(544, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "B/T :";
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
            this.orderid,
            this.ordernum,
            this.orderdate,
            this.customer,
            this.enduser,
            this.shipto,
            this.custpo,
            this.socode,
            this.term,
            this.bt,
            this.ordertype,
            this.totalweight,
            this.totalamount});
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
            this.dgvList.Size = new System.Drawing.Size(725, 534);
            this.dgvList.TabIndex = 18;
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            // 
            // txtCustID
            // 
            this.txtCustID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustID.Location = new System.Drawing.Point(110, 38);
            this.txtCustID.Name = "txtCustID";
            this.txtCustID.Size = new System.Drawing.Size(124, 21);
            this.txtCustID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Code :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.butSelect);
            this.groupBox1.Controls.Add(this.butClear);
            this.groupBox1.Controls.Add(this.butSearch);
            this.groupBox1.Controls.Add(this.txtBT);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCustID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Location = new System.Drawing.Point(337, 38);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(124, 21);
            this.textBox4.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(266, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "S/O Code :";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(337, 11);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(124, 21);
            this.textBox3.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(245, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "Customer PO :";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(110, 11);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(124, 21);
            this.textBox2.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Order No. :";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(110, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(124, 21);
            this.textBox1.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "End user Code :";
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvList);
            this.splitContainer1.Size = new System.Drawing.Size(725, 639);
            this.splitContainer1.SplitterDistance = 101;
            this.splitContainer1.TabIndex = 1;
            // 
            // orderid
            // 
            this.orderid.HeaderText = "ID";
            this.orderid.Name = "orderid";
            this.orderid.ReadOnly = true;
            this.orderid.Width = 43;
            // 
            // ordernum
            // 
            this.ordernum.HeaderText = "Order No.";
            this.ordernum.Name = "ordernum";
            this.ordernum.ReadOnly = true;
            this.ordernum.Width = 84;
            // 
            // orderdate
            // 
            this.orderdate.HeaderText = "Order Date";
            this.orderdate.Name = "orderdate";
            this.orderdate.ReadOnly = true;
            this.orderdate.Width = 91;
            // 
            // customer
            // 
            this.customer.HeaderText = "Customer";
            this.customer.Name = "customer";
            this.customer.ReadOnly = true;
            this.customer.Width = 84;
            // 
            // enduser
            // 
            this.enduser.HeaderText = "End User";
            this.enduser.Name = "enduser";
            this.enduser.ReadOnly = true;
            this.enduser.Width = 82;
            // 
            // shipto
            // 
            this.shipto.HeaderText = "Ship To";
            this.shipto.Name = "shipto";
            this.shipto.ReadOnly = true;
            this.shipto.Width = 73;
            // 
            // custpo
            // 
            this.custpo.HeaderText = "Customer PO";
            this.custpo.Name = "custpo";
            this.custpo.ReadOnly = true;
            this.custpo.Width = 104;
            // 
            // socode
            // 
            this.socode.HeaderText = "S/O Code";
            this.socode.Name = "socode";
            this.socode.ReadOnly = true;
            this.socode.Width = 83;
            // 
            // term
            // 
            this.term.HeaderText = "Term";
            this.term.Name = "term";
            this.term.ReadOnly = true;
            this.term.Width = 60;
            // 
            // bt
            // 
            this.bt.HeaderText = "Bussiness Type";
            this.bt.Name = "bt";
            this.bt.ReadOnly = true;
            this.bt.Width = 116;
            // 
            // ordertype
            // 
            this.ordertype.HeaderText = "Order Type";
            this.ordertype.Name = "ordertype";
            this.ordertype.ReadOnly = true;
            this.ordertype.Width = 91;
            // 
            // totalweight
            // 
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.totalweight.DefaultCellStyle = dataGridViewCellStyle1;
            this.totalweight.HeaderText = "Total Weight";
            this.totalweight.Name = "totalweight";
            this.totalweight.ReadOnly = true;
            this.totalweight.Width = 99;
            // 
            // totalamount
            // 
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.totalamount.DefaultCellStyle = dataGridViewCellStyle2;
            this.totalamount.HeaderText = "Total Amount";
            this.totalamount.Name = "totalamount";
            this.totalamount.ReadOnly = true;
            this.totalamount.Width = 103;
            // 
            // OrderHeadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 639);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderHeadDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order Head Dialog";
            this.Load += new System.EventHandler(this.OrderHeadDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butSelect;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.TextBox txtBT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.TextBox txtCustID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordernum;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn enduser;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipto;
        private System.Windows.Forms.DataGridViewTextBoxColumn custpo;
        private System.Windows.Forms.DataGridViewTextBoxColumn socode;
        private System.Windows.Forms.DataGridViewTextBoxColumn term;
        private System.Windows.Forms.DataGridViewTextBoxColumn bt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordertype;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalweight;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalamount;
    }
}