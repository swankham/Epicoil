using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Epicoil.Library.Models;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.StoreInPlan;

namespace Epicoil.Appl.Presentations
{
    public partial class POLineDialog : Form
    {
        private readonly IPORepo _repo;
        private readonly IStoreInPlanRepo _repoSpl;
        private int PONUM;
        public POLineModel paramLine;
        //public int POLine;
        //public decimal POBalance;

        public POLineDialog(int PONum)
        {
            InitializeComponent();
            this._repo = new PORepo();
            this._repoSpl = new StoreInPlanRepo();
            this.paramLine = new POLineModel();
            this.PONUM = PONum;
            //this.POBalance = 0;
        }

        private void ListToGrid(IEnumerable<POLineModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                decimal POBalance = p.POWeight - _repoSpl.GetReceivedWeight(PONUM, p.POLine);
                dgvList.Rows.Add(p.PONum, p.POLine, p.LineDesc, p.POWeight, POBalance, p.Thick, p.Width, p.Length, p.CommodityName
                                 , p.SpecCode, p.SpecName, p.CoatingCode, p.CoatingName, p.EndUserID, p.EndUserName
                                 , p.ActlEndUserID, p.ActlEndUserName);

                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void POLineDialog_Load(object sender, EventArgs e)
        {
            var list = _repo.GetByPO(PONUM);
            ListToGrid(list);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                paramLine.PONum = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells["ponum1"].Value);
                paramLine.POLine = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells["poline1"].Value);
                paramLine.BalanceWeight = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells["balweight"].Value);
                this.Close();
            }
        }

    }
}