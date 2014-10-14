using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreIn;
using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Common;
using Epicoil.Library.Repositories.StoreIn;
using Epicoil.Library.Repositories.StoreInPlan;

namespace Epicoil.Appl.Presentations.StoreIn
{
    public partial class StoreInBalance : BaseSession
    {
        private readonly IStoreInPlanRepo _repoPln;
        private readonly IStoreInRepo _repo;

        private IEnumerable<StoreInPlanDialogModel> StoreInPlanBln;
        private StoreInPlanDialogModel FilterData;
        public StoreInBalance(SessionInfo _session = null, StoreInPlanDialogModel model = null)
        {
            InitializeComponent();
            this._repoPln = new StoreInPlanRepo();
            this._repo = new StoreInRepo();
            this.StoreInPlanBln = new List<StoreInPlanDialogModel>();
            this.FilterData = model;
        }

        private void StoreInBalance_Load(object sender, EventArgs e)
        {          
            if (FilterData != null)
            {
                StoreInPlanDialogModel filter = new StoreInPlanDialogModel();
                filter.StoreInPlanNum = FilterData.StoreInPlanNum;
                filter.InvoiceNum = FilterData.InvoiceNum;
                filter.InvoiceDateFrom = FilterData.InvoiceDateFrom;
                filter.InvoiceDateTo = FilterData.InvoiceDateTo;

                StoreInPlanBln = _repoPln.GetByFilter(filter, 0);
            }
            SetGrid(StoreInPlanBln);
        }

        private void SetGrid(IEnumerable<StoreInPlanDialogModel> item)
        {
            //GetDetail
            dgvList.Rows.Clear();
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.StoreInPlanId, p.StoreInPlanNum, p.InvoiceNum, p.InvoiceDate
                                , p.SupplierName, p.MakerCode, p.MakerName, p.MillCode, p.MillName
                                , p.CurrencyCode, p.BussinessType.Trim() + " : " + p.BussinessTypeName, p.Vessel
                                , p.LoadPort, p.ArivePort, p.ETDDate, p.ETADate);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void SetDetail(IEnumerable<StoreInHeadBalance> item)
        {
            //GetDetail
            dataGridView2.Rows.Clear();            
            int i = 0;
            foreach (var p in item)
            {
                dataGridView2.Rows.Add(p.StoreInPlanId, p.TransactionID, p.StoreInNum, p.StoreInDate, p.StoreInPlanNum
                                , p.InvoiceNum, p.SupplierName, p.InvoiceDate, p.MakerCode, p.MakerName, p.MillCode, p.MillName
                                , p.CurrencyCode, p.BussinessType + " " + p.BussinessTypeName, p.Vessel
                                , p.LoadPort, p.ArivePort, p.ETDDate, p.StoreInStatus);
                if (i % 2 == 1)
                {
                    this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }

            if(dataGridView2.Rows.Count >= 1)
            {
                IEnumerable<StoreInDetail> result = this._repo.GetDetailArticle(Convert.ToInt32(this.dataGridView2.Rows[0].Cells[0].Value.ToString())
                                                    , Convert.ToInt32(this.dataGridView2.Rows[0].Cells[1].Value.ToString()));
                SetDetailArticle(result);
            }
        }

        private void SetDetailArticle(IEnumerable<StoreInDetail> item)
        {
            //GetDetail
            dataGridView1.Rows.Clear();
            int i = 0;
            foreach (var p in item)
            {
                dataGridView1.Rows.Add(i + 1, p.ArticleNo, p.StockNo, p.PONumber, p.POLine, p.CommodityCode + " : " + p.CommodityName, p.SpecCode + " : " + p.SpecName
                                , p.Thick, p.Width, p.Length, p.Quantity, p.Weight, p.DutyRate, p.BussinessType + " : "+p.BussinessTypeName
                                , p.EndUserName, p.ActlEndUserName, p.StoreInFlag);
                if (i % 2 == 1)
                {
                    this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IEnumerable<StoreInDetail> result = this._repo.GetDetailArticle(Convert.ToInt32(this.dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString())
                                                                                , Convert.ToInt32(this.dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString()));
                SetDetailArticle(result);
            }
        }

        private void dgvList_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IEnumerable<StoreInHeadBalance> result = this._repo.GetStoreInBalanceAll("MfgSys", Convert.ToInt32(this.dgvList.Rows[e.RowIndex].Cells[0].Value.ToString()));
                SetDetail(result);
            }
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            StoreInPlanDialogModel model = new StoreInPlanDialogModel();
            model.StoreInPlanNum = txtStoreInPlanNo.Text.Trim();
            model.InvoiceNum = txtInvoiceNo.Text.Trim();
            model.InvoiceDateFrom = dtpInvoiceDateFrom.Value;
            model.InvoiceDateTo = dtpInvoiceDateTo.Value;

            var result = _repoPln.GetByFilter(model, 0);
            SetGrid(result);
        }
    }
}
