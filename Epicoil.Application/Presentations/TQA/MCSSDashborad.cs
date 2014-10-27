using Epicoil.Appl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Common;
using Epicoil.Library.Repositories.TQA;
using Epicoil.Library.Models.TQA;

namespace Epicoil.Appl.Presentations.TQA
{
    public partial class MCSSDashborad : BaseSession
    {
        private readonly IMcssRepo _repo;
        private List<MCSS> McssList;
        private string McssNum;

        public MCSSDashborad(SessionInfo _session = null, MCSS model = null)
        {
            InitializeComponent();
            this._repo = new McssRepo();
            this.McssList = new List<MCSS>();
            this.McssNum = "";
            epiSession = _session;
        }

        private void MCSSDashborad_Load(object sender, EventArgs e)
        {
            ResetData();
            if (epiSession.SessionID == null)
            {
                Login frm = new Login();
                frm.ShowDialog();
                MCSSDashborad_Load(sender, e);
            }
            else if (epiSession.SessionID == "x")
            {
                Application.Exit();
            }
            else
            {
                this.Text = epiSession.CompanyName;
                ResetData();
                return;
            }
        }

        private void ListToGrid(IEnumerable<MCSS> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add("", p.Plant, "", p.McssNum, p.RequestDate.ToShortDateString(), p.SupplierCode, p.CustID
                                   , p.MakerCode, p.MillCode, p.Coating2, p.CommodityCode, p.MatSpec2
                                   , p.Thick.ToString("#,##0.000"), p.Width.ToString("#,##0.000"), p.Length.ToString("#,##0.000"), p.TISINo);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void ResetData()
        {
            dgvList.Rows.Clear();
            chkStatus.Checked = false;
            txtMCSSNum.Text = "";
        }

        private void mnuEditFind_Click(object sender, EventArgs e)
        {
            dgvList.Rows.Clear();
            MCSS model = new MCSS();

            model.Plant = epiSession.PlantID;

            if (txtMCSSNum.Text != null)
            {
                model.McssNum = txtMCSSNum.Text.Trim();
            }

            var list = _repo.GetByFilter(dtpDataFrom.Value, dtpDateTo.Value, model, chkStatus.Checked).ToList();
            ListToGrid(list);
        }

        private void mnuEditClear_Click(object sender, EventArgs e)
        {
            ResetData();
        }

        private void tlbClear_Click(object sender, EventArgs e)
        {
            mnuEditClear_Click(sender, e);
        }

        private void tlbFind_Click(object sender, EventArgs e)
        {
            mnuEditFind_Click(sender, e);
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tlbOpen_Click(object sender, EventArgs e)
        {
            if (this.McssNum != "")
            {
                var model = _repo.Get(epiSession.PlantID, this.McssNum);
                McssMaster frm = new McssMaster(model, epiSession);
                frm.Show();
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                this.McssNum = dgvList.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            else { this.McssNum = ""; }
        }

        private void tlbNew_Click(object sender, EventArgs e)
        {
            MCSS model = new MCSS();
            model.SpecialRefs = new List<SpecialRef>();
            model.InsertState = true;
            McssMaster frm = new McssMaster(model, epiSession);
            frm.Show();
        }
    }
}