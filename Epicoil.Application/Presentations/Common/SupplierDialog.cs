using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Repositories;

namespace Epicoil.Appl.Presentations
{
    public partial class SupplierDialog : Form
    {
        private readonly ISupplierRepo _repo;
        public string VendorId;
        public string VendorName;

        public SupplierDialog()
        {
            InitializeComponent();
            this._repo = new SupplierRepo();
            this.VendorId = "";
            this.VendorName = "";
        }

        private void ListToGrid(IEnumerable<SupplierModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.VendorID, p.VendorName, p.Address);

                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void SupplierDialog_Load(object sender, EventArgs e)
        {
            var list = _repo.GetAll();
            ListToGrid(list);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            SupplierModel model = new SupplierModel();
            model.VendorName = txtName.Text.ToString();
            var list = _repo.Get(model);
            dgvList.Rows.Clear();
            ListToGrid(list);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            VendorId = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            VendorName = dgvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.Close();
        }
    }
}