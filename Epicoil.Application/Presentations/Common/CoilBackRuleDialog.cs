using Epicoil.Library.Models;
using Epicoil.Library.Repositories;
using System;
using System.Collections.Generic;

namespace Epicoil.Appl.Presentations
{
    public partial class CoilBackRuleDialog : BaseSession
    {
        private readonly ICoilBackRuleRepo _repo;
        public string Code;
        public string Description;

        public CoilBackRuleDialog()
        {
            InitializeComponent();
            this._repo = new CoilBackRuleRepo();
            this.Code = "";
            this.Description = "";
        }

        private void CoilBackRuleDialog_Load(object sender, EventArgs e)
        {
            var result = _repo.GetAll();
            SetGrid(result);
        }

        private void SetGrid(IEnumerable<CoilBackRuleModel> list)
        {
            dgvList.Rows.Clear();
            foreach (var item in list)
            {
                dgvList.Rows.Add(item.RuleID, item.Thick, item.Width
                                , item.Weight, item.Description);
            }
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            CoilBackRuleModel model = new CoilBackRuleModel();
            model.RuleID = txtID.Text;

            var result = _repo.GetByFilter(model);
            SetGrid(result);
        }

        private void dgvList_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            Code = dgvList.Rows[e.RowIndex].Cells["ruleid"].Value.ToString();
            Description = dgvList.Rows[e.RowIndex].Cells["desc"].Value.ToString();
            this.Close();
        }
    }
}