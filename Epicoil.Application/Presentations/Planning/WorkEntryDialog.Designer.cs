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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkEntryDialog));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtProcessStep = new System.Windows.Forms.TextBox();
            this.cmbPossession = new System.Windows.Forms.ComboBox();
            this.cmbOrderType = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtDueTo = new System.Windows.Forms.DateTimePicker();
            this.dtDueFrom = new System.Windows.Forms.DateTimePicker();
            this.dtWOTo = new System.Windows.Forms.DateTimePicker();
            this.dtWOFrom = new System.Windows.Forms.DateTimePicker();
            this.cmbProcessLine = new System.Windows.Forms.ComboBox();
            this.txtPIC = new System.Windows.Forms.TextBox();
            this.txtWONo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvWorkOrder = new System.Windows.Forms.DataGridView();
            this.workordernumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WODate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PIC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Possession = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkOrder)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.txtProcessStep);
            this.splitContainer1.Panel1.Controls.Add(this.cmbPossession);
            this.splitContainer1.Panel1.Controls.Add(this.cmbOrderType);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelect);
            this.splitContainer1.Panel1.Controls.Add(this.btnClear);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.dtDueTo);
            this.splitContainer1.Panel1.Controls.Add(this.dtDueFrom);
            this.splitContainer1.Panel1.Controls.Add(this.dtWOTo);
            this.splitContainer1.Panel1.Controls.Add(this.dtWOFrom);
            this.splitContainer1.Panel1.Controls.Add(this.cmbProcessLine);
            this.splitContainer1.Panel1.Controls.Add(this.txtPIC);
            this.splitContainer1.Panel1.Controls.Add(this.txtWONo);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvWorkOrder);
            this.splitContainer1.Size = new System.Drawing.Size(997, 483);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtProcessStep
            // 
            this.txtProcessStep.Location = new System.Drawing.Point(133, 47);
            this.txtProcessStep.Name = "txtProcessStep";
            this.txtProcessStep.Size = new System.Drawing.Size(115, 21);
            this.txtProcessStep.TabIndex = 29;
            // 
            // cmbPossession
            // 
            this.cmbPossession.FormattingEnabled = true;
            this.cmbPossession.Location = new System.Drawing.Point(662, 49);
            this.cmbPossession.Name = "cmbPossession";
            this.cmbPossession.Size = new System.Drawing.Size(108, 23);
            this.cmbPossession.TabIndex = 28;
            // 
            // cmbOrderType
            // 
            this.cmbOrderType.FormattingEnabled = true;
            this.cmbOrderType.Location = new System.Drawing.Point(662, 20);
            this.cmbOrderType.Name = "cmbOrderType";
            this.cmbOrderType.Size = new System.Drawing.Size(109, 23);
            this.cmbOrderType.TabIndex = 27;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(814, 83);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(109, 30);
            this.btnSelect.TabIndex = 26;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(814, 49);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(109, 30);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(814, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(109, 30);
            this.btnSearch.TabIndex = 24;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtDueTo
            // 
            this.dtDueTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDueTo.Location = new System.Drawing.Point(472, 47);
            this.dtDueTo.Name = "dtDueTo";
            this.dtDueTo.Size = new System.Drawing.Size(101, 21);
            this.dtDueTo.TabIndex = 23;
            // 
            // dtDueFrom
            // 
            this.dtDueFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDueFrom.Location = new System.Drawing.Point(366, 47);
            this.dtDueFrom.Name = "dtDueFrom";
            this.dtDueFrom.Size = new System.Drawing.Size(100, 21);
            this.dtDueFrom.TabIndex = 22;
            // 
            // dtWOTo
            // 
            this.dtWOTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtWOTo.Location = new System.Drawing.Point(472, 18);
            this.dtWOTo.Name = "dtWOTo";
            this.dtWOTo.Size = new System.Drawing.Size(101, 21);
            this.dtWOTo.TabIndex = 21;
            // 
            // dtWOFrom
            // 
            this.dtWOFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtWOFrom.Location = new System.Drawing.Point(366, 18);
            this.dtWOFrom.Name = "dtWOFrom";
            this.dtWOFrom.Size = new System.Drawing.Size(100, 21);
            this.dtWOFrom.TabIndex = 20;
            // 
            // cmbProcessLine
            // 
            this.cmbProcessLine.FormattingEnabled = true;
            this.cmbProcessLine.Location = new System.Drawing.Point(133, 74);
            this.cmbProcessLine.Name = "cmbProcessLine";
            this.cmbProcessLine.Size = new System.Drawing.Size(115, 23);
            this.cmbProcessLine.TabIndex = 18;
            // 
            // txtPIC
            // 
            this.txtPIC.Location = new System.Drawing.Point(366, 74);
            this.txtPIC.Name = "txtPIC";
            this.txtPIC.Size = new System.Drawing.Size(121, 21);
            this.txtPIC.TabIndex = 9;
            // 
            // txtWONo
            // 
            this.txtWONo.Location = new System.Drawing.Point(133, 20);
            this.txtWONo.Name = "txtWONo";
            this.txtWONo.Size = new System.Drawing.Size(115, 21);
            this.txtWONo.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(586, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "Possession";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(586, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Order Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(262, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "P-I-C";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(263, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Due Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(262, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Work Order Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Work Order No.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Process Step";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Process Line";
            // 
            // dgvWorkOrder
            // 
            this.dgvWorkOrder.AllowUserToAddRows = false;
            this.dgvWorkOrder.AllowUserToDeleteRows = false;
            this.dgvWorkOrder.AllowUserToResizeRows = false;
            this.dgvWorkOrder.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvWorkOrder.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvWorkOrder.BackgroundColor = System.Drawing.Color.White;
            this.dgvWorkOrder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWorkOrder.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvWorkOrder.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvWorkOrder.ColumnHeadersHeight = 25;
            this.dgvWorkOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.workordernumber,
            this.ProcessStep,
            this.ProcessLine,
            this.WODate,
            this.DueDate,
            this.PIC,
            this.OrderType,
            this.Possession});
            this.dgvWorkOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWorkOrder.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvWorkOrder.EnableHeadersVisualStyles = false;
            this.dgvWorkOrder.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvWorkOrder.Location = new System.Drawing.Point(0, 0);
            this.dgvWorkOrder.Margin = new System.Windows.Forms.Padding(4);
            this.dgvWorkOrder.MultiSelect = false;
            this.dgvWorkOrder.Name = "dgvWorkOrder";
            this.dgvWorkOrder.ReadOnly = true;
            this.dgvWorkOrder.RowHeadersVisible = false;
            this.dgvWorkOrder.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvWorkOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWorkOrder.Size = new System.Drawing.Size(997, 339);
            this.dgvWorkOrder.TabIndex = 18;
            // 
            // workordernumber
            // 
            this.workordernumber.HeaderText = "Work Order No.";
            this.workordernumber.Name = "workordernumber";
            this.workordernumber.ReadOnly = true;
            this.workordernumber.Width = 115;
            // 
            // ProcessStep
            // 
            this.ProcessStep.HeaderText = "Process Step";
            this.ProcessStep.Name = "ProcessStep";
            this.ProcessStep.ReadOnly = true;
            this.ProcessStep.Width = 103;
            // 
            // ProcessLine
            // 
            this.ProcessLine.HeaderText = "Process Line";
            this.ProcessLine.Name = "ProcessLine";
            this.ProcessLine.ReadOnly = true;
            this.ProcessLine.Width = 102;
            // 
            // WODate
            // 
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            dataGridViewCellStyle1.NullValue = null;
            this.WODate.DefaultCellStyle = dataGridViewCellStyle1;
            this.WODate.HeaderText = "Work Order Date";
            this.WODate.Name = "WODate";
            this.WODate.ReadOnly = true;
            this.WODate.Width = 122;
            // 
            // DueDate
            // 
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            this.DueDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.DueDate.HeaderText = "Due Date";
            this.DueDate.Name = "DueDate";
            this.DueDate.ReadOnly = true;
            this.DueDate.Width = 83;
            // 
            // PIC
            // 
            this.PIC.HeaderText = "P-I-C";
            this.PIC.Name = "PIC";
            this.PIC.ReadOnly = true;
            this.PIC.Width = 58;
            // 
            // OrderType
            // 
            this.OrderType.HeaderText = "Order Type";
            this.OrderType.Name = "OrderType";
            this.OrderType.ReadOnly = true;
            this.OrderType.Width = 91;
            // 
            // Possession
            // 
            this.Possession.HeaderText = "Possession";
            this.Possession.Name = "Possession";
            this.Possession.ReadOnly = true;
            this.Possession.Width = 94;
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
            this.Load += new System.EventHandler(this.WorkEntryDialog_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvWorkOrder;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtDueTo;
        private System.Windows.Forms.DateTimePicker dtDueFrom;
        private System.Windows.Forms.DateTimePicker dtWOTo;
        private System.Windows.Forms.DateTimePicker dtWOFrom;
        private System.Windows.Forms.ComboBox cmbProcessLine;
        private System.Windows.Forms.TextBox txtPIC;
        private System.Windows.Forms.TextBox txtWONo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPossession;
        private System.Windows.Forms.ComboBox cmbOrderType;
        private System.Windows.Forms.TextBox txtProcessStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn workordernumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn WODate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PIC;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Possession;
    }
}