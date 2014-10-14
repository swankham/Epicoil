using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Repositories;

namespace Epicoil.Appl.Presentations
{
    public partial class BussinessTypeDialog : Form
    {
        private readonly IBussinessTypeRepo _repo;
        public string Code;
        public string Description;

        public BussinessTypeDialog()
        {
            InitializeComponent();
            this._repo = new BussinessTypeRepo();
            this.Code = "";
            this.Description = "";
        }

        private void ListToGrid(IEnumerable<BussinessTypeModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.BussinessCode, p.BussinessName, "");
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void BussinessTypeDialog_Load(object sender, EventArgs e)
        {
            var list = _repo.GetAll();
            ListToGrid(list);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            BussinessTypeModel model = new BussinessTypeModel();
            model.BussinessName = txtFilter.Text.ToString();

            var list = _repo.GetByFilter(model);
            dgvList.Rows.Clear();
            ListToGrid(list);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Code = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            Description = dgvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.Close();
        }
    }
}