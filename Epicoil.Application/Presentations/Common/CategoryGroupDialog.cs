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
    public partial class CategoryGroupDialog : Form
    {
        private readonly ICategoryGroupRepo  _repo;
        public string Code;
        public string Name;

        public CategoryGroupDialog()
        {
            InitializeComponent();
            this._repo = new CategoryGroupRepo();
            this.Code = "";
            this.Name = "";
        }

        private void ListToGrid(IEnumerable<CategoryGroupModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.CategoryGroupCode, p.CategoryGroupName);

                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void CategoryGroupDialog_Load(object sender, EventArgs e)
        {
            var list = _repo.GetAll();
            ListToGrid(list);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            CategoryGroupModel model = new CategoryGroupModel();
            model.CategoryGroupName = txtFilter.Text.ToString();
            var list = _repo.Get(model);
            dgvList.Rows.Clear();
            ListToGrid(list);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Code = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            Name = dgvList.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.Close();
        }
    }
}
