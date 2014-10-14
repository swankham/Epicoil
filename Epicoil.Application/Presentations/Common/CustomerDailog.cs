using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Repositories;

namespace Epicoil.Appl.Presentations
{
    public partial class CustomerDailog : Form
    {
        private readonly ICustomerRepo _repo;
        public string CustId;
        public string CustName;

        public CustomerDailog()
        {
            InitializeComponent();
            this._repo = new CustomerRepo();
            this.CustId = "";
            this.CustName = "";
        }

        private void CustomerDailog_Load(object sender, EventArgs e)
        {
            var list = _repo.GetAllCustomer();
            ListToGrid(list);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            CustomerModel model = new CustomerModel();
            model.CustName = txtCustomerName.Text.ToString();
            var list = _repo.GetCustomerByFilter(model);
            dgvList.Rows.Clear();
            ListToGrid(list);
        }

        private void ListToGrid(IEnumerable<CustomerModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.CustId, p.CustName, p.Address);

                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CustId = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            CustName = dgvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.Close();
        }
    }
}