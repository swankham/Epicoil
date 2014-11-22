namespace Epicoil.Appl.Presentations.Production
{
    partial class MaterialTracker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialTracker));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPossession = new System.Windows.Forms.ComboBox();
            this.txtCoating = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkProcess = new System.Windows.Forms.CheckBox();
            this.chkUnpacked = new System.Windows.Forms.CheckBox();
            this.chkFound = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butClear = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.cmbProcessLine = new System.Windows.Forms.ComboBox();
            this.txtCommodity = new System.Windows.Forms.TextBox();
            this.txtSpec = new System.Windows.Forms.TextBox();
            this.txtWorkOrderNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.workid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionLineID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.found = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.unpack = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.processline = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.processingline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workorder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thick1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.width1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commodity1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spec1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coating1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.possession = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mcssno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.cmbPossession);
            this.splitContainer1.Panel1.Controls.Add(this.txtCoating);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.cmbProcessLine);
            this.splitContainer1.Panel1.Controls.Add(this.txtCommodity);
            this.splitContainer1.Panel1.Controls.Add(this.txtSpec);
            this.splitContainer1.Panel1.Controls.Add(this.txtWorkOrderNum);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvList);
            this.splitContainer1.Size = new System.Drawing.Size(1040, 662);
            this.splitContainer1.SplitterDistance = 99;
            this.splitContainer1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Possession :";
            this.label6.Visible = false;
            // 
            // cmbPossession
            // 
            this.cmbPossession.FormattingEnabled = true;
            this.cmbPossession.Location = new System.Drawing.Point(115, 65);
            this.cmbPossession.Name = "cmbPossession";
            this.cmbPossession.Size = new System.Drawing.Size(141, 23);
            this.cmbPossession.TabIndex = 13;
            this.cmbPossession.Visible = false;
            // 
            // txtCoating
            // 
            this.txtCoating.Location = new System.Drawing.Point(380, 65);
            this.txtCoating.Name = "txtCoating";
            this.txtCoating.Size = new System.Drawing.Size(141, 21);
            this.txtCoating.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(319, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Coating :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkProcess);
            this.groupBox1.Controls.Add(this.chkUnpacked);
            this.groupBox1.Controls.Add(this.chkFound);
            this.groupBox1.Location = new System.Drawing.Point(527, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 84);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // chkProcess
            // 
            this.chkProcess.AutoSize = true;
            this.chkProcess.Location = new System.Drawing.Point(56, 60);
            this.chkProcess.Name = "chkProcess";
            this.chkProcess.Size = new System.Drawing.Size(87, 19);
            this.chkProcess.TabIndex = 2;
            this.chkProcess.Text = "Processing";
            this.chkProcess.UseVisualStyleBackColor = true;
            // 
            // chkUnpacked
            // 
            this.chkUnpacked.AutoSize = true;
            this.chkUnpacked.Location = new System.Drawing.Point(56, 35);
            this.chkUnpacked.Name = "chkUnpacked";
            this.chkUnpacked.Size = new System.Drawing.Size(82, 19);
            this.chkUnpacked.TabIndex = 1;
            this.chkUnpacked.Text = "Unpacked";
            this.chkUnpacked.UseVisualStyleBackColor = true;
            // 
            // chkFound
            // 
            this.chkFound.AutoSize = true;
            this.chkFound.Location = new System.Drawing.Point(56, 11);
            this.chkFound.Name = "chkFound";
            this.chkFound.Size = new System.Drawing.Size(61, 19);
            this.chkFound.TabIndex = 0;
            this.chkFound.Text = "Found";
            this.chkFound.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butClear);
            this.panel1.Controls.Add(this.butSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(840, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 99);
            this.panel1.TabIndex = 9;
            // 
            // butClear
            // 
            this.butClear.Location = new System.Drawing.Point(68, 42);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(120, 23);
            this.butClear.TabIndex = 1;
            this.butClear.Text = "Clear";
            this.butClear.UseVisualStyleBackColor = true;
            this.butClear.Click += new System.EventHandler(this.butClear_Click);
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(68, 13);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(120, 23);
            this.butSearch.TabIndex = 0;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // cmbProcessLine
            // 
            this.cmbProcessLine.FormattingEnabled = true;
            this.cmbProcessLine.Location = new System.Drawing.Point(115, 38);
            this.cmbProcessLine.Name = "cmbProcessLine";
            this.cmbProcessLine.Size = new System.Drawing.Size(141, 23);
            this.cmbProcessLine.TabIndex = 8;
            // 
            // txtCommodity
            // 
            this.txtCommodity.Location = new System.Drawing.Point(380, 38);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Size = new System.Drawing.Size(141, 21);
            this.txtCommodity.TabIndex = 7;
            // 
            // txtSpec
            // 
            this.txtSpec.Location = new System.Drawing.Point(380, 11);
            this.txtSpec.Name = "txtSpec";
            this.txtSpec.Size = new System.Drawing.Size(141, 21);
            this.txtSpec.TabIndex = 6;
            // 
            // txtWorkOrderNum
            // 
            this.txtWorkOrderNum.Location = new System.Drawing.Point(115, 14);
            this.txtWorkOrderNum.Name = "txtWorkOrderNum";
            this.txtWorkOrderNum.Size = new System.Drawing.Size(141, 21);
            this.txtWorkOrderNum.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Commodity :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Spec :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Process Line :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Work Order No. :";
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
            this.dgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvList.ColumnHeadersHeight = 25;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.workid,
            this.TransactionLineID,
            this.found,
            this.unpack,
            this.processline,
            this.processingline,
            this.workorder,
            this.serialNo,
            this.thick1,
            this.width1,
            this.length1,
            this.weight,
            this.commodity1,
            this.spec1,
            this.coating1,
            this.bt1,
            this.possession,
            this.mcssno});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1040, 559);
            this.dgvList.TabIndex = 21;
            this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellContentClick);
            // 
            // workid
            // 
            this.workid.HeaderText = "WID";
            this.workid.Name = "workid";
            this.workid.ReadOnly = true;
            this.workid.Width = 54;
            // 
            // TransactionLineID
            // 
            this.TransactionLineID.HeaderText = "ID";
            this.TransactionLineID.Name = "TransactionLineID";
            this.TransactionLineID.ReadOnly = true;
            this.TransactionLineID.Width = 43;
            // 
            // found
            // 
            this.found.HeaderText = "Found";
            this.found.Name = "found";
            this.found.Width = 47;
            // 
            // unpack
            // 
            this.unpack.HeaderText = "Unpacked";
            this.unpack.Name = "unpack";
            this.unpack.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.unpack.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.unpack.Width = 87;
            // 
            // processline
            // 
            this.processline.HeaderText = "Processing";
            this.processline.Name = "processline";
            this.processline.ReadOnly = true;
            this.processline.Width = 73;
            // 
            // processingline
            // 
            this.processingline.HeaderText = "Process Line";
            this.processingline.Name = "processingline";
            this.processingline.ReadOnly = true;
            this.processingline.Width = 102;
            // 
            // workorder
            // 
            this.workorder.HeaderText = "Work Order No.";
            this.workorder.Name = "workorder";
            this.workorder.ReadOnly = true;
            this.workorder.Width = 115;
            // 
            // serialNo
            // 
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            this.serialNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.serialNo.HeaderText = "Serial No.";
            this.serialNo.Name = "serialNo";
            this.serialNo.ReadOnly = true;
            this.serialNo.Width = 85;
            // 
            // thick1
            // 
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.thick1.DefaultCellStyle = dataGridViewCellStyle2;
            this.thick1.HeaderText = "Thick";
            this.thick1.Name = "thick1";
            this.thick1.ReadOnly = true;
            this.thick1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.thick1.Width = 60;
            // 
            // width1
            // 
            this.width1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.width1.DefaultCellStyle = dataGridViewCellStyle3;
            this.width1.HeaderText = "Width";
            this.width1.Name = "width1";
            this.width1.ReadOnly = true;
            this.width1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.width1.Width = 62;
            // 
            // length1
            // 
            this.length1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.length1.DefaultCellStyle = dataGridViewCellStyle4;
            this.length1.HeaderText = "Length";
            this.length1.Name = "length1";
            this.length1.ReadOnly = true;
            this.length1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.length1.Width = 69;
            // 
            // weight
            // 
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0";
            this.weight.DefaultCellStyle = dataGridViewCellStyle5;
            this.weight.HeaderText = "Weight";
            this.weight.Name = "weight";
            this.weight.ReadOnly = true;
            this.weight.Width = 69;
            // 
            // commodity1
            // 
            this.commodity1.HeaderText = "Commodity";
            this.commodity1.Name = "commodity1";
            this.commodity1.ReadOnly = true;
            this.commodity1.Width = 93;
            // 
            // spec1
            // 
            this.spec1.HeaderText = "Spec";
            this.spec1.Name = "spec1";
            this.spec1.ReadOnly = true;
            this.spec1.Width = 59;
            // 
            // coating1
            // 
            this.coating1.HeaderText = "Coating";
            this.coating1.Name = "coating1";
            this.coating1.ReadOnly = true;
            this.coating1.Width = 73;
            // 
            // bt1
            // 
            this.bt1.HeaderText = "BT";
            this.bt1.Name = "bt1";
            this.bt1.ReadOnly = true;
            this.bt1.Width = 46;
            // 
            // possession
            // 
            this.possession.HeaderText = "Possession";
            this.possession.Name = "possession";
            this.possession.ReadOnly = true;
            this.possession.Width = 94;
            // 
            // mcssno
            // 
            this.mcssno.HeaderText = "MCSS No";
            this.mcssno.Name = "mcssno";
            this.mcssno.ReadOnly = true;
            this.mcssno.Width = 85;
            // 
            // MaterialTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 662);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaterialTracker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Material Tracker";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MaterialTracker_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.TextBox txtSpec;
        private System.Windows.Forms.TextBox txtWorkOrderNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbProcessLine;
        private System.Windows.Forms.TextBox txtCommodity;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkProcess;
        private System.Windows.Forms.CheckBox chkUnpacked;
        private System.Windows.Forms.CheckBox chkFound;
        private System.Windows.Forms.TextBox txtCoating;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbPossession;
        private System.Windows.Forms.DataGridViewTextBoxColumn workid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionLineID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn found;
        private System.Windows.Forms.DataGridViewCheckBoxColumn unpack;
        private System.Windows.Forms.DataGridViewCheckBoxColumn processline;
        private System.Windows.Forms.DataGridViewTextBoxColumn processingline;
        private System.Windows.Forms.DataGridViewTextBoxColumn workorder;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn thick1;
        private System.Windows.Forms.DataGridViewTextBoxColumn width1;
        private System.Windows.Forms.DataGridViewTextBoxColumn length1;
        private System.Windows.Forms.DataGridViewTextBoxColumn weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn commodity1;
        private System.Windows.Forms.DataGridViewTextBoxColumn spec1;
        private System.Windows.Forms.DataGridViewTextBoxColumn coating1;
        private System.Windows.Forms.DataGridViewTextBoxColumn bt1;
        private System.Windows.Forms.DataGridViewTextBoxColumn possession;
        private System.Windows.Forms.DataGridViewTextBoxColumn mcssno;
    }
}