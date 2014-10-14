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
using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories.StoreInPlan;
using Epicoil.Appl;

namespace Epicoil.Appl.Presentations.StoreInPlan
{
    public partial class ImexConfirmCheckList : BaseSession
    {
        private readonly IStoreInPlanRepo _repo;

        public ImexConfirmCheckList(SessionInfo _session = null, StoreInPlanHead model = null)
        {
            InitializeComponent();
            this._repo = new StoreInPlanRepo();
            epiSession = _session;
        }

        private void ImexConfirmCheckList_Load(object sender, EventArgs e)
        {
            if (epiSession.SessionID == null)
            {
                Login frm = new Login();
                frm.ShowDialog();
            }
            else
            {
                this.Text = epiSession.PlantName;
                dtpInvoiceDateFrom.Value = DateTime.Now.AddDays(-10);
                dtpInvoiceDateTo.Value = DateTime.Now.AddDays(+1);
                return;
            }
            ImexConfirmCheckList_Load(sender, e);
        }

        private void SetDetail(IEnumerable<ImexCheckModel> item)
        {
            //GetDetail
            dgvList.Rows.Clear();
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.StoreInPlanId, p.ImexConfirm, p.IMexConfirmText, p.UserGroup, p.LastUpdateDate, p.StoreInPlanNum
                                , p.InvoiceNum, p.SupplierName, p.InvoiceDate, p.MakerCode, p.MakerName, p.MillCode, p.MillName
                                , p.CurrencyCode, p.BussinessType + " " + p.BussinessTypeName, p.Vessel
                                , p.LoadPort, p.ArivePort, p.ETDDate, p.ETADate);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void tlbClear_Click(object sender, EventArgs e)
        {
            dgvList.Rows.Clear();
        }

        private void tlbInactive_Click(object sender, EventArgs e)
        {
            SetDetail(_repo.GetAllIMEX());
        }

        private void tlbIndetail_Click(object sender, EventArgs e)
        {
            if (dgvList.Rows.Count == 0)
            {
                return;
            }
            int iRow = dgvList.CurrentRow.Index;
            string StoreInPlanNum = dgvList.Rows[iRow].Cells["storeinplanno"].Value.GetString();

            var result = _repo.GetByID(StoreInPlanNum);

            StoreInPlan frm = new StoreInPlan(epiSession, result);
            frm.Show();

        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            ImexCheckModel model = new ImexCheckModel();
            model.StoreInPlanNum = txtStoreInPlanNo.Text.Trim();
            model.InvoiceNum = txtInvoiceNo.Text.Trim();

            var result = _repo.GetAllIMEXByFilter(model, dtpInvoiceDateFrom.Value, dtpInvoiceDateTo.Value);
            SetDetail(result);
        }
    }
}
