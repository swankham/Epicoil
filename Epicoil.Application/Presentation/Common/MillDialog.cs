using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Repositories;

namespace Epicoil.Appl.Presentations
{
    public partial class MillDialog : Form
    {
        private readonly IMillRepo _repo;
        public string MakerCode;
        public string MillCode;
        public string MillName;
        private string paramMaker;

        public MillDialog(string pMakerCode = "")
        {
            InitializeComponent();
            this._repo = new MillRepo();
            this.MakerCode = "";
            this.MillCode = "";
            this.MillName = "";
            this.paramMaker = pMakerCode;
        }

        private void ListToGrid(IEnumerable<MillModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.MakerCode, p.MillCode, p.MillName);

                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void MillDialog_Load(object sender, EventArgs e)
        {
            if (paramMaker != "")
            {
                var result = _repo.GetAll(paramMaker);
                ListToGrid(result);
            }
            else
            {
                var result = _repo.GetAll();
                ListToGrid(result);
            }
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            MillModel model = new MillModel();
            model.MakerCode = txtMaker.Text.ToString();
            model.MillCode = txtMillCode.Text.ToString();
            model.MillName = txtMillName.Text.ToString();

            var list = _repo.Get(model);
            dgvList.Rows.Clear();
            ListToGrid(list);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MakerCode = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            MillCode = dgvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            MillName = dgvList.Rows[e.RowIndex].Cells[2].Value.ToString();
            this.Close();
        }
    }
}
