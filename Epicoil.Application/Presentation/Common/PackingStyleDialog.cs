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
    public partial class PackingStyleDialog : Form
    {
        private readonly IPackingStyle _repo;
        public string StyleCode;

        public PackingStyleDialog()
        {
            InitializeComponent();
            this._repo = new PackingStyle();
            this.StyleCode = "";
        }

        private void ListToGrid(IEnumerable<PackingStyleModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.CodeNum, p.StyleTypeName, p.CoilSkidName, p.CoilWrappingName, p.CoilStrappingName
                                ,p.SheetSkidName, p.SheetWrappingName, p.SheetStrappingName);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void PackingStyleDialog_Load(object sender, EventArgs e)
        {
            var list = _repo.GetAll();
            ListToGrid(list);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            StyleCode = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.Close();
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            PackingStyleModel model = new PackingStyleModel();
            model.CodeNum = txtCoating.Text.ToString();
            model.StyleType = cmbStyleType.SelectedIndex;

            var list = _repo.GetByFilter(model);
            dgvList.Rows.Clear();
            ListToGrid(list);
        }
    }
}
