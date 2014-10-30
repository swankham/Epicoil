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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvNor = new System.Windows.Forms.DataGridView();
            this.NORNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mill = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.txtCoatingCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSpecCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCommodityCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNor)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLengthMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLengthMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThickMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThickMin)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNor
            // 
            this.dgvNor.AllowUserToAddRows = false;
            this.dgvNor.AllowUserToDeleteRows = false;
            this.dgvNor.AllowUserToResizeRows = false;
            this.dgvNor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvNor.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvNor.BackgroundColor = System.Drawing.Color.White;
            this.dgvNor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNor.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvNor.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvNor.ColumnHeadersHeight = 25;
            this.dgvNor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NORNum,
            this.cmdty,
            this.spec,
            this.coating,
            this.thick,
            this.width,
            this.length,
            this.customer,
            this.maker,
            this.mill});
            this.dgvNor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvNor.EnableHeadersVisualStyles = false;
            this.dgvNor.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvNor.Location = new System.Drawing.Point(14, 149);
            this.dgvNor.Margin = new System.Windows.Forms.Padding(5);
            this.dgvNor.MultiSelect = false;
            this.dgvNor.Name = "dgvNor";
            this.dgvNor.RowHeadersVisible = false;
            this.dgvNor.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvNor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNor.Size = new System.Drawing.Size(860, 445);
            this.dgvNor.TabIndex = 19;
            // 
            // NORNum
            // 
            this.NORNum.HeaderText = "NOR No.";
            this.NORNum.Name = "NORNum";
            this.NORNum.Width = 80;
            // 
            // cmdty
            // 
            this.cmdty.HeaderText = "Cmdty";
            this.cmdty.Name = "cmdty";
            this.cmdty.ReadOnly = true;
            this.cmdty.Width = 65;
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
            // thick
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = "0";
            this.thick.DefaultCellStyle = dataGridViewCellStyle7;
            this.thick.HeaderText = "Thick";
            this.thick.Name = "thick";
            this.thick.ReadOnly = true;
            this.thick.Width = 60;
            // 
            // width
            // 
            this.width.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = "0";
            this.width.DefaultCellStyle = dataGridViewCellStyle8;
            this.width.HeaderText = "Width";
            this.width.Name = "width";
            this.width.ReadOnly = true;
            this.width.Width = 62;
            // 
            // length
            // 
            this.length.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = "0";
            this.length.DefaultCellStyle = dataGridViewCellStyle9;
            this.length.HeaderText = "Length";
            this.length.Name = "length";
            this.length.ReadOnly = true;
            this.length.Width = 69;
            // 
            // customer
            // 
            this.customer.HeaderText = "Customer";
            this.customer.Name = "customer";
            this.customer.ReadOnly = true;
            this.customer.Width = 84;
            // 
            // maker
            // 
            this.maker.HeaderText = "Maker";
            this.maker.Name = "maker";
            this.maker.ReadOnly = true;
            this.maker.Width = 66;
            // 
            // mill
            // 
            this.mill.HeaderText = "Mill";
            this.mill.Name = "mill";
            this.mill.ReadOnly = true;
            this.mill.Width = 51;
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
            this.groupBox1.Controls.Add(this.txtCoatingCode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSpecCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCommodityCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCustID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(860, 143);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // numLengthMax
            // 
            this.numLengthMax.DecimalPlaces = 2;
            this.numLengthMax.Location = new System.Drawing.Point(743, 69);
            this.numLengthMax.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numLengthMax.Name = "numLengthMax";
            this.numLengthMax.Size = new System.Drawing.Size(96, 21);
            this.numLengthMax.TabIndex = 21;
            this.numLengthMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numLengthMin
            // 
            this.numLengthMin.DecimalPlaces = 2;
            this.numLengthMin.Location = new System.Drawing.Point(626, 69);
            this.numLengthMin.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numLengthMin.Name = "numLengthMin";
            this.numLengthMin.Size = new System.Drawing.Size(96, 21);
            this.numLengthMin.TabIndex = 20;
            this.numLengthMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numWidthMax
            // 
            this.numWidthMax.DecimalPlaces = 2;
            this.numWidthMax.Location = new System.Drawing.Point(743, 42);
            this.numWidthMax.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numWidthMax.Name = "numWidthMax";
            this.numWidthMax.Size = new System.Drawing.Size(96, 21);
            this.numWidthMax.TabIndex = 19;
            this.numWidthMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numWidthMin
            // 
            this.numWidthMin.DecimalPlaces = 2;
            this.numWidthMin.Location = new System.Drawing.Point(626, 42);
            this.numWidthMin.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numWidthMin.Name = "numWidthMin";
            this.numWidthMin.Size = new System.Drawing.Size(96, 21);
            this.numWidthMin.TabIndex = 18;
            this.numWidthMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numThickMax
            // 
            this.numThickMax.DecimalPlaces = 2;
            this.numThickMax.Location = new System.Drawing.Point(743, 15);
            this.numThickMax.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numThickMax.Name = "numThickMax";
            this.numThickMax.Size = new System.Drawing.Size(96, 21);
            this.numThickMax.TabIndex = 17;
            this.numThickMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numThickMin
            // 
            this.numThickMin.DecimalPlaces = 2;
            this.numThickMin.Location = new System.Drawing.Point(626, 15);
            this.numThickMin.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numThickMin.Name = "numThickMin";
            this.numThickMin.Size = new System.Drawing.Size(96, 21);
            this.numThickMin.TabIndex = 16;
            this.numThickMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // butSelect
            // 
            this.butSelect.Location = new System.Drawing.Point(692, 103);
            this.butSelect.Name = "butSelect";
            this.butSelect.Size = new System.Drawing.Size(124, 30);
            this.butSelect.TabIndex = 3;
            this.butSelect.Text = "Select";
            this.butSelect.UseVisualStyleBackColor = true;
            this.butSelect.Click += new System.EventHandler(this.butSelect_Click);
            // 
            // butClear
            // 
            this.butClear.Location = new System.Drawing.Point(562, 103);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(124, 30);
            this.butClear.TabIndex = 2;
            this.butClear.Text = "Clear";
            this.butClear.UseVisualStyleBackColor = true;
            this.butClear.Click += new System.EventHandler(this.butClear_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(559, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 15);
            this.label14.TabIndex = 11;
            this.label14.Text = "Length :";
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(431, 103);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(124, 30);
            this.butSearch.TabIndex = 1;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // txtMillCode
            // 
            this.txtMillCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMillCode.Location = new System.Drawing.Point(391, 72);
            this.txtMillCode.Name = "txtMillCode";
            this.txtMillCode.Size = new System.Drawing.Size(144, 21);
            this.txtMillCode.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(567, 45);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 15);
            this.label13.TabIndex = 10;
            this.label13.Text = "Width :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(319, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "Mill Code :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(570, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 15);
            this.label12.TabIndex = 9;
            this.label12.Text = "Thick :";
            // 
            // txtMakerCode
            // 
            this.txtMakerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMakerCode.Location = new System.Drawing.Point(391, 44);
            this.txtMakerCode.Name = "txtMakerCode";
            this.txtMakerCode.Size = new System.Drawing.Size(144, 21);
            this.txtMakerCode.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(724, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 15);
            this.label11.TabIndex = 8;
            this.label11.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(301, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 15);
            this.label8.TabIndex = 12;
            this.label8.Text = "Maker Code :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(725, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 15);
            this.label10.TabIndex = 7;
            this.label10.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(725, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 15);
            this.label9.TabIndex = 6;
            this.label9.Text = "-";
            // 
            // txtCoatingCode
            // 
            this.txtCoatingCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCoatingCode.Location = new System.Drawing.Point(391, 17);
            this.txtCoatingCode.Name = "txtCoatingCode";
            this.txtCoatingCode.Size = new System.Drawing.Size(144, 21);
            this.txtCoatingCode.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(294, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Coating Code :";
            // 
            // txtSpecCode
            // 
            this.txtSpecCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpecCode.Location = new System.Drawing.Point(119, 73);
            this.txtSpecCode.Name = "txtSpecCode";
            this.txtSpecCode.Size = new System.Drawing.Size(144, 21);
            this.txtSpecCode.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Spec Code :";
            // 
            // txtCommodityCode
            // 
            this.txtCommodityCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCommodityCode.Location = new System.Drawing.Point(119, 45);
            this.txtCommodityCode.Name = "txtCommodityCode";
            this.txtCommodityCode.Size = new System.Drawing.Size(144, 21);
            this.txtCommodityCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Commodity Code :";
            // 
            // txtCustID
            // 
            this.txtCustID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustID.Location = new System.Drawing.Point(119, 17);
            this.txtCustID.Name = "txtCustID";
            this.txtCustID.Size = new System.Drawing.Size(144, 21);
            this.txtCustID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Code :";
            // 
            // NorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 609);
            this.Controls.Add(this.dgvNor);
            this.Controls.Add(this.groupBox1);
            this.Name = "NorDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NOR Selecting";
            this.Load += new System.EventHandler(this.NorDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLengthMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLengthMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThickMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThickMin)).EndInit();
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
        private System.Windows.Forms.TextBox txtCoatingCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSpecCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCommodityCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvNor;
        private System.Windows.Forms.DataGridViewTextBoxColumn NORNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdty;
        private System.Windows.Forms.DataGridViewTextBoxColumn spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn coating;
        private System.Windows.Forms.DataGridViewTextBoxColumn thick;
        private System.Windows.Forms.DataGridViewTextBoxColumn width;
        private System.Windows.Forms.DataGridViewTextBoxColumn length;
        private System.Windows.Forms.DataGridViewTextBoxColumn customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn maker;
        private System.Windows.Forms.DataGridViewTextBoxColumn mill;

    }
}