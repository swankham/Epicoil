using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Repositories;

namespace Epicoil.Appl.Presentations
{
    public partial class MakerDialog : Form
    {
        private readonly IMakerRepo _repo;
        public string MakerCode;
        public string MakerName;

        public MakerDialog()
        {
            InitializeComponent();
            this._repo = new MakerRepo();
            this.MakerCode = "";
            this.MakerName = "";
        }

        private void ListToGrid(IEnumerable<MakerModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.MakerCode, p.MakerName);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void MakerDialog_Load(object sender, EventArgs e)
        {
            var list = _repo.GetAll();
            ListToGrid(list);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            MakerModel model = new MakerModel();
            model.MakerName = txtFilter.Text.ToString();

            var list = _repo.GetByFilter(model);
            dgvList.Rows.Clear();
            ListToGrid(list);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MakerCode = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            MakerName = dgvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.Close();
        }
    }
}