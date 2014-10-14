using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Common;
using Epicoil.Library.Repositories.StoreIn;
using Epicoil.Library.Repositories.StoreInPlan;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Epicoil.Appl.Presentations.StoreInPlan
{
    public partial class StoreInPlan : BaseSession
    {
        private readonly IStoreInPlanRepo _repo;
        private readonly IStoreInRepo _repoIn;
        private readonly ICurrenciesRepo _repoCurr;

        private readonly ISupplierRepo _repoSupplier;
        private readonly IBussinessTypeRepo _repoBT;
        private readonly IMakerRepo _repoMaker;
        private readonly IMillRepo _repoMill;
        private readonly ICommodity _repoCmdt;
        private readonly ISpecRepo _repoSpec;
        private readonly ICoatingRepo _repoCoating;
        private readonly IWharehouseRepo _repoWhse;
        private readonly IImportPortRepo _repoPort;
        private readonly ICustomerRepo _repoCust;
        private readonly ILookupRepo _repoLookup;

        private StoreInPlanHead HeadContent;
        private StoreInPlanHead ModelClone;
        private POLineModel POTrans;
        private bool IMEXReviewFlag;
        private string SourceStr;

        public StoreInPlan(SessionInfo _session = null, StoreInPlanHead model = null)
        {
            InitializeComponent();
            this._repo = new StoreInPlanRepo();
            this._repoIn = new StoreInRepo();
            this._repoCurr = new CurrenciesRepo();
            this._repoSupplier = new SupplierRepo();
            this._repoBT = new BussinessTypeRepo();
            this._repoMaker = new MakerRepo();
            this._repoMill = new MillRepo();
            this._repoCmdt = new Commodity();
            this._repoSpec = new SpecRepo();
            this._repoCoating = new CoatingRepo();
            this._repoWhse = new WharehouseRepo();
            this._repoPort = new ImportPortRepo();
            this._repoCust = new CustomerRepo();
            this._repoLookup = new LookupRepo();
            this.SourceStr = "";

            //Initial Session and content
            this.HeadContent = new StoreInPlanHead();
            epiSession = _session;

            if (model == null)
            {
                this.IMEXReviewFlag = false;
            }
            else
            {
                this.HeadContent = model;
                this.IMEXReviewFlag = true;
            }
            this.ModelClone = new StoreInPlanHead();
            this.POTrans = new POLineModel();
        }

        private void StoreInPlan_Load(object sender, EventArgs e)
        {
            if (epiSession.SessionID == null)
            {
                Login frm = new Login();
                frm.ShowDialog();
            }
            else if (epiSession.SessionID == "x")
            {
                this.Close();
                this.Dispose();
                Environment.Exit(1);
            }
            else
            {
                //this.Text = epiSession.PlantName;

                if (HeadContent.StoreInPlanId == 0 && HeadContent != null)
                {
                    HeadContent.InvoiceDate = DateTime.Now;
                    HeadContent.ETADate = DateTime.Now;
                    HeadContent.ETDDate = DateTime.Now;
                }
                else
                {
                    txtStoreInPlanNum.Text = HeadContent.StoreInPlanNum;
                    SetHeaderContent(HeadContent);
                }
                ModelClone = (StoreInPlanHead)HeadContent.Clone();

                LockHeaderControl();
                return;
            }
            StoreInPlan_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (StoreInPlanDialog frm = new StoreInPlanDialog())
            {
                frm.ShowDialog();
                if (frm.HeadContent.StoreInPlanNum != null)
                {
                    HeadContent = frm.HeadContent;
                    ModelClone = (StoreInPlanHead)HeadContent.Clone();
                }
            }
            txtStoreInPlanNum.Text = HeadContent.StoreInPlanNum;
            SetHeaderContent(HeadContent);
        }

        private void txtStoreInPlanNum_Leave(object sender, EventArgs e)
        {
            string code = txtStoreInPlanNum.Text.Trim();
            if (code != ModelClone.StoreInPlanNum && txtStoreInPlanNum.Text != "")
            {
                var result = _repo.GetByID(code);

                if (result == null)
                {
                    button1_Click(sender, e);
                }
                else
                {
                    HeadContent = result;
                    ModelClone = (StoreInPlanHead)HeadContent.Clone();
                }
                //SetHeaderContent(HeadContent);
            }
        }

        private void ClearHeaderContent()
        {
            txtStoreInPlanNum.DataBindings.Clear();
            txtInvoiceNum.DataBindings.Clear();
            txtSupplierCode.DataBindings.Clear();
            txtSupplierName.DataBindings.Clear();
            txtCustID.DataBindings.Clear();
            txtCustomerName.DataBindings.Clear();
            txtMakerCode.DataBindings.Clear();
            txtMakerName.DataBindings.Clear();
            txtMillCode.DataBindings.Clear();
            txtMillName.DataBindings.Clear();
            txtBussinessType.DataBindings.Clear();
            txtBussinessTypeName.DataBindings.Clear();
            txtVessel.DataBindings.Clear();
            txtExchangeRate.DataBindings.Clear();
            cmdCurrencyCode.DataBindings.Clear();
            cmbArivePort.DataBindings.Clear();
            cmbLoadPort.DataBindings.Clear();

            txtPoLine.DataBindings.Clear();
            txtNumberOfArticle.DataBindings.Clear();
            txtReceiptWeight.DataBindings.Clear();
            txtIMEXRemark.DataBindings.Clear();

            txtIMEXRemark.Clear();
            txtStoreInPlanNum.Clear();
            txtInvoiceNum.Clear();
            txtSupplierCode.Clear();
            txtSupplierName.Clear();
            txtCustID.Clear();
            txtCustomerName.Clear();
            txtMakerCode.Clear();
            txtMakerName.Clear();
            txtMillCode.Clear();
            txtMillName.Clear();
            txtBussinessType.Clear();
            txtBussinessTypeName.Clear();
            txtVessel.Clear();
            txtExchangeRate.Clear();

            //dgvList.Rows.Clear();
            //dataGridView1.Rows.Clear();
            //dataGridView2.Rows.Clear();

            errorProvider1.Clear();
            panel4.Visible = false;
            lblValidation.Text = "";
        }

        private void LockHeaderControl()
        {
            txtInvoiceNum.ReadOnly = true;
            butSupplier.Enabled = false;
            txtSupplierCode.ReadOnly = true;
            butCustomer.Enabled = false;
            txtCustID.ReadOnly = true;
            butMaker.Enabled = false;
            txtMakerCode.ReadOnly = true;
            butMill.Enabled = false;
            txtMillCode.ReadOnly = true;
            cmbLoadPort.Enabled = false;
            cmbArivePort.Enabled = false;
            dtpETADate.Enabled = false;
            dtpETDDate.Enabled = false;
            dtpInvoiceDate.Enabled = false;
            cmdCurrencyCode.Enabled = false;
            butBusinessType.Enabled = false;
            txtBussinessType.ReadOnly = true;
            txtVessel.ReadOnly = true;
            cmdCurrencyCode.Enabled = false;

            //dataGridView2.ReadOnly = true;
        }

        private void UnLockHeaderControl()
        {
            txtInvoiceNum.ReadOnly = false;
            butSupplier.Enabled = true;
            txtSupplierCode.ReadOnly = false;
            butCustomer.Enabled = true;
            txtCustID.ReadOnly = false;
            butMaker.Enabled = true;
            txtMakerCode.ReadOnly = false;
            butMill.Enabled = true;
            txtMillCode.ReadOnly = false;
            dtpInvoiceDate.Enabled = true;
            cmdCurrencyCode.Enabled = true;
            butBusinessType.Enabled = true;
            txtBussinessType.ReadOnly = false;
            cmdCurrencyCode.Enabled = true;
            dtpETADate.DataBindings.Clear();
            dtpETDDate.DataBindings.Clear();
            dtpInvoiceDate.DataBindings.Clear();

            if (HeadContent.ImportFlag == 0)
            {
                txtVessel.ReadOnly = false;
                cmbLoadPort.Enabled = true;
                cmbArivePort.Enabled = true;
                dtpETADate.Enabled = true;
                dtpETDDate.Enabled = true;
            }
            else if (HeadContent.ImportFlag == 1)
            {
                txtVessel.ReadOnly = true;
                cmbLoadPort.Enabled = false;
                cmbArivePort.Enabled = false;
                dtpETADate.Enabled = false;
                dtpETDDate.Enabled = false;
            }
            else
            {
                txtVessel.ReadOnly = false;
                cmbLoadPort.Enabled = true;
                cmbArivePort.Enabled = true;
                dtpETADate.Enabled = true;
                dtpETDDate.Enabled = true;
            }
        }

        private void SetHeaderContent(StoreInPlanHead model)
        {
            ClearHeaderContent();
            txtStoreInPlanNum.DataBindings.Add("Text", model, "StoreInPlanNum", false, DataSourceUpdateMode.OnPropertyChanged);
            txtInvoiceNum.DataBindings.Add("Text", model, "InvoiceNum", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSupplierCode.DataBindings.Add("Text", model, "SupplierCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSupplierName.DataBindings.Add("Text", model, "SupplierName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtCustID.DataBindings.Add("Text", model, "CustID", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCustomerName.DataBindings.Add("Text", model, "CustomerName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtMakerCode.DataBindings.Add("Text", model, "MakerCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMakerName.DataBindings.Add("Text", model, "MakerName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMillCode.DataBindings.Add("Text", model, "MillCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMillName.DataBindings.Add("Text", model, "MillName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBussinessType.DataBindings.Add("Text", model, "BussinessType", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBussinessTypeName.DataBindings.Add("Text", model, "BussinessTypeName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtVessel.DataBindings.Add("Text", model, "Vessel", false, DataSourceUpdateMode.OnPropertyChanged);
            txtExchangeRate.DataBindings.Add("Text", model, "ExchangeRate", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,##0.000000");

            txtIMEXRemark.DataBindings.Add("Text", model, "ImexRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            dtpETADate.Value = model.ETADate;
            dtpETDDate.Value = model.ETDDate;
            dtpInvoiceDate.Value = model.InvoiceDate;

            cmdCurrencyCode.DataSource = model.CurrenciesList.ToList();
            cmdCurrencyCode.DisplayMember = "CurrencyCode";
            cmdCurrencyCode.DataBindings.Add("Text", model, "CurrencyCode");

            cmbLoadPort.DataSource = model.PortList.ToList();
            cmbLoadPort.DisplayMember = "PortCode";
            cmbLoadPort.DataBindings.Add("Text", model, "LoadPort", false, DataSourceUpdateMode.OnPropertyChanged);

            cmbArivePort.DataSource = model.PortList.ToList();
            cmbArivePort.DisplayMember = "PortCode";
            cmbArivePort.DataBindings.Add("Text", model, "ArivePort", false, DataSourceUpdateMode.OnPropertyChanged);

            actionToolStripMenuItem.Visible = false;

            if (model.ImportFlag == 0)
            {
                tabControl1.SelectedIndex = 0;
                tabControl1.TabPages[0].Text = "Import";
                dataGridView2.ReadOnly = true;
            }
            else if (model.ImportFlag == 1)
            {
                tabControl1.SelectedIndex = 0;
                tabControl1.TabPages[0].Text = "Domestic";
                actionToolStripMenuItem.Visible = true;
                dataGridView2.ReadOnly = true;
            }
            else
            {
                actionToolStripMenuItem.Visible = true;
                tabControl1.SelectedIndex = 1;
                dataGridView2.ReadOnly = false;
            }

            dgvList.Rows.Clear();
            dataGridView1.Rows.Clear();

            if (model.InsertState)
            {
                UnLockHeaderControl();
            }
            else
            {
                if (HeadContent.StoreInFlag == "1" || HeadContent.ImexConfirm == "1")
                {
                    LockHeaderControl();
                    if (HeadContent.StoreInFlag == "1")
                    {
                        butArticleDetail.Enabled = false;
                        toolStripButton1.Visible = false;
                    }
                }
                else// if (IMEXReviewFlag == true)
                {
                    UnLockHeaderControl();
                    butArticleDetail.Enabled = true;
                    toolStripButton1.Visible = true;
                    butSupplier.Enabled = false;
                    txtSupplierCode.ReadOnly = true;
                    butMaker.Enabled = false;
                    txtMakerCode.ReadOnly = true;
                    butMill.Enabled = false;
                    txtMillCode.ReadOnly = true;
                    butCustomer.Enabled = false;
                    txtCustID.ReadOnly = true;
                    cmdCurrencyCode.Enabled = false;

                    if (model.ImportFlag == 0 && HeadContent.ImexConfirm == "2")
                    {
                        panel4.Visible = true;
                        txtIMEXRemark.ReadOnly = true;
                    }
                }

                if (model.ImexConfirm == "1")
                {
                    chkConfirm.Checked = true;
                }
                else
                {
                    chkConfirm.Checked = false;
                }
                IEnumerable<StoreInPlanDetail> result = new List<StoreInPlanDetail>();

                if (model.ImportFlag == 2)
                {
                    //if (!string.IsNullOrEmpty(HeadContent.StoreInPlanNum))
                    //{
                    result = _repo.GetDetailArticleITAKU(HeadContent.StoreInPlanId);
                    SetItakuDetail(result);
                    //}
                }
                else
                {
                    result = _repo.GetDetail(HeadContent.StoreInPlanId);
                    SetDetail(result);
                    if (dgvList.Rows.Count >= 1)
                    {
                        IEnumerable<StoreInPlanDetail> resultArtcle = this._repo.GetDetailArticle(this.HeadContent.StoreInPlanId, Convert.ToInt32(this.dgvList.Rows[0].Cells[2].Value.ToString()));
                        SetDetailArticle(resultArtcle);
                    }
                }
            }

            if (!HeadContent.InsertState)
            {
                butChooseFile.Enabled = false;
            }
            else
            {
                butChooseFile.Enabled = true;
            }

            if (IMEXReviewFlag == true)
            {
                toolStripDropDownButton1.Enabled = false;
                mnuNew.Enabled = false;
                mnuEditClear.Enabled = false;
                actionToolStripMenuItem.Enabled = false;
                toolStripButton1.Enabled = false;
                tlbClear.Enabled = false;
                panel4.Visible = true;
                txtIMEXRemark.ReadOnly = false;
                butArticleDetail.Enabled = false;
            }

            txtNumberOfArticle.DataBindings.Add("Text", model, "NumberOfRcv", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0");
        }

        private void SetDetail(IEnumerable<StoreInPlanDetail> item)
        {
            //GetDetail
            dgvList.Rows.Clear();
            int i = 0;
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.PONumber, p.SaleContract, p.POLine, p.CommodityCode, p.SpecCode, p.Quantity, p.Weight, p.DutyRate, p.BussinessTypeName, p.UnitPrice, p.Amount);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void SetDetailArticle(IEnumerable<StoreInPlanDetail> item)
        {
            //GetDetail
            int i = 0;
            dataGridView1.Rows.Clear();
            foreach (var p in item)
            {
                dataGridView1.Rows.Add(p.LineID, p.SeqId, p.TaxPaid, p.ArticleNo, p.Quantity, p.Weight, p.Place, p.Note, p.EndUserName, p.ActlEndUserName, false, p.StoreInFlag);
                if (p.StoreInFlag == 1)
                {
                    dataGridView1.Rows[i].ReadOnly = true;
                }

                if (i % 2 == 1)
                {
                    this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
            if (HeadContent.StoreInFlag == "1" || IMEXReviewFlag == true)
            {
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView2.ReadOnly = true;
            }
        }

        private void ClearPOLineTrans()
        {
            POTrans = null;
            txtPoNumber.Clear();
            txtSaleContract.Clear();
            txtNumberOfArticle.Clear();
            txtPoLine.Clear();
            txtReceiptWeight.Clear();
        }

        private void SetPOLineTrans(POLineModel model)
        {
            txtPoLine.DataBindings.Clear();
            txtReceiptWeight.DataBindings.Clear();

            txtPoLine.DataBindings.Add("Text", model, "POLine", true, DataSourceUpdateMode.Never, 0, "#,##0");
            txtReceiptWeight.DataBindings.Add("Text", model, "BalanceWeight", true, DataSourceUpdateMode.Never, 0, "#,##0.###");
        }

        private void tlbClear_Click(object sender, EventArgs e)
        {
            ClearHeaderContent();
            ClearPOLineTrans();

            dataGridView2.Rows.Clear();
            dataGridView2.ReadOnly = true;
            txtFilePath.Clear();
            cmdSheet.DataSource = null;

            this.HeadContent = new StoreInPlanHead();
            this.ModelClone = new StoreInPlanHead();

            HeadContent.StoreInFlag = null;
            HeadContent.InvoiceDate = DateTime.Now;
            HeadContent.ETADate = DateTime.Now;
            HeadContent.ETDDate = DateTime.Now;

            ModelClone = (StoreInPlanHead)HeadContent.Clone();
            LockHeaderControl();
        }

        private void cmdCurrencyCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var result = _repoCurr.GetByID(cmdCurrencyCode.Text.Trim());
            HeadContent.ExchangeRate = result.ExchangeRate;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetNew(0);
        }

        private void importToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            importToolStripMenuItem_Click(sender, e);
        }

        private void domesticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetNew(1);
        }

        private void GetNew(int ImportFlag)
        {
            //tabControl1.TabPages.Remove(tabPage1);
            //tabControl1.TabPages.Remove(tabPage2);
            HeadContent = new StoreInPlanHead();
            HeadContent.InsertState = true;
            HeadContent.StoreInPlanId = 0;
            HeadContent.StoreInPlanNum = "";
            HeadContent.InvoiceDate = DateTime.Now;
            HeadContent.ETADate = DateTime.Now;
            HeadContent.ETDDate = DateTime.Now;
            HeadContent.StoreInFlag = "0";
            HeadContent.CurrenciesList = _repoCurr.GetAll();
            HeadContent.PortList = this._repoPort.GetAll();
            HeadContent.ImportFlag = ImportFlag;
            ModelClone = (StoreInPlanHead)HeadContent.Clone();
            SetHeaderContent(HeadContent);
        }

        private void domesticToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            domesticToolStripMenuItem_Click(sender, e);
        }

        private void butSupplier_Click(object sender, EventArgs e)
        {
            using (SupplierDialog frm = new SupplierDialog())
            {
                frm.ShowDialog();

                HeadContent.SupplierCode = frm.VendorId;
                HeadContent.SupplierName = frm.VendorName;
            }
            SetHeaderContent(HeadContent);
        }

        private void butMaker_Click(object sender, EventArgs e)
        {
            using (MakerDialog frm = new MakerDialog())
            {
                frm.ShowDialog();
                string code = frm.MakerCode;
                if (code != ModelClone.MakerCode)
                {
                    HeadContent.MakerCode = code;
                    HeadContent.MakerName = frm.MakerName;
                    ModelClone.MakerCode = code;
                    ModelClone.MakerName = frm.MakerName;
                    HeadContent.MillCode = "";
                    HeadContent.MillName = "";
                }
            }
            SetHeaderContent(HeadContent);
        }

        private void butMill_Click(object sender, EventArgs e)
        {
            using (MillDialog frm = new MillDialog(txtMakerCode.Text.ToString()))
            {
                frm.ShowDialog();

                HeadContent.MillCode = frm.MillCode;
                HeadContent.MillName = frm.MillName;
            }
            SetHeaderContent(HeadContent);
        }

        private void txtSupplierCode_Leave(object sender, EventArgs e)
        {
            string code = txtSupplierCode.Text.Trim();
            if (code != ModelClone.SupplierCode && txtSupplierCode.Text != "" && HeadContent.InsertState)
            {
                var result = _repoSupplier.GetByID(code);

                if (result == null)
                {
                    butSupplier_Click(sender, e);
                }
                else
                {
                    HeadContent.SupplierCode = result.VendorID;
                    HeadContent.SupplierName = result.VendorName;
                }
            }
        }

        private void txtMakerCode_Leave(object sender, EventArgs e)
        {
            string code = txtMakerCode.Text.Trim();
            if (code != ModelClone.MakerCode && txtMakerCode.Text != "" && HeadContent.InsertState)
            {
                var result = _repoMaker.GetByID(code);

                if (result == null)
                {
                    butMaker_Click(sender, e);
                }
                else
                {
                    HeadContent.MakerCode = result.MakerCode;
                    HeadContent.MakerName = result.MakerName;
                }
                //SetHeaderContent(HeadContent);
            }
        }

        private void txtMillCode_Leave(object sender, EventArgs e)
        {
            string code = txtMillCode.Text.Trim();
            if (code != ModelClone.MillCode && txtMillCode.Text != "" && HeadContent.InsertState)
            {
                var result = _repoMill.GetByID(code, HeadContent.MakerCode);

                if (result == null)
                {
                    butMill_Click(sender, e);
                }
                else
                {
                    HeadContent.MillCode = result.MillCode;
                    HeadContent.MillName = result.MillName;
                }
                //SetHeaderContent(HeadContent);
            }
        }

        private void butBusinessType_Click(object sender, EventArgs e)
        {
            using (BussinessTypeDialog frm = new BussinessTypeDialog())
            {
                frm.ShowDialog();

                HeadContent.BussinessType = frm.Code;
                HeadContent.BussinessTypeName = frm.Description;
            }
            SetHeaderContent(HeadContent);
        }

        private void txtBussinessType_Leave(object sender, EventArgs e)
        {
            string code = txtBussinessType.Text.Trim();
            string msg = "";
            if (code != ModelClone.BussinessType && txtBussinessType.Text != "" && HeadContent.InsertState)
            {
                var result = _repoBT.GetByID(code);

                if (result == null)
                {
                    butBusinessType_Click(sender, e);
                }
                else
                {
                    HeadContent.BussinessType = result.BussinessCode;
                    HeadContent.BussinessTypeName = result.BussinessName;
                }
                //SetHeaderContent(HeadContent);
            }
        }

        private void butArticleDetail_Click(object sender, EventArgs e)
        {
            bool ErrorFlag = false;
            if (HeadContent.InsertState)
            {
                MessageBox.Show("Please save store in plan no : " + this.txtStoreInPlanNum.Text.ToString() + " before to adding article line.", "Please save.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                ErrorFlag = true;
            }
            else
            {
                if (string.IsNullOrEmpty(txtPoNumber.Text))
                {
                    errorProvider1.SetError(txtPoNumber, "Please fill the required field.");
                    ErrorFlag = true;
                }

                if (string.IsNullOrEmpty(txtPoLine.Text))
                {
                    errorProvider1.SetError(txtPoLine, "Please fill the required field.");
                    ErrorFlag = true;
                }

                if (Convert.ToInt32(string.IsNullOrEmpty(txtNumberOfArticle.Text.Trim()) ? "0" : txtPoLine.Text.Trim()) <= 0)
                {
                    errorProvider1.SetError(txtNumberOfArticle, "Please fill the required field.");
                    ErrorFlag = true;
                }

                if (Convert.ToDecimal(string.IsNullOrEmpty(txtReceiptWeight.Text.Trim()) ? "0" : txtReceiptWeight.Text.Trim()) <= 0)
                {
                    errorProvider1.SetError(txtReceiptWeight, "Please fill the required field.");
                    ErrorFlag = true;
                }
            }

            if (!ErrorFlag)
            {
                StoreInPlanDetail paramModel = new StoreInPlanDetail();
                //decimal ReceiptWeight = Convert.ToDecimal(string.IsNullOrEmpty(txtReceiptWeight.Text.Trim()) ? "0" : txtReceiptWeight.Text.Trim());
                paramModel = _repo.GetPoLineDetail(txtPoNumber.Text.Trim(), Convert.ToInt32(txtPoLine.Text.Trim()));

                if (paramModel == null)
                {
                    MessageBox.Show("This PO line dose not exist.", "Invalid value.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                paramModel.PONumber = txtPoNumber.Text.Trim();
                paramModel.StoreInPlanId = HeadContent.StoreInPlanId;
                paramModel.PONum = POTrans.PONum;
                paramModel.PackingNumber = HeadContent.InvoiceNum;
                paramModel.POLine = Convert.ToInt32(txtPoLine.Text.Trim());
                paramModel.WeightBalnce = _repo.GetReceivedWeight(paramModel.PONum, paramModel.POLine);
                paramModel.POAllowance = _repo.GetMCSSAllowance(paramModel.PartNum);
                int NumberOfArt = Convert.ToInt32(string.IsNullOrEmpty(txtNumberOfArticle.Text.Trim()) ? "0" : txtNumberOfArticle.Text.Trim());
                decimal decAllowance = (POTrans.BalanceWeight * paramModel.POAllowance) / 100;
                if (Convert.ToDecimal(txtReceiptWeight.Text) <= (POTrans.BalanceWeight + decAllowance))
                {
                    ReceiveArticleDialog frm = new ReceiveArticleDialog(paramModel, epiSession, NumberOfArt, Convert.ToDecimal(txtReceiptWeight.Text));
                    frm.ShowDialog();
                    SetHeaderContent(HeadContent);
                    ClearPOLineTrans();
                }
                else
                {
                    MessageBox.Show("Receipt weight must be less than or equal Remaining weight.", "Invalid value.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void txtPoNumber_Leave(object sender, EventArgs e)
        {
            string code = txtPoNumber.Text.Trim();
            int PONum = 0;
            string result = string.Empty;

            if (HeadContent.StoreInFlag == "0" && !string.IsNullOrEmpty(code))
            {
                if (string.IsNullOrEmpty(HeadContent.SupplierCode) && string.IsNullOrEmpty(HeadContent.MakerCode))
                {
                    GetHeader header = _repo.GetHeaderByPONum(txtPoNumber.Text.Trim());
                    if (header == null)
                    {
                        MessageBox.Show("PO Number : " + this.txtPoNumber.Text.ToString() + " doesn't exists.", "Please try agian.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                    HeadContent.SupplierCode = header.SupplierCode;
                    HeadContent.SupplierName = header.SupplierName;
                    HeadContent.MakerCode = header.MakerCode;
                    HeadContent.MakerName = header.MakerName;
                    HeadContent.MillCode = header.MillCode;
                    HeadContent.MillName = header.MillName;
                    HeadContent.CustID = header.CustID;
                    HeadContent.CustomerName = header.CustomerName;
                    HeadContent.ETADate = DateTime.Now;
                    HeadContent.ETDDate = DateTime.Now;
                    HeadContent.InvoiceDate = DateTime.Now;
                    HeadContent.CurrencyCode = header.CurrencyCode;
                    HeadContent.ExchangeRate = header.ExchangeRate;
                    SetHeaderContent(HeadContent);
                }

                bool getPO = _repo.GetPOByPONumber(epiSession.PlantID, code, HeadContent, out result, out PONum);
                if (!getPO)
                {
                    MessageBox.Show("PO Number : " + this.txtPoNumber.Text.ToString() + " doesn't exists.", "Please try agian.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.txtPoNumber.Focus();
                    this.txtPoNumber.SelectAll();
                }
                else
                {
                    txtSaleContract.Text = result;
                    using (POLineDialog frm = new POLineDialog(PONum))
                    {
                        frm.ShowDialog();
                        POTrans = frm.paramLine;
                        SetPOLineTrans(POTrans);
                    }
                    //txtNumberOfArticle.Focus();
                    //txtNumberOfArticle.SelectAll();
                }
            }
        }

        private void txtSaleContract_Leave(object sender, EventArgs e)
        {
            string code = txtSaleContract.Text.Trim();
            string result = "";
            if (code != "")
            {
                if (!_repo.GetPOBySaleContract(epiSession.PlantID, code, HeadContent, out result))
                {
                    MessageBox.Show("Sale contract  : " + this.txtSaleContract.Text.ToString() + " doesn't exists.", "Please try agian.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.txtSaleContract.Focus();
                    this.txtSaleContract.SelectAll();
                }
                else
                {
                    txtPoNumber.Text = result;
                }
            }
        }

        private void txtPoLine_Leave(object sender, EventArgs e)
        {
            int code = Convert.ToInt32(string.IsNullOrEmpty(txtPoLine.Text.Trim()) ? "0" : txtPoLine.Text.Trim());
            string result = "";
            if (code != 0)
            {
                if (!_repo.GetPOLine(txtPoNumber.Text.Trim(), txtSaleContract.Text.Trim(), HeadContent, code, out result))
                {
                    MessageBox.Show("PO Line no : " + this.txtPoLine.Text.ToString() + " doesn't exists.", "Please try agian.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.txtPoLine.Focus();
                    this.txtPoLine.SelectAll();
                }
            }
        }

        private bool ValidateHeaderError()
        {
            bool errorFlag = false;
            errorProvider1.Clear();
            if (txtInvoiceNum.Text == "")
            {
                errorProvider1.SetError(txtInvoiceNum, "Please fill the required field.");
                errorFlag = true;
            }
            else if (_repo.CheckInvoiceExisting(txtInvoiceNum.Text.Trim()) && HeadContent.InsertState == true)
            {
                errorProvider1.SetError(txtInvoiceNum, "This invoice number is duplicate.");
                errorFlag = true;
            }

            if (txtSupplierCode.Text == "")
            {
                errorProvider1.SetError(txtSupplierCode, "Please fill the required field.");
                errorFlag = true;
            }

            if (txtMakerCode.Text == "")
            {
                errorProvider1.SetError(txtMakerCode, "Please fill the required field.");
                errorFlag = true;
            }

            if (txtMillCode.Text == "")
            {
                errorProvider1.SetError(txtMillCode, "Please fill the required field.");
                errorFlag = true;
            }

            if (HeadContent.ImportFlag == 0)
            {
                if (txtVessel.Text == "")
                {
                    errorProvider1.SetError(txtVessel, "Please fill the required field.");
                    errorFlag = true;
                }

                if (cmbArivePort.Text == "")
                {
                    errorProvider1.SetError(cmbArivePort, "Please fill the required field.");
                    errorFlag = true;
                }
            }

            return errorFlag;
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            bool error = false;
            //ถ้ายังไม่ทำรายการ Store In
            if (HeadContent.StoreInFlag == "0")
            {
                //ITAKU
                if (HeadContent.ImportFlag == 2)
                {
                    if (!ValidateGridCell(ref dataGridView2))
                    {
                        error = false;
                    }
                    else
                    {
                        error = true;
                    }
                }
                //Import & Domestic
                else
                {
                    if (!ValidateHeaderError())
                    {
                        if (HeadContent.ImexConfirm == null)
                        {
                            HeadContent.ImexConfirm = "0";
                        }
                        //ถ้าเป็นการ Review ของ IMEX
                        if (IMEXReviewFlag)
                        {
                            if (HeadContent.ImexConfirm == "0" || HeadContent.ImexConfirm == "3")
                            {
                                if (chkConfirm.Checked)
                                {
                                    if (MessageBox.Show("Are you sure to confirm.", "Question?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        HeadContent.ImexConfirm = "1";
                                    }
                                }
                                else
                                {
                                    if (MessageBox.Show("Are you sure to reject.", "Question?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        HeadContent.ImexConfirm = "2";
                                        HeadContent.ImexRemark = txtIMEXRemark.Text;
                                    }
                                }
                            }
                            HeadContent.UserGroup = "IMEX";
                        }
                        //ไม่ใช่การ Review ของ IMEX หมายถึงผู้ทำรายการเป็นแผนกอื่นที่ไม่ใช่ IMEX
                        else
                        {
                            //ถ้าสถานะ IMEX = Reject
                            if (HeadContent.ImexConfirm == "2")
                            {
                                if (MessageBox.Show("Are you sure to reply to IMEX.", "Question?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //Set สถานะเป็น Sale reply to IMEX
                                    HeadContent.ImexConfirm = "3";
                                    HeadContent.ImexRemark = txtIMEXRemark.Text + Environment.NewLine + "Sale Reply on Date: " + DateTime.Now.ToLongDateString() + " Time: " + DateTime.Now.ToLongTimeString();
                                }
                            }
                            HeadContent.UserGroup = "Sale";
                        }
                    }
                    else
                    {
                        error = true;
                    }
                }
                if (!error)
                {
                    HeadContent.CurrencyCode = cmdCurrencyCode.Text.Trim();
                    HeadContent = _repo.SaveHead(HeadContent, epiSession);
                    UpdateArticleLine();
                    SetHeaderContent(HeadContent);
                }
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IEnumerable<StoreInPlanDetail> result = this._repo.GetDetailArticle(this.HeadContent.StoreInPlanId, Convert.ToInt32(this.dgvList.Rows[e.RowIndex].Cells[2].Value.ToString()));
                SetDetailArticle(result);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (HeadContent.ImportFlag == 2)
            {
                int jRow = dataGridView2.CurrentRow.Index;
                if (!Convert.ToBoolean(dataGridView2.Rows[jRow].Cells["storein"].Value))
                {
                    _repo.DeleteLine(Convert.ToInt32(this.dataGridView2.Rows[jRow].Cells[0].Value.ToString().Trim()));

                    var result = _repo.GetDetailArticleITAKU(HeadContent.StoreInPlanId);
                    SetItakuDetail(result);
                }
            }
            else
            {
                if (dataGridView1.Rows.Count >= 1)
                {
                    int iRow = dataGridView1.CurrentRow.Index;
                    int jRow = dgvList.CurrentRow.Index;
                    if (!Convert.ToBoolean(dataGridView1.Rows[iRow].Cells["StoreInFlag"].Value))
                    {
                        _repo.DeleteLine(Convert.ToInt32(this.dataGridView1.Rows[iRow].Cells[0].Value.ToString().Trim()));

                        IEnumerable<StoreInPlanDetail> result = this._repo.GetDetailArticle(this.HeadContent.StoreInPlanId, Convert.ToInt32(this.dgvList.Rows[jRow].Cells[2].Value.ToString()));
                        SetDetailArticle(result);
                    }
                }
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            tlbSave_Click(sender, e);
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (HeadContent.StoreInFlag == "0")
            {
                dataGridView1.Rows[e.RowIndex].Cells["edit"].Value = true;
            }
        }

        private void UpdateArticleLine()
        {
            if (HeadContent.ImportFlag == 2)
            {
                for (int i = 0; (this.dataGridView2.Rows.Count) > i; i++)
                {
                    if (!string.IsNullOrEmpty(dataGridView2.Rows[i].Cells["id1"].Value.GetString()))
                    {
                        StoreInPlanDetail param = new StoreInPlanDetail();
                        param.SeqId = Convert.ToInt32(dataGridView2.Rows[i].Cells["lineid"].Value);
                        param.StoreInPlanId = HeadContent.StoreInPlanId;
                        param.LineID = Convert.ToInt32(dataGridView2.Rows[i].Cells["id1"].Value);
                        param.ArticleNo = dataGridView2.Rows[i].Cells["article"].Value.GetString();
                        param.CommodityCode = dataGridView2.Rows[i].Cells["commodity1"].Value.GetString();
                        param.SpecCode = dataGridView2.Rows[i].Cells["spec1"].Value.GetString();
                        param.CoatingCode = dataGridView2.Rows[i].Cells["Coating"].Value.GetString();
                        param.Thick = Convert.ToDecimal(dataGridView2.Rows[i].Cells["thick"].Value);
                        param.Width = Convert.ToDecimal(dataGridView2.Rows[i].Cells["width"].Value);
                        param.Length = Convert.ToDecimal(dataGridView2.Rows[i].Cells["length"].Value);
                        param.Quantity = Convert.ToDecimal(dataGridView2.Rows[i].Cells["quantity2"].Value);
                        param.Weight = Convert.ToDecimal(dataGridView2.Rows[i].Cells["weight2"].Value);
                        param.Place = dataGridView2.Rows[i].Cells["place1"].Value.GetString();
                        param.PackingNumber = dataGridView2.Rows[i].Cells["packingno1"].Value.GetString();
                        param.Category = dataGridView2.Rows[i].Cells["category"].Value.GetString();
                        param.SaleContract = dataGridView2.Rows[i].Cells["mksale"].Value.GetString();
                        param.Note = dataGridView2.Rows[i].Cells["note1"].Value.GetString();

                        _repo.SaveArticle(param, epiSession);
                    }
                }
            }
            else
            {
                for (int i = 0; (this.dataGridView1.Rows.Count) > i; i++)
                {
                    if (dataGridView1.Columns[10].Name == "edit" && (bool)dataGridView1[10, i].EditedFormattedValue)
                    {
                        StoreInPlanDetail param = new StoreInPlanDetail();
                        param.SeqId = Convert.ToInt32(dataGridView1.Rows[i].Cells["SeqId"].Value);
                        param.StoreInPlanId = HeadContent.StoreInPlanId;
                        param.LineID = Convert.ToInt32(dataGridView1.Rows[i].Cells["id"].Value);
                        param.ArticleNo = dataGridView1.Rows[i].Cells["articleno"].Value.GetString();
                        param.Quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["quantity1"].Value);
                        param.Weight = Convert.ToDecimal(dataGridView1.Rows[i].Cells["weight1"].Value);
                        param.Place = dataGridView1.Rows[i].Cells["place"].Value.GetString();

                        DataGridViewRow row = dataGridView1.Rows
                                                .Cast<DataGridViewRow>()
                                                .Where(r => r.Cells["articleno"].Value.ToString().Equals(dataGridView1.Rows[i].Cells["articleno"].Value.ToString())).First();

                        int rowIndex = row.Index;
                        if (rowIndex != i)
                        {
                            dataGridView1.CurrentCell = dataGridView1[3, i];
                            MessageBox.Show("Article No is duplicate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            _repo.SaveArticle(param, epiSession);
                        }                        
                    }
                }
            }
        }

        private void goToStoreInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HeadContent.StoreInPlanId == 0) return;
            var result = _repoIn.GetStoreInPlanByID(HeadContent.StoreInPlanId);
            Epicoil.Appl.Presentations.StoreIn.StoreIn frm = new Epicoil.Appl.Presentations.StoreIn.StoreIn(epiSession, result);

            frm.Show();
        }

        private void itakuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.ReadOnly = true;
            txtFilePath.Clear();
            cmdSheet.DataSource = null;
            GetNew(2);
        }

        private void CopyData(ref DataGridView dgv)
        {
            if (dgv.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    // Add the selection to the clipboard.
                    Clipboard.SetDataObject(dgv.GetClipboardContent());
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show("The Clipboard could not be accessed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CutData(ref DataGridView dgv)
        {
            //Copy to clipboard
            CopyData(ref dgv);

            //Clear selected cells
            foreach (DataGridViewCell dgvCell in dgv.SelectedCells)
                dgvCell.Value = string.Empty;
        }

        private void PasteData(ref DataGridView dgv)
        {
            char[] rowSplitter = { '\n', '\r' };  // Cr and Lf.
            char columnSplitter = '\t';         // Tab.

            IDataObject dataInClipboard = Clipboard.GetDataObject();
            try
            {
                string stringInClipboard =
                    dataInClipboard.GetData(DataFormats.Text).ToString();

                string[] rowsInClipboard = stringInClipboard.Split(rowSplitter,
                    StringSplitOptions.RemoveEmptyEntries);

                int r = dgv.SelectedCells[0].RowIndex;
                int c = dgv.SelectedCells[0].ColumnIndex;

                if (dgv.Rows.Count < (r + rowsInClipboard.Length))
                    dgv.Rows.Add(r + rowsInClipboard.Length - dgv.Rows.Count);

                // Loop through lines:
                int iRow = 0;
                while (iRow < rowsInClipboard.Length)
                {
                    // Split up rows to get individual cells:
                    string[] valuesInRow =
                        rowsInClipboard[iRow].Split(columnSplitter);

                    // Cycle through cells.
                    // Assign cell value only if within columns of grid:
                    int jCol = 0;
                    while (jCol < valuesInRow.Length)
                    {
                        if ((dgv.ColumnCount - 1) >= (c + jCol))
                            dgv.Rows[r + iRow].Cells[c + jCol].Value =
                            valuesInRow[jCol];

                        jCol += 1;
                    }

                    iRow += 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedCells.Count > 0 && dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].ColumnIndex != 1)
                {
                    mnuContext.Items[0].Visible = true;
                    mnuContext.Items[1].Visible = true;
                    mnuContext.Items[2].Visible = true;
                    mnuContext.Items[3].Visible = true;
                    mnuContext.Items[4].Visible = true;
                    mnuContext.Items[5].Visible = true;
                    mnuContext.Items[6].Visible = true;
                    mnuContext.Items[7].Visible = false;
                    mnuContext.Items[8].Visible = false;
                    mnuContext.Items[9].Visible = false;
                    mnuContext.Items[10].Visible = false;
                    //mnuContext.Items[11].Visible = false;
                    dataGridView2.ContextMenuStrip = mnuContext;
                    if (dataGridView2.Columns[e.ColumnIndex].Name == "spec1"/* || dataGridView2.Columns[e.ColumnIndex].Name == "specname"*/)
                    {
                        mnuContext.Items[7].Visible = true;
                        this.SourceStr = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    }
                    else
                    {
                        mnuContext.Items[7].Visible = false;
                    }

                    if (dataGridView2.Columns[e.ColumnIndex].Name == "Coating"/* || dataGridView2.Columns[e.ColumnIndex].Name == "coatingname"*/)
                    {
                        mnuContext.Items[10].Visible = true;
                        this.SourceStr = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    }
                    else
                    {
                        mnuContext.Items[10].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void mnucut_Click(object sender, EventArgs e)
        {
            CutData(ref dataGridView2);
        }

        private void mnucopy_Click(object sender, EventArgs e)
        {
            CopyData(ref dataGridView2);
        }

        private void mnupaste_Click(object sender, EventArgs e)
        {
            if (HeadContent.ImportFlag == 2)
            {
                if (dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[dataGridView2.CurrentCell.ColumnIndex].ColumnIndex == 1) return;
                PasteData(ref dataGridView2);
            }
        }

        private void mnudeleterow_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void mnuclearall_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
        }

        private void butChooseFile_Click(object sender, EventArgs e)
        {
            string[] FileSheet;
            string FileName = string.Empty;
            if (!string.IsNullOrEmpty(HeadContent.SupplierCode))
            {
                try
                {
                    OpenFileDialog openfile1 = new OpenFileDialog();
                    if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        FileName = openfile1.FileName;
                        this.txtFilePath.Text = FileName;
                        FileSheet = _repo.GetTableFromExcel(FileName);

                        List<string> Sheets = new List<string>(FileSheet);

                        cmdSheet.DataSource = null;
                        cmdSheet.DataSource = Sheets;
                    }
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please select supplier.", "Data not valid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void butLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                HeadContent.StoreInPlanFileDetails = _repo.GetFileDetail(txtFilePath.Text, cmdSheet.Text);
                var row = HeadContent.StoreInPlanFileDetails.ToList();

                HeadContent.Vessel = row[0].Vessel;
                HeadContent.MakerCode = row[0].MakerCode;
                HeadContent.CustID = row[0].CustID;

                SetHeaderContent(HeadContent);
                SetItakuDetailByFile(HeadContent.StoreInPlanFileDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "File not valid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void SetItakuDetail(IEnumerable<StoreInPlanDetail> item)
        {
            //GetDetail
            dataGridView2.Rows.Clear();
            int i = 0;
            foreach (var p in item)
            {
                int line = 0;
                int seq = i + 1;
                if (p.LineID != 0) line = p.LineID;
                if (p.SeqId != 0) seq = p.SeqId;

                dataGridView2.Rows.Add(line, seq, p.ArticleNo, p.CommodityCode, p.CommodityName, p.SpecCode, p.SpecName, p.CoatingCode,
                                        p.CoatingName, p.Thick, p.Width, p.Length, p.Quantity, p.Weight,
                                        p.Place, p.PackingNumber, p.Category, p.SaleContract, p.Note, false, p.StoreInFlag);

                if (p.StoreInFlag == 1)
                {
                    dataGridView2.Rows[i].ReadOnly = true;
                }
                if (i % 2 == 1)
                {
                    this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void SetItakuDetailByFile(IEnumerable<ExternalFileModel> item)
        {
            //GetDetail
            dataGridView2.Rows.Clear();
            int i = 0;
            foreach (var p in item)
            {
                int line = 0;
                int seq = i + 1;
                if (p.LineID != 0) line = p.LineID;
                if (p.SeqId != 0) seq = p.SeqId;

                dataGridView2.Rows.Add(line, seq, p.ArticleNo, p.Commodity, "", p.Specification, "", p.Coating, "",
                                        p.Thick, p.Width, p.Length, p.Quantity, p.Weight,
                                        p.Place, p.PackingNo, p.Category, p.MakerSaleContract, p.Note, false, false);
                if (i % 2 == 1)
                {
                    this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        /// <summary>
        /// For ITAKU
        /// </summary>
        /// <param name="grid"> DataGridView Name</param>
        /// <returns></returns>
        private bool ValidateGridCell(ref DataGridView grid)
        {
            lblValidation.Text = "";
            bool error = false;

            //Mapping data header for Maker and Customer
            //1. Check Maker
            if (string.IsNullOrEmpty(HeadContent.MakerName))
            {
                var result = _repoMaker.GetByID(HeadContent.MakerCode);
                if (result == null)
                {
                    MappingLookupModel model = new MappingLookupModel();
                    model.TypeCode = "MAKER";
                    model.SupplierCode = HeadContent.SupplierCode;
                    model.SupCode = txtMakerCode.Text.Trim();
                    var lookupResult = _repoLookup.GetByID(model);
                    if (lookupResult != null)
                    {
                        HeadContent.MakerCode = lookupResult.UCCCode;
                        var makerrow = _repoMaker.GetByID(HeadContent.MakerCode);
                        HeadContent.MakerName = makerrow.MakerName;
                    }
                    else
                    {
                        errorProvider1.SetError(txtMakerCode, "This Maker Code dosen't exist in the system.");
                        lblValidation.Text = "This Maker Code dosen't exist in the system.";
                        txtMakerCode.Focus();
                        return true;
                    }
                }
                else
                {
                    HeadContent.MakerCode = result.MakerCode;
                    HeadContent.MakerName = result.MakerName;
                }
            }

            //2. Check Customer
            if (string.IsNullOrEmpty(HeadContent.CustomerName))
            {
                var result = _repoCust.GetCustomerByID(HeadContent.CustID);
                if (result == null)
                {
                    MappingLookupModel model = new MappingLookupModel();
                    model.TypeCode = "CUST";
                    model.SupplierCode = HeadContent.SupplierCode;
                    model.SupCode = txtCustID.Text.Trim();
                    var lookupResult = _repoLookup.GetByID(model);
                    if (lookupResult != null)
                    {
                        HeadContent.CustID = lookupResult.UCCCode;
                        var custRow = _repoCust.GetCustomerByID(HeadContent.CustID);
                        HeadContent.CustomerName = custRow.CustName;
                    }
                    else
                    {
                        errorProvider1.SetError(txtCustID, "This Customer Code dosen't exist in the system.");
                        lblValidation.Text = "This Customer Code dosen't exist in the system.";
                        txtCustID.Focus();
                        return true;
                    }
                }
                else
                {
                    HeadContent.CustID = result.CustId;
                    HeadContent.CustomerName = result.CustName;
                }
            }
            //Set header content
            SetHeaderContent(HeadContent);

            for (int i = 0; (grid.Rows.Count) > i; i++)
            {
                if (string.IsNullOrEmpty(grid.Rows[i].Cells["id1"].Value.GetString())) return error;

                CommodityModel CommodityByRow = new CommodityModel();
                SpecModel SpecByRow = new SpecModel();
                CoatingModel CoatingByRow = new CoatingModel();
                WharehouseModel WhseByRow = new WharehouseModel();

                for (int j = 0; (grid.Columns.Count) > j; j++)
                {
                    try
                    {
                        //Article Duplicate
                        if (grid.Columns[j].Name == "article")
                        {
                            if (string.IsNullOrEmpty(grid[j, i].Value.GetString()))
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Please fill data on current focus.";
                                return true;
                            }
                            else if (_repo.CheckArticleExisting(grid.Rows[i].Cells["article"].Value.GetString(), Convert.ToInt32(grid.Rows[i].Cells["id1"].Value.GetString().GetInt())))
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "This Article number is duplicate.";
                                return true;
                            }
                            else
                            {
                                lblValidation.Text = "";
                            }
                        }

                        //Commodity	validation
                        if (grid.Columns[j].Name == "commodity1")
                        {
                            if (string.IsNullOrEmpty(grid[j, i].Value.GetString()))
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Please fill data on current focus.";
                                return true;
                            }

                            CommodityByRow = _repoCmdt.GetByID(grid.Rows[i].Cells["commodity1"].Value.GetString());
                            if (CommodityByRow == null)
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "This Commodity Code dosen't exist in the system.";
                                return true;
                            }
                            else
                            {
                                lblValidation.Text = "";
                            }
                        }

                        //Specification depend on commodity
                        if (grid.Columns[j].Name == "spec1")
                        {
                            if (string.IsNullOrEmpty(grid[j, i].Value.GetString()))
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Please fill data on current focus.";
                                return true;
                            }

                            SpecByRow = _repoSpec.GetByID(grid.Rows[i].Cells["spec1"].Value.GetString(), CommodityByRow.CommodityCode);
                            if (SpecByRow == null)
                            {
                                MappingLookupModel model = new MappingLookupModel();
                                model.TypeCode = "SPEC";
                                model.SupplierCode = HeadContent.SupplierCode;
                                model.SupCode = grid.Rows[i].Cells["spec1"].Value.GetString().Trim();
                                var result = _repoLookup.GetByID(model);
                                if (result != null)
                                {
                                    grid.Rows[i].Cells["spec1"].Value = result.UCCCode;
                                    grid.Rows[i].Cells["specname"].Value = _repoSpec.GetByID(result.UCCCode, result.UCCCodeForeign).SpecName;
                                    grid.Rows[i].Cells["commodity1"].Value = result.UCCCodeForeign;
                                    grid.Rows[i].Cells["cmdtname"].Value = _repoCmdt.GetByID(result.UCCCodeForeign).CommodityName;
                                }
                            }

                            SpecByRow = _repoSpec.GetByID(grid.Rows[i].Cells["spec1"].Value.GetString(), CommodityByRow.CommodityCode);
                            if (SpecByRow == null)
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "This Spec Code dosen't exist in the system or not relate with Commodity Code.";
                                return true;
                            }
                            else
                            {
                                lblValidation.Text = "";
                            }
                        }

                        //Commodity require coating
                        if (grid.Columns[j].Name == "Coating")
                        {
                            if (string.IsNullOrEmpty(grid[j, i].Value.GetString()) && CommodityByRow.CoatingRequire)
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Commodity [" + CommodityByRow.CommodityName + "] is require coating.";
                                return true;
                            }

                            if (!string.IsNullOrEmpty(grid[j, i].Value.GetString()))
                            {
                                CoatingByRow = _repoCoating.GetByID(grid.Rows[i].Cells["Coating"].Value.GetString());
                                if (CoatingByRow == null)
                                {
                                    grid.CurrentCell = grid[j, i];
                                    lblValidation.Text = "This Coating dosen't exist in the system.";
                                    return true;
                                }
                                else
                                {
                                    lblValidation.Text = "";
                                }
                            }
                        }

                        //Thick
                        if (grid.Columns[j].Name == "thick")
                        {
                            decimal tp;
                            if (!decimal.TryParse(Convert.ToString(grid.Rows[i].Cells["thick"].Value.GetString()), out tp))
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Please enter numeric.";
                                return true;
                            }
                            else if (Convert.ToString(grid.Rows[i].Cells["thick"].Value.GetString()) == "0")
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Can't fill be zero.";
                                return true;
                            }
                            else
                            {
                                lblValidation.Text = "";
                            }
                        }

                        //Width
                        if (grid.Columns[j].Name == "width")
                        {
                            decimal tp;
                            if (!decimal.TryParse(Convert.ToString(grid.Rows[i].Cells["width"].Value.GetString()), out tp))
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Please enter numeric.";
                                return true;
                            }
                            else if (Convert.ToString(grid.Rows[i].Cells["width"].Value.GetString()) == "0")
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Can't fill be zero.";
                                return true;
                            }
                            else
                            {
                                lblValidation.Text = "";
                            }
                        }

                        //Length
                        if (grid.Columns[j].Name == "length")
                        {
                            decimal tp;
                            if (!decimal.TryParse(Convert.ToString(grid.Rows[i].Cells["length"].Value.GetString()), out tp))
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Please enter numeric.";
                                return true;
                            }
                            else
                            {
                                lblValidation.Text = "";
                            }
                        }

                        //Quantity
                        if (grid.Columns[j].Name == "quantity2")
                        {
                            decimal tp;
                            if (!decimal.TryParse(Convert.ToString(grid.Rows[i].Cells["quantity2"].Value.GetString()), out tp))
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Please enter numeric.";
                                return true;
                            }
                            else if (Convert.ToString(grid.Rows[i].Cells["quantity2"].Value.GetString()) == "0")
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Can't fill be zero.";
                                return true;
                            }
                            else
                            {
                                lblValidation.Text = "";
                            }
                        }

                        //Weight
                        if (grid.Columns[j].Name == "weight2")
                        {
                            decimal tp;
                            if (!decimal.TryParse(Convert.ToString(grid.Rows[i].Cells["weight2"].Value.GetString()), out tp))
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Please enter numeric.";
                                return true;
                            }
                            else if (Convert.ToString(grid.Rows[i].Cells["weight2"].Value.GetString()) == "0")
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Can't fill be zero.";
                                return true;
                            }
                            else
                            {
                                lblValidation.Text = "";
                            }
                        }

                        //Place dose existing
                        if (grid.Columns[j].Name == "place1")
                        {
                            if (string.IsNullOrEmpty(grid[j, i].Value.GetString()))
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "Please fill data on current focus.";
                                return true;
                            }

                            WhseByRow = _repoWhse.GetByID(grid.Rows[i].Cells["place1"].Value.GetString());
                            if (WhseByRow == null)
                            {
                                grid.CurrentCell = grid[j, i];
                                lblValidation.Text = "This Place dosen't exist in the system.";
                                return true;
                            }
                            else
                            {
                                lblValidation.Text = "";
                            }
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        return false;
                    }
                }
            }

            //if (Convert.ToDecimal(txtWeightBalnce.Text) != Convert.ToDecimal(txtRemainingWeight.Text))
            //{
            //    error = true;
            //    label5.Text = "Receipt weight must be equal Actual receipt weight!.";
            //}
            //else
            //{
            //    label5.Text = "";
            //}
            return error;
        }

        private void butValidate_Click(object sender, EventArgs e)
        {
            ValidateGridCell(ref dataGridView2);
        }

        private void getITAKUTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<StoreInPlanDetail> ds = new List<StoreInPlanDetail>();
            DisplayInExcel(ds);
        }

        private static void DisplayInExcel(IEnumerable<StoreInPlanDetail> model)
        {
            var excelApp = new Excel.Application();
            // Make the object visible.
            excelApp.Visible = true;

            // Create a new, empty workbook and add it to the collection returned
            // by property Workbooks. The new workbook becomes the active workbook.
            // Add has an optional parameter for specifying a praticular template.
            // Because no argument is sent in this example, Add creates a new workbook.
            excelApp.Workbooks.Add();

            // This example uses a single workSheet. The explicit type casting is
            // removed in a later procedure.
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            // Establish column headings in cells A1 and B1.
            workSheet.Cells[1, "A"] = "Maker";
            workSheet.Cells[1, "B"] = "Cust";
            workSheet.Cells[1, "C"] = "Vessel";
            workSheet.Cells[1, "D"] = "ETA";
            workSheet.Cells[1, "E"] = "Commodity";
            workSheet.Cells[1, "F"] = "Invoice";
            workSheet.Cells[1, "G"] = "Spec";
            workSheet.Cells[1, "H"] = "Thick";
            workSheet.Cells[1, "I"] = "Width";
            workSheet.Cells[1, "J"] = "Length";
            workSheet.Cells[1, "K"] = "Qty";
            workSheet.Cells[1, "L"] = "Weight";
            workSheet.Cells[1, "M"] = "MakerNo";
            workSheet.Cells[1, "N"] = "MPO";
            workSheet.Cells[1, "O"] = "CC";
            workSheet.Cells[1, "P"] = "Location";
            workSheet.Cells[1, "Q"] = "GRADE";
            workSheet.Cells[1, "R"] = "Remark";

            //var row = 1;
            //foreach (var acct in model)
            //{
            //    row++;
            //    workSheet.Cells[row, "A"] = acct.LineID;
            //    workSheet.Cells[row, "B"] = acct.ArticleNo;
            //}
            workSheet.Cells.Font.Size = 10;
        }

        private void butCustomer_Click(object sender, EventArgs e)
        {
            using (CustomerDailog frm = new CustomerDailog())
            {
                frm.ShowDialog();

                HeadContent.CustID = frm.CustId;
                HeadContent.CustomerName = frm.CustName;
            }
            SetHeaderContent(HeadContent);
        }

        private void mnuMappingItem_Click(object sender, EventArgs e)
        {
            using (MappingSpecDialog frm = new MappingSpecDialog(HeadContent.SupplierCode, HeadContent.SupplierName, this.SourceStr))
            {
                frm.ShowDialog();
            }
        }

        private void txtMakerCode_MouseUp(object sender, MouseEventArgs e)
        {
            mnuContext.Items[0].Visible = false;
            mnuContext.Items[1].Visible = false;
            mnuContext.Items[2].Visible = false;
            mnuContext.Items[3].Visible = false;
            mnuContext.Items[4].Visible = false;
            mnuContext.Items[5].Visible = false;
            mnuContext.Items[6].Visible = false;
            mnuContext.Items[7].Visible = false;
            mnuContext.Items[8].Visible = false;
            mnuContext.Items[9].Visible = true;
            mnuContext.Items[10].Visible = false;
            //mnuContext.Items[11].Visible = false;
        }

        private void txtCustID_MouseUp(object sender, MouseEventArgs e)
        {
            mnuContext.Items[0].Visible = false;
            mnuContext.Items[1].Visible = false;
            mnuContext.Items[2].Visible = false;
            mnuContext.Items[3].Visible = false;
            mnuContext.Items[4].Visible = false;
            mnuContext.Items[5].Visible = false;
            mnuContext.Items[6].Visible = false;
            mnuContext.Items[7].Visible = false;
            mnuContext.Items[8].Visible = true;
            mnuContext.Items[9].Visible = false;
            mnuContext.Items[10].Visible = false;
            //mnuContext.Items[11].Visible = false;
        }

        private void addMappingDataForMakerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (MappingMakerDialog frm = new MappingMakerDialog(HeadContent.SupplierCode, HeadContent.SupplierName, txtMakerCode.Text.Trim()))
            {
                frm.ShowDialog();
            }
        }

        private void addMappingDataForCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (MappingCustomerDialog frm = new MappingCustomerDialog(HeadContent.SupplierCode, HeadContent.SupplierName, txtCustID.Text.Trim()))
            {
                frm.ShowDialog();
            }
        }

        private void selectCoatingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CoatingDialog frm = new CoatingDialog())
            {
                frm.ShowDialog();

                dataGridView2.SelectedCells[0].Value = frm.Coating;
                dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["coatingname"].Value = frm.CoatingName;
                //McssContent.Coating2 = frm.CoatingName;
                //McssContent.CoatingWeight1 = frm.FrontPlate;
                //McssContent.CoatingWeight2 = frm.BackPlate;
            }
        }

        private void cancelStoreInPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_repo.CheckStoreInFlagExist(HeadContent.StoreInPlanId))
            {
                MessageBox.Show("ไม่สามารถยกเลิกรายการ Store In Plan นี้ได้ เนื่องจากยังมีรายการ Store In อยู่", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (MessageBox.Show("Are you sure to Cancel.", "Question?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_repo.CloseStoreInPlan(HeadContent.StoreInPlanId))
                    {
                        MessageBox.Show("Close completed.", "Result");
                        tlbClear_Click(sender, e);
                    }
                }
            }
        }
    }
}