using Epicoil.Appl.Presentations.StoreInPlan;
using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreIn;
using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.StoreIn;
using Epicoil.Library.Repositories.StoreInPlan;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.StoreIn
{
    public partial class StoreIn : BaseSession
    {
        private readonly IStoreInPlanRepo _repoPlan;
        private readonly IStoreInRepo _repo;
        private readonly IWharehouseRepo _repoWhse;
        private StoreInHead HeadContent;
        private StoreInHead ModelClone;

        public StoreIn(SessionInfo _session = null, StoreInHead model = null)
        {
            InitializeComponent();
            this._repoPlan = new StoreInPlanRepo();
            this._repo = new StoreInRepo();
            this._repoWhse = new WharehouseRepo();

            //Initial Session and content
            this.HeadContent = new StoreInHead();
            epiSession = _session;
            if (model != null) this.HeadContent = model;

            this.ModelClone = new StoreInHead();
            HeadContent.InsertState = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string type;
            int status = 1;
            if (HeadContent.InsertState == true)
            {
                status = 0;
            }

            if (HeadContent.Possession == 2)
            {
                type = "";
            }
            else
            {
                type = "1";
            }

            using (StoreInPlanDialog frm = new StoreInPlanDialog(status, type))
            {
                frm.ShowDialog();
                var result = _repo.GetStoreInPlanByID(frm.HeadContent.StoreInPlanId);
                if (result == null)
                {
                    return;
                }
                else
                {
                    result.Possession = HeadContent.Possession;
                    HeadContent = result;
                    ModelClone = (StoreInHead)HeadContent.Clone();
                }
            }
            txtStoreInPlanNum.Text = HeadContent.StoreInPlanNum;
            SetHeaderContent(HeadContent);
        }

        private void ClearHeaderContent()
        {
            txtStoreInNum.DataBindings.Clear();
            txtStoreInPlanNum.DataBindings.Clear();
            txtInvoiceNum.DataBindings.Clear();
            txtSupplierCode.DataBindings.Clear();
            txtSupplierName.DataBindings.Clear();
            txtMakerCode.DataBindings.Clear();
            txtMakerName.DataBindings.Clear();
            txtMillCode.DataBindings.Clear();
            txtMillName.DataBindings.Clear();
            txtBussinessType.DataBindings.Clear();
            txtBussinessTypeName.DataBindings.Clear();
            txtVessel.DataBindings.Clear();
            txtArivePort.DataBindings.Clear();
            txtLoadPort.DataBindings.Clear();
            txtInvoiceDate.DataBindings.Clear();
            txtETADate.DataBindings.Clear();
            txtETDDate.DataBindings.Clear();
            txtPoscession.DataBindings.Clear();

            txtStoreInNum.Clear();
            txtStoreInPlanNum.Clear();
            txtInvoiceNum.Clear();
            txtSupplierCode.Clear();
            txtSupplierName.Clear();
            txtMakerCode.Clear();
            txtMakerName.Clear();
            txtMillCode.Clear();
            txtMillName.Clear();
            txtBussinessType.Clear();
            txtBussinessTypeName.Clear();
            txtVessel.Clear();
            txtArivePort.Clear();
            txtLoadPort.Clear();
            txtInvoiceDate.Clear();
            txtETADate.Clear();
            txtETDDate.Clear();
            //cmbDutyRate.Items.Clear();
            txtPoscession.Clear();

            chkAll.Checked = false;
            chkAllLine.Checked = false;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            tlbSave.Enabled = true;
        }

        private void SetHeaderContent(StoreInHead model, bool saved = false)
        {
            ClearHeaderContent();
            txtStoreInNum.DataBindings.Add("Text", model, "StoreInNum", false, DataSourceUpdateMode.OnPropertyChanged);
            txtStoreInPlanNum.DataBindings.Add("Text", model, "StoreInPlanNum", false, DataSourceUpdateMode.OnPropertyChanged);
            txtInvoiceNum.DataBindings.Add("Text", model, "InvoiceNum", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSupplierCode.DataBindings.Add("Text", model, "SupplierCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSupplierName.DataBindings.Add("Text", model, "SupplierName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMakerCode.DataBindings.Add("Text", model, "MakerCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMakerName.DataBindings.Add("Text", model, "MakerName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMillCode.DataBindings.Add("Text", model, "MillCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMillName.DataBindings.Add("Text", model, "MillName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBussinessType.DataBindings.Add("Text", model, "BussinessType", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBussinessTypeName.DataBindings.Add("Text", model, "BussinessTypeName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtVessel.DataBindings.Add("Text", model, "Vessel", false, DataSourceUpdateMode.OnPropertyChanged);

            txtLoadPort.DataBindings.Add("Text", model, "LoadPort", false, DataSourceUpdateMode.OnPropertyChanged);
            txtArivePort.DataBindings.Add("Text", model, "ArivePort", false, DataSourceUpdateMode.OnPropertyChanged);

            txtETADate.DataBindings.Add("Text", model, "ETADate", false, DataSourceUpdateMode.OnPropertyChanged);
            txtETDDate.DataBindings.Add("Text", model, "ETDDate", false, DataSourceUpdateMode.OnPropertyChanged);
            txtInvoiceDate.DataBindings.Add("Text", model, "InvoiceDate", false, DataSourceUpdateMode.OnPropertyChanged);

            //if (HeadContent.Possession != 2)
            //{
            txtPoscession.DataBindings.Add("Text", model, "PossessionPromt", false, DataSourceUpdateMode.OnPropertyChanged);
            //}

            if (HeadContent.InsertState == true)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }

            if (model.StoreInFlag == "1")
            {
                tlbSave.Enabled = false;
            }
            else
            {
                tlbSave.Enabled = true;
            }

            //If not yet saving Store In.
            if (!saved)
            {
                var result = _repo.GetDetail(HeadContent.StoreInPlanId);
                SetDetail(result);
            }
            else
            {
                var result = _repo.GetDetailByStoreIn(HeadContent.StoreInID);
                SetDetail(result);
            }            
        }

        private void SetDetail(IEnumerable<StoreInDetail> item, bool saved = false)
        {
            //GetDetail
            dataGridView1.Rows.Clear();

            int i = 0;
            foreach (var p in item)
            {
                dataGridView1.Rows.Add(saved, p.LineID, p.PONum, p.PONumber, p.POLine, p.CommodityCode, p.SpecCode, p.Thick.ToString("#,##0.###"), p.Width.ToString("#,##0.###"), p.Length.ToString("#,##0.###")
                                            , p.Quantity, 0, p.DutyRate.ToString("#,##0.###"), p.StoreInPlanId, p.TransactionID);
                if (i % 2 == 1)
                {
                    this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void SetDetailArticle(IEnumerable<StoreInDetail> list, bool saved = false)
        {
            int i = 0;
            //dataGridView2.Rows.Clear();
            //List<WharehouseModel> locationList = _repoWhse.GetAll(epiSession.PlantID).ToList();
            foreach (var t in list)
            {
                dataGridView2.Rows.Add(saved, i + 1, t.ArticleNo, t.Thick, t.Width, t.Length, t.Quantity, t.Weight, t.Place
                                        , false, "", t.StockNo, t.LineID, t.POLine);
                if (i % 2 == 1)
                {
                    this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void tlbClear_Click(object sender, EventArgs e)
        {
            ClearHeaderContent();
            HeadContent = new StoreInHead();
            HeadContent.InsertState = false;
            HeadContent.StoreInFlag = "0";
            HeadContent.Possession = 3;
            ModelClone = (StoreInHead)HeadContent.Clone();
            SetHeaderContent(HeadContent);
            //button1.Enabled = true;
        }

        private void tlbInactive_Click(object sender, EventArgs e)
        {
            //SetHeaderContent(HeadContent);
            //bool result = ValidateContent();
            //ReceiptDataSet ReceiptHed = new ReceiptDataSet();
            //ReceiptHed = _repo.GetRowsToNewRcvHead(HeadContent.StoreInPlanId, epiSession);
            //_repo.GetRowsToNewRcvDetail(HeadContent.StoreInPlanId, epiSession, ReceiptHed);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //ClearHeaderContent();
            //HeadContent = new StoreInHead();
            //HeadContent.StoreInFlag = "0";
            //HeadContent.InsertState = true;
            //ModelClone = (StoreInHead)HeadContent.Clone();
            //SetHeaderContent(HeadContent);
            //button1.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView2.Rows.Clear();
            //dataGridView2.DataSource = null;
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            //bool IsSuccess;
            string msg;
            int StoreInid;
            int SelectedCount = 0;

            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }

            if (!HashListSeleted())
            {
                MessageBox.Show("Please select rows in grid.", "Please try agian.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (!HashArticleSelected(out SelectedCount))
            {
                MessageBox.Show("Please select rows in grid.", "Please try agian.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (SelectedCount > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to save store in. \rAfter your save, you can't modify it.", "Please Confirm.", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int iRow = dataGridView1.CurrentRow.Index;
                    List<StoreInDetail> selectedList = new List<StoreInDetail>();

                    for (int i = 0; (this.dataGridView2.Rows.Count) > i; i++)
                    {
                        if ((bool)dataGridView2[0, i].EditedFormattedValue && string.IsNullOrEmpty(dataGridView2.Rows[i].Cells["stock"].Value.GetString()))
                        {
                            dataGridView2.CurrentCell = dataGridView2[0, i];

                            var item = new StoreInDetail();
                            item = _repo.GetArticleListToSave(Convert.ToInt32(dataGridView2.Rows[i].Cells["lineid"].Value));
                            item.Location = dataGridView2.Rows[i].Cells["Place"].Value.GetString();
                            item.NGStatus = Convert.ToInt32(dataGridView2.Rows[i].Cells["ngflag"].Value.GetBoolean());
                            item.NGRemark = dataGridView2.Rows[i].Cells["ngRemark"].Value.GetString();
                            HeadContent.PONum = item.PONum;
                            HeadContent.PONumber = item.PONumber;
                            selectedList.Add(item);
                        }
                    }

                    if (selectedList != null)
                    {
                        HeadContent.StoreInDate = DateTime.Now;
                        HeadContent = _repo.InsertStoreIn(HeadContent, epiSession);
                        _repo.InsertStoreInLine(selectedList, epiSession, HeadContent.StoreInID, HeadContent.TransactionID, out msg);
                    }
                    SetHeaderContent(HeadContent);
                    MessageBox.Show("Save Store In completed. And then will be create Part in Epicor.", "Progression.", MessageBoxButtons.OK);
                    Progression frm1 = new Progression("NEWPART", HeadContent.TransactionID, epiSession, HeadContent.Possession);
                    frm1.ShowDialog();

                    tlbSave.Enabled = false;
                    SetHeaderContent(HeadContent, true);
                    var result = _repo.GetDetailArticle(HeadContent.StoreInPlanId, HeadContent.TransactionID);
                    SetDetailArticle(result, true);
                }
            }
        }

        private void StoreIn_Load(object sender, EventArgs e)
        {
            if (epiSession.SessionID == null)
            {
                Login frm = new Login();
                frm.ShowDialog();
            }
            else if (epiSession.SessionID == "x")
            {
                this.Dispose();
                this.Close();
                Environment.Exit(1);
            }
            else
            {
                this.Text = epiSession.PlantName;

                if (HeadContent.StoreInPlanId != 0)
                {
                    txtStoreInNum.Text = HeadContent.StoreInNum;
                    SetHeaderContent(HeadContent);
                }

                ModelClone = (StoreInHead)HeadContent.Clone();
                return;
            }
            StoreIn_Load(sender, e);
        }

        private void iTAKUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearHeaderContent();
            HeadContent = new StoreInHead();
            HeadContent.StoreInFlag = "0";
            HeadContent.Possession = 2;  //Set Posession (ITAKU)
            HeadContent.InsertState = true;
            ModelClone = (StoreInHead)HeadContent.Clone();
            SetHeaderContent(HeadContent);
            button1.Enabled = true;
        }

        private void jISHAZAIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearHeaderContent();
            HeadContent = new StoreInHead();
            HeadContent.StoreInFlag = "0";
            HeadContent.Possession = 0;  //Set Posession (JISHAZAI)
            HeadContent.InsertState = true;
            ModelClone = (StoreInHead)HeadContent.Clone();
            SetHeaderContent(HeadContent);
            button1.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "selected" && dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
            {
                bool isChecked = (bool)dataGridView1[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                if (!isChecked)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["weight"].Value = 0;
                }
                ListArticle();
                dataGridView1.EndEdit();
            }
        }

        private bool HashListSeleted()
        {
            bool HashSelected = false;
            for (int i = 0; (this.dataGridView1.Rows.Count) > i; i++)
            {
                if (dataGridView1.Columns[0].Name == "selected" && (bool)dataGridView1[0, i].EditedFormattedValue)
                {
                    HashSelected = true;
                }
                dataGridView1.CurrentCell = dataGridView1[0, i];
            }
            return HashSelected;
        }

        private bool HashArticleSelected(out int rowSelected)
        {
            bool HashSelected = false;
            int rowSelect = 0;
            for (int i = 0; (this.dataGridView2.Rows.Count) > i; i++)
            {
                if ((bool)dataGridView2[0, i].EditedFormattedValue)
                {
                    rowSelect++;
                    if (!string.IsNullOrEmpty(dataGridView2.Rows[i].Cells["Place"].Value.GetString()))
                    {
                        HashSelected = true;
                        rowSelected = rowSelect;
                        return HashSelected;
                    }
                }
                dataGridView2.CurrentCell = dataGridView2[0, i];
            }
            rowSelected = rowSelect;
            return HashSelected;
        }

        private bool ListArticle()
        {
            bool error = false;
            dataGridView2.Rows.Clear();
            for (int i = 0; (this.dataGridView1.Rows.Count) > i; i++)
            {
                if (dataGridView1.Columns[0].Name == "selected" && (bool)dataGridView1[0, i].EditedFormattedValue)
                {
                    int POLine = Convert.ToInt32(dataGridView1.Rows[i].Cells["poline"].Value);
                    int LineID = Convert.ToInt32(dataGridView1.Rows[i].Cells["lineId1"].Value);
                    string PONum = dataGridView1.Rows[i].Cells["ponum"].Value.ToString();
                    IEnumerable<StoreInDetail> result = new List<StoreInDetail>();
                    if (HeadContent.Possession != 2)
                    {
                        result = this._repo.GetDetailArticle(this.HeadContent.StoreInPlanId, PONum, POLine);
                    }
                    else
                    {
                        result = this._repo.GetDetailArticleITAKU(LineID);
                    }

                    SetDetailArticle(result);
                }
            }
            return error;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (Convert.ToBoolean(row.Cells["selected"].Value) == false && chkAll.Checked == true)
            //    {
            //        row.Cells["selected"].Value = true;
            //    }
            //    else if (Convert.ToBoolean(row.Cells["selected"].Value) == false && chkAll.Checked == false)
            //    {
            //        row.Cells["selected"].Value = false;
            //    }
            //    else { row.Cells["selected"].Value = false; }
            //}
        }

        private void chkAll_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (chkAll.Checked == true)
                {
                    row.Cells["selected"].Value = true;
                }
                else
                {
                    row.Cells["selected"].Value = false;
                }
                ListArticle();
            }
        }

        private void chkAllLine_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                bool changeWeight = true;
                if (chkAllLine.Checked == true)
                {
                    if ((bool)row.Cells["select"].Value)
                    {
                        changeWeight = false;
                    }
                    row.Cells["select"].Value = true;
                }
                else
                {
                    if (!(bool)row.Cells["select"].Value)
                    {
                        changeWeight = false;
                    }
                    row.Cells["select"].Value = false;
                }

                if (changeWeight)
                {
                    DataGridViewCellEventArgs ev = new DataGridViewCellEventArgs(row.Cells["select"].ColumnIndex, row.Index);
                    dataGridView2_CellContentClick(sender, ev);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "select" && dataGridView2.CurrentCell is DataGridViewCheckBoxCell)
            {
                bool isChecked = (bool)dataGridView2[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                decimal Weight = (decimal)dataGridView2.Rows[e.RowIndex].Cells["weight1"].Value.GetDecimal();
                int LineID = 0;
                if (HeadContent.Possession != 2)
                {
                    LineID = (int)dataGridView2.Rows[e.RowIndex].Cells["poline1"].Value.GetInt();
                }
                else
                {
                    LineID = (int)dataGridView2.Rows[e.RowIndex].Cells["lineid"].Value.GetInt();
                }

                SumWeightByRow(isChecked, Weight, LineID);
                dataGridView2.EndEdit();
            }
        }

        private void SumWeightByRow(bool iChecked, decimal iWeight, int iLine)
        {
            int rowIndex = -1;
            //weight
            DataGridViewRow row = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells["poline"].Value.ToString().Equals(iLine.ToString()) || r.Cells["lineId1"].Value.ToString().Equals(iLine.ToString()))
                .First();

            rowIndex = row.Index;
            decimal lWeight = (decimal)dataGridView1.Rows[rowIndex].Cells["weight"].Value.GetDecimal();
            if (iChecked)
            {
                dataGridView1.Rows[rowIndex].Cells["weight"].Value = lWeight + iWeight;
            }
            else
            {
                dataGridView1.Rows[rowIndex].Cells["weight"].Value = lWeight - iWeight;
            }
        }

        private bool NewReceipt(out string errorMsg)
        {
            errorMsg = "";
            bool IsSuccess = false;
            List<StoreInDetail> param = new List<StoreInDetail>();

            for (int i = 0; (this.dataGridView1.Rows.Count) > i; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells["selected"].Value)
                {
                    StoreInDetail result = new StoreInDetail();
                    result.PONum = (int)dataGridView1.Rows[i].Cells["pono"].Value.GetInt();
                    result.POLine = (int)dataGridView1.Rows[i].Cells["poline"].Value.GetInt();
                    result.LineID = (int)dataGridView1.Rows[i].Cells["lineId1"].Value.GetInt();
                    result.VendorNum = (int)dataGridView1.Rows[i].Cells["vendornum"].Value.GetInt();
                    result.StoreInNum = HeadContent.StoreInNum;

                    param.Add(result);
                }
            }

            //_repo.GetNewRcv(param, epiSession, out IsSuccess, out errorMsg);
            return IsSuccess;
        }

        private void storeInBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StoreInPlanDialogModel model = new StoreInPlanDialogModel();
            model.StoreInPlanId = HeadContent.StoreInPlanId;
            model.StoreInPlanNum = HeadContent.StoreInPlanNum;
            model.InvoiceNum = HeadContent.InvoiceNum;
            model.InvoiceDateFrom = HeadContent.InvoiceDate;
            model.InvoiceDateTo = HeadContent.InvoiceDate;

            StoreInBalance frm = new StoreInBalance(epiSession, model);
            frm.Show();
        }
    }
}