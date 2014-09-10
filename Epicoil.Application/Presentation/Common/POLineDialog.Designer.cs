namespace Epicoil.Appl.Presentations
{
    partial class POLineDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POLineDialog));
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.ponum1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poline1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linedesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poweight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balweight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commodityname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speccode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.specname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coatingname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actuser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
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
            this.dgvList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvList.ColumnHeadersHeight = 25;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ponum1,
            this.poline1,
            this.linedesc,
            this.poweight,
            this.balweight,
            this.thick,
            this.width,
            this.length,
            this.commodityname,
            this.speccode,
            this.specname,
            this.coating,
            this.coatingname,
            this.enduser,
            this.actuser});
            this.dgvList.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(738, 182);
            this.dgvList.TabIndex = 16;
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            // 
            // ponum1
            // 
            this.ponum1.HeaderText = "PO Num";
            this.ponum1.Name = "ponum1";
            this.ponum1.ReadOnly = true;
            this.ponum1.Visible = false;
            this.ponum1.Width = 71;
            // 
            // poline1
            // 
            this.poline1.HeaderText = "Line No.";
            this.poline1.Name = "poline1";
            this.poline1.ReadOnly = true;
            this.poline1.Width = 71;
            // 
            // linedesc
            // 
            this.linedesc.HeaderText = "Line Description";
            this.linedesc.Name = "linedesc";
            this.linedesc.ReadOnly = true;
            this.linedesc.Width = 107;
            // 
            // poweight
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N3";
            dataGridViewCellStyle1.NullValue = "0";
            this.poweight.DefaultCellStyle = dataGridViewCellStyle1;
            this.poweight.HeaderText = "PO Weight/Qty";
            this.poweight.Name = "poweight";
            this.poweight.ReadOnly = true;
            this.poweight.Width = 104;
            // 
            // balweight
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N3";
            dataGridViewCellStyle2.NullValue = "0";
            this.balweight.DefaultCellStyle = dataGridViewCellStyle2;
            this.balweight.HeaderText = "Remaining Weight/Qty";
            this.balweight.Name = "balweight";
            this.balweight.ReadOnly = true;
            this.balweight.Width = 139;
            // 
            // thick
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.thick.DefaultCellStyle = dataGridViewCellStyle3;
            this.thick.HeaderText = "Thick";
            this.thick.Name = "thick";
            this.thick.ReadOnly = true;
            this.thick.Width = 58;
            // 
            // width
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.width.DefaultCellStyle = dataGridViewCellStyle4;
            this.width.HeaderText = "Width";
            this.width.Name = "width";
            this.width.ReadOnly = true;
            this.width.Width = 59;
            // 
            // length
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0";
            this.length.DefaultCellStyle = dataGridViewCellStyle5;
            this.length.HeaderText = "Length";
            this.length.Name = "length";
            this.length.ReadOnly = true;
            this.length.Width = 64;
            // 
            // commodityname
            // 
            this.commodityname.HeaderText = "Commodity";
            this.commodityname.Name = "commodityname";
            this.commodityname.ReadOnly = true;
            this.commodityname.Width = 82;
            // 
            // speccode
            // 
            this.speccode.HeaderText = "Spec Code";
            this.speccode.Name = "speccode";
            this.speccode.ReadOnly = true;
            this.speccode.Width = 84;
            // 
            // specname
            // 
            this.specname.HeaderText = "Spec Name";
            this.specname.Name = "specname";
            this.specname.ReadOnly = true;
            this.specname.Width = 87;
            // 
            // coating
            // 
            this.coating.HeaderText = "Coating";
            this.coating.Name = "coating";
            this.coating.ReadOnly = true;
            this.coating.Width = 67;
            // 
            // coatingname
            // 
            this.coatingname.HeaderText = "Coating Name";
            this.coatingname.Name = "coatingname";
            this.coatingname.ReadOnly = true;
            this.coatingname.Width = 98;
            // 
            // enduser
            // 
            this.enduser.HeaderText = "End User";
            this.enduser.Name = "enduser";
            this.enduser.ReadOnly = true;
            this.enduser.Width = 75;
            // 
            // actuser
            // 
            this.actuser.HeaderText = "Actual End User";
            this.actuser.Name = "actuser";
            this.actuser.ReadOnly = true;
            this.actuser.Width = 108;
            // 
            // POLineDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(738, 182);
            this.Controls.Add(this.dgvList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "POLineDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PO Line";
            this.Load += new System.EventHandler(this.POLineDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ponum1;
        private System.Windows.Forms.DataGridViewTextBoxColumn poline1;
        private System.Windows.Forms.DataGridViewTextBoxColumn linedesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn poweight;
        private System.Windows.Forms.DataGridViewTextBoxColumn balweight;
        private System.Windows.Forms.DataGridViewTextBoxColumn thick;
        private System.Windows.Forms.DataGridViewTextBoxColumn width;
        private System.Windows.Forms.DataGridViewTextBoxColumn length;
        private System.Windows.Forms.DataGridViewTextBoxColumn commodityname;
        private System.Windows.Forms.DataGridViewTextBoxColumn speccode;
        private System.Windows.Forms.DataGridViewTextBoxColumn specname;
        private System.Windows.Forms.DataGridViewTextBoxColumn coating;
        private System.Windows.Forms.DataGridViewTextBoxColumn coatingname;
        private System.Windows.Forms.DataGridViewTextBoxColumn enduser;
        private System.Windows.Forms.DataGridViewTextBoxColumn actuser;
    }
}