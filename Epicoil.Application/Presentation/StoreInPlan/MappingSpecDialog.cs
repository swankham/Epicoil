using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories.StoreInPlan;

namespace Epicoil.Appl.Presentations.StoreInPlan
{
    public partial class MappingSpecDialog : Form
    {
        private readonly ILookupRepo _repo;
        public MappingLookupModel HeadContent;

        public MappingSpecDialog(string supplierCode, string supplierName, string source)
        {
            InitializeComponent();
            this._repo = new LookupRepo();
            this.HeadContent = new MappingLookupModel();
            HeadContent.SupplierCode = supplierCode;
            HeadContent.SupplierName = supplierName;
            HeadContent.SupCode = source;
        }

        private void MappingSpecDialog_Load(object sender, EventArgs e)
        {
            SetHeadContent(HeadContent);
            var result = _repo.GetAll("SPEC", HeadContent.SupplierCode);
            SetGrid(result);
        }

        private void SetHeadContent(MappingLookupModel model)
        {
            //txtStoreInPlanNum.DataBindings.Add("Text", model, "StoreInPlanNum", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSupplierName.Text = model.SupplierName;
            txtSource.Text = model.SupCode;
        }

        private void butCommodity_Click(object sender, EventArgs e)
        {
            using (CommodityDialog frm = new CommodityDialog())
            {
                frm.ShowDialog();
                string code = frm.Code;
                if (code != txtCommodityCode.Text.Trim())
                {
                    txtCommodityCode.Text = code;
                    txtCommodityName.Text = frm.Description;
                    txtMatSpec1.Text = "";
                    txtMatSpec2.Text = "";
                }
            }
        }

        private void butSpec_Click(object sender, EventArgs e)
        {
            using (SpecDialog frm = new SpecDialog(txtCommodityCode.Text.ToString()))
            {
                frm.ShowDialog();

                txtMatSpec1.Text = frm.SpecID;
                txtMatSpec2.Text = frm.SpecName;
            }
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSource.Text))
            {
                MessageBox.Show("Please fill the source data.", "Data not valid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSource.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtMatSpec1.Text))
            {
                MessageBox.Show("Please fill the specification data.", "Data not valid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSource.Focus();
                return;
            }
            MappingLookupModel model = new MappingLookupModel();
            model.TypeCode = "SPEC";
            model.SupplierCode = HeadContent.SupplierCode;
            model.SupCode = txtSource.Text.Trim();
            model.UCCCode = txtMatSpec1.Text.Trim();
            model.UCCCodeForeign = txtCommodityCode.Text.Trim();
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
                dgvList.Rows.Add(p.LookupID, p.TypeCode, p.SupCode, p.UCCCode, p.UCCCodeForeign);
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
            model.TypeCode = "SPEC";
            model.LookupID = HeadContent.LookupID;
            model.SupplierCode = HeadContent.SupplierCode;
            model.SupCode = txtSource.Text.Trim();
            model.UCCCode = txtMatSpec1.Text.Trim();
            model.UCCCodeForeign = txtCommodityCode.Text.Trim();
            var result = _repo.Delete(model);
            SetGrid(result);
        }
    }
}