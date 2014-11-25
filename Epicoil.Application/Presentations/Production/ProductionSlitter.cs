using Epicoil.Appl.Presentations.Planning;
using Epicoil.Library;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Models.Production;
using Epicoil.Library.Repositories.Production;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Production
{
    public partial class ProductionSlitter : BaseSession
    {
        private readonly IProductionRepo _repo;
        private ProductionHeadModel HeaderContent;
        private DateTime timestamp;
        private bool pauseFlag;
        private DateTime timepause;

        private Dictionary<int, string> cuttingList;

        public ProductionSlitter(SessionInfo _session = null, ProductionHeadModel model = null)
        {
            InitializeComponent();
            epiSession = _session;
            _repo = new ProductionRepo();
            HeaderContent = new ProductionHeadModel();
            cuttingList = new Dictionary<int, string>();
            pauseFlag = false;
            if (model != null)
            {
                this.HeaderContent = model;
            }
        }

        #region Set Properties Control

        private void SetTimeControl(string action)
        {
            butOK.Enabled = false;
            butPrint.Enabled = false;
            butAddCut.Enabled = false;
            butDeleteCut.Enabled = false;
            pauseFlag = false;
            tbutNew.Enabled = false;
            tbutSave.Enabled = false;
            tbutClean.Enabled = false;
            tbutClose.Enabled = false;

            butStart.Enabled = false;
            butFinish.Enabled = false;
            butA.Enabled = false;
            butB.Enabled = false;
            butC.Enabled = false;
            butD.Enabled = false;
            butE.Enabled = false;
            butF.Enabled = false;
            butG.Enabled = false;
            butH.Enabled = false;
            butI.Enabled = false;
            butJ.Enabled = false;
            butK.Enabled = false;
            butL.Enabled = false;
            butM.Enabled = false;
            butN.Enabled = false;
            butO.Enabled = false;
            butP.Enabled = false;
            butQ.Enabled = false;
            butOther.Enabled = false;
            butConfirm.Enabled = false;
            chkRealTime.Enabled = false;
            chkRealTime.Checked = false;
            txtHH.ReadOnly = true;
            txtMM.ReadOnly = true;
            txtSS.ReadOnly = true;
            txtHH.Text = "00";
            txtMM.Text = "00";
            txtSS.Text = "00";

            switch (action)
            {
                case "Start":
                    timer1.Enabled = true;
                    txtTimeStamp.Text = "00:00:00";
                    txtTimePause.Text = "00:00:00";

                    butOK.Enabled = true;
                    butPrint.Enabled = true;
                    //butAddCut.Enabled = true;
                    //butDeleteCut.Enabled = true;

                    tbutSave.Enabled = true;

                    butStart.Enabled = false;
                    butFinish.Enabled = true;
                    butA.Enabled = true;
                    butB.Enabled = true;
                    butC.Enabled = true;
                    butD.Enabled = true;
                    butE.Enabled = true;
                    butF.Enabled = true;
                    butG.Enabled = true;
                    butH.Enabled = true;
                    butI.Enabled = true;
                    butJ.Enabled = true;
                    butK.Enabled = true;
                    butL.Enabled = true;
                    butM.Enabled = true;
                    butN.Enabled = true;
                    butO.Enabled = true;
                    butP.Enabled = true;
                    butQ.Enabled = true;
                    butOther.Enabled = true;
                    break;
                    
                case "Pause":
                    timepause = DateTime.Now;
                    pauseFlag = true;
                    //timer2.Enabled = true;
                    butA.Enabled = false;
                    butB.Enabled = false;
                    butC.Enabled = false;
                    butD.Enabled = false;
                    butE.Enabled = false;
                    butF.Enabled = false;
                    butG.Enabled = false;
                    butH.Enabled = false;
                    butI.Enabled = false;
                    butJ.Enabled = false;
                    butK.Enabled = false;
                    butL.Enabled = false;
                    butM.Enabled = false;
                    butN.Enabled = false;
                    butO.Enabled = false;
                    butP.Enabled = false;
                    butQ.Enabled = false;
                    butOther.Enabled = false;
                    butConfirm.Enabled = true;
                    chkRealTime.Enabled = true;
                    txtHH.ReadOnly = false;
                    txtMM.ReadOnly = false;
                    txtSS.ReadOnly = false;
                    break;

                case "Stop":
                    butStart.Enabled = true;
                    tbutNew.Enabled = true;
                    tbutSave.Enabled = true;
                    tbutClean.Enabled = true;
                    tbutClose.Enabled = true;
                    break;

                case "Confirm":
                    timer2.Enabled = false;
                    txtTimePause.Text = "00:00:00";
                    butOK.Enabled = true;
                    butPrint.Enabled = true;
                    //butAddCut.Enabled = true;
                    //butDeleteCut.Enabled = true;
                    butStart.Enabled = false;
                    butFinish.Enabled = true;
                    butA.Enabled = true;
                    butB.Enabled = true;
                    butC.Enabled = true;
                    butD.Enabled = true;
                    butE.Enabled = true;
                    butF.Enabled = true;
                    butG.Enabled = true;
                    butH.Enabled = true;
                    butI.Enabled = true;
                    butJ.Enabled = true;
                    butK.Enabled = true;
                    butL.Enabled = true;
                    butM.Enabled = true;
                    butN.Enabled = true;
                    butO.Enabled = true;
                    butP.Enabled = true;
                    butQ.Enabled = true;
                    butOther.Enabled = true;
                    butConfirm.Enabled = false;
                    chkRealTime.Enabled = false;
                    txtHH.ReadOnly = true;
                    txtMM.ReadOnly = true;
                    txtSS.ReadOnly = true;
                    break;

                case "Ready":
                    txtTimeStamp.Text = "00:00:00";
                    txtTimePause.Text = "00:00:00";
                    butStart.Enabled = true;
                    tbutNew.Enabled = true;
                    break;

                case "Normal":
                    txtTimeStamp.Text = "00:00:00";
                    txtTimePause.Text = "00:00:00";
                    butStart.Enabled = false;
                    tbutNew.Enabled = false;
                    break;
            }

            string cutline = HeaderContent.CutSeqStr;
            if (!string.IsNullOrEmpty(cutline))
            {
                if (action != "Pause") SetModifyCut(cutline);
            }
        }

        #endregion Set Properties Control

        #region From Event

        private void ProductionSlitter_Load(object sender, System.EventArgs e)
        {
            SetTimeControl("Normal");
        }

        private void butStart_Click(object sender, System.EventArgs e)
        {
            txtTimeStamp.Text = "00:00:00";
            txtTimePause.Text = "00:00:00";
            timestamp = DateTime.Now;
            SetTimeControl("Start");
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            TimeSpan intev = DateTime.Now - timestamp;
            txtTimeStamp.Text = String.Format("{0:00}:{1:00}:{2:00}", intev.Hours, intev.Minutes, intev.Seconds);

            if (pauseFlag)
            {
                TimeSpan intevpause = DateTime.Now - timepause;
                txtTimePause.Text = String.Format("{0:00}:{1:00}:{2:00}", intevpause.Hours, intevpause.Minutes, intevpause.Seconds);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //TimeSpan intev = DateTime.Now - timepause;
            //txtTimePause.Text = String.Format("{0:00}:{1:00}:{2:00}", intev.Hours, intev.Minutes, intev.Seconds);
        }

        private void butA_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butB_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butC_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butD_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butE_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butF_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butFinish_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            SetTimeControl("Stop");
        }

        private void butConfirm_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            SetTimeControl("Confirm");
        }

        private void chkRealTime_CheckedChanged(object sender, EventArgs e)
        {
            TimeSpan intev = DateTime.Now - timepause;
            txtHH.Text = String.Format("{0:00}", intev.Hours);
            txtMM.Text = String.Format("{0:00}", intev.Minutes);
            txtSS.Text = String.Format("{0:00}", intev.Seconds);
        }

        private void butG_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butH_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butI_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butJ_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butK_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butL_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butM_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butN_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butO_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butP_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butQ_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butOther_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
        }

        private void butWorkOrder_Click(object sender, EventArgs e)
        {
            using (WorkEntryDialog frm = new WorkEntryDialog(epiSession))
            {
                frm.ShowDialog();
                if (frm._selected != null)
                {
                    HeaderContent.WorkOrderID = frm._selected.WorkOrderID;
                    HeaderContent.WorkOrderNum = frm._selected.WorkOrderNum;
                    HeaderContent.ProcessLines = frm._selected.ResourceList.ToList();
                    HeaderContent.ProcessLineID = frm._selected.ProcessLineId;
                    HeaderContent.SerialLines = _repo.GetAllSerialByWorkOrder(HeaderContent.WorkOrderID).ToList();
                    HeaderContent.Materials = frm._selected.Materails.ToList();
                    //Set content and list Material was add from dialog.
                    SetHeadContent(HeaderContent);
                    //ListSerialCuttingToGrid(HeaderContent.SerialLines);
                }
            }
        }

        private void cmbCutLine_Leave(object sender, EventArgs e)
        {
            ShowCuttingSerail();
        }

        private void ProductionSlitter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer1.Enabled)
            {
                MessageBox.Show("This WorkOrder is running, so you can't close.", "Job is running.", MessageBoxButtons.OK);
                // Cancel the Closing event from closing the form.
                e.Cancel = true;
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            decimal cutWeight;
            if (!decimal.TryParse(txtCuttingWeigthManual.Text, out cutWeight))
            {
                MessageBox.Show("Cutting weight value must be Number type.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AverageWeightToCuttingLine(cutWeight);
        }

        private void dgvSerialCuttingList_SelectionChanged(object sender, EventArgs e)
        {
            SetSelectionMaterial();
        }

        private void dgvSerialCuttingList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
        }

        private void dgvSerialCuttingList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool changeState = false;
            string colName = dgvSerialCuttingList.Columns[e.ColumnIndex].Name;
            string strVal = dgvSerialCuttingList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetString();
            string serialLineId = dgvSerialCuttingList.Rows[e.RowIndex].Cells["serialLineID"].Value.GetString();

            var result = (from item in HeaderContent.SerialLines
                          where item.SerialLineID == Convert.ToInt32(serialLineId)
                          select item).First();
            switch (colName)
            {
                case "weightact":
                    changeState = (result.WeightActual != Convert.ToDecimal(strVal));
                    result.WeightActual = Convert.ToDecimal(strVal);
                    result.SetLengthActualM();
                    dgvSerialCuttingList.Rows[e.RowIndex].Cells["lengthact"].Value = result.LengthActual;
                    break;

                case "lengthact":
                    changeState = (result.LengthActual != Convert.ToDecimal(strVal));
                    result.LengthActual = Convert.ToDecimal(strVal);
                    result.SetWeightActualKg();
                    dgvSerialCuttingList.Rows[e.RowIndex].Cells["weightact"].Value = result.WeightActual;
                    break;

                case "ngflag":
                    changeState = (result.NGFlag != Convert.ToBoolean(strVal));
                    result.NGFlag = Convert.ToBoolean(strVal);
                    break;
            }
        }

        private void dgvSerialCuttingList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var dataGridView = (DataGridView)sender;

            if (dataGridView.Columns[e.ColumnIndex].Name != "ngflag") return;
            var cell = dataGridView.Rows[e.RowIndex].Cells["ngflag"];

            if (cell.Value == null) cell.Value = false;
            cell.Value = !(bool)cell.Value;
            dataGridView.EndEdit();
        }

        #endregion From Event

        #region Customize Method

        private void ShowCuttingSerail()
        {
            //if (string.IsNullOrEmpty(cmbCutLine.Text)) return;
            string cutline = HeaderContent.CutSeqStr;
            SetModifyCut(cutline);
            ListSerialCuttingToGrid(HeaderContent.SerialLines.Where(x => x.CutSeqStr == cutline));
        }

        private void SetModifyCut(string cutline)
        {
            string matSerial = (from item in HeaderContent.SerialLines
                                where item.CutSeqStr == cutline
                                select item.MaterialSerialNo).First();

            var maxCut = HeaderContent.SerialLines
                                        .Where(p => p.MaterialSerialNo == matSerial)
                                        .Max(p => p.CutSeq);

            if (Math.Round(Convert.ToDecimal(cutline), 0) == Math.Round(maxCut, 0))
            {
                butAddCut.Enabled = true;
                butDeleteCut.Enabled = true;
            }
            else
            {
                butAddCut.Enabled = false;
                butDeleteCut.Enabled = false;
            }
        }

        private void SetHeadContent(ProductionHeadModel model)
        {
            ClearHeaderContent();
            //TextBox
            txtWorkOrderNum.DataBindings.Add("Text", model, "WorkOrderNum", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPICName.DataBindings.Add("Text", epiSession, "UserName", false, DataSourceUpdateMode.OnPropertyChanged);

            //ComboBox
            cmbProcessLine.DataSource = model.ProcessLines.ToList();
            cmbProcessLine.DisplayMember = "ResourceDescription";
            cmbProcessLine.ValueMember = "ResourceID";
            cmbProcessLine.DataBindings.Add("SelectedValue", model, "ProcessLineId", false, DataSourceUpdateMode.OnPropertyChanged);

            cmbCutLine.DataSource = model.SerialLines.GroupBy(x => new { x.CutSeq })
                                                         .Select(g => g.First()).ToList();
            cmbCutLine.DisplayMember = "CutSeqStr";
            cmbCutLine.ValueMember = "CutSeqStr";
            cmbCutLine.DataBindings.Add("SelectedValue", model, "CutSeqStr", false, DataSourceUpdateMode.OnPropertyChanged);

            //DatePicker
            //dtpProductionDate.Value = model.ProductionDate;

            SetTimeControl("Ready");
            if (cmbCutLine.Items.Count > 0) cmbCutLine.SelectedIndex = 0;

            ShowCuttingSerail();
            ListMaterialGrid(model.Materials);
        }

        private void ClearHeaderContent()
        {
            //TextBox
            txtWorkOrderNum.DataBindings.Clear();
            txtPICName.DataBindings.Clear();

            //DatePicker
            dtpProductionDate.Value = DateTime.Now;

            //ComboBox
            cmbProcessLine.DataBindings.Clear();
            cmbProcessLine.DataSource = null;
            cmbProcessLine.Text = "";
            cmbProcessLine.Items.Clear();

            cmbCutLine.DataBindings.Clear();
            cmbCutLine.DataSource = null;
            cmbCutLine.Text = "";
            cmbCutLine.Items.Clear();
        }

        private void ListSerialCuttingToGrid(IEnumerable<SerialCuttingModel> item)
        {
            int i = 0;
            dgvSerialCuttingList.Rows.Clear();
            var sumWeigth = item.Sum(p => p.UnitWeight);
            foreach (var p in item)
            {
                dgvSerialCuttingList.Rows.Add(p.SerialLineID, p.SerialNo, p.MaterialSerialNo, p.Thick, p.Width, p.Length, p.LengthActual, p.LengthM
                                             , p.WeightActual, p.UnitWeight, (p.UnitWeight / sumWeigth) * 100, p.NGFlag, p.CutSeq, p.CommodityCode + " - " + p.CommodityName
                                             , p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName
                                             , p.BussinessType + " - " + p.BussinessTypeName, p.PossessionName);
                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvSerialCuttingList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void ListMaterialGrid(IEnumerable<MaterialModel> item)
        {
            int i = 0;
            dgvMaterial.Rows.Clear();
            foreach (var p in item)
            {
                //p.CalculateUsingLength();
                dgvMaterial.Rows.Add(p.TransactionLineID, p.MCSSNo, i + 1, p.SerialNo, p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName, p.Thick, p.Width, p.Length
                    , p.Weight, p.UsingWeight
                    , p.RemainWeight, p.LengthM, p.UsingLengthM, p.RemainLengthM
                    , (p.Length == 0) ? 1 : p.QuantityPack, (p.UsingQuantity == 0) ? ((p.Length == 0) ? 1 : p.QuantityPack) : p.UsingQuantity
                    , p.RemainQuantity, p.CBSelect
                    , Enum.GetName(typeof(MaterialStatus), int.Parse(p.Status)), p.Note, p.BussinessType + " - " + p.BussinessTypeName, Enum.GetName(typeof(ProductStatus), int.Parse(p.ProductStatus)));
                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvMaterial.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void SetSelectionMaterial()
        {
            if (dgvMaterial.Rows.Count == 0) return;
            int currentRow = dgvSerialCuttingList.CurrentCell.RowIndex;
            string matSN = dgvSerialCuttingList.Rows[currentRow].Cells["materialsn"].Value.GetString();

            int index = -1;

            DataGridViewRow row = dgvMaterial.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells["article"].Value.ToString().Equals(matSN))
                .First();

            index = row.Index;

            dgvMaterial.Rows[index].Selected = true;
        }

        private void AverageWeightToCuttingLine(decimal cutWeight)
        {
            decimal rate = cutWeight / 100;
            foreach (DataGridViewRow row in dgvSerialCuttingList.Rows)
            {
                string serialLineId = row.Cells["serialLineID"].Value.GetString();

                var result = (from item in HeaderContent.SerialLines
                              where item.SerialLineID == Convert.ToInt32(serialLineId)
                              select item).First();

                decimal perct = Convert.ToDecimal(row.Cells["percent"].Value);
                decimal weightPerCut = perct * rate;

                result.WeightActual = weightPerCut;
                result.SetLengthActualM();

                UpdateCuttingWeightCell(row, result);
            }
        }

        private void UpdateCuttingWeightCell(DataGridViewRow row, SerialCuttingModel model)
        {
            row.Cells["weightact"].Value = model.WeightActual.ToString("#,###.00");
            row.Cells["lengthact"].Value = model.LengthActual.ToString("#,###.00");
            dgvSerialCuttingList.EndEdit();
        }

        #endregion Customize Method
    }
}