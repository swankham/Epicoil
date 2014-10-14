using System;
using System.Drawing;
using Epicoil.Appl;
using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Planning;
using Epicoil.Library.Repositories.Common;
using System.Windows.Forms;
using System.Collections.Generic;
namespace Epicoil.Appl.Presentations.Planning
{
    public partial class DiePatternMaster : BaseSession
    {
        private readonly IDieMasterRepo _repo;
        public string PatternPara;
        public string StorePerPcsPara;
        public string RemarkPara;
        public DiePatternMaster()
        {
            InitializeComponent();
            this._repo = new DieMasterRepo();
            this.PatternPara = "";
            this.StorePerPcsPara = "";
            this.RemarkPara = "";
        }

        private void DiePatternMaster_Load(object sender, EventArgs e)
        {
            var result = _repo.GetPatternAll();
            SetGrid(result);
        }

        private void SetGrid(IEnumerable<DiePatternModel> list)
        {
            dgvList.Rows.Clear();
            foreach (var item in list)
            {
                dgvList.Rows.Add(item.PatternID ,item.StrokePerPcs,item.FROutPut,item.DROutPut ,item.OPOutPut,item.Block ,item.Remark);

            }
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            DiePatternModel model = new DiePatternModel();
            model.PatternID = txtPattern.Text;
            var result = _repo.GetByFilter(model);
            SetGrid(result);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PatternPara = dgvList.Rows[e.RowIndex].Cells["Pattern"].Value.ToString ();
            StorePerPcsPara = dgvList.Rows[e.RowIndex].Cells["StorkePerPcs"].Value.ToString();
            RemarkPara = dgvList.Rows[e.RowIndex].Cells["Remark"].Value.ToString();
            this.Close(); 
        }

    }
}
