namespace Epicoil.Appl.Presentations.Planning
{
    partial class DieMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DieMaster));
            this.btnDieSearch = new System.Windows.Forms.Button();
            this.txtDieSearch = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPatternRemark = new System.Windows.Forms.TextBox();
            this.txtStrokePcs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.btnPattern = new System.Windows.Forms.Button();
            this.txtDieName = new System.Windows.Forms.TextBox();
            this.txtDieCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tblNew = new System.Windows.Forms.ToolStripButton();
            this.tlbSave = new System.Windows.Forms.ToolStripButton();
            this.tblDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlbClear = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.diecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDieRemark = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDieSearch
            // 
            this.btnDieSearch.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnDieSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDieSearch.Image = global::Epicoil.Appl.Properties.Resources._find;
            this.btnDieSearch.Location = new System.Drawing.Point(169, 56);
            this.btnDieSearch.Name = "btnDieSearch";
            this.btnDieSearch.Size = new System.Drawing.Size(28, 20);
            this.btnDieSearch.TabIndex = 41;
            this.btnDieSearch.UseVisualStyleBackColor = true;
            this.btnDieSearch.Click += new System.EventHandler(this.btnDieSearch_Click);
            // 
            // txtDieSearch
            // 
            this.txtDieSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDieSearch.Location = new System.Drawing.Point(0, 56);
            this.txtDieSearch.Name = "txtDieSearch";
            this.txtDieSearch.Size = new System.Drawing.Size(163, 20);
            this.txtDieSearch.TabIndex = 40;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPatternRemark);
            this.groupBox1.Controls.Add(this.txtStrokePcs);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPattern);
            this.groupBox1.Controls.Add(this.btnPattern);
            this.groupBox1.Controls.Add(this.txtDieName);
            this.groupBox1.Controls.Add(this.txtDieCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(203, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 117);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            // 
            // txtPatternRemark
            // 
            this.txtPatternRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPatternRemark.Location = new System.Drawing.Point(185, 63);
            this.txtPatternRemark.Name = "txtPatternRemark";
            this.txtPatternRemark.ReadOnly = true;
            this.txtPatternRemark.Size = new System.Drawing.Size(314, 20);
            this.txtPatternRemark.TabIndex = 8;
            // 
            // txtStrokePcs
            // 
            this.txtStrokePcs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStrokePcs.Location = new System.Drawing.Point(79, 88);
            this.txtStrokePcs.Name = "txtStrokePcs";
            this.txtStrokePcs.ReadOnly = true;
            this.txtStrokePcs.Size = new System.Drawing.Size(100, 20);
            this.txtStrokePcs.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Stroke/Pcs :";
            // 
            // txtPattern
            // 
            this.txtPattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPattern.Location = new System.Drawing.Point(79, 63);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(100, 20);
            this.txtPattern.TabIndex = 5;
            this.txtPattern.Leave += new System.EventHandler(this.txtPattern_Leave);
            // 
            // btnPattern
            // 
            this.btnPattern.Location = new System.Drawing.Point(6, 62);
            this.btnPattern.Name = "btnPattern";
            this.btnPattern.Size = new System.Drawing.Size(70, 23);
            this.btnPattern.TabIndex = 4;
            this.btnPattern.Text = "Pattern";
            this.btnPattern.UseVisualStyleBackColor = true;
            this.btnPattern.Click += new System.EventHandler(this.btnPattern_Click);
            // 
            // txtDieName
            // 
            this.txtDieName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDieName.Location = new System.Drawing.Point(79, 38);
            this.txtDieName.Name = "txtDieName";
            this.txtDieName.Size = new System.Drawing.Size(237, 20);
            this.txtDieName.TabIndex = 3;
            // 
            // txtDieCode
            // 
            this.txtDieCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDieCode.Location = new System.Drawing.Point(79, 13);
            this.txtDieCode.Name = "txtDieCode";
            this.txtDieCode.ReadOnly = true;
            this.txtDieCode.Size = new System.Drawing.Size(100, 20);
            this.txtDieCode.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Die Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Die Code :";
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(23, 20);
            this.toolStrip2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tblNew,
            this.tlbSave,
            this.tblDelete,
            this.tlbRefresh,
            this.tlbClear});
            this.toolStrip2.Location = new System.Drawing.Point(0, 24);
            this.toolStrip2.Margin = new System.Windows.Forms.Padding(2);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(2);
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(757, 27);
            this.toolStrip2.TabIndex = 37;
            this.toolStrip2.Text = "Tool Bar";
            // 
            // tblNew
            // 
            this.tblNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblNew.Image = global::Epicoil.Appl.Properties.Resources.epicor_new;
            this.tblNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tblNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblNew.Name = "tblNew";
            this.tblNew.Size = new System.Drawing.Size(23, 20);
            this.tblNew.Text = "New";
            this.tblNew.Click += new System.EventHandler(this.tblNew_Click);
            // 
            // tlbSave
            // 
            this.tlbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbSave.Image = global::Epicoil.Appl.Properties.Resources.epicor_save;
            this.tlbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbSave.Name = "tlbSave";
            this.tlbSave.Size = new System.Drawing.Size(23, 20);
            this.tlbSave.Text = "Save";
            this.tlbSave.Click += new System.EventHandler(this.tlbSave_Click);
            // 
            // tblDelete
            // 
            this.tblDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblDelete.Image = global::Epicoil.Appl.Properties.Resources.epicor_delete;
            this.tblDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tblDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblDelete.Name = "tblDelete";
            this.tblDelete.Size = new System.Drawing.Size(23, 20);
            this.tblDelete.Text = "Delete";
            this.tblDelete.Click += new System.EventHandler(this.tblDelete_Click);
            // 
            // tlbRefresh
            // 
            this.tlbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbRefresh.Image = global::Epicoil.Appl.Properties.Resources.epicor_refresh;
            this.tlbRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbRefresh.Name = "tlbRefresh";
            this.tlbRefresh.Size = new System.Drawing.Size(23, 20);
            this.tlbRefresh.Text = "Refresh";
            this.tlbRefresh.Click += new System.EventHandler(this.tlbRefresh_Click);
            // 
            // tlbClear
            // 
            this.tlbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbClear.Image = global::Epicoil.Appl.Properties.Resources.epicor_clear;
            this.tlbClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbClear.Name = "tlbClear";
            this.tlbClear.Size = new System.Drawing.Size(23, 20);
            this.tlbClear.Text = "Clear";
            this.tlbClear.Click += new System.EventHandler(this.tlbClear_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(757, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::Epicoil.Appl.Properties.Resources._new;
            this.newToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Epicoil.Appl.Properties.Resources.epicor_save;
            this.saveToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::Epicoil.Appl.Properties.Resources.epicor_delete;
            this.deleteToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(151, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::Epicoil.Appl.Properties.Resources.file_exit;
            this.exitToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Image = global::Epicoil.Appl.Properties.Resources.epicor_clear;
            this.clearToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::Epicoil.Appl.Properties.Resources.epicor_refresh;
            this.refreshToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 25;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.diecode,
            this.diename});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(0, 82);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(197, 328);
            this.dataGridView1.TabIndex = 43;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // diecode
            // 
            this.diecode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.diecode.HeaderText = "Die Code";
            this.diecode.Name = "diecode";
            this.diecode.ReadOnly = true;
            this.diecode.Width = 74;
            // 
            // diename
            // 
            this.diename.HeaderText = "Die Name";
            this.diename.Name = "diename";
            this.diename.ReadOnly = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(206, 176);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 95;
            this.label15.Text = "Remark :";
            // 
            // txtDieRemark
            // 
            this.txtDieRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDieRemark.Location = new System.Drawing.Point(209, 192);
            this.txtDieRemark.Multiline = true;
            this.txtDieRemark.Name = "txtDieRemark";
            this.txtDieRemark.Size = new System.Drawing.Size(537, 82);
            this.txtDieRemark.TabIndex = 96;
            // 
            // DieMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 415);
            this.Controls.Add(this.txtDieRemark);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDieSearch);
            this.Controls.Add(this.txtDieSearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "DieMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Die Master";
            this.Load += new System.EventHandler(this.DieMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        public System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tblNew;
        private System.Windows.Forms.ToolStripButton tlbSave;
        private System.Windows.Forms.ToolStripButton tblDelete;
        private System.Windows.Forms.ToolStripButton tlbRefresh;
        private System.Windows.Forms.ToolStripButton tlbClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDieSearch;
        private System.Windows.Forms.Button btnDieSearch;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Button btnPattern;
        private System.Windows.Forms.TextBox txtDieName;
        private System.Windows.Forms.TextBox txtDieCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn diecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn diename;
        private System.Windows.Forms.TextBox txtStrokePcs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPatternRemark;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDieRemark;
    }
}