using Epicoil.Appl.Presentations.Sales;
using Epicoil.Library;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Models.Sales;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Planning;
using Epicoil.Library.Repositories.Sales;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class WorkEntry : BaseSession
    {
        #region Fields

        private readonly IClassMasterRepo _repoCls;
        private readonly IWorkEntryRepo _repo;
        private readonly IResourceRepo _repoRes;

        private readonly ISaleOrderRepo _repoSale;
        private readonly IUserCodeRepo _repoUcd;
        private readonly IResourceRepo _reRes;
        private ClassMasterModel _class;
        private PlanningHeadModel HeaderContent;

        #endregion Fields

        #region Constructors

        public WorkEntry(SessionInfo _session = null, PlanningHeadModel model = null)
        {
            InitializeComponent();
            this._repoRes = new ResourceRepo();
            this._repoUcd = new UserCodeRepo();
            this._repo = new WorkEntryRepo();
            this._reRes = new ResourceRepo();
            this._repoSale = new SaleOrderRepo();
            this._repoCls = new ClassMasterRepo();

            this.HeaderContent = new PlanningHeadModel();
            this._class = new ClassMasterModel();

            //Initial Session and content
            this.HeaderContent = new PlanningHeadModel();
            epiSession = _session;
            if (model != null)
            {
                this.HeaderContent = model;
            }
        }

        #endregion Constructors

        #region Binding Data

        private void ClearHeaderContent()
        {
            butConfirm.Visible = false;
            butGenSN.Visible = false;
            butSimulate.Visible = false;
            txtYield.BackColor = SystemColors.Control;

            #region Clear TextBox binding

            txtWorkOrderNum.DataBindings.Clear();
            txtProcessStep.DataBindings.Clear();
            txtPICName.DataBindings.Clear();
            txtUsingWeight.DataBindings.Clear();
            txtInputWeight.DataBindings.Clear();
            txtRewindWeight.DataBindings.Clear();
            txtOutputWeight.DataBindings.Clear();
            txtBT.DataBindings.Clear();
            txtLossWeight.DataBindings.Clear();
            txtYield.DataBindings.Clear();
            txtTotalMaterialAmount.DataBindings.Clear();
            txtTotalWidth.DataBindings.Clear();

            #endregion Clear TextBox binding

            #region Clear ComboBox binding

            cmbProcessLine.DataBindings.Clear();
            cmbOrderType.DataBindings.Clear();
            cmbPossession.DataBindings.Clear();
            cmbProcessLine.DataSource = null;
            cmbOrderType.DataSource = null;
            cmbPossession.DataSource = null;
            cmbProcessLine.Text = "";
            cmbOrderType.Text = "";
            cmbPossession.Text = "";
            cmbProcessLine.Items.Clear();
            cmbOrderType.Items.Clear();
            cmbPossession.Items.Clear();

            #endregion Clear ComboBox binding

            #region Clear CheckBox binding

            chkLVTrim.DataBindings.Clear();
            chkPackingPlan.DataBindings.Clear();
            chkPackingPlan.Checked = false;
            chkLVTrim.Checked = false;

            #endregion Clear CheckBox binding

            #region Clear DatePicker value

            dptIssueDate.Value = DateTime.Now;
            dptDueDate.Value = DateTime.Now;

            #endregion Clear DatePicker value

            #region Clear TextBox text content

            txtWorkOrderNum.Clear();
            txtProcessStep.Clear();
            txtPICName.Clear();
            txtUsingWeight.Clear();
            txtInputWeight.Clear();
            txtRewindWeight.Clear();
            txtOutputWeight.Clear();
            txtBT.Clear();
            txtLossWeight.Clear();
            txtYield.Clear();
            txtTotalMaterialAmount.Clear();
            txtTotalWidth.Clear();

            #endregion Clear TextBox text content
        }

        private void ResetCoilBackGrid()
        {
            dgvCoilBack.Rows.Clear();
        }

        private void ResetCuttingGrid()
        {
            dgvCutting.Rows.Clear();
        }

        private void ResetMaterialGrid()
        {
            dgvMaterial.Rows.Clear();
        }

        private void SetFormState()
        {
            switch (HeaderContent.FormState)
            {
                #region 0 = Nothing.

                case 0:
                    tbutNewWork.Enabled = true;
                    tbutNewMaterial.Enabled = false;
                    tbutNewCutting.Enabled = false;
                    tbutSave.Enabled = false;
                    tbutCalculate.Enabled = false;
                    tbutSimulate.Enabled = false;
                    tbutCreateSerial.Enabled = false;
                    tlbClear.Enabled = false;
                    tbutCancelWorkOrder.Enabled = false;
                    butWorkOrder.Enabled = true;
                    break;

                #endregion 0 = Nothing.

                #region 1 = New Transaction.

                case 1:
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = false;
                    tbutNewCutting.Enabled = false;
                    tbutSave.Enabled = true;
                    tbutCalculate.Enabled = false;
                    tbutSimulate.Enabled = false;
                    tbutCreateSerial.Enabled = false;
                    tlbClear.Enabled = true;
                    tbutCancelWorkOrder.Enabled = false;
                    butWorkOrder.Enabled = false;
                    ResetMaterialGrid();
                    ResetCoilBackGrid();
                    ResetCuttingGrid();
                    break;

                #endregion 1 = New Transaction.

                #region 2 = Transaction was save.

                case 2:
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = true;
                    tbutNewCutting.Enabled = true;
                    tlbClear.Enabled = true;
                    butWorkOrder.Enabled = false;
                    break;

                #endregion 2 = Transaction was save.

                #region 3 = Selected material.

                case 3:
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = true;
                    tbutNewCutting.Enabled = true;
                    tbutSave.Enabled = true;
                    tbutCalculate.Enabled = true;
                    tbutSimulate.Enabled = false;
                    tbutCreateSerial.Enabled = false;
                    tlbClear.Enabled = true;
                    tbutCancelWorkOrder.Enabled = true;
                    butWorkOrder.Enabled = false;
                    break;

                #endregion 3 = Selected material.

                #region 4 = Calculated.

                case 4:
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = true;
                    tbutNewCutting.Enabled = true;
                    tbutSave.Enabled = true;
                    tbutCalculate.Enabled = true;
                    tbutSimulate.Enabled = true;
                    tbutCreateSerial.Enabled = true;
                    tlbClear.Enabled = true;
                    tbutCancelWorkOrder.Enabled = true;
                    butWorkOrder.Enabled = false;
                    break;

                #endregion 4 = Calculated.
            }
            butAddMaterial.Enabled = tbutNewMaterial.Enabled;
        }

        private void SetHeadContent(PlanningHeadModel model)
        {
            //Call clear binding data that anchor with object.
            ClearHeaderContent();
            model.CalculationHeader(model);

            //Default PIC.
            model.PIC = epiSession.UserID;
            model.PICName = epiSession.UserName;

            #region Binding data to TextBox objects.

            txtWorkOrderNum.DataBindings.Add("Text", model, "WorkOrderNum", false, DataSourceUpdateMode.OnPropertyChanged);
            txtProcessStep.DataBindings.Add("Text", model, "ProcessStep", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPICName.DataBindings.Add("Text", model, "PICName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtUsingWeight.DataBindings.Add("Text", model, "UsingWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0");
            txtInputWeight.DataBindings.Add("Text", model, "InputWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0");
            txtRewindWeight.DataBindings.Add("Text", model, "RewindWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0");
            txtOutputWeight.DataBindings.Add("Text", model, "OutputWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0");
            txtBT.DataBindings.Add("Text", model, "BT", false, DataSourceUpdateMode.OnPropertyChanged);
            txtLossWeight.DataBindings.Add("Text", model, "LossWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0");
            txtYield.DataBindings.Add("Text", model, "Yield", true, DataSourceUpdateMode.OnPropertyChanged, 1, "##0.00");
            txtTotalMaterialAmount.DataBindings.Add("Text", model, "TotalMaterialAmount", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0.00");
            txtTotalWidth.DataBindings.Add("Text", model, "TotalWidth", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0.00");

            #endregion Binding data to TextBox objects.

            #region Binding data to ComboBox objects.

            cmbProcessLine.DataSource = model.Resources.ToList();
            cmbProcessLine.DisplayMember = "ResourceDescription";
            cmbProcessLine.ValueMember = "ResourceID";
            cmbProcessLine.DataBindings.Add("SelectedValue", model, "ProcessLineId", false, DataSourceUpdateMode.OnPropertyChanged);

            cmbOrderType.DataSource = model.OrderTypeList.ToList();
            cmbOrderType.DisplayMember = "CodeDesc";
            cmbOrderType.ValueMember = "CodeID";
            cmbOrderType.DataBindings.Add("SelectedValue", model, "OrderType", false, DataSourceUpdateMode.OnPropertyChanged);

            cmbPossession.DataSource = model.Possessions.ToList();
            cmbPossession.DisplayMember = "CodeDesc";
            cmbPossession.ValueMember = "CodeID";
            cmbPossession.DataBindings.Add("SelectedValue", model, "Possession", false, DataSourceUpdateMode.OnPropertyChanged);

            #endregion Binding data to ComboBox objects.

            #region Binding data to CheckBox objects.

            chkPackingPlan.DataBindings.Add("Checked", model, "PackingPlan", false, DataSourceUpdateMode.OnPropertyChanged);
            chkLVTrim.DataBindings.Add("Checked", model, "LVTrim", false, DataSourceUpdateMode.OnPropertyChanged);

            #endregion Binding data to CheckBox objects.

            #region Set value to DatePicker

            dptIssueDate.Value = model.IssueDate;
            dptDueDate.Value = model.DueDate;

            #endregion Set value to DatePicker

            #region Automate set Yield status.

            if (model.CheckYeild(model.Yield))
            {
                //If work-order is Hold status. But yield value is OK and then system will be automatic unlock holding.
                if (model.Completed == 2)
                    model.Completed = Convert.ToInt32(_repo.UnlockHold(HeaderContent.WorkOrderID));
            }
            else
            {
                txtYield.BackColor = Color.Yellow;
            }

            #endregion Automate set Yield status.

            #region Lock object can be modify depend on the Cutting Line and Materials.

            if (model.CuttingDesign.ToList().Count > 0 || model.Materials.ToList().Count > 0)
            {
                cmbProcessLine.Enabled = false;
                cmbOrderType.Enabled = false;
                cmbPossession.Enabled = false;
            }
            else
            {
                cmbOrderType.Enabled = true;
                cmbProcessLine.Enabled = true;
                cmbPossession.Enabled = true;
            }

            #endregion Lock object can be modify depend on the Cutting Line and Materials.

            #region Lock object can be modify depend on the Complete status.

            if (model.Completed == 1)
            {   //Green color.
                butConfirm.BackColor = Color.FromArgb(161, 205, 95);
                dptIssueDate.Enabled = false;
                dptDueDate.Enabled = false;
                //txtProcessStep.ReadOnly = true;
                chkPackingPlan.Enabled = false;
                chkLVTrim.Enabled = false;

                //Set visible properties to status buttons.
                butConfirm.Visible = Convert.ToBoolean(model.Completed);
                butSimulate.Visible = Convert.ToBoolean(model.SimulateFlag);
                butGenSN.Visible = Convert.ToBoolean(model.Completed);
            }
            else if (model.Completed == 0)
            {
                dptIssueDate.Enabled = true;
                dptDueDate.Enabled = true;
                //txtProcessStep.ReadOnly = false;
                chkPackingPlan.Enabled = true;
                chkLVTrim.Enabled = true;

                //Set visible properties to status buttons.
                butConfirm.Visible = Convert.ToBoolean(model.Completed);
                butSimulate.Visible = Convert.ToBoolean(model.SimulateFlag);
                butGenSN.Visible = Convert.ToBoolean(model.Completed);
            }
            else if (model.Completed == 2)
            {
                butConfirm.Visible = true;
                butSimulate.Visible = true;
                butConfirm.BackColor = Color.Red;
            }

            #endregion Lock object can be modify depend on the Complete status.

            //Set text properties to status buttons.
            butConfirm.Text = model.CompletedStr;
            butSimulate.Text = model.SimulateFlagStr;
            butGenSN.Text = model.GenSerialFlagStr.Replace("_", " ");
        }

        private void SetPermissCuttingDesign()
        {
            dgvCutting.Columns["thick1"].ReadOnly = true;
            dgvCutting.Columns["width1"].ReadOnly = true;
            dgvCutting.Columns["length1"].ReadOnly = true;
            dgvCutting.Columns["status1"].ReadOnly = true;
            dgvCutting.Columns["stand"].ReadOnly = true;
            dgvCutting.Columns["cutdiv"].ReadOnly = true;

            if (HeaderContent.ProcessLine.ResourceGrpID == "S")
            {
                dgvCutting.Columns["width1"].ReadOnly = false;
                dgvCutting.Columns["status1"].ReadOnly = false;
                dgvCutting.Columns["stand"].ReadOnly = false;
                dgvCutting.Columns["cutdiv"].ReadOnly = false;
                dgvCutting.Columns["qtyPack1"].ReadOnly = true;
            }
            else if (HeaderContent.ProcessLine.ResourceGrpID == "L")
            {
                dgvCutting.Columns["length1"].ReadOnly = false;
                dgvCutting.Columns["status1"].ReadOnly = false;
                dgvCutting.Columns["qtyPack1"].ReadOnly = false; //calqty
            }
            else if (HeaderContent.ProcessLine.ResourceGrpID == "R")
            {
                dgvCutting.Columns["width1"].ReadOnly = false;
                dgvCutting.Columns["length1"].ReadOnly = false;
                dgvCutting.Columns["status1"].ReadOnly = false;
                dgvCutting.Columns["qtyPack1"].ReadOnly = true;
            }
        }

        #endregion Binding Data

        #region Form Events

        private void butAddMaterial_Click(object sender, EventArgs e)
        {
            //Exit this function if order completed.
            if (OrderCompleted()) return;

            //Initialization
            HeaderContent.FormState = 3;
            SetFormState();

            //Default direction for all (Class/Machine etc.).
            SetDirectionPattern();

            //Get Material that compatible with Machine was select.
            var result = _repo.GetAllMatByFilter(epiSession.PlantID, HeaderContent);

            #region Call the Material List forms and passing material result by filter.

            using (MaterialSelecting frm = new MaterialSelecting(epiSession, result, HeaderContent))
            {
                frm.ShowDialog();
                HeaderContent.Materials = _repo.GetAllMaterial(epiSession.PlantID, HeaderContent.WorkOrderID).ToList();

                //
                if (string.IsNullOrEmpty(HeaderContent.Possession))
                {
                    HeaderContent.Possession = frm._selected.Possession.GetString();
                }
            }

            #endregion Call the Material List forms and passing material result by filter.

            #region Set Material Grid after selected material.

            try
            {
                ListMaterialGrid(HeaderContent.Materials);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            #endregion Set Material Grid after selected material.

            #region Summaries Input/Using weight

            HeaderContent.SumInputWeight(HeaderContent);
            HeaderContent.SumUsingWeight(HeaderContent.Materials);

            #endregion Summaries Input/Using weight

            #region Set content and list Material was add from dialog.

            SetHeadContent(HeaderContent);
            ListCuttingGrid(HeaderContent.CuttingDesign);

            #endregion Set content and list Material was add from dialog.
        }

        private void butWorkOrder_Click(object sender, EventArgs e)
        {
            using (CoilBackRuleDialog frm = new CoilBackRuleDialog(epiSession))
            {
                frm.ShowDialog();
                txtWorkOrderNum.Text = frm.Code;
            }
        }

        private void butWorkOrder_Click_1(object sender, EventArgs e)
        {
            using (WorkEntryDialog frm = new WorkEntryDialog(epiSession))
            {
                frm.ShowDialog();
                if (frm._selected != null)
                {
                    HeaderContent = frm._selected;
                    HeaderContent.FormState = 3;
                    SetFormState();
                    //Summaries using weight
                    HeaderContent.SumUsingWeight(HeaderContent.Materials);
                    //Set content and list Material was add from dialog.
                    SetHeadContent(HeaderContent);
                    //Set Material Grid.
                    try
                    {
                        ListMaterialGrid(HeaderContent.Materials);
                        ListCuttingGrid(HeaderContent.CuttingDesign);
                        ListCoilBackGrid(HeaderContent.CoilBacks);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private bool CheckCoilBackState(MaterialModel param)
        {
            //Get coil-back existing row by TransactionLineID.
            var result = (from item in HeaderContent.CoilBacks
                          where item.TransactionLineID == param.TransactionLineID
                          select item).FirstOrDefault();

            //If remain-weight greater than zero will be to add/modify coil-back.
            if (param.RemainWeight > 0)
            {
                return true;
            }
            //Will be to delete coil-back.
            else if (param.RemainWeight == 0 && result != null)
            {
                return false;
            }
            //Nothing else to delete coil-back.
            else
            {
                return false;
            }
        }

        private void cmbPossession_Leave(object sender, EventArgs e)
        {
            if (HeaderContent.Materials.ToList().Count > 0)
            {
                int result = HeaderContent.Materials.Max(p => p.Possession);
                if (cmbPossession.SelectedValue.ToString() != result.ToString())
                {
                    MessageBox.Show("Please select 'Possession' that compatible with material.", "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbPossession.SelectedValue = result.ToString();
                    return;
                }
            }

            if (HeaderContent.CuttingDesign.ToList().Count > 0)
            {
                int result = HeaderContent.CuttingDesign.Max(p => p.Possession);
                if (cmbPossession.SelectedValue.ToString() != result.ToString())
                {
                    MessageBox.Show("Please select 'Possession' that compatible with cutting design.", "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbPossession.SelectedValue = result.ToString();
                    return;
                }
            }
        }

        private void cmbPossession_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void cmbProcessLine_Leave(object sender, EventArgs e)
        {
            if (HeaderContent.Materials.ToList().Count > 0)
            {
                MessageBox.Show("Please clear all material before change process line.", "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProcessLine.SelectedValue = HeaderContent.ProcessLine.ResourceID;
                return;
            }

            if (HeaderContent.CuttingDesign.ToList().Count > 0)
            {
                MessageBox.Show("Please clear all cutting design before change process line.", "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProcessLine.SelectedValue = HeaderContent.ProcessLine.ResourceID;
                return;
            }
        }

        private void dgvCutting_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (OrderCompleted()) return;

            OrderHeadModel result = new OrderHeadModel();
            int i; decimal d; string riskFlag = string.Empty; string msg = string.Empty;
            string colName = dgvCutting.Columns[e.ColumnIndex].Name;
            string lineId = dgvCutting.Rows[e.RowIndex].Cells["lineid"].Value.GetString();
            string resultValue = dgvCutting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetString();
            bool changeState = false;

            CutDesignModel rowData = new CutDesignModel();
            rowData = (from item in HeaderContent.CuttingDesign
                       where item.LineID == Convert.ToInt32(lineId)
                       select item).FirstOrDefault();
            rowData.CompleteRow = false;
            switch (colName)
            {
                case "sono":

                    #region Verify S/O No.

                    if (HeaderContent.Materials.ToList().Count == 0) HeaderContent.BussinessType = "";
                    if (HeaderContent.CuttingDesign.ToList().Count == 1) HeaderContent.BussinessType = "";

                    if (!string.IsNullOrEmpty(resultValue) && rowData.SONo != resultValue)
                    {
                        var resultTmp = _repoSale.GetOrderHeader(HeaderContent, resultValue).ToList();

                        dgvCutting.Rows[e.RowIndex].Cells["soline"].Value = string.Empty;
                        if (resultTmp.ToList().Count == 0)
                        {
                            var soResult = _repoSale.GetOrderHeader(HeaderContent);
                            using (OrderHeadDialog frm = new OrderHeadDialog(epiSession, soResult))
                            {
                                frm.ShowDialog();
                                result = frm._selected;
                                if (result == null)
                                {
                                    dgvCutting.Rows[e.RowIndex].Cells["sono"].Value = string.Empty;
                                    dgvCutting.Rows[e.RowIndex].Cells["soline"].Value = string.Empty;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            result = resultTmp.FirstOrDefault();
                        }

                        //Set header value.
                        HeaderContent.OrderType = result.OrderType;
                        if (!string.IsNullOrEmpty(result.OrderNumber))
                        {
                            HeaderContent.CuttingDesign = addSOLine(Convert.ToInt32(lineId), result.OrderNumber);

                            rowData = (from item in HeaderContent.CuttingDesign
                                       where item.LineID == Convert.ToInt32(lineId)
                                       select item).FirstOrDefault();

                            dgvCutting.Rows[e.RowIndex].Cells["sono"].Value = rowData.SONo;
                            dgvCutting.Rows[e.RowIndex].Cells["customer"].Value = result.CustID;
                            dgvCutting.Rows[e.RowIndex].Cells["soline"].Value = rowData.SOLine;
                            dgvCutting.Rows[e.RowIndex].Cells["norno"].Value = rowData.NORNum;
                            dgvCutting.Rows[e.RowIndex].Cells["commodity1"].Value = rowData.CommodityCode;
                            dgvCutting.Rows[e.RowIndex].Cells["spec1"].Value = rowData.SpecCode;
                            dgvCutting.Rows[e.RowIndex].Cells["coating1"].Value = rowData.CoatingCode;
                            dgvCutting.Rows[e.RowIndex].Cells["thick1"].Value = rowData.Thick;
                            dgvCutting.Rows[e.RowIndex].Cells["width1"].Value = rowData.Width;
                            dgvCutting.Rows[e.RowIndex].Cells["length1"].Value = rowData.Length;
                            dgvCutting.Rows[e.RowIndex].Cells["status1"].Value = "";
                            dgvCutting.Rows[e.RowIndex].Cells["soweight"].Value = rowData.SOWeight;
                            dgvCutting.Rows[e.RowIndex].Cells["soqty"].Value = rowData.SOQuantity;
                            dgvCutting.Rows[e.RowIndex].Cells["qtyPack1"].Value = rowData.QtyPack;
                            dgvCutting.Rows[e.RowIndex].Cells["pack"].Value = rowData.Pack;
                            dgvCutting.Rows[e.RowIndex].Cells["bt1"].Value = rowData.BussinessType;
                        }
                        else
                        {
                            dgvCutting.Rows[e.RowIndex].Cells["sono"].Value = rowData.SONo;
                        }
                    }

                    #endregion Verify S/O No.

                    break;

                case "soline":
                    changeState = (rowData.SOLine != Convert.ToInt32(resultValue));
                    rowData.SOLine = Convert.ToInt32(string.IsNullOrEmpty(resultValue) ? "0" : resultValue);
                    break;

                case "width1":
                    if (!decimal.TryParse(resultValue, out d))
                    {
                        //e.Cancel = true;
                        return;
                    }
                    changeState = (rowData.Width != Convert.ToDecimal(resultValue));
                    rowData.Width = Convert.ToDecimal(string.IsNullOrEmpty(resultValue) ? "0M" : resultValue);
                    break;

                case "length1":
                    if (!decimal.TryParse(resultValue, out d))
                    {
                        return;
                    }
                    changeState = (rowData.Length != Convert.ToDecimal(resultValue));
                    rowData.Length = Convert.ToDecimal(string.IsNullOrEmpty(resultValue) ? "0M" : resultValue);
                    break;

                case "status1":
                    changeState = (rowData.Status != resultValue);
                    if (changeState)
                    {
                        if (HeaderContent.ProcessLine.ResourceGrpID == "L")
                        {
                            if (resultValue == "F" && !string.IsNullOrEmpty(rowData.NORNum))
                            {
                                dgvCutting.Rows[e.RowIndex].Cells["length1"].ReadOnly = true;
                            }
                            else
                            {
                                dgvCutting.Rows[e.RowIndex].Cells["length1"].ReadOnly = false;
                            }
                        }
                        else if (HeaderContent.ProcessLine.ResourceGrpID == "S")
                        {
                            if (resultValue == "S" && !string.IsNullOrEmpty(rowData.NORNum))
                            {
                                dgvCutting.Rows[e.RowIndex].Cells["width1"].ReadOnly = true;
                            }
                            else
                            {
                                dgvCutting.Rows[e.RowIndex].Cells["width1"].ReadOnly = false;
                            }
                        }
                    }
                    rowData.Status = resultValue;
                    break;

                case "stand":
                    if (!int.TryParse(resultValue, out i))
                    {
                        return;
                    }
                    changeState = (rowData.Stand != Convert.ToInt32(resultValue));
                    rowData.Stand = Convert.ToInt32(resultValue);
                    break;

                case "cutdiv":
                    if (!int.TryParse(resultValue, out i))
                    {
                        return;
                    }
                    changeState = (rowData.CutDivision != Convert.ToInt32(resultValue));
                    rowData.CutDivision = Convert.ToInt32(resultValue);
                    break;

                case "note1":
                    changeState = (rowData.Note != resultValue);
                    rowData.Note = resultValue;
                    break;

                case "soqty":
                    changeState = (rowData.SOQuantity != Convert.ToDecimal(resultValue));
                    rowData.SOQuantity = Convert.ToDecimal(resultValue);
                    break;

                case "calqty":
                    changeState = (rowData.CalQuantity != Convert.ToDecimal(resultValue));
                    rowData.CalQuantity = Convert.ToDecimal(resultValue);
                    break;

                case "qtyPack1":
                    changeState = (rowData.QtyPack != Convert.ToDecimal(resultValue));
                    rowData.QtyPack = Convert.ToDecimal(resultValue);
                    break;
            }

            if (changeState)
            {
                rowData.CalculateRows(HeaderContent);
                bool validRow = rowData.ValidateByRow(HeaderContent, out riskFlag, out msg);
                dgvCutting.Rows[e.RowIndex].Cells["rowValidated"].Value = validRow;
                if (!validRow)
                {
                    if (riskFlag == "ERROR")
                    {
                        DialogResult diaResult = MessageBox.Show(msg, "Row validate error!.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                //Set rial time to change weight by row.
                dgvCutting.Rows[e.RowIndex].Cells["unitweight1"].Value = rowData.UnitWeight;
                dgvCutting.Rows[e.RowIndex].Cells["totalweight"].Value = rowData.TotalWeight;
                dgvCutting.Rows[e.RowIndex].Cells["totallength"].Value = rowData.TotalLength;
                rowData.CompleteRow = validRow;
                HeaderContent.CuttingDesign = _repo.SaveLineCutting(epiSession, HeaderContent, rowData).ToList();
                SetHeadContent(HeaderContent);
                ListMaterialGrid(HeaderContent.Materials);
            }
        }

        private void dgvCutting_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
        }

        private void dgvMaterial_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (OrderCompleted()) return;
            string risk = string.Empty;
            string msg = string.Empty;
            bool changeState = false;
            string colHeadName = dgvMaterial.Columns[e.ColumnIndex].HeaderText;
            string colName = dgvMaterial.Columns[e.ColumnIndex].Name;
            string strVal = dgvMaterial.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetString();
            string weight = dgvMaterial.Rows[e.RowIndex].Cells["weight"].Value.GetString();
            string qtyPack = dgvMaterial.Rows[e.RowIndex].Cells["qtyPack"].Value.GetString();
            string transId = dgvMaterial.Rows[e.RowIndex].Cells["transactionlineid"].Value.GetString();
            decimal parseDec = 0M;

            //Abort validation if cell is not in the column.
            if (colName == "usingweight" || colName == "quantity" || colName == "RemQuantity" || colName == "qtyPack")
            {
                if (!Decimal.TryParse(strVal, out parseDec))
                {
                    dgvMaterial.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0M;
                    dgvMaterial.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Format(@"{0} must be format valid.", colHeadName);
                    return;
                }
                else
                {
                    dgvMaterial.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
                }
            }

            var result = (from item in HeaderContent.Materials
                          where item.TransactionLineID == Convert.ToInt32(transId)
                          select item).FirstOrDefault();

            switch (colName)
            {
                case "usingweight":
                    changeState = (result.UsingWeight != Convert.ToDecimal(strVal));
                    if (changeState)
                    {
                        HeaderContent.Materials.Where(p => p.TransactionLineID.ToString().Equals(transId)).ToList()
                                                            .ForEach(i => i.UsingWeight = Convert.ToDecimal(strVal));

                        var coilExist = HeaderContent.CoilBacks.Where(p => p.TransactionLineID.ToString().Equals(transId)).ToList();

                        if (result.ValidateToCoilBackAuto(HeaderContent.CoilBackRoles, out risk, out msg) || coilExist.Count > 0)
                        {
                            MessageBox.Show("The remain weight to matched the coil back rule, and then we will Create/Update coil back.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            #region Bind data.

                            result.CBSelect = true;
                            CoilBackModel CBack = new CoilBackModel();
                            CBack.WorkOrderID = HeaderContent.WorkOrderID;
                            CBack.TransactionLineID = result.TransactionLineID;
                            CBack.CommodityCode = result.CommodityCode;
                            CBack.SpecCode = result.SpecCode;
                            CBack.CoatingCode = result.CoatingCode;
                            CBack.Thick = result.Thick;
                            CBack.Width = result.Width;
                            CBack.Length = result.Length;
                            CBack.Weight = result.RemainWeight;
                            CBack.Qty = result.RemainQuantity;
                            CBack.MCSSNo = result.MCSSNo;
                            CBack.OldSerial = result.SerialNo;
                            CBack.Gravity = result.Gravity;
                            CBack.FrontPlate = result.FrontPlate;
                            CBack.BackPlate = result.BackPlate;
                            CBack.Status = result.Status;
                            CBack.BussinessType = result.BussinessType;
                            CBack.Possession = result.Possession;
                            CBack.ProductStatus = Convert.ToInt32(result.ProductStatus);
                            CBack.Note = msg;

                            #endregion Bind data.

                            HeaderContent.CoilBacks = _repo.SaveCoilBack(epiSession, CBack).ToList();
                            result.CBSelect = true;
                        }
                        else if (coilExist.Count > 0 && result.RemainWeight == 0)
                        {
                            MessageBox.Show("The RemainWeight = 0 still have CoilBack then system will delete coil-back!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            HeaderContent.CoilBacks = _repo.DeleteCoilBack(epiSession, HeaderContent.WorkOrderID, result.TransactionLineID).ToList();
                            result.CBSelect = false;
                        }
                        result.CalculateUsingLength();
                    }
                    break;

                case "usingLengthM":
                    changeState = (result.UsingLengthM != Convert.ToDecimal(strVal));
                    string usingLengthM = dgvMaterial.Rows[e.RowIndex].Cells["usingLengthM"].Value.GetString();
                    string LengthM = dgvMaterial.Rows[e.RowIndex].Cells["LengthM"].Value.GetString();
                    decimal val = (Convert.ToDecimal(weight) * Convert.ToDecimal(usingLengthM)) / Convert.ToDecimal(LengthM);

                    result.UsingWeight = val;
                    result.UsingLengthM = Convert.ToDecimal(usingLengthM);
                    break;

                case "SelectCB":
                    bool chk = (bool)dgvMaterial.Rows[e.RowIndex].Cells["SelectCB"].Value;
                    changeState = (result.CBSelect != chk);

                    //When was value change.
                    if (changeState)
                    {
                        if (CheckCoilBackState(result) && chk)     //Insert/Update coil-back
                        {
                            DialogResult diaResulta = MessageBox.Show("Are you sure to add coil back.", "Row validate.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (diaResulta == DialogResult.Yes)
                            {
                                result.CBSelect = UpdateCoilBack(result, true);
                            }
                        }
                        else if (!chk)    //Delete coil-back
                        {
                            DialogResult diaResult = MessageBox.Show("Are you sure to delete coil-back.", "Row validate error!.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (diaResult == DialogResult.Yes)
                            {
                                result.CBSelect = UpdateCoilBack(result, false);
                            }
                        }
                    }

                    break;
            }

            if (changeState)
            {
                dgvMaterial.Rows[e.RowIndex].Cells["usingweight"].Value = result.UsingWeight;
                dgvMaterial.Rows[e.RowIndex].Cells["remainWeight"].Value = result.RemainWeight;
                dgvMaterial.Rows[e.RowIndex].Cells["RemQuantity"].Value = result.RemainQuantity;
                dgvMaterial.Rows[e.RowIndex].Cells["SelectCB"].Value = result.CBSelect;
                dgvMaterial.Rows[e.RowIndex].Cells["LengthM"].Value = result.LengthM;
                dgvMaterial.Rows[e.RowIndex].Cells["usingLengthM"].Value = result.UsingLengthM;
                dgvMaterial.Rows[e.RowIndex].Cells["remainLengthM"].Value = result.RemainLengthM;
                result.WorkDate = dptIssueDate.Value;
                var res = _repo.SaveMaterial(epiSession, result);
                HeaderContent.SumUsingWeight(HeaderContent.Materials);
                HeaderContent.CuttingDesign = HeaderContent.ReCalculateCuttingLine();
                ListCuttingGrid(HeaderContent.CuttingDesign);
                SetHeadContent(HeaderContent);
                ListCoilBackGrid(HeaderContent.CoilBacks);
            }
        }

        private void dptDueDate_ValueChanged(object sender, EventArgs e)
        {
            HeaderContent.DueDate = dptDueDate.Value;
        }

        private void dptIssueDate_ValueChanged(object sender, EventArgs e)
        {
            HeaderContent.IssueDate = dptIssueDate.Value;
        }

        private void processingThisLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCutting.Focused == false) return;
            string riskFlag = string.Empty;
            string msg = string.Empty;

            if (HeaderContent.CoilBacks.Count > 0)
            {
                MessageBox.Show("If would you like to calculate you must be deleted CoilBack before.", "Data invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (HeaderContent.ProcessLine.ResourceGrpID == "S")
            {
                HeaderContent.CuttingDesign = _repo.GenerateCuttingLine(epiSession, HeaderContent, out riskFlag, out msg).ToList();
                if (!string.IsNullOrEmpty(msg))
                {
                    MessageBox.Show(msg, "Data invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (HeaderContent.ProcessLine.ResourceGrpID == "R")
            {
            }
            //Do not delete this section.
            else if (HeaderContent.ProcessLine.ResourceGrpID == "L")
            {
                SimulateLeveller();
                LevellerResetCutting();
                MaterialReset();

                var result = HeaderContent.CuttingDesign.Where(i => i.CompleteRow == false);
                if (result.ToList().Count > 0)
                {
                    HeaderContent.Completed = 0;
                    HeaderContent.SimulateFlag = 0;
                }
                else
                {
                    //HeaderContent.Completed = 0;
                    HeaderContent.SimulateFlag = 1;
                }
                //HeaderContent.CuttingDesign = _repo.GenerateCuttingLineForLeveller(epiSession, HeaderContent, out riskFlag, out msg).ToList();
            }

            SetHeadContent(HeaderContent);
            ListCuttingGrid(HeaderContent.CuttingDesign);
            ListMaterialGrid(HeaderContent.Materials);
        }

        private void tbutCalculate_Click(object sender, EventArgs e)
        {
            //Simulated Complete.
            RecheckCuttingByRow();
            SetHeadContent(HeaderContent);
            ListCuttingGrid(HeaderContent.CuttingDesign);

            string objectValid;
            string messageValid;
            var result = HeaderContent.ValidateToSave(HeaderContent, out objectValid, out messageValid);
            if (result)
            {
                HeaderContent.FormState = 4;
                SetFormState();
            }
        }

        private void tbutNewCoilBack_Click(object sender, EventArgs e)
        {
        }

        private void tbutNewCutting_Click(object sender, EventArgs e)
        {
            CutDesignModel model = new CutDesignModel();

            if (HeaderContent.Materials.ToList().Count == 0)
            {
                model.Status = "S";
                model.Stand = 1;
                model.CutDivision = 1;
                model.DeliveryDate = DateTime.Now;
                model.BussinessType = string.IsNullOrEmpty(HeaderContent.BussinessType.GetString()) ? "" : HeaderContent.BussinessType;
            }
            else
            {
                var result = (from item in HeaderContent.Materials
                              select item).FirstOrDefault();

                model.Status = "S";
                model.DeliveryDate = DateTime.Now;
                model.BussinessType = string.IsNullOrEmpty(HeaderContent.BussinessType.GetString()) ? "" : HeaderContent.BussinessType;
                model.Possession = string.IsNullOrEmpty(HeaderContent.Possession.GetString()) ? 0 : Convert.ToInt32(HeaderContent.Possession);
                model.CommodityCode = result.CommodityCode;
                model.SpecCode = result.SpecCode;
                model.CoatingCode = result.CoatingCode;
                model.Thick = result.Thick;
                model.Width = result.Width;
                model.Length = result.Length;
                model.Stand = 1;
                model.CutDivision = 1;
                model.UnitWeight = HeaderContent.UsingWeight;
                model.TotalWeight = HeaderContent.UsingWeight;
                model.CustID = result.CustID;
                model.BussinessType = result.BussinessType;
                model.ProductStatus = string.IsNullOrEmpty(result.ProductStatus.GetString()) ? 0 : Convert.ToInt32(result.ProductStatus);
            }

            HeaderContent.CuttingDesign = _repo.SaveLineCutting(epiSession, HeaderContent, model).ToList();
            ListCuttingGrid(HeaderContent.CuttingDesign);
            //SetPermissCuttingDesign();
            //dgvCutting.Columns["thick1"].ReadOnly = false;
            //dgvCutting.Columns["width1"].ReadOnly = false;
            //dgvCutting.Columns["length1"].ReadOnly = false;
        }

        private void tbutNewMaterial_Click(object sender, EventArgs e)
        {
            //Selected Complete.
            HeaderContent.FormState = 3;
            SetFormState();
            butAddMaterial_Click(sender, e);
        }

        private void tbutNewWork_Click(object sender, EventArgs e)
        {
            HeaderContent = new PlanningHeadModel();
            HeaderContent.New(epiSession.PlantID);
            HeaderContent.PIC = epiSession.UserID;
            HeaderContent.PICName = epiSession.UserName;
            SetFormState();
            SetHeadContent(HeaderContent);
            //cmbProcessStep.SelectedIndex = 0;
        }

        private void tbutSave_Click(object sender, EventArgs e)
        {
            string objectValid;
            string messageValid;

            RecheckCuttingByRow();
            SetHeadContent(HeaderContent);
            ListCuttingGrid(HeaderContent.CuttingDesign);

            IEnumerable<MaterialModel> model = new List<MaterialModel>();
            HeaderContent.ProcessLine = _reRes.GetByID(epiSession.PlantID, HeaderContent.ProcessLineId);

            var result = HeaderContent.ValidateToSave(HeaderContent, out objectValid, out messageValid);

            if (!result)
            {
                MessageBox.Show(messageValid, "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (HeaderContent.ProcessLine.ResourceGrpID == "L" && HeaderContent.SimulateFlag == 1 && HeaderContent.Completed == 0 && HeaderContent.CheckYeild(HeaderContent.Yield))
                {
                    DialogResult diaResult = MessageBox.Show("This Order already simulated. Do you want to confirm.", "Question.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (diaResult == DialogResult.Yes)
                    {
                        HeaderContent.Completed = 1;
                    }
                }

                HeaderContent.OperationState = 1;
                HeaderContent = _repo.Save(epiSession, HeaderContent);
            }

            //Validate completed.
            HeaderContent.Saved();
            SetHeadContent(HeaderContent);
            SetFormState();

            //ListCuttingGrid need final step only.
            ListCuttingGrid(HeaderContent.CuttingDesign);
        }

        private void tbutSimulate_Click(object sender, EventArgs e)
        {
            if (HeaderContent.Completed == 1)
            {
                MessageBox.Show("This work order has completed process, can't re-simulate.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (HeaderContent.GenSerialFlag == 1)
            {
                MessageBox.Show("This work order has generated serial, can't re-simulate.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (HeaderContent.ProcessLine.ResourceGrpID == "S")
            {
                //Simulated Complete.
                var resExisting = _repo.GetSimulateAll(HeaderContent.WorkOrderID);
                if (resExisting.ToList().Count > 0)
                {
                    DialogResult diaResult = MessageBox.Show("Simulate line has already, are you sure to clear all.", "Question.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (diaResult == DialogResult.Yes)
                    {
                        _repo.ClearSimulateLines(HeaderContent.WorkOrderID);
                    }
                    else
                    {
                        return;
                    }
                }
                SimulateActionModel simModel = new SimulateActionModel();
                simModel.WorkOrderID = HeaderContent.WorkOrderID;
                simModel.WorkOrderNum = HeaderContent.WorkOrderNum;
                simModel.MaterialWeight = HeaderContent.InputWeight;
                simModel.ProductWeight = HeaderContent.OutputWeight;
                simModel.RewindWeight = HeaderContent.RewindWeight;
                simModel.Yield = HeaderContent.Yield;
                simModel.TrimWeight = HeaderContent.CuttingDesign.Where(i => i.Status.Equals("S")).Sum(i => i.TotalWeight);

                simModel.Cuttings = _repo.InsertSimulate(epiSession, HeaderContent).ToList();
                simModel.Materials = HeaderContent.Materials.ToList();
                using (SimulateEntry frm = new SimulateEntry(epiSession, HeaderContent, simModel))
                {
                    frm.ShowDialog();
                    HeaderContent = frm.HeadModel;
                }

                SetHeadContent(HeaderContent);
                ListMaterialGrid(HeaderContent.Materials);
                ListCuttingGrid(HeaderContent.CuttingDesign);
                ListCoilBackGrid(HeaderContent.CoilBacks);
                tbutCalculate_Click(sender, e);
                //tbutSave_Click(sender, e);
                HeaderContent.SimulateFlag = 1;
            }
            else if (HeaderContent.ProcessLine.ResourceGrpID == "R")
            {
                SimulateReshearHeadModel simModel = new SimulateReshearHeadModel();
                simModel.Materials = HeaderContent.Materials.ToList();
                simModel.Cuttings = HeaderContent.CuttingDesign.ToList();
                simModel.WorkOrderID = HeaderContent.WorkOrderID;
                simModel.WorkOrderNum = HeaderContent.WorkOrderNum;

                using (SimulateReShear frm = new SimulateReShear(epiSession, HeaderContent, simModel))
                {
                    frm.ShowDialog();
                    HeaderContent = frm.HeadModel;
                }
            }
        }

        private void tlbClear_Click(object sender, EventArgs e)
        {
            //Simulated Complete.
            HeaderContent.PreLoad();
            SetFormState();
            ClearHeaderContent();
            ResetMaterialGrid();
            ResetCuttingGrid();
            ResetCoilBackGrid();
        }

        private void tlbDelete_Click(object sender, EventArgs e)
        {
            if (OrderCompleted()) return;
            string validateMsg = string.Empty;

            //Delete Material list
            if (dgvMaterial.Focused && dgvMaterial.Rows.Count != 0)
            {
                int iRow = dgvMaterial.CurrentRow.Index;
                MaterialModel mat = _repo.GetMaterial(Convert.ToInt32(dgvMaterial.Rows[iRow].Cells["transactionlineid"].Value.ToString()));
                if (HeaderContent.ValidateToDelMaterial(mat, out validateMsg))
                {
                    if (_repo.DeleteMaterail(epiSession, mat, out validateMsg))
                    {
                        HeaderContent.Materials = _repo.GetAllMaterial(epiSession.PlantID, HeaderContent.WorkOrderID).ToList();
                        SetHeadContent(HeaderContent);

                        //Set Material Grid.
                        try
                        {
                            ListMaterialGrid(HeaderContent.Materials);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show(validateMsg, "Delete material error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(validateMsg, "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            //Delete Coil Back list
            if (dgvCoilBack.Focused) MessageBox.Show("dgvCoilBack");

            //Delete Cutting list
            if (dgvCutting.Focused && dgvCutting.Rows.Count != 0)
            {
                int iRow = dgvCutting.CurrentRow.Index;
                CutDesignModel cut = _repo.GetCuttingByID(Convert.ToInt32(dgvCutting.Rows[iRow].Cells["lineid"].Value.ToString()));
                if (_repo.DeleteCutting(epiSession, cut, out validateMsg))
                {
                    HeaderContent.CuttingDesign = _repo.GetCuttingLines(HeaderContent.WorkOrderID).ToList();
                    SetHeadContent(HeaderContent);
                    //if (dgvCutting.Rows.Count == 0) tbutNewCutting.Enabled = true;
                    //Set Cutting Grid.
                    try
                    {
                        ListCuttingGrid(HeaderContent.CuttingDesign);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "After deleted line to be set grid.");
                    }
                }
                else
                {
                    MessageBox.Show(validateMsg, "Delete Cutting error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void tbutCancelWorkOrder_Click(object sender, EventArgs e)
        {
        }

        private void WorkEntrySlitter_Load(object sender, EventArgs e)
        {
            HeaderContent.PreLoad();
            SetFormState();
        }

        private void addSOLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCutting.Rows.Count == 0) return;
            string lineId = dgvCutting.Rows[dgvCutting.CurrentRow.Index].Cells["lineid"].Value.GetString();
            string sono = dgvCutting.Rows[dgvCutting.CurrentRow.Index].Cells["sono"].Value.GetString();
            if (!string.IsNullOrEmpty(sono))
            {
                HeaderContent.CuttingDesign = addSOLine(Convert.ToInt32(lineId), sono);
            }
        }

        private void butAddCoilBack_Click(object sender, EventArgs e)
        {
            ListCuttingGrid(HeaderContent.CuttingDesign);
        }

        private void butConfirm_Click(object sender, EventArgs e)
        {
            if (HeaderContent.Completed == 1 && HeaderContent.SimulateFlag == 1 && HeaderContent.GenSerialFlag == 0)
            {
                DialogResult diaResulta = MessageBox.Show("Are you sure to unconfirm.", "Question.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (diaResulta == DialogResult.Yes)
                {
                    if (_repo.UnConfirmWork(HeaderContent.WorkOrderID))
                    {
                        MessageBox.Show("Unconfirmed Order completed.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HeaderContent = _repo.GetWorkById(HeaderContent.WorkOrderID, Convert.ToInt32(HeaderContent.ProcessStep), epiSession.PlantID);
                        SetHeadContent(HeaderContent);
                        ListMaterialGrid(HeaderContent.Materials);
                    }
                }
            }
        }

        private void butGenSN_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            IEnumerable<GeneratedSerialModel> serialLines = new List<GeneratedSerialModel>();
            IEnumerable<SimulateModel> simResult = new List<SimulateModel>();
            IEnumerable<SimulateModel> coilBack = new List<SimulateModel>();
            if (HeaderContent.GenSerialFlag == 0)
            {
                if (HeaderContent.ProcessLine.ResourceGrpID == "S")
                {
                    simResult = _repo.GetSimulateAll(HeaderContent.WorkOrderID);
                }
                else if (HeaderContent.ProcessLine.ResourceGrpID == "L")
                {
                    simResult = _repo.GetSimulateLeveller(HeaderContent.WorkOrderID);
                }

                coilBack = _repo.GetSimulateCoilBack(HeaderContent.WorkOrderID);

                if (coilBack.ToList().Count > 0)
                {
                    var cbResult = _repo.GenerateSerial(epiSession, coilBack, HeaderContent.WorkOrderID);
                }

                if (simResult.ToList().Count > 0)
                {
                    serialLines = _repo.GenerateSerial(epiSession, simResult, HeaderContent.WorkOrderID);
                    _repo.ImportSerialToEpicor(epiSession, HeaderContent, out msg);
                    HeaderContent.GenSerialFlag = 1;
                }
            }
            else
            {
                serialLines = _repo.GetSerialAllByWorkOrder(HeaderContent.WorkOrderID);
            }

            using (SerialList frm = new SerialList(epiSession, serialLines, HeaderContent))
            {
                frm.ShowDialog();
                HeaderContent.GenSerialFlag = Convert.ToInt32(frm.GenSNComplete);
                SetHeadContent(HeaderContent);
            }
        }

        private void butSimulate_Click(object sender, EventArgs e)
        {
            if (HeaderContent.ProcessLine.ResourceGrpID == "S")
            {
                SimulateActionModel simModel = new SimulateActionModel();
                simModel.WorkOrderID = HeaderContent.WorkOrderID;
                simModel.WorkOrderNum = HeaderContent.WorkOrderNum;
                simModel.MaterialWeight = HeaderContent.InputWeight;
                simModel.ProductWeight = HeaderContent.OutputWeight;
                simModel.Yield = HeaderContent.Yield;
                simModel.TrimWeight = HeaderContent.CuttingDesign.Where(i => i.Status.Equals("S")).Sum(i => i.TotalWeight);
                simModel.Cuttings = _repo.GetSimulateAll(HeaderContent.WorkOrderID).ToList();
                simModel.Materials = HeaderContent.Materials.ToList();
                using (SimulateEntry frm = new SimulateEntry(epiSession, HeaderContent, simModel))
                {
                    frm.ShowDialog();
                    HeaderContent = frm.HeadModel;
                }
            }

            SetHeadContent(HeaderContent);
            ListMaterialGrid(HeaderContent.Materials);
            ListCuttingGrid(HeaderContent.CuttingDesign);
            ListCoilBackGrid(HeaderContent.CoilBacks);
            dgvMaterial.EndEdit();
        }

        private void cmbOrderType_Leave(object sender, EventArgs e)
        {
            if (HeaderContent.CuttingDesign.ToList().Count > 0)
            {
                MessageBox.Show("Please clear all cutting design before change process line.", "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProcessLine.SelectedValue = HeaderContent.ProcessLine.ResourceID;
                return;
            }
        }

        private void dgvMaterial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        #endregion Form Events

        #region Methods

        public bool ValidateUsing(decimal usingVal, decimal baseVal, bool weightValid, out string col, out string msg)
        {
            bool valid = true;
            col = (weightValid) ? "Weight" : "Quantity";
            msg = string.Empty;

            if (usingVal > baseVal)
            {
                msg = string.Format("Using {0} must less than or equals with {0}.", (weightValid) ? "Weight" : "Quantity");
                valid = false;
            }
            return valid;
        }

        private IList<CutDesignModel> addSOLine(int lineID, string soNumber)
        {
            string risk = string.Empty;
            string msg = string.Empty;

            //Load data from current selected row to rowData.
            var rowData = (from item in HeaderContent.CuttingDesign
                           where item.LineID == Convert.ToInt32(lineID)
                           select item).FirstOrDefault();

            OrderDetailModel resultLine = new OrderDetailModel();
            IEnumerable<OrderDetailModel> resultParam = new List<OrderDetailModel>();
            SetDirectionPattern();

            resultParam = _repoSale.GetOrderDtlByFilter(soNumber, HeaderContent);
            using (OrderLineDialog frm = new OrderLineDialog(epiSession, resultParam))
            {
                frm.ShowDialog();
                resultLine = frm._selected;
                if (resultLine.OrderNum == null)
                {
                    return HeaderContent.CuttingDesign;
                }
            }

            HeaderContent.ClassID = resultLine.ClassID;
            HeaderContent.Possession = Convert.ToString(resultLine.Possession);
            HeaderContent.BussinessType = resultLine.BussinessType;

            rowData.SONo = soNumber;
            rowData.CustID = resultLine.CustID;
            rowData.SOLine = resultLine.OrderLine;
            rowData.NORNum = resultLine.NORNo;
            rowData.CommodityCode = resultLine.CommodityCode;
            rowData.SpecCode = resultLine.SpecCode;
            rowData.CoatingCode = resultLine.CoatingCode;
            rowData.Thick = resultLine.Thick;
            rowData.Width = resultLine.Width;
            rowData.Length = resultLine.Length;
            rowData.Status = "";
            rowData.SOWeight = resultLine.SOWeight;
            rowData.SOQuantity = resultLine.SOQuantity;
            rowData.QtyPack = resultLine.QtyPack;
            rowData.Pack = resultLine.Pack;
            rowData.BussinessType = resultLine.BussinessType;
            rowData.CompleteRow = rowData.ValidateByRow(HeaderContent, out risk, out msg);
            return _repo.SaveLineCutting(epiSession, HeaderContent, rowData).ToList();
        }

        private void LevellerResetCutting()
        {
            string riskFlag = string.Empty; string msg = string.Empty;
            foreach (var p in HeaderContent.CuttingDesign)
            {
                decimal bQty = 0;
                var result = HeaderContent.LevSimulates.Where(i => i.CuttingLineID == p.LineID).ToList();
                if (result.Count != 0)
                {
                    bQty = HeaderContent.LevSimulates.Where(i => i.CuttingLineID == p.LineID).Sum(i => i.CalQuantity).GetDecimal();
                }

                p.CalQuantity = bQty;
                p.CalculateRows(HeaderContent);
                p.CompleteRow = p.ValidateByRow(HeaderContent, out riskFlag, out msg);
                HeaderContent.CuttingDesign = _repo.SaveLineCutting(epiSession, HeaderContent, p).ToList();
            }
        }

        private void ListCoilBackGrid(IEnumerable<CoilBackModel> item)
        {
            int i = 0;
            if (dgvCoilBack.Rows.Count != 0) dgvCoilBack.Rows.Clear();
            foreach (var p in item)
            {
                dgvCoilBack.Rows.Add(p.TransactionLineID, p.Serial, p.CommodityCode + " - " + p.CommodityName, p.SpecCode + " - " + p.SpecName
                                    , p.CoatingCode + " - " + p.CoatingName, p.Thick, p.Width, p.Length, p.Weight, p.LengthM, p.MCSSNo
                                    , "", Enum.GetName(typeof(MaterialStatus), int.Parse(p.Status)), p.Note);

                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvCoilBack.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void ListCuttingGrid(IEnumerable<CutDesignModel> item)
        {
            int i = 0;
            if (dgvCutting.Rows.Count != 0) dgvCutting.Rows.Clear();
            foreach (var p in item)
            {
                dgvCutting.Rows.Add(p.LineID, i + 1, p.SONo, (p.SOLine == 0) ? "" : p.SOLine.ToString(), p.NORNum, p.CustID, p.CommodityCode, p.SpecCode
                                    , p.CoatingCode, p.Direction.GetString(), p.Thick, p.Width, p.Length, p.Status, p.Stand, p.CutDivision, p.Note
                                    , p.UnitWeight, p.TotalWeight, p.TotalLength, p.SOWeight, p.SOQuantity, p.QtyPerMaterial, p.CalQuantity, p.QtyPack, p.Pack
                                    , p.BussinessType + " - " + p.BussinessTypeName, Enum.GetName(typeof(Possession), p.Possession), p.CompleteRow);

                #region Set cells permission
                dgvCutting.Columns["direction"].Visible = false;
                dgvCutting.Columns["qtypermat"].Visible = false;
                dgvCutting.Rows[i].Cells["sono"].ReadOnly = true;
                dgvCutting.Rows[i].Cells["width1"].ReadOnly = true;
                dgvCutting.Rows[i].Cells["length1"].ReadOnly = true;
                dgvCutting.Rows[i].Cells["status1"].ReadOnly = true;
                dgvCutting.Rows[i].Cells["qtyPack1"].ReadOnly = true;
                dgvCutting.Rows[i].Cells["calqty"].ReadOnly = true;

                if (HeaderContent.Completed == 0)
                {
                    if (HeaderContent.ProcessLine.ResourceGrpID == "L")
                    {
                        dgvCutting.Rows[i].Cells["length1"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["soqty"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["qtyPack1"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["status1"].ReadOnly = false;
                    }

                    if (HeaderContent.ProcessLine.ResourceGrpID == "R")
                    {
                        dgvCutting.Columns["direction"].Visible = true;
                        dgvCutting.Columns["qtypermat"].Visible = true;
                        dgvCutting.Rows[i].Cells["direction"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["qtypermat"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["width1"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["length1"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["status1"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["qtyPack1"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["calqty"].ReadOnly = false;
                    }

                    if (HeaderContent.ProcessLine.ResourceGrpID == "S")
                    {
                        dgvCutting.Rows[i].Cells["width1"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["stand"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["status1"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["cutdiv"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["note1"].ReadOnly = false;
                    }

                    if (!string.IsNullOrEmpty(p.Status) && p.Status == "F")
                    {
                        this.dgvCutting.Rows[i].Cells["width1"].ReadOnly = true;
                        this.dgvCutting.Rows[i].Cells["length1"].ReadOnly = true;
                    }

                    if (p.CompleteRow)
                    {
                        dgvCutting.Rows[i].Cells["sono"].ReadOnly = true;
                    }
                    else
                    {
                        dgvCutting.Rows[i].Cells["sono"].ReadOnly = false;
                    }
                }

                #endregion Set cells permission

                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvCutting.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
            //SetPermissCuttingDesign();
            if (HeaderContent.ProcessLine.ResourceGrpID == "S")
            {
                if (dgvCutting.Rows.Count == 0)
                {
                    tbutNewCutting.Enabled = true;
                }
                else
                {
                    tbutNewCutting.Enabled = false;
                }
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

                if (HeaderContent.Completed == 1)
                {
                    dgvMaterial.Columns["usingLengthM"].ReadOnly = true;
                    dgvMaterial.Columns["usingweight"].ReadOnly = true;
                    dgvMaterial.Columns["quantity"].ReadOnly = true;
                    if (HeaderContent.GenSerialFlag == 1) dgvMaterial.Columns["SelectCB"].ReadOnly = true;
                }
                else
                {
                    if (p.Length == 0)
                    {
                        dgvMaterial.Columns["quantity"].ReadOnly = true;
                        dgvMaterial.Columns["usingweight"].ReadOnly = false;
                    }
                    else
                    {
                        dgvMaterial.Columns["quantity"].ReadOnly = false;
                        dgvMaterial.Columns["usingweight"].ReadOnly = true;
                    }

                    if (HeaderContent.ProcessLine.ResourceGrpID == "L")
                    {
                        dgvMaterial.Columns["usingLengthM"].ReadOnly = false;
                        dgvMaterial.Columns["usingweight"].ReadOnly = true;
                        dgvMaterial.Columns["quantity"].ReadOnly = true;
                    }
                    else
                    {
                        dgvMaterial.Columns["usingLengthM"].ReadOnly = true;
                    }
                    dgvMaterial.Columns["SelectCB"].ReadOnly = false;
                }

                i++;
            }
        }

        private void MaterialReset()
        {
            string riskFlag = string.Empty; string msg = string.Empty;
            foreach (var m in HeaderContent.Materials)
            {
                decimal bQty = 0;
                var result = HeaderContent.LevSimulates.Where(i => i.MaterialTransLineID == m.TransactionLineID).ToList();
                if (result.Count != 0)
                {
                    bQty = HeaderContent.LevSimulates.Where(i => i.MaterialTransLineID == m.TransactionLineID).Min(i => i.RemainLengthM);
                }

                m.WorkDate = HeaderContent.IssueDate;
                m.UsingLengthM = m.LengthM - bQty;
                m.ConvertUsingLengthToUsingWeight();
                var res = _repo.SaveMaterial(epiSession, m);
            }

            HeaderContent.Materials = _repo.GetAllMaterial(epiSession.PlantID, HeaderContent.WorkOrderID).ToList();
        }

        private void RecheckCuttingByRow()
        {
            int i = 0;
            foreach (DataGridViewRow row in dgvCutting.Rows)
            {
                dgvCutting.CurrentCell = dgvCutting[1, i];
                i++;
            }
        }

        private void SetDirectionPattern()
        {
            HeaderContent.Class = new ClassMasterModel();
            if (HeaderContent.Class == null) HeaderContent.Class = new ClassMasterModel();

            //Default direction of material pattern by Machine.
            HeaderContent.ProcessLine = _reRes.GetByID(epiSession.PlantID, cmbProcessLine.SelectedValue.ToString());

            //When we selected materials, need to fix direction of material pattern.
            if (HeaderContent.Materials.ToList().Count > 0)
            {
                var mat = HeaderContent.Materials.FirstOrDefault();

                //Change default direction of material pattern.
                HeaderContent.ProcessLine.ThickMin = mat.Thick;
                HeaderContent.ProcessLine.ThickMax = mat.Thick;
                HeaderContent.ProcessLine.WidthMin = mat.Width;
                HeaderContent.ProcessLine.WidthMax = mat.Width;
                HeaderContent.ProcessLine.LengthMin = mat.Length;
                HeaderContent.ProcessLine.LengthMax = mat.Length;
            }

            //Verify to make sure there already Class pattern.
            if (HeaderContent.ClassID == 0)
            {
                //HeaderContent.CurrentClass.CustomerReq = 1;
                HeaderContent.Class.ComudityReq = 1;
                HeaderContent.Class.SpecCodeReq = 1;
                HeaderContent.Class.PlateCodeReq = 1;
            }
            else
            {
                HeaderContent.Class = _repoCls.GetByID(epiSession.PlantID, HeaderContent.ClassID);
            }
        }

        private bool UpdateCoilBack(MaterialModel result, bool chk)
        {
            bool bolResult = false;
            if (chk)
            {
                #region Bind data.

                result.CBSelect = true;
                CoilBackModel CBack = new CoilBackModel();
                CBack.WorkOrderID = HeaderContent.WorkOrderID;
                CBack.TransactionLineID = result.TransactionLineID;
                CBack.CommodityCode = result.CommodityCode;
                CBack.SpecCode = result.SpecCode;
                CBack.CoatingCode = result.CoatingCode;
                CBack.Thick = result.Thick;
                CBack.Width = result.Width;
                CBack.Length = result.Length;
                CBack.Weight = result.RemainWeight;
                CBack.Qty = result.RemainQuantity;
                CBack.MCSSNo = result.MCSSNo;
                CBack.OldSerial = result.SerialNo;
                CBack.Gravity = result.Gravity;
                CBack.FrontPlate = result.FrontPlate;
                CBack.BackPlate = result.BackPlate;
                CBack.Status = result.Status;
                CBack.BussinessType = result.BussinessType;
                CBack.Possession = result.Possession;
                CBack.ProductStatus = Convert.ToInt32(result.ProductStatus);
                CBack.Note = "Add manual";

                #endregion Bind data.

                HeaderContent.CoilBacks = _repo.SaveCoilBack(epiSession, CBack).ToList();
                bolResult = true;
            }
            else if (!chk)
            {
                HeaderContent.CoilBacks = _repo.DeleteCoilBack(epiSession, HeaderContent.WorkOrderID, result.TransactionLineID).ToList();
                bolResult = false;
            }

            return bolResult;
        }

        private bool OrderCompleted()
        {
            if (HeaderContent.Completed == 1)
            {
                MessageBox.Show("Work Order has confirmed. if you want to update, please unconfirmed before.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SimulateLeveller()
        {
            HeaderContent.LevSimulates = _repo.GetLevellerSimAll(HeaderContent.WorkOrderID).ToList();
            HeaderContent.Materials = _repo.GetAllMaterial(epiSession.PlantID, HeaderContent.WorkOrderID).ToList();

            #region Looping by materials

            foreach (var mat in HeaderContent.Materials.Where(m => m.RemainLengthM > 0).OrderBy(m => m.TransactionLineID))
            {
                //Initial remaining material length from using length(Meter* 1,000).
                decimal matRemLength = mat.RemainLengthM * 1000;

                #region Looping by materials

                foreach (var item in HeaderContent.CuttingDesign.Where(i => i.CalQuantity != i.SOQuantity))
                {
                    //Summary quantity for a cutting design has simulated from simulation lines and assign to [bQty].
                    decimal bQty = HeaderContent.LevSimulates.Where(p => p.CuttingLineID == item.LineID).Sum(i => i.CalQuantity);

                    #region Conditions implement

                    /*
                     * Quantity already simulated[bQty] if not equal SO Qty.
                     * Remaining material length greater than cutting length per unit.
                     */
                    if (item.SOQuantity != bQty && matRemLength >= item.Length)
                    {
                        //For remaining material length can be used to cutting possible and assign to [calQty:Value is possible].
                        decimal calQty = Math.Floor(matRemLength / item.Length);

                        #region Calculate by conditional.

                        /*
                         * calQty           : จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่
                         * bQty             : จำนวนที่ Simulate ไปแล้วสำหรับ Cut Design นี้
                         * item.SOQuantity  : จำนวนที่ต้องตัดทั้งหมด
                         * item.CalQuantity : จำนวนที่สั่งตัดหลังจากการคำนวณ
                         * item.Length      : ความยาวของชิ้นงานที่ตัดใน 1 หน่วย
                        */

                        #region ยังไม่ยอดเคยถูก Simulate

                        if (bQty == 0)
                        {
                            #region จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่ เหลือมากกว่าจำนวนที่สั่ง

                            if (calQty >= item.SOQuantity)
                            {
                                //จำนวนที่สั่งตัดหลังจากการคำนวณ = (จำนวนที่สั่ง)
                                item.CalQuantity = item.SOQuantity;
                                item.CompleteRow = true;

                                //ความยาว Material ที่เหลือ (มิลลิเมตร) = (จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่ - จำนวนที่สั่งตัดหลังจากการคำนวณ) * ความยาวต่อชิ้นงาน (มิลลิเมตร)
                                matRemLength = Math.Floor((calQty - (item.CalQuantity)) * item.Length);
                            }

                            #endregion จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่ เหลือมากกว่าจำนวนที่สั่ง

                            #region จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่ เหลือน้อยกว่าจำนวนที่สั่ง

                            else if (calQty < item.SOQuantity)
                            {
                                //จำนวนที่สั่งตัดหลังจากการคำนวณ = (จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่)
                                item.CalQuantity = calQty;
                                item.CompleteRow = false;

                                //ความยาว Material ที่เหลือ (มิลลิเมตร) = 0
                                matRemLength = 0;
                            }

                            #endregion จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่ เหลือน้อยกว่าจำนวนที่สั่ง
                        }

                        #endregion ยังไม่ยอดเคยถูก Simulate

                        #region มียอดที่ Simulate ไปบ้างแล้ว

                        else if (bQty != 0)
                        {
                            #region จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่ เหลือมากกว่า [(จำนวนที่สั่ง) - (ยอดที่ Simulate ไปก่อนหน้านี้)]

                            if (calQty >= (item.SOQuantity - bQty))
                            {
                                //จำนวนที่สั่งตัดหลังจากการคำนวณ = (จำนวนที่สั่ง) - (มียอดที่ Simulate ไปก่อนหน้านี้)
                                item.CalQuantity = item.SOQuantity - bQty;
                                item.CompleteRow = true;

                                // ความยาว Material ที่เหลือ (มิลลิเมตร) = [จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่ - (จำนวนที่สั่งตัดหลังจากการคำนวณ)] * ความยาวต่อชิ้นงาน (มิลลิเมตร)
                                matRemLength = (calQty - (item.CalQuantity)) * item.Length;
                            }

                            #endregion จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่ เหลือมากกว่า [(จำนวนที่สั่ง) - (ยอดที่ Simulate ไปก่อนหน้านี้)]

                            #region จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่ เหลือน้อยกว่า [(จำนวนที่สั่ง) - (ยอดที่ Simulate ไปก่อนหน้านี้)]

                            else if (calQty < item.SOQuantity - bQty)
                            {
                                //จำนวนที่สั่งตัดหลังจากการคำนวณ = (จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่)
                                item.CalQuantity = calQty;

                                //ถ้าหากจำนวนที่ตัด + จำนวนที่ Simulate ไปแล้ว = SO Qty จะเป็นจริง ไม่เช่น จะเป็นเท็จ
                                item.CompleteRow = (item.SOQuantity == (item.CalQuantity + bQty));

                                //ความยาว Material ที่เหลือ (มิลลิเมตร) = 0
                                matRemLength = 0;
                            }

                            #endregion จำนวนชิ้นงานที่สามารถสั่งตัดได้ทั้งหมด โดยคิดจากความยาว Material ที่มีอยู่ เหลือน้อยกว่า [(จำนวนที่สั่ง) - (ยอดที่ Simulate ไปก่อนหน้านี้)]
                        }

                        #endregion มียอดที่ Simulate ไปบ้างแล้ว

                        #endregion Calculate by conditional.

                        LevellerSimulateModel lev = new LevellerSimulateModel();
                        lev.Plant = epiSession.PlantID;
                        lev.WorkOrderID = HeaderContent.WorkOrderID;
                        lev.CuttingLineID = item.LineID;
                        lev.MaterialTransLineID = mat.TransactionLineID;
                        lev.SOQuantity = Convert.ToInt32(item.SOQuantity);
                        lev.CalQuantity = Convert.ToInt32(item.CalQuantity);
                        lev.RemainWeight = 0;// item.CalUnitWgt(item.Thick, item.Width, item.Length, item.Gravity, item.FrontPlate, item.BackPlate);
                        lev.RemainLength = matRemLength;
                        lev.RemainLengthM = matRemLength / 1000;
                        lev.UsingLength = item.CalQuantity * item.Length;
                        lev.UsingLengthM = (item.CalQuantity * item.Length) / 1000;
                        HeaderContent.LevSimulates = _repo.SaveLevellerSimulate(epiSession, lev).ToList();
                        item.CalculateRows(HeaderContent);
                    }

                    #endregion Conditions implement
                }

                #endregion Looping by materials
            }

            #endregion Looping by materials
        }

        #endregion Methods
    }
}