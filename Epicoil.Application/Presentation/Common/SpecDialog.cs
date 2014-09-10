using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Repositories;

namespace Epicoil.Appl.Presentations
{
    public partial class SpecDialog : Form
    {
        private readonly ISpecRepo _repo;
        public string SpecID;
        public string SpecName;
        public string Commodity;
        public int RequireCoating;
        private string paramCommodity;

        public SpecDialog(string pCommodity = "")
        {
            InitializeComponent();
            this._repo = new SpecRepo();
            this.SpecID = "";
            this.SpecName = "";
            this.Commodity = "";
            this.RequireCoating = 0;
            this.paramCommodity = pCommodity;
        }

        private void ListToGrid(IEnumerable<SpecModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.SpecID, p.SpecName, p.Gravity, p.Commodity, p.RequireCoating.ToString("0"));

                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void SpecDialog_Load(object sender, EventArgs e)
        {
            if (paramCommodity != "")
            {
                var result = _repo.GetAll(paramCommodity);
                ListToGrid(result);
            }else
            {
                var result = _repo.GetAll();
                ListToGrid(result);
            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SpecID = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            SpecName = dgvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            Commodity = dgvList.Rows[e.RowIndex].Cells[3].Value.ToString();
            RequireCoating = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[4].Value.ToString());
            this.Close();
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            SpecModel model = new SpecModel();
            model.SpecID = txtSpecID.Text.ToString();
            model.SpecName = txtSpecName.Text.ToString();
            model.Commodity = txtCommodity.Text.ToString();

            var list = _repo.Get(model);
            dgvList.Rows.Clear();
            ListToGrid(list);
        }
    }
}