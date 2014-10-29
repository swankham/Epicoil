namespace Epicoil.Appl.Presentations.Planning
{
    partial class NorDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numLengthMax = new System.Windows.Forms.NumericUpDown();
            this.numLengthMin = new System.Windows.Forms.NumericUpDown();
            this.numWidthMax = new System.Windows.Forms.NumericUpDown();
            this.numWidthMin = new System.Windows.Forms.NumericUpDown();
            this.numThickMax = new System.Windows.Forms.NumericUpDown();
            this.numThickMin = new System.Windows.Forms.NumericUpDown();
            this.butSelect = new System.Windows.Forms.Button();
            this.butClear = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.butSearch = new System.Windows.Forms.Button();
            this.txtMillCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMakerCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCoatingCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSpecCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCommodityCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvMaterial = new System.Windows.Forms.DataGridView();
            this.NORNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLengthMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLengthMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThickMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThickMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numLengthMax);
            this.groupBox1.Controls.Add(this.numLengthMin);
            this.groupBox1.Controls.Add(this.numWidthMax);
            this.groupBox1.Controls.Add(this.numWidthMin);
            this.groupBox1.Controls.Add(this.numThickMax);
            this.groupBox1.Controls.Add(this.numThickMin);
            this.groupBox1.Controls.Add(this.butSelect);
            this.groupBox1.Controls.Add(this.butClear);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.butSearch);
            this.groupBox1.Controls.Add(this.txtMillCode);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtMakerCode);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtCategory);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCoatingCode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSpecCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCommodityCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCustID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(952, 124);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // numLengthMax
            // 
            this.numLengthMax.DecimalPlaces = 2;
            this.numLengthMax.Location = new System.Drawing.Point(861, 62);
            this.numLengthMax.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numLengthMax.Name = "numLengthMax";
            this.numLengthMax.Size = new System.Drawing.Size(82, 20);
            this.numLengthMax.TabIndex = 21;
            this.numLengthMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numLengthMin
            // 
            this.numLengthMin.DecimalPlaces = 2;
            this.numLengthMin.Location = new System.Drawing.Point(760, 62);
            this.numLengthMin.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numLengthMin.Name = "numLengthMin";
            this.numLengthMin.Size = new System.Drawing.Size(82, 20);
            this.numLengthMin.TabIndex = 20;
            this.numLengthMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numWidthMax
            // 
            this.numWidthMax.DecimalPlaces = 2;
            this.numWidthMax.Location = new System.Drawing.Point(861, 39);
            this.numWidthMax.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numWidthMax.Name = "numWidthMax";
            this.numWidthMax.Size = new System.Drawing.Size(82, 20);
            this.numWidthMax.TabIndex = 19;
            this.numWidthMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numWidthMin
            // 
            this.numWidthMin.DecimalPlaces = 2;
            this.numWidthMin.Location = new System.Drawing.Point(760, 39);
            this.numWidthMin.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numWidthMin.Name = "numWidthMin";
            this.numWidthMin.Size = new System.Drawing.Size(82, 20);
            this.numWidthMin.TabIndex = 18;
            this.numWidthMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numThickMax
            // 
            this.numThickMax.DecimalPlaces = 2;
            this.numThickMax.Location = new System.Drawing.Point(861, 16);
            this.numThickMax.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numThickMax.Name = "numThickMax";
            this.numThickMax.Size = new System.Drawing.Size(82, 20);
            this.numThickMax.TabIndex = 17;
            this.numThickMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numThickMin
            // 
            this.numThickMin.DecimalPlaces = 2;
            this.numThickMin.Location = new System.Drawing.Point(760, 16);
            this.numThickMin.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numThickMin.Name = "numThickMin";
            this.numThickMin.Size = new System.Drawing.Size(82, 20);
            this.numThickMin.TabIndex = 16;
            this.numThickMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // butSelect
            // 
            this.butSelect.Location = new System.Drawing.Point(837, 89);
            this.butSelect.Name = "butSelect";
            this.butSelect.Size = new System.Drawing.Size(106, 26);
            this.butSelect.TabIndex = 3;
            this.butSelect.Text = "Select";
            this.butSelect.UseVisualStyleBackColor = true;
            // 
            // butClear
            // 
            this.butClear.Location = new System.Drawing.Point(725, 89);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(106, 26);
            this.butClear.TabIndex = 2;
            this.butClear.Text = "Clear";
            this.butClear.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(703, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Length :";
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(613, 89);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(106, 26);
            this.butSearch.TabIndex = 1;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = true;
            // 
            // txtMillCode
            // 
            this.txtMillCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMillCode.Location = new System.Drawing.Point(548, 39);
            this.txtMillCode.Name = "txtMillCode";
            this.txtMillCode.Size = new System.Drawing.Size(96, 20);
            this.txtMillCode.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(710, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Width :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(486, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Mill Code :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(712, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Thick :";
            // 
            // txtMakerCode
            // 
            this.txtMakerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMakerCode.Location = new System.Drawing.Point(548, 15);
            this.txtMakerCode.Name = "txtMakerCode";
            this.txtMakerCode.Size = new System.Drawing.Size(96, 20);
            this.txtMakerCode.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(844, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(10, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(471, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Maker Code :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(845, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(845, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "-";
            // 
            // txtCategory
            // 
            this.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCategory.Location = new System.Drawing.Point(335, 39);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(124, 20);
            this.txtCategory.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(274, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Category :";
            // 
            // txtCoatingCode
            // 
            this.txtCoatingCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCoatingCode.Location = new System.Drawing.Point(335, 15);
            this.txtCoatingCode.Name = "txtCoatingCode";
            this.txtCoatingCode.Size = new System.Drawing.Size(124, 20);
            this.txtCoatingCode.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(252, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Coating Code :";
            // 
            // txtSpecCode
            // 
            this.txtSpecCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpecCode.Location = new System.Drawing.Point(102, 63);
            this.txtSpecCode.Name = "txtSpecCode";
            this.txtSpecCode.Size = new System.Drawing.Size(124, 20);
            this.txtSpecCode.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Spec Code :";
            // 
            // txtCommodityCode
            // 
            this.txtCommodityCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCommodityCode.Location = new System.Drawing.Point(102, 39);
            this.txtCommodityCode.Name = "txtCommodityCode";
            this.txtCommodityCode.Size = new System.Drawing.Size(124, 20);
            this.txtCommodityCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Commodity Code :";
            // 
            // txtCustID
            // 
            this.txtCustID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustID.Location = new System.Drawing.Point(102, 15);
            this.txtCustID.Name = "txtCustID";
            this.txtCustID.Size = new System.Drawing.Size(124, 20);
            this.txtCustID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Code :";
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
            this.dgvMaterial.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvMaterial.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvMaterial.ColumnHeadersHeight = 25;
            this.dgvMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMaterial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NORNum,
            this.cmdty,
            this.spec,
            this.coating,
            this.thick,
            this.width,
            this.length,
            this.Category,
            this.customer,
            this.maker,
            this.mill});
            this.dgvMaterial.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvMaterial.EnableHeadersVisualStyles = false;
            this.dgvMaterial.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvMaterial.Location = new System.Drawing.Point(12, 129);
            this.dgvMaterial.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMaterial.MultiSelect = false;
            this.dgvMaterial.Name = "dgvMaterial";
            this.dgvMaterial.RowHeadersVisible = false;
            this.dgvMaterial.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterial.Size = new System.Drawing.Size(952, 386);
            this.dgvMaterial.TabIndex = 19;
            // 
            // NORNum
            // 
            this.NORNum.HeaderText = "NOR No.";
            this.NORNum.Name = "NORNum";
            this.NORNum.Width = 75;
            // 
            // cmdty
            // 
            this.cmdty.HeaderText = "Cmdty";
            this.cmdty.Name = "cmdty";
            this.cmdty.ReadOnly = true;
            this.cmdty.Width = 60;
            // 
            // spec
            // 
            this.spec.HeaderText = "Spec";
            this.spec.Name = "spec";
            this.spec.ReadOnly = true;
            this.spec.Width = 56;
            // 
            // coating
            // 
            this.coating.HeaderText = "Coating";
            this.coating.Name = "coating";
            this.coating.ReadOnly = true;
            this.coating.Width = 67;
            // 
            // thick
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.thick.DefaultCellStyle = dataGridViewCellStyle1;
            this.thick.HeaderText = "Thick";
            this.thick.Name = "thick";
            this.thick.ReadOnly = true;
            this.thick.Width = 58;
            // 
            // width
            // 
            this.width.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.width.DefaultCellStyle = dataGridViewCellStyle2;
            this.width.HeaderText = "Width";
            this.width.Name = "width";
            this.width.ReadOnly = true;
            this.width.Width = 59;
            // 
            // length
            // 
            this.length.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.length.DefaultCellStyle = dataGridViewCellStyle3;
            this.length.HeaderText = "Length";
            this.length.Name = "length";
            this.length.ReadOnly = true;
            this.length.Width = 64;
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.Width = 73;
            // 
            // customer
            // 
            this.customer.HeaderText = "Customer";
            this.customer.Name = "customer";
            this.customer.ReadOnly = true;
            this.customer.Width = 75;
            // 
            // maker
            // 
            this.maker.HeaderText = "Maker";
            this.maker.Name = "maker";
            this.maker.ReadOnly = true;
            this.maker.Width = 61;
            // 
            // mill
            // 
            this.mill.HeaderText = "Mill";
            this.mill.Name = "mill";
            this.mill.ReadOnly = true;
            this.mill.Width = 46;
            // 
            // NorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 528);
            this.Controls.Add(this.dgvMaterial);
            this.Controls.Add(this.groupBox1);
            this.Name = "NorDialog";
            this.Text = "NOR Selecting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLengthMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLengthMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThickMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThickMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numLengthMax;
        private System.Windows.Forms.NumericUpDown numLengthMin;
        private System.Windows.Forms.NumericUpDown numWidthMax;
        private System.Windows.Forms.NumericUpDown numWidthMin;
        private System.Windows.Forms.NumericUpDown numThickMax;
        private System.Windows.Forms.NumericUpDown numThickMin;
        private System.Windows.Forms.Button butSelect;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.TextBox txtMillCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMakerCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCoatingCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSpecCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCommodityCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn NORNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdty;
        private System.Windows.Forms.DataGridViewTextBoxColumn spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn coating;
        private System.Windows.Forms.DataGridViewTextBoxColumn thick;
        private System.Windows.Forms.DataGridViewTextBoxColumn width;
        private System.Windows.Forms.DataGridViewTextBoxColumn length;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn maker;
        private System.Windows.Forms.DataGridViewTextBoxColumn mill;

    }
}