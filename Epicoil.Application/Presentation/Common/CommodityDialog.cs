using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Repositories;

namespace Epicoil.Appl.Presentations
{
    public partial class CommodityDialog : Form
    {
        private readonly ICommodity _repo;
        public string Code;
        public string Description;
        public bool RequireCoating;

        public CommodityDialog()
        {
            InitializeComponent();
            this._repo = new Commodity();
            this.Code = "";
            this.Description = "";
            this.RequireCoating = false;
        }

        private void ListToGrid(IEnumerable<CommodityModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.CommodityCode, p.CommodityName, p.CoatingRequire);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void CommodityDialog_Load(object sender, EventArgs e)
        {
            var list = _repo.GetAll();
            ListToGrid(list);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            CommodityModel model = new CommodityModel();
            model.CommodityCode = txtSpecID.Text.ToString();

            var list = _repo.GetByFilter(model);
            dgvList.Rows.Clear();
            ListToGrid(list);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Code = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            Description = dgvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            RequireCoating = Convert.ToBoolean(dgvList.Rows[e.RowIndex].Cells[2].Value.ToString());
            this.Close();
        }
    }
}