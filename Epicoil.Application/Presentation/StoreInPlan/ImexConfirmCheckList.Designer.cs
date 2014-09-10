namespace Epicoil.Appl.Presentations.StoreInPlan
{
    partial class ImexConfirmCheckList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImexConfirmCheckList));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tlbIndetail = new System.Windows.Forms.ToolStripButton();
            this.tlbInactive = new System.Windows.Forms.ToolStripButton();
            this.tlbClear = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.goToStoreInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpInvoiceDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpInvoiceDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.txtStoreInPlanNo = new System.Windows.Forms.TextBox();
            this.butSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statuscode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.confirm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usergroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastupdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storeinplanno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issuedate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.makercode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.makername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.millcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.millname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currencycode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bussinesstype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vessel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loadport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arriveport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etddate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etadate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(23, 20);
            this.toolStrip2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbIndetail,
            this.tlbInactive,
            this.tlbClear});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 23);
            this.toolStrip2.Margin = new System.Windows.Forms.Padding(2);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(2);
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(1005, 34);
            this.toolStrip2.TabIndex = 38;
            this.toolStrip2.Text = "Tool Bar";
            // 
            // tlbIndetail
            // 
            this.tlbIndetail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbIndetail.Image = global::Epicoil.Appl.Properties.Resources.epicor_open;
            this.tlbIndetail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlbIndetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbIndetail.Name = "tlbIndetail";
            this.tlbIndetail.Size = new System.Drawing.Size(23, 27);
            this.tlbIndetail.Text = "View Detail";
            this.tlbIndetail.Click += new System.EventHandler(this.tlbIndetail_Click);
            // 
            // tlbInactive
            // 
            this.tlbInactive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbInactive.Image = global::Epicoil.Appl.Properties.Resources.epicor_refresh;
            this.tlbInactive.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlbInactive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbInactive.Name = "tlbInactive";
            this.tlbInactive.Size = new System.Drawing.Size(23, 27);
            this.tlbInactive.Text = "Refresh";
            this.tlbInactive.Click += new System.EventHandler(this.tlbInactive_Click);
            // 
            // tlbClear
            // 
            this.tlbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbClear.Image = global::Epicoil.Appl.Properties.Resources.epicor_clear;
            this.tlbClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbClear.Name = "tlbClear";
            this.tlbClear.Size = new System.Drawing.Size(23, 27);
            this.tlbClear.Text = "Clear";
            this.tlbClear.Click += new System.EventHandler(this.tlbClear_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(1005, 23);
            this.menuStrip1.TabIndex = 39;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToStoreInToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 19);
            this.mnuFile.Text = "File";
            // 
            // goToStoreInToolStripMenuItem
            // 
            this.goToStoreInToolStripMenuItem.Image = global::Epicoil.Appl.Properties.Resources.epicor_open;
            this.goToStoreInToolStripMenuItem.Name = "goToStoreInToolStripMenuItem";
            this.goToStoreInToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.goToStoreInToolStripMenuItem.Text = "View &Detail";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(135, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileExit.Image")));
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.mnuFileExit.Size = new System.Drawing.Size(138, 22);
            this.mnuFileExit.Text = "Exit";
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditClear,
            this.mnuRefresh,
            this.toolStripMenuItem2});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 19);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuEditClear
            // 
            this.mnuEditClear.Image = global::Epicoil.Appl.Properties.Resources.epicor_clear;
            this.mnuEditClear.Name = "mnuEditClear";
            this.mnuEditClear.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.mnuEditClear.Size = new System.Drawing.Size(159, 22);
            this.mnuEditClear.Text = "Clear";
            // 
            // mnuRefresh
            // 
            this.mnuRefresh.Image = global::Epicoil.Appl.Properties.Resources.epicor_refresh;
            this.mnuRefresh.Name = "mnuRefresh";
            this.mnuRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.mnuRefresh.Size = new System.Drawing.Size(159, 22);
            this.mnuRefresh.Text = "Refresh";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(156, 6);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpInvoiceDateTo);
            this.panel1.Controls.Add(this.dtpInvoiceDateFrom);
            this.panel1.Controls.Add(this.txtInvoiceNo);
            this.panel1.Controls.Add(this.txtStoreInPlanNo);
            this.panel1.Controls.Add(this.butSearch);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1005, 55);
            this.panel1.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(497, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "To";
            // 
            // dtpInvoiceDateTo
            // 
            this.dtpInvoiceDateTo.Location = new System.Drawing.Point(523, 27);
            this.dtpInvoiceDateTo.Name = "dtpInvoiceDateTo";
            this.dtpInvoiceDateTo.Size = new System.Drawing.Size(146, 20);
            this.dtpInvoiceDateTo.TabIndex = 16;
            // 
            // dtpInvoiceDateFrom
            // 
            this.dtpInvoiceDateFrom.Location = new System.Drawing.Point(345, 27);
            this.dtpInvoiceDateFrom.Name = "dtpInvoiceDateFrom";
            this.dtpInvoiceDateFrom.Size = new System.Drawing.Size(146, 20);
            this.dtpInvoiceDateFrom.TabIndex = 15;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Location = new System.Drawing.Point(107, 27);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(146, 20);
            this.txtInvoiceNo.TabIndex = 14;
            // 
            // txtStoreInPlanNo
            // 
            this.txtStoreInPlanNo.Location = new System.Drawing.Point(107, 5);
            this.txtStoreInPlanNo.Name = "txtStoreInPlanNo";
            this.txtStoreInPlanNo.Size = new System.Drawing.Size(146, 20);
            this.txtStoreInPlanNo.TabIndex = 13;
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(675, 26);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(139, 23);
            this.butSearch.TabIndex = 12;
            this.butSearch.Text = "Search...";
            this.butSearch.UseVisualStyleBackColor = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Invoice Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Invoice No. :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Store In Plan No. :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1005, 618);
            this.panel2.TabIndex = 41;
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
            this.id,
            this.statuscode,
            this.confirm,
            this.usergroup,
            this.lastupdate,
            this.storeinplanno,
            this.invoiceno,
            this.supplier,
            this.issuedate,
            this.makercode,
            this.makername,
            this.millcode,
            this.millname,
            this.currencycode,
            this.bussinesstype,
            this.vessel,
            this.loadport,
            this.arriveport,
            this.etddate,
            this.etadate});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1005, 618);
            this.dgvList.TabIndex = 16;
            // 
            // id
            // 
            this.id.HeaderText = "storeinid";
            this.id.Name = "id";
            this.id.Visible = false;
            this.id.Width = 71;
            // 
            // statuscode
            // 
            this.statuscode.HeaderText = "statuscode";
            this.statuscode.Name = "statuscode";
            this.statuscode.ReadOnly = true;
            this.statuscode.Visible = false;
            this.statuscode.Width = 84;
            // 
            // confirm
            // 
            this.confirm.HeaderText = "Confirm Status";
            this.confirm.Name = "confirm";
            this.confirm.ReadOnly = true;
            this.confirm.Width = 99;
            // 
            // usergroup
            // 
            this.usergroup.HeaderText = "Last update by";
            this.usergroup.Name = "usergroup";
            this.usergroup.ReadOnly = true;
            this.usergroup.Width = 101;
            // 
            // lastupdate
            // 
            this.lastupdate.HeaderText = "Last Update";
            this.lastupdate.Name = "lastupdate";
            this.lastupdate.ReadOnly = true;
            this.lastupdate.Width = 89;
            // 
            // storeinplanno
            // 
            this.storeinplanno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.storeinplanno.HeaderText = "Store In Plant No.";
            this.storeinplanno.Name = "storeinplanno";
            this.storeinplanno.ReadOnly = true;
            this.storeinplanno.Width = 115;
            // 
            // invoiceno
            // 
            this.invoiceno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.invoiceno.HeaderText = "Invoice No.";
            this.invoiceno.Name = "invoiceno";
            this.invoiceno.ReadOnly = true;
            this.invoiceno.Width = 86;
            // 
            // supplier
            // 
            this.supplier.HeaderText = "Supplier";
            this.supplier.Name = "supplier";
            this.supplier.ReadOnly = true;
            this.supplier.Width = 69;
            // 
            // issuedate
            // 
            this.issuedate.HeaderText = "Issue Date";
            this.issuedate.Name = "issuedate";
            this.issuedate.ReadOnly = true;
            this.issuedate.Width = 82;
            // 
            // makercode
            // 
            this.makercode.HeaderText = "Maker Code";
            this.makercode.Name = "makercode";
            this.makercode.ReadOnly = true;
            this.makercode.Width = 89;
            // 
            // makername
            // 
            this.makername.HeaderText = "Maker Name";
            this.makername.Name = "makername";
            this.makername.ReadOnly = true;
            this.makername.Width = 92;
            // 
            // millcode
            // 
            this.millcode.HeaderText = "Mill Code";
            this.millcode.Name = "millcode";
            this.millcode.ReadOnly = true;
            this.millcode.Width = 74;
            // 
            // millname
            // 
            this.millname.HeaderText = "Mill Name";
            this.millname.Name = "millname";
            this.millname.ReadOnly = true;
            this.millname.Width = 77;
            // 
            // currencycode
            // 
            this.currencycode.HeaderText = "Currency Code";
            this.currencycode.Name = "currencycode";
            this.currencycode.ReadOnly = true;
            this.currencycode.Width = 101;
            // 
            // bussinesstype
            // 
            this.bussinesstype.HeaderText = "Bussiness Type";
            this.bussinesstype.Name = "bussinesstype";
            this.bussinesstype.ReadOnly = true;
            this.bussinesstype.Width = 105;
            // 
            // vessel
            // 
            this.vessel.HeaderText = "Vessel";
            this.vessel.Name = "vessel";
            this.vessel.ReadOnly = true;
            this.vessel.Width = 62;
            // 
            // loadport
            // 
            this.loadport.HeaderText = "Load Port";
            this.loadport.Name = "loadport";
            this.loadport.ReadOnly = true;
            this.loadport.Width = 77;
            // 
            // arriveport
            // 
            this.arriveport.HeaderText = "Arrive Port";
            this.arriveport.Name = "arriveport";
            this.arriveport.ReadOnly = true;
            this.arriveport.Width = 80;
            // 
            // etddate
            // 
            this.etddate.HeaderText = "ETD Date";
            this.etddate.Name = "etddate";
            this.etddate.ReadOnly = true;
            this.etddate.Width = 79;
            // 
            // etadate
            // 
            this.etadate.HeaderText = "ETA Date";
            this.etadate.Name = "etadate";
            this.etadate.ReadOnly = true;
            this.etadate.Width = 78;
            // 
            // ImexConfirmCheckList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1005, 730);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImexConfirmCheckList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IMEX Confirm list";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ImexConfirmCheckList_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tlbIndetail;
        private System.Windows.Forms.ToolStripButton tlbInactive;
        private System.Windows.Forms.ToolStripButton tlbClear;
        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuEditClear;
        private System.Windows.Forms.ToolStripMenuItem mnuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem goToStoreInToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn statuscode;
        private System.Windows.Forms.DataGridViewTextBoxColumn confirm;
        private System.Windows.Forms.DataGridViewTextBoxColumn usergroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastupdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn storeinplanno;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceno;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn issuedate;
        private System.Windows.Forms.DataGridViewTextBoxColumn makercode;
        private System.Windows.Forms.DataGridViewTextBoxColumn makername;
        private System.Windows.Forms.DataGridViewTextBoxColumn millcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn millname;
        private System.Windows.Forms.DataGridViewTextBoxColumn currencycode;
        private System.Windows.Forms.DataGridViewTextBoxColumn bussinesstype;
        private System.Windows.Forms.DataGridViewTextBoxColumn vessel;
        private System.Windows.Forms.DataGridViewTextBoxColumn loadport;
        private System.Windows.Forms.DataGridViewTextBoxColumn arriveport;
        private System.Windows.Forms.DataGridViewTextBoxColumn etddate;
        private System.Windows.Forms.DataGridViewTextBoxColumn etadate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDateTo;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDateFrom;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.TextBox txtStoreInPlanNo;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}