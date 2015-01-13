using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Epicoil.Appl.Presentations.Planning;
using Epicoil.Appl.Reports.Production;
using Epicoil.Library;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Models.Production;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Planning;
using Epicoil.Library.Repositories.Production;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Production
{
    public partial class ProductionSlitter : BaseSession
    {
        private readonly IProductionRepo _repo;
        private readonly IUserCodeRepo _repoUcd;
        private readonly IResourceRepo _repoRes;
        private readonly IWorkEntryRepo _repoPlan;

        private ProductionHeadModel HeaderContent;
        private DateTime timestamp;
        private bool pauseFlag;
        private DateTime timepause;

        private Dictionary<int, string> cuttingList;

        public ProductionSlitter(SessionInfo _session = null, ProductionHeadModel model = null)
        {
            InitializeComponent();
            epiSession = _session;
            this._repo = new ProductionRepo();
            this._repoPlan = new WorkEntryRepo();
            this._repoRes = new ResourceRepo();
            this._repoUcd = new UserCodeRepo();
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
            #region Initial properties.

            HeaderContent.ActionState = action;
            butOK.Enabled = false;
            butWorkOrder.Enabled = true;
            butPrint.Enabled = false;
            butAddCut.Enabled = false;
            butDeleteCut.Enabled = false;
            pauseFlag = false;

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
            txtRemark.Text = "";

            #endregion Initial properties.

            #region Set by behavior.

            switch (action)
            {
                case "Start":
                    timer1.Enabled = true;
                    txtTimeStamp.Text = "00:00:00";
                    txtTimePause.Text = "00:00:00";
                    butOK.Enabled = true;
                    butWorkOrder.Enabled = false;
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

                case "OK":
                    timer1.Enabled = true;
                    txtTimeStamp.Text = "00:00:00";
                    txtTimePause.Text = "00:00:00";
                    butOK.Enabled = true;
                    butPrint.Enabled = true;
                    butWorkOrder.Enabled = false;
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

                case "Print":
                    timer1.Enabled = true;
                    txtTimeStamp.Text = "00:00:00";
                    txtTimePause.Text = "00:00:00";
                    butOK.Enabled = true;
                    butWorkOrder.Enabled = false;
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
                    butWorkOrder.Enabled = false;
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
                    //tbutNew.Enabled = true;
                    tbutSave.Enabled = true;
                    tbutClean.Enabled = true;
                    tbutClose.Enabled = true;
                    break;

                case "Confirm":
                    timer2.Enabled = false;
                    txtTimePause.Text = "00:00:00";
                    butOK.Enabled = true;
                    butPrint.Enabled = true;
                    tbutSave.Enabled = true;
                    butWorkOrder.Enabled = false;
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
                    butWorkOrder.Enabled = false;
                    break;

                case "Normal":
                    txtTimeStamp.Text = "00:00:00";
                    txtTimePause.Text = "00:00:00";
                    butStart.Enabled = false;
                    butWorkOrder.Enabled = true;
                    break;

                case "Lock":
                    tbutSave.Enabled = false;
                    dgvSerialCuttingList.ReadOnly = true;
                    butStart.Enabled = false;
                    butPrint.Enabled = true;
                    break;
            }

            #endregion Set by behavior.

            if (timer1.Enabled == true)
            {
                tbutNew.Enabled = false;
            }
            else
            {
                tbutNew.Enabled = true;
            }

            if (LockCutCalculation())
            {
                dgvSerialCuttingList.Columns["lengthact"].ReadOnly = true;
                dgvSerialCuttingList.Columns["weightact"].ReadOnly = true;
                dgvSerialCuttingList.Columns["ngflag"].ReadOnly = true;
                butOK.Enabled = false;
            }
            else
            {
                dgvSerialCuttingList.Columns["lengthact"].ReadOnly = false;
                dgvSerialCuttingList.Columns["weightact"].ReadOnly = false;
                dgvSerialCuttingList.Columns["ngflag"].ReadOnly = false;
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
            if (string.IsNullOrEmpty(cmbCutLine.Text))
            {
                MessageBox.Show("Please fill cut seq.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCutLine.Focus();
                return;
            }

            txtTimeStamp.Text = "00:00:00";
            txtTimePause.Text = "00:00:00";

            HeaderContent.ProductionDate = DateTime.Now;
            HeaderContent.StartTime = DateTime.Now;
            HeaderContent.PuaseTime = 0;
            HeaderContent.FinishTime = DateTime.Now;
            HeaderContent.CompleteFlag = 0;
            HeaderContent.OperationState = 2;
            HeaderContent = _repo.SaveHead(epiSession, HeaderContent);

            timestamp = HeaderContent.StartTime;
            SetTimeControl("Start");
            HeaderContent.CutSeqStr = cmbCutLine.SelectedValue.ToString();
            SetHeadContent(HeaderContent);
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
            GetReasonDetail("A");
        }

        private void butB_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("B");
        }

        private void butC_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("C");
        }

        private void butD_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("D");
        }

        private void butE_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("E");
        }

        private void butF_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("F");
        }

        private void butG_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("G");
        }

        private void butH_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("H");
        }

        private void butI_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("I");
        }

        private void butJ_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("J");
        }

        private void butK_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("K");
        }

        private void butL_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("L");
        }

        private void butM_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("M");
        }

        private void butN_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("N");
        }

        private void butO_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("O");
        }

        private void butP_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("P");
        }

        private void butQ_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("Q");
        }

        private void butOther_Click(object sender, EventArgs e)
        {
            SetTimeControl("Pause");
            GetReasonDetail("Other");
        }

        private void butFinish_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            SetTimeControl("Stop");
        }

        private void butConfirm_Click(object sender, EventArgs e)
        {
            decimal d;
            if (!decimal.TryParse(txtHH.Text.GetString(), out d))
            {
                MessageBox.Show("Data type not valid.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHH.Focus();
                return;
            }

            if (!decimal.TryParse(txtMM.Text.GetString(), out d))
            {
                MessageBox.Show("Data type not valid.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMM.Focus();
                return;
            }

            if (!decimal.TryParse(txtSS.Text.GetString(), out d))
            {
                MessageBox.Show("Data type not valid.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSS.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtRemark.Text))
            {
                MessageBox.Show("Please fill reason.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRemark.Focus();
                return;
            }

            if (string.IsNullOrEmpty(HeaderContent.CutSeqStr))
            {
                MessageBox.Show("Please fill cut seq.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCutLine.Focus();
                return;
            }

            string duratTime = string.Format("{0}:{1}:{2}", txtHH.Text, txtMM.Text, txtSS.Text);
            if (duratTime != "00:00:00")
            {
                LineStopModel model = new LineStopModel();
                model.ProductionID = HeaderContent.ProductionID;
                model.WorkOrderID = HeaderContent.WorkOrderID;
                model.StopCode = HeaderContent.Reason;
                model.Description = txtRemark.Text;
                model.DurationTime = duratTime;
                model.CutSeq = Convert.ToDecimal(HeaderContent.CutSeqStr);

                if (_repo.SaveLineStop(epiSession, model))
                {
                    timer2.Enabled = false;
                    SetTimeControl("Confirm");
                }
            }
        }

        private void chkRealTime_CheckedChanged(object sender, EventArgs e)
        {
            TimeSpan intev = DateTime.Now - timepause;
            txtHH.Text = String.Format("{0:00}", intev.Hours);
            txtMM.Text = String.Format("{0:00}", intev.Minutes);
            txtSS.Text = String.Format("{0:00}", intev.Seconds);
        }

        private void butWorkOrder_Click(object sender, EventArgs e)
        {
            PlanningHeadModel plan = new PlanningHeadModel();
            plan.Plant = epiSession.PlantID;
            plan.Completed = 1; //Completed.
            plan.GenSerialFlag = 1;  //Serial has created.
            plan.OperationState = 2; //Stay on production operation.

            var result = _repoPlan.GetWorkAll(plan);
            using (WorkEntryDialog frm = new WorkEntryDialog(epiSession, result))
            {
                frm.ShowDialog();
                if (frm._selected != null)
                {
                    HeaderContent.OperationState = frm._selected.OperationState;
                    HeaderContent.WorkOrderID = frm._selected.WorkOrderID;
                    HeaderContent.WorkOrderNum = frm._selected.WorkOrderNum;

                    //When work order not complete.
                    if (HeaderContent.OperationState == 2)
                    {
                        HeaderContent.ProcessLines = frm._selected.Resources.ToList();
                        HeaderContent.ProcessLineID = frm._selected.ProcessLineId;
                        HeaderContent.SerialLines = _repo.GetAllSerialByWorkOrder(HeaderContent.WorkOrderID).ToList();
                        HeaderContent.Materials = frm._selected.Materials.ToList();
                    }
                }
            }

            HeaderContent.Reasons = _repo.GetAllReasonAll().ToList();

            //Set content and list Material was add from dialog.
            SetHeadContent(HeaderContent);
            if (HeaderContent.OperationState == 3 || HeaderContent.OperationState == 4)
            {
                SetTimeControl("Lock");
            }
            else
            {
                SetTimeControl("Ready");
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
            if (!timer1.Enabled)
            {
                MessageBox.Show("Pleas start work before calculate weight.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtCuttingWeigthManual.Text, out cutWeight))
            {
                MessageBox.Show("Cutting weight value must be Number type.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HeaderContent.CutSeq = Convert.ToDecimal(cmbCutLine.Text);
            AverageWeightToCuttingLine(cutWeight);
            SetTimeControl("OK");
        }

        private void dgvSerialCuttingList_SelectionChanged(object sender, EventArgs e)
        {
            SetSelectionMaterial();
        }

        private void dgvSerialCuttingList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool changeState = false;
            string colName = dgvSerialCuttingList.Columns[e.ColumnIndex].Name;
            string strVal = dgvSerialCuttingList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetString();
            string serialLineId = dgvSerialCuttingList.Rows[e.RowIndex].Cells["serialLineID"].Value.GetString();

            var result = (from item in HeaderContent.SerialLines
                          where item.SerialLineID == Convert.ToInt32(serialLineId)
                          select item).FirstOrDefault();
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
            if (dgvSerialCuttingList.Rows[e.RowIndex].Cells["ngflag"].ReadOnly == true) return;
            var dataGridView = (DataGridView)sender;

            if (dataGridView.Columns[e.ColumnIndex].Name != "ngflag") return;
            var cell = dataGridView.Rows[e.RowIndex].Cells["ngflag"];

            if (cell.Value == null) cell.Value = false;
            cell.Value = !(bool)cell.Value;
            dataGridView.EndEdit();
        }

        private void tbutNew_Click(object sender, EventArgs e)
        {
            HeaderContent = new ProductionHeadModel();
            SetHeadContent(HeaderContent);
            SetTimeControl("Normal");

            PlanningHeadModel plan = new PlanningHeadModel();
            plan.Plant = epiSession.PlantID;
            plan.Completed = 1; //Completed.
            plan.GenSerialFlag = 1;  //Serial has created.
            plan.OperationState = 1; //Stay on planing operation.

            var result = _repoPlan.GetWorkAll(plan);
            using (WorkEntryDialog frm = new WorkEntryDialog(epiSession, result))
            {
                frm.ShowDialog();
                if (frm._selected != null)
                {
                    HeaderContent.OperationState = frm._selected.OperationState;
                    HeaderContent.WorkOrderID = frm._selected.WorkOrderID;
                    HeaderContent.WorkOrderNum = frm._selected.WorkOrderNum;
                    HeaderContent.ProcessLines = frm._selected.Resources.ToList();
                    HeaderContent.ProcessLineID = frm._selected.ProcessLineId;
                    HeaderContent.SerialLines = _repo.GetAllSerialByWorkOrder(HeaderContent.WorkOrderID).ToList();
                    HeaderContent.Materials = frm._selected.Materials.ToList();
                    HeaderContent.Reasons = _repo.GetAllReasonAll().ToList();
                    //Set content and list Material was add from dialog.
                    SetHeadContent(HeaderContent);
                }
                else
                {
                    SetTimeControl("Normal");
                    return;
                }
            }

            if (HeaderContent.OperationState == 3 || HeaderContent.OperationState == 4)
            {
                SetTimeControl("Lock");
            }
            else
            {
                SetTimeControl("Ready");
            }
        }

        private void tbutSave_Click(object sender, EventArgs e)
        {
        }

        private void butPrint_Click(object sender, EventArgs e)
        {
            var result = HeaderContent.SerialLines.Where(i => i.CutSeq == HeaderContent.CutSeq).Min(i => i.LengthActual);
            if (!timer1.Enabled)
            {
                MessageBox.Show("Pleas start work before calculate weight.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (result == 0)
            {
                MessageBox.Show("Please complete all data in the Grid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_repo.SaveSerialCutting(epiSession, HeaderContent))
            {
                CuttedLineUpModel cutline = new CuttedLineUpModel();
                var matlineid = (from item in HeaderContent.SerialLines
                                 where item.CutSeq == HeaderContent.CutSeq
                                 select item.MaterialTransLineID).First();

                cutline.ProductionID = HeaderContent.ProductionID;
                cutline.WorkOrderID = HeaderContent.WorkOrderID;
                cutline.CutSeq = HeaderContent.CutSeq;
                cutline.StartTime = DateTime.Now;
                cutline.FinishTime = DateTime.Now;
                cutline.MaterialTransLineID = matlineid;
                HeaderContent.Cutteds = _repo.SaveCuttedLineUp(epiSession, cutline).ToList();
                SetTimeControl("Print");

                ReportClass rptH = new ReportClass();
                dsProduction ds = new dsProduction();
                string outPath = Application.StartupPath + "\\Out\\test.pdf";
                rptH.FileName = Application.StartupPath + "\\Reports\\Production\\ProductionLabel1.rpt";
                foreach (var item in HeaderContent.SerialLines.Where(i => i.CutSeq == HeaderContent.CutSeq))
                {
                    ds.Label.AddLabelRow(item.SerialNo, item.NGFlag.ToString());
                }
                rptH.Load();
                ds.AcceptChanges();
                rptH.SetDataSource(ds);
                rptH.Refresh();
                rptH.ExportToDisk(ExportFormatType.PortableDocFormat, outPath);
                OpenPdfFile(outPath);
            }
        }

        private void OpenPdfFile(string outPath)
        {
            try
            {
                Process.Start(outPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please install MicrosoftOffice/Pdf Reader to view files", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void butAddCut_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                MessageBox.Show("Pleas start work before calculate weight.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void butDeleteCut_Click(object sender, EventArgs e)
        {
            DialogResult diaResult = MessageBox.Show("Are you sure to delete this cut.", "Question.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diaResult == DialogResult.Yes)
            {
                int indexOld = cmbCutLine.SelectedIndex;

                _repo.DeteteCutFromPlan(HeaderContent.WorkOrderID, HeaderContent.CutSeq);
                HeaderContent = _repo.GetProdHead(HeaderContent.WorkOrderID);
                SetHeadContent(HeaderContent);
                cmbCutLine.SelectedIndex = indexOld - 1;
                ShowCuttingSerail();
            }
        }

        #endregion From Event

        #region Customize Method

        private void ShowCuttingSerail()
        {
            if (cmbCutLine.Text != "")
            {
                HeaderContent.CutSeq = Convert.ToDecimal(cmbCutLine.Text);
                if (!CheckValidSequenceCutting(HeaderContent.CutSeq))
                {
                    MessageBox.Show("Cutting sequence invalid please check cutting sequence.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCutLine.Focus();
                    return;
                }

                string cutline = HeaderContent.CutSeqStr;

                if (HeaderContent.OperationState != 3 || HeaderContent.OperationState != 4)
                {
                    SetModifyCut(cutline);
                }
                ListSerialCuttingToGrid(HeaderContent.SerialLines.Where(x => x.CutSeqStr == cutline));

                if (LockCutCalculation())
                {
                    dgvSerialCuttingList.Columns["lengthact"].ReadOnly = true;
                    dgvSerialCuttingList.Columns["weightact"].ReadOnly = true;
                    dgvSerialCuttingList.Columns["ngflag"].ReadOnly = true;
                    butOK.Enabled = false;
                    butPrint.Enabled = true;
                }
                else
                {
                    dgvSerialCuttingList.Columns["lengthact"].ReadOnly = false;
                    dgvSerialCuttingList.Columns["weightact"].ReadOnly = false;
                    dgvSerialCuttingList.Columns["ngflag"].ReadOnly = false;
                    butOK.Enabled = true;
                    butPrint.Enabled = false;
                }
            }
        }

        private bool CheckValidSequenceCutting(decimal p)
        {
            bool valid = true;
            if (timer1.Enabled)
            {
                var material = (from item in HeaderContent.SerialLines
                                where item.CutSeq == p
                                select item.MaterialTransLineID).FirstOrDefault();

                var minCut = HeaderContent.SerialLines.Where(x => x.MaterialTransLineID == material).Min(x => x.CutSeq);
                p = Math.Round(p, 0);
                if (p == minCut)
                {
                    valid = true;
                }
                else
                {
                    var exist = HeaderContent.Cutteds.Where(x => x.MaterialTransLineID == material && x.CutSeq == (p - 1)).ToList().Count;
                    valid = (exist > 0);
                }
            }

            return valid;
        }

        private bool LockCutCalculation()
        {
            bool locked = false;
            if (cmbCutLine.Text != "")
            {
                HeaderContent.CutSeq = Convert.ToDecimal(cmbCutLine.Text);
                var compltd = (from item in HeaderContent.Cutteds
                               where item.CutSeq == HeaderContent.CutSeq
                               select item.CompleteFlag).FirstOrDefault();
                if (compltd == 1) locked = true;
            }
            return locked;
        }

        private void SetModifyCut(string cutline)
        {
            if (cutline == null) return;
            string matSerial = (from item in HeaderContent.SerialLines
                                where item.CutSeqStr == cutline
                                select item.MaterialSerialNo).FirstOrDefault();

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
            //if (model.OperationState == 3 || model.OperationState == 4)
            //{
            //    SetTimeControl("Lock");
            //}
            //else
            //{
            //    SetTimeControl("Ready");
            //}

            //if (cmbCutLine.Items.Count > 0) cmbCutLine.SelectedIndex = 0;

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
            if (dgvSerialCuttingList.Rows.Count == 0) return;

            int currentRow = dgvSerialCuttingList.CurrentCell.RowIndex;
            string matSN = dgvSerialCuttingList.Rows[currentRow].Cells["materialsn"].Value.GetString();

            int index = -1;

            DataGridViewRow row = dgvMaterial.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells["article"].Value.ToString().Equals(matSN))
                .FirstOrDefault();

            if (row == null) return;
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

        private void GetReasonDetail(string p)
        {
            var reason = HeaderContent.Reasons.Where(i => i.ReasonCode == p).First();
            HeaderContent.Reason = reason.ReasonCode;
            txtRemark.Text = reason.ReasonDescription;
        }

        #endregion Customize Method
    }
}