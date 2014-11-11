namespace Epicoil.Appl.Presentations.Planning
{
    partial class SerialList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerialList));
            this.dgvCutting = new System.Windows.Forms.DataGridView();
            this.serialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thick1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.width1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lengthM1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitweight1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalweight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commodity1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spec1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coating1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.possession = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCutting)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCutting
            // 
            this.dgvCutting.AllowUserToAddRows = false;
            this.dgvCutting.AllowUserToDeleteRows = false;
            this.dgvCutting.AllowUserToResizeRows = false;
            this.dgvCutting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCutting.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCutting.BackgroundColor = System.Drawing.Color.White;
            this.dgvCutting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCutting.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvCutting.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvCutting.ColumnHeadersHeight = 25;
            this.dgvCutting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCutting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialNo,
            this.thick1,
            this.width1,
            this.length1,
            this.lengthM1,
            this.unitweight1,
            this.totalweight,
            this.commodity1,
            this.spec1,
            this.coating1,
            this.bt1,
            this.possession});
            this.dgvCutting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCutting.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvCutting.EnableHeadersVisualStyles = false;
            this.dgvCutting.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvCutting.Location = new System.Drawing.Point(0, 0);
            this.dgvCutting.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCutting.Name = "dgvCutting";
            this.dgvCutting.RowHeadersVisible = false;
            this.dgvCutting.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvCutting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCutting.Size = new System.Drawing.Size(883, 524);
            this.dgvCutting.TabIndex = 20;
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
            // lengthM1
            // 
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0";
            this.lengthM1.DefaultCellStyle = dataGridViewCellStyle5;
            this.lengthM1.HeaderText = "Length(M)";
            this.lengthM1.Name = "lengthM1";
            this.lengthM1.ReadOnly = true;
            this.lengthM1.Width = 88;
            // 
            // unitweight1
            // 
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0";
            this.unitweight1.DefaultCellStyle = dataGridViewCellStyle6;
            this.unitweight1.HeaderText = "Unit Weight";
            this.unitweight1.Name = "unitweight1";
            this.unitweight1.ReadOnly = true;
            this.unitweight1.Width = 94;
            // 
            // totalweight
            // 
            this.totalweight.HeaderText = "Status";
            this.totalweight.Name = "totalweight";
            this.totalweight.ReadOnly = true;
            this.totalweight.Width = 65;
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
            // SerialList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 524);
            this.Controls.Add(this.dgvCutting);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SerialList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serial List";
            this.Load += new System.EventHandler(this.SerialList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCutting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCutting;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn thick1;
        private System.Windows.Forms.DataGridViewTextBoxColumn width1;
        private System.Windows.Forms.DataGridViewTextBoxColumn length1;
        private System.Windows.Forms.DataGridViewTextBoxColumn lengthM1;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitweight1;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalweight;
        private System.Windows.Forms.DataGridViewTextBoxColumn commodity1;
        private System.Windows.Forms.DataGridViewTextBoxColumn spec1;
        private System.Windows.Forms.DataGridViewTextBoxColumn coating1;
        private System.Windows.Forms.DataGridViewTextBoxColumn bt1;
        private System.Windows.Forms.DataGridViewTextBoxColumn possession;
    }
}