using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories.StoreInPlan;

namespace Epicoil.Appl.Presentations.StoreInPlan
{
    public partial class MappingCustomerDialog : Form
    {
        private readonly ILookupRepo _repo;
        public MappingLookupModel HeadContent;

        public MappingCustomerDialog(string supplierCode, string supplierName, string source)
        {
            InitializeComponent();
            this._repo = new LookupRepo();
            this.HeadContent = new MappingLookupModel();
            HeadContent.SupplierCode = supplierCode;
            HeadContent.SupplierName = supplierName;
            HeadContent.SupCode = source;
        }

        private void MappingCustomerDialog_Load(object sender, EventArgs e)
        {
            SetHeadContent(HeadContent);
            var result = _repo.GetAll("CUST", HeadContent.SupplierCode);
            SetGrid(result);
        }

        private void SetHeadContent(MappingLookupModel model)
        {
            txtSupplierName.Text = model.SupplierName;
            txtSource.Text = model.SupCode;
        }

        private void butCustomer_Click(object sender, EventArgs e)
        {
            using (CustomerDailog frm = new CustomerDailog())
            {
                frm.ShowDialog();

                txtCustID.Text = frm.CustId;
                txtCustomerName.Text = frm.CustName;
            }
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSource.Text))
            {
                MessageBox.Show("Please fill the source data.", "Data not valid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSource.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCustID.Text))
            {
                MessageBox.Show("Please fill the customer data.", "Data not valid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSource.Focus();
                return;
            }
            MappingLookupModel model = new MappingLookupModel();
            model.TypeCode = "CUST";
            model.SupplierCode = HeadContent.SupplierCode;
            model.SupCode = txtSource.Text.Trim();
            model.UCCCode = txtCustID.Text.Trim();
            model.UCCCodeForeign = string.Empty;
            var result = _repo.Save(model);
            SetGrid(result);
        }

        private void SetGrid(IEnumerable<MappingLookupModel> item)
        {
            //GetDetail
            dgvList.Rows.Clear();
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.LookupID, p.TypeCode, p.SupCode, p.UCCCode);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                HeadContent.LookupID = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            else { HeadContent.LookupID = 0; }
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            if (HeadContent.LookupID == 0)
            {
                MessageBox.Show("Please select line.", "Data not valid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MappingLookupModel model = new MappingLookupModel();
            model.TypeCode = "CUST";
            model.LookupID = HeadContent.LookupID;
            model.SupplierCode = HeadContent.SupplierCode;
            model.SupCode = txtSource.Text.Trim();
            model.UCCCode = txtCustID.Text.Trim();
            var result = _repo.Delete(model);
            SetGrid(result);
        }
    }
}