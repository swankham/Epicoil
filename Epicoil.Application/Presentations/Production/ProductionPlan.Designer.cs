namespace Epicoil.Appl.Presentations.Production
{
    partial class ProductionPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductionPlan));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboMachine = new System.Windows.Forms.ComboBox();
            this.cboMachineGroup = new System.Windows.Forms.ComboBox();
            this.rdoMachine = new System.Windows.Forms.RadioButton();
            this.rdoMachineGroup = new System.Windows.Forms.RadioButton();
            this.rdoAllMachine = new System.Windows.Forms.RadioButton();
            this.dtpDueDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDueDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpWorkDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpWorkDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtWorkOrderNumTo = new System.Windows.Forms.TextBox();
            this.txtWorkOrderNumForm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.butClear = new System.Windows.Forms.Button();
            this.butSearch = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvWorkOrder = new System.Windows.Forms.DataGridView();
            this.dgvPlanned = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butLeft = new System.Windows.Forms.Button();
            this.butRight = new System.Windows.Forms.Button();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tbExport = new System.Windows.Forms.ToolStripButton();
            this.seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workordernumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WODate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Possession = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PIC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yield = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanned)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1227, 662);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 42;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cboMachine);
            this.panel1.Controls.Add(this.cboMachineGroup);
            this.panel1.Controls.Add(this.rdoMachine);
            this.panel1.Controls.Add(this.rdoMachineGroup);
            this.panel1.Controls.Add(this.rdoAllMachine);
            this.panel1.Controls.Add(this.dtpDueDateTo);
            this.panel1.Controls.Add(this.dtpDueDateFrom);
            this.panel1.Controls.Add(this.dtpWorkDateTo);
            this.panel1.Controls.Add(this.dtpWorkDateFrom);
            this.panel1.Controls.Add(this.txtWorkOrderNumTo);
            this.panel1.Controls.Add(this.txtWorkOrderNumForm);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.butClear);
            this.panel1.Controls.Add(this.butSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1227, 100);
            this.panel1.TabIndex = 0;
            // 
            // cboMachine
            // 
            this.cboMachine.FormattingEnabled = true;
            this.cboMachine.Location = new System.Drawing.Point(673, 70);
            this.cboMachine.Name = "cboMachine";
            this.cboMachine.Size = new System.Drawing.Size(121, 23);
            this.cboMachine.TabIndex = 18;
            // 
            // cboMachineGroup
            // 
            this.cboMachineGroup.FormattingEnabled = true;
            this.cboMachineGroup.Location = new System.Drawing.Point(673, 43);
            this.cboMachineGroup.Name = "cboMachineGroup";
            this.cboMachineGroup.Size = new System.Drawing.Size(121, 23);
            this.cboMachineGroup.TabIndex = 17;
            // 
            // rdoMachine
            // 
            this.rdoMachine.AutoSize = true;
            this.rdoMachine.Location = new System.Drawing.Point(547, 71);
            this.rdoMachine.Name = "rdoMachine";
            this.rdoMachine.Size = new System.Drawing.Size(89, 19);
            this.rdoMachine.TabIndex = 16;
            this.rdoMachine.Text = "By Machine";
            this.rdoMachine.UseVisualStyleBackColor = true;
            // 
            // rdoMachineGroup
            // 
            this.rdoMachineGroup.AutoSize = true;
            this.rdoMachineGroup.Location = new System.Drawing.Point(547, 44);
            this.rdoMachineGroup.Name = "rdoMachineGroup";
            this.rdoMachineGroup.Size = new System.Drawing.Size(110, 19);
            this.rdoMachineGroup.TabIndex = 15;
            this.rdoMachineGroup.Text = "Machine Group";
            this.rdoMachineGroup.UseVisualStyleBackColor = true;
            // 
            // rdoAllMachine
            // 
            this.rdoAllMachine.AutoSize = true;
            this.rdoAllMachine.Checked = true;
            this.rdoAllMachine.Location = new System.Drawing.Point(547, 15);
            this.rdoAllMachine.Name = "rdoAllMachine";
            this.rdoAllMachine.Size = new System.Drawing.Size(89, 19);
            this.rdoAllMachine.TabIndex = 14;
            this.rdoAllMachine.TabStop = true;
            this.rdoAllMachine.Text = "All Machine";
            this.rdoAllMachine.UseVisualStyleBackColor = true;
            // 
            // dtpDueDateTo
            // 
            this.dtpDueDateTo.Location = new System.Drawing.Point(351, 68);
            this.dtpDueDateTo.Name = "dtpDueDateTo";
            this.dtpDueDateTo.Size = new System.Drawing.Size(158, 21);
            this.dtpDueDateTo.TabIndex = 13;
            // 
            // dtpDueDateFrom
            // 
            this.dtpDueDateFrom.Location = new System.Drawing.Point(164, 68);
            this.dtpDueDateFrom.Name = "dtpDueDateFrom";
            this.dtpDueDateFrom.Size = new System.Drawing.Size(157, 21);
            this.dtpDueDateFrom.TabIndex = 12;
            // 
            // dtpWorkDateTo
            // 
            this.dtpWorkDateTo.Location = new System.Drawing.Point(351, 41);
            this.dtpWorkDateTo.Name = "dtpWorkDateTo";
            this.dtpWorkDateTo.Size = new System.Drawing.Size(158, 21);
            this.dtpWorkDateTo.TabIndex = 11;
            // 
            // dtpWorkDateFrom
            // 
            this.dtpWorkDateFrom.Location = new System.Drawing.Point(164, 41);
            this.dtpWorkDateFrom.Name = "dtpWorkDateFrom";
            this.dtpWorkDateFrom.Size = new System.Drawing.Size(157, 21);
            this.dtpWorkDateFrom.TabIndex = 10;
            // 
            // txtWorkOrderNumTo
            // 
            this.txtWorkOrderNumTo.Location = new System.Drawing.Point(351, 14);
            this.txtWorkOrderNumTo.Name = "txtWorkOrderNumTo";
            this.txtWorkOrderNumTo.Size = new System.Drawing.Size(158, 21);
            this.txtWorkOrderNumTo.TabIndex = 9;
            // 
            // txtWorkOrderNumForm
            // 
            this.txtWorkOrderNumForm.Location = new System.Drawing.Point(164, 14);
            this.txtWorkOrderNumForm.Name = "txtWorkOrderNumForm";
            this.txtWorkOrderNumForm.Size = new System.Drawing.Size(157, 21);
            this.txtWorkOrderNumForm.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(328, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "to";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Work Order Due Date :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Work Order Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "to";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Work Order No. :";
            // 
            // butClear
            // 
            this.butClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butClear.Location = new System.Drawing.Point(1103, 51);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(119, 42);
            this.butClear.TabIndex = 1;
            this.butClear.Text = "Reset Filter";
            this.butClear.UseVisualStyleBackColor = true;
            this.butClear.Click += new System.EventHandler(this.butClear_Click);
            // 
            // butSearch
            // 
            this.butSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSearch.Location = new System.Drawing.Point(1103, 3);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(119, 42);
            this.butSearch.TabIndex = 0;
            this.butSearch.Text = "Search";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvWorkOrder);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvPlanned);
            this.splitContainer2.Panel2.Controls.Add(this.panel4);
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Size = new System.Drawing.Size(1227, 558);
            this.splitContainer2.SplitterDistance = 585;
            this.splitContainer2.TabIndex = 20;
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
            this.id,
            this.workordernumber,
            this.ProcessStep,
            this.ProcessLine,
            this.WODate,
            this.DueDate,
            this.Possession,
            this.PIC,
            this.yield});
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
            this.dgvWorkOrder.Size = new System.Drawing.Size(585, 558);
            this.dgvWorkOrder.TabIndex = 20;
            // 
            // dgvPlanned
            // 
            this.dgvPlanned.AllowUserToAddRows = false;
            this.dgvPlanned.AllowUserToDeleteRows = false;
            this.dgvPlanned.AllowUserToResizeRows = false;
            this.dgvPlanned.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPlanned.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPlanned.BackgroundColor = System.Drawing.Color.White;
            this.dgvPlanned.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPlanned.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvPlanned.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvPlanned.ColumnHeadersHeight = 25;
            this.dgvPlanned.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seq,
            this.idd,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dgvPlanned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlanned.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvPlanned.EnableHeadersVisualStyles = false;
            this.dgvPlanned.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvPlanned.Location = new System.Drawing.Point(34, 31);
            this.dgvPlanned.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPlanned.MultiSelect = false;
            this.dgvPlanned.Name = "dgvPlanned";
            this.dgvPlanned.ReadOnly = true;
            this.dgvPlanned.RowHeadersVisible = false;
            this.dgvPlanned.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPlanned.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlanned.Size = new System.Drawing.Size(604, 527);
            this.dgvPlanned.TabIndex = 21;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dateTimePicker1);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(34, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(604, 31);
            this.panel4.TabIndex = 22;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(68, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(157, 21);
            this.dateTimePicker1.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Plan Date :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.butLeft);
            this.panel2.Controls.Add(this.butRight);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(34, 558);
            this.panel2.TabIndex = 1;
            // 
            // butLeft
            // 
            this.butLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.butLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butLeft.Image = global::Epicoil.Appl.Properties.Resources.leftArrow;
            this.butLeft.Location = new System.Drawing.Point(4, 98);
            this.butLeft.Name = "butLeft";
            this.butLeft.Size = new System.Drawing.Size(28, 25);
            this.butLeft.TabIndex = 1;
            this.butLeft.UseVisualStyleBackColor = true;
            this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
            // 
            // butRight
            // 
            this.butRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.butRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butRight.Image = global::Epicoil.Appl.Properties.Resources.rightArrow;
            this.butRight.Location = new System.Drawing.Point(3, 67);
            this.butRight.Name = "butRight";
            this.butRight.Size = new System.Drawing.Size(28, 25);
            this.butRight.TabIndex = 0;
            this.butRight.UseVisualStyleBackColor = true;
            this.butRight.Click += new System.EventHandler(this.butRight_Click);
            // 
            // toolbar
            // 
            this.toolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolbar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolbar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbar.ImageScalingSize = new System.Drawing.Size(23, 20);
            this.toolbar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.tbExport});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.toolbar.Name = "toolbar";
            this.toolbar.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.toolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolbar.Size = new System.Drawing.Size(1227, 28);
            this.toolbar.TabIndex = 41;
            this.toolbar.Text = "Tool Bar";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 21);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 21);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // tbExport
            // 
            this.tbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbExport.Image = ((System.Drawing.Image)(resources.GetObject("tbExport.Image")));
            this.tbExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbExport.Name = "tbExport";
            this.tbExport.Size = new System.Drawing.Size(23, 21);
            this.tbExport.Text = "Export to excel";
            // 
            // seq
            // 
            this.seq.HeaderText = "Seq.";
            this.seq.Name = "seq";
            this.seq.ReadOnly = true;
            this.seq.Width = 56;
            // 
            // idd
            // 
            this.idd.HeaderText = "ID";
            this.idd.Name = "idd";
            this.idd.ReadOnly = true;
            this.idd.Visible = false;
            this.idd.Width = 43;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Work Order No.";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 115;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Step";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 56;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Process Line";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 102;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn4.HeaderText = "Work Order Date";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 122;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle5.Format = "dd/MM/yyyy";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn5.HeaderText = "Due Date";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 83;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Possession";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 94;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "P-I-C";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 58;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0";
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn8.HeaderText = "Yield [%]";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 78;
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 43;
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
            this.ProcessStep.HeaderText = "Step";
            this.ProcessStep.Name = "ProcessStep";
            this.ProcessStep.ReadOnly = true;
            this.ProcessStep.Width = 56;
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
            // Possession
            // 
            this.Possession.HeaderText = "Possession";
            this.Possession.Name = "Possession";
            this.Possession.ReadOnly = true;
            this.Possession.Width = 94;
            // 
            // PIC
            // 
            this.PIC.HeaderText = "P-I-C";
            this.PIC.Name = "PIC";
            this.PIC.ReadOnly = true;
            this.PIC.Width = 58;
            // 
            // yield
            // 
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.yield.DefaultCellStyle = dataGridViewCellStyle3;
            this.yield.HeaderText = "Yield [%]";
            this.yield.Name = "yield";
            this.yield.ReadOnly = true;
            this.yield.Width = 78;
            // 
            // ProductionPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 690);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductionPlan";
            this.Text = "Production Plan";
            this.Load += new System.EventHandler(this.ProductionPlan_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanned)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.DateTimePicker dtpDueDateTo;
        private System.Windows.Forms.DateTimePicker dtpDueDateFrom;
        private System.Windows.Forms.DateTimePicker dtpWorkDateTo;
        private System.Windows.Forms.DateTimePicker dtpWorkDateFrom;
        private System.Windows.Forms.TextBox txtWorkOrderNumTo;
        private System.Windows.Forms.TextBox txtWorkOrderNumForm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoMachine;
        private System.Windows.Forms.RadioButton rdoMachineGroup;
        private System.Windows.Forms.RadioButton rdoAllMachine;
        private System.Windows.Forms.ComboBox cboMachine;
        private System.Windows.Forms.ComboBox cboMachineGroup;
        private System.Windows.Forms.ToolStripButton tbExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderType;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvWorkOrder;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butLeft;
        private System.Windows.Forms.Button butRight;
        private System.Windows.Forms.DataGridView dgvPlanned;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn workordernumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn WODate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Possession;
        private System.Windows.Forms.DataGridViewTextBoxColumn PIC;
        private System.Windows.Forms.DataGridViewTextBoxColumn yield;
        private System.Windows.Forms.DataGridViewTextBoxColumn seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn idd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}