using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Repositories;

namespace Epicoil.Appl.Presentations
{
    public partial class CoatingDialog : Form
    {
        private readonly ICoatingRepo _repo;
        public string Coating;
        public string CoatingName;
        public string Abbr;
        public decimal FrontPlate;
        public decimal BackPlate;

        public CoatingDialog()
        {
            InitializeComponent();
            this._repo = new CoatingRepo();
            this.Coating = "";
            this.CoatingName = "";
            this.Abbr = "";
            this.FrontPlate = 0;
            this.BackPlate = 0;
        }

        private void ListToGrid(IEnumerable<CoatingModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.CoatingPlate, p.CoatingName, p.Abbr, p.FrontPlate, p.BackPlate);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void CoatingDialog_Load(object sender, EventArgs e)
        {
            var list = _repo.GetAll();
            ListToGrid(list);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Coating = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            CoatingName = dgvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            Abbr = dgvList.Rows[e.RowIndex].Cells[2].Value.ToString();
            FrontPlate = Convert.ToDecimal(dgvList.Rows[e.RowIndex].Cells[3].Value.ToString());
            BackPlate = Convert.ToDecimal(dgvList.Rows[e.RowIndex].Cells[4].Value.ToString());
            this.Close();
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            CoatingModel model = new CoatingModel();
            model.CoatingPlate = txtCoating.Text.ToString();
            model.CoatingName = txtName.Text.ToString();

            var list = _repo.Get(model);
            dgvList.Rows.Clear();
            ListToGrid(list);
        }
    }
}