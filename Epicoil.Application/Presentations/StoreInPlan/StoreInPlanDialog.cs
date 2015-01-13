using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories.StoreInPlan;
using Epicoil.Library.Models;

namespace Epicoil.Appl.Presentations.StoreInPlan
{
    public partial class StoreInPlanDialog : BaseSession
    {
        private readonly IStoreInPlanRepo _repo;
        public StoreInPlanHeadModel HeadContent;
        private IList<StoreInPlanDialogModel> list;


        public StoreInPlanDialog(SessionInfo _session, IList<StoreInPlanDialogModel> param = null)//, int Status = 2, string TransType = "")
        {
            InitializeComponent();
            this._repo = new StoreInPlanRepo();
            this.HeadContent = new StoreInPlanDialogModel();
            this.list = param;
            epiSession = _session;
            //this.status = Status;
            //this.Type = TransType;
        }

        private void ListToGrid(IList<StoreInPlanDialogModel> item)
        {
            int i = 0;
            //string Status = "";
            //if (status != 2)
            //{
            //    dgvList.Columns[5].HeaderText = "PO Number";
            //}

            foreach (var p in item)
            {
                //if (status != 2)
                //{
                //    Status = p.PONumber;
                //}
                //else { Status = p.IMexConfirmText; }

                dgvList.Rows.Add(p.StoreInPlanId, p.StoreInPlanNum, p.InvoiceNum, p.OrderType, p.SupplierName, p.InvoiceDate, p.IMexConfirmText);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void StoreInPlanDialog_Load(object sender, EventArgs e)
        {
            //IEnumerable<StoreInPlanDialogModel> list = new List<StoreInPlanDialogModel>();
            //if (status == 2)
            //{
            //    list = _repo.GetAll().ToList();
            //}
            //else if (this.Type == "")
            //{
            //    list = _repo.GetAll(status).ToList();
            //}
            //else
            //{
            //    list = _repo.GetAll(status, this.Type).ToList();
            //}

            ListToGrid(list);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HeadContent = _repo.GetByID(dgvList.Rows[e.RowIndex].Cells[1].Value.ToString());
            this.Close();
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            //StoreInPlanDialogModel model = new StoreInPlanDialogModel();
            //model.StoreInPlanNum = txtFilter1.Text.ToString();
            //model.InvoiceNum = txtFilter2.Text.ToString();
            //model.PONumber = txtFilter3.Text.ToString();
            //model.ImportFlag = status;
            //model.TransactionType = this.Type;

            //int FilterType = 0;
            //if (status == 2)
            //{
            //    FilterType = 0;
            //}
            //else if (this.Type == "")
            //{
            //    FilterType = 1;
            //}
            //else
            //{
            //    FilterType = 2;
            //}

            //var list = _repo.GetByFilter(model, FilterType);
            //dgvList.Rows.Clear();
            //ListToGrid(list.ToList());
        }

    }
}