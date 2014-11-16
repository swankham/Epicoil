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
        private readonly IResourceRepo _repoRes;

        private readonly IUserCodeRepo _repoUcd;
        private readonly IWorkEntryRepo _repo;
        private readonly IResourceRepo _reRes;
        private readonly ISaleOrderRepo _repoSale;
        public readonly IClassMasterRepo _repoCls;

        private PlanningHeadModel HeaderContent;
        private ClassMasterModel _class;

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

        #region Set Form Properties

        private void SetFormState()
        {
            switch (HeaderContent.FormState)
            {
                case 0: /// 0 = Nothing.
                    tbutNewWork.Enabled = true;
                    tbutNewMaterial.Enabled = false;
                    tbutNewCutting.Enabled = false;
                    //tbutNewCoilBack.Enabled = false;
                    tbutSave.Enabled = false;
                    tbutCalculate.Enabled = false;
                    tbutSimulate.Enabled = false;
                    tbutCreateSerial.Enabled = false;
                    tlbClear.Enabled = false;
                    tbutCancelWorkOrder.Enabled = false;
                    butWorkOrder.Enabled = true;
                    break;

                case 1: /// 1 = New Transaction.
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = false;
                    tbutNewCutting.Enabled = false;
                    //tbutNewCoilBack.Enabled = false;
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

                case 2: /// 2 = Transaction was save.
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = true;
                    tbutNewCutting.Enabled = true;
                    tlbClear.Enabled = true;
                    butWorkOrder.Enabled = false;
                    break;

                case 3: /// 3 = Selected materail.
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = true;
                    tbutNewCutting.Enabled = true;
                    //tbutNewCoilBack.Enabled = false;
                    tbutSave.Enabled = true;
                    tbutCalculate.Enabled = true;
                    tbutSimulate.Enabled = false;
                    tbutCreateSerial.Enabled = false;
                    tlbClear.Enabled = true;
                    tbutCancelWorkOrder.Enabled = true;
                    butWorkOrder.Enabled = false;
                    break;

                case 4: /// 4 = Calculated.
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = true;
                    tbutNewCutting.Enabled = true;
                    //tbutNewCoilBack.Enabled = true;
                    tbutSave.Enabled = true;
                    tbutCalculate.Enabled = true;
                    tbutSimulate.Enabled = true;
                    tbutCreateSerial.Enabled = true;
                    tlbClear.Enabled = true;
                    tbutCancelWorkOrder.Enabled = true;
                    butWorkOrder.Enabled = false;
                    break;
            }
            butAddMaterial.Enabled = tbutNewMaterial.Enabled;
        }

        private void SetHeadContent(PlanningHeadModel model)
        {
            ClearHeaderContent();
            model.PIC = epiSession.UserID;
            model.PICName = epiSession.UserName;
            //TextBox
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

            //ComboBox
            cmbProcessLine.DataSource = model.ResourceList.ToList();
            cmbProcessLine.DisplayMember = "ResourceDescription";
            cmbProcessLine.ValueMember = "ResourceID";
            cmbProcessLine.DataBindings.Add("SelectedValue", model, "ProcessLineId", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbOrderType.DataSource = model.OrderTypeList.ToList();
            cmbOrderType.DisplayMember = "CodeDesc";
            cmbOrderType.ValueMember = "CodeID";
            cmbOrderType.DataBindings.Add("SelectedValue", model, "OrderType", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbPossession.DataSource = model.PossessionList.ToList();
            cmbPossession.DisplayMember = "CodeDesc";
            cmbPossession.ValueMember = "CodeID";
            cmbPossession.DataBindings.Add("SelectedValue", model, "Possession", false, DataSourceUpdateMode.OnPropertyChanged);

            //CheckBox
            chkPackingPlan.DataBindings.Add("Checked", model, "PackingPlan", false, DataSourceUpdateMode.OnPropertyChanged);
            chkLVTrim.DataBindings.Add("Checked", model, "LVTrim", false, DataSourceUpdateMode.OnPropertyChanged);

            butSimulate.Enabled = Convert.ToBoolean(model.SimulateFlag);
            butSimulate.Text = model.SimulateFlagStr;
            butGenSN.Enabled = Convert.ToBoolean(model.GenSerialFlag);
            butGenSN.Text = model.GenSerialFlagStr;

            //DatePicker
            dptIssueDate.Value = model.IssueDate;
            dptDueDate.Value = model.DueDate;

            if (model.CheckYeild(model.Yield))
            {
                if (model.Completed == 2) model.Completed = Convert.ToInt32(_repo.UnlockHold(HeaderContent.WorkOrderID));
                txtYield.BackColor = SystemColors.Control;
                if (model.SimulateFlag == 1)
                {
                    butConfirm.Text = model.CompletedStr;
                    butConfirm.Visible = true;
                }
                else
                {
                    butConfirm.Visible = false;
                }

                if (model.Completed == 1)
                {
                    butConfirm.BackColor = Color.FromArgb(161, 205, 95);
                }
                else if (model.Completed == 0)
                {
                    butConfirm.BackColor = Color.Yellow;
                }
                else
                {
                    butConfirm.BackColor = Color.Red;
                }
            }
            else
            {
                txtYield.BackColor = Color.Yellow;
            }

            butConfirm.Visible = Convert.ToBoolean(model.Completed);
            butConfirm.Text = model.CompletedStr;

            if (model.Completed == 1)
            {
                butConfirm.BackColor = Color.FromArgb(161, 205, 95);
                butGenSN.Enabled = true;
                butGenSN.Text = model.GenSerialFlagStr.Replace("_", " ");
            }
            else if (model.Completed == 0)
            {
                butConfirm.BackColor = Color.Yellow;
            }
            else
            {
                butConfirm.BackColor = Color.Red;
            }
        }

        private void ClearHeaderContent()
        {
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

            //ComboBox
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

            //CheckBox
            chkLVTrim.DataBindings.Clear();
            chkPackingPlan.DataBindings.Clear();
            chkPackingPlan.Checked = false;
            chkLVTrim.Checked = false;

            //DatePicker
            dptIssueDate.Value = DateTime.Now;
            dptDueDate.Value = DateTime.Now;

            //Clear Content
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
        }

        private void ResetMaterialGrid()
        {
            dgvMaterial.Rows.Clear();
        }

        private void ResetCoilBackGrid()
        {
            dgvCoilBack.Rows.Clear();
        }

        private void ResetCuttingGrid()
        {
            dgvCutting.Rows.Clear();
        }

        private void SetPermissCuttingDesign()
        {
            dgvCutting.Columns["thick1"].ReadOnly = true;
            dgvCutting.Columns["width1"].ReadOnly = true;
            dgvCutting.Columns["length1"].ReadOnly = true;
            dgvCutting.Columns["status1"].ReadOnly = true;
            dgvCutting.Columns["stand"].ReadOnly = true;
            dgvCutting.Columns["cutdiv"].ReadOnly = true;

            if (HeaderContent.ProcessLineDetail.ResourceGrpID == "S")
            {
                dgvCutting.Columns["width1"].ReadOnly = false;
                dgvCutting.Columns["status1"].ReadOnly = false;
                dgvCutting.Columns["stand"].ReadOnly = false;
                dgvCutting.Columns["cutdiv"].ReadOnly = false;
                dgvCutting.Columns["qtyPack1"].ReadOnly = true;
            }
            else if (HeaderContent.ProcessLineDetail.ResourceGrpID == "L")
            {
                dgvCutting.Columns["length1"].ReadOnly = false;
                dgvCutting.Columns["status1"].ReadOnly = false;
                dgvCutting.Columns["qtyPack1"].ReadOnly = false; //calqty
            }
            else if (HeaderContent.ProcessLineDetail.ResourceGrpID == "R")
            {
                dgvCutting.Columns["width1"].ReadOnly = false;
                dgvCutting.Columns["length1"].ReadOnly = false;
                dgvCutting.Columns["status1"].ReadOnly = false;
                dgvCutting.Columns["qtyPack1"].ReadOnly = true;
            }
        }

        #endregion Set Form Properties

        #region Evnet

        private void WorkEntrySlitter_Load(object sender, EventArgs e)
        {
            HeaderContent.PreLoad();
            SetFormState();
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
            //if (OrderCompleted()) return;
            string objectValid;
            string messageValid;

            RecheckCuttingByRow();
            HeaderContent.CalculationHeader(HeaderContent);
            SetHeadContent(HeaderContent);
            ListCuttingGrid(HeaderContent.CuttingDesign);

            IEnumerable<MaterialModel> model = new List<MaterialModel>();
            HeaderContent.ProcessLineDetail = _reRes.GetByID(epiSession.PlantID, HeaderContent.ProcessLineId);

            var result = HeaderContent.ValidateToSave(HeaderContent, out objectValid, out messageValid);

            if (!result)
            {
                MessageBox.Show(messageValid, "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (HeaderContent.ProcessLineDetail.ResourceGrpID == "L" && HeaderContent.SimulateFlag == 1 && HeaderContent.Completed == 0)
                {
                    DialogResult diaResult = MessageBox.Show("This Order already simulated. Do you want to confirm.", "Question.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (diaResult == DialogResult.Yes)
                    {
                        HeaderContent.Completed = 1;
                    }
                }

                HeaderContent = _repo.Save(epiSession, HeaderContent);
            }

            //Validate complated.
            HeaderContent.Saved();
            SetHeadContent(HeaderContent);
            SetFormState();

            //ListCuttingGrid need final step only.
            ListCuttingGrid(HeaderContent.CuttingDesign);
        }

        private void tbutNewMaterial_Click(object sender, EventArgs e)
        {
            //Selected Complate.
            HeaderContent.FormState = 3;
            SetFormState();
            butAddMaterial_Click(sender, e);
        }

        private void tbutNewCoilBack_Click(object sender, EventArgs e)
        {
        }

        private void tbutSimulate_Click(object sender, EventArgs e)
        {
            if (HeaderContent.Completed == 1) return;
            //Simulated Complate.
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
            simModel.Materials = HeaderContent.Materails.ToList();
            using (SimulateEntry frm = new SimulateEntry(epiSession, HeaderContent, simModel))
            {
                frm.ShowDialog();
                HeaderContent = frm.HeadModel;
            }
            ListMaterialGrid(HeaderContent.Materails);
            ListCuttingGrid(HeaderContent.CuttingDesign);
            tbutCalculate_Click(sender, e);
            SetHeadContent(HeaderContent);
            tbutSave_Click(sender, e);
            HeaderContent.SimulateFlag = 1;
            SetFormState();
        }

        private void tlbClear_Click(object sender, EventArgs e)
        {
            //Simulated Complate.
            HeaderContent.PreLoad();
            SetFormState();
            ClearHeaderContent();
            ResetMaterialGrid();
            ResetCuttingGrid();
            ResetCoilBackGrid();
        }

        private void tbutCalculate_Click(object sender, EventArgs e)
        {
            //Simulated Complate.
            RecheckCuttingByRow();
            HeaderContent.CalculationHeader(HeaderContent);
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

        private void butAddMaterial_Click(object sender, EventArgs e)
        {
            if (OrderCompleted()) return;
            //Initialization
            HeaderContent.FormState = 3;
            SetFormState();
            HeaderContent.MaterialPattern = new MaterialModel();

            //Default direction for all (Class/Machine etc.).
            SetDirectionPatter();

            //If material list > 0 must be assign materail filtering.
            if (HeaderContent.Materails.ToList().Count > 0 && HeaderContent.CuttingDesign.ToList().Count == 0)
            {
                //HeaderContent.MaterialPattern.CommodityCode = mat.CommodityCode;
                //HeaderContent.MaterialPattern.SpecCode = mat.SpecCode;
                //HeaderContent.MaterialPattern.CoatingCode = mat.CoatingCode;
                //HeaderContent.MaterialPattern.Possession = mat.Possession;
                //HeaderContent.MaterialPattern.BussinessType = mat.BussinessType;
            }

            //If material list > 0 must be assign materail filtering.
            if (HeaderContent.Materails.ToList().Count == 0 && HeaderContent.CuttingDesign.ToList().Count > 0)
            {
                //var nor = HeaderContent.CuttingDesign.FirstOrDefault();
                //HeaderContent.MaterialPattern.CommodityCode = nor.CommodityCode;
                //HeaderContent.MaterialPattern.SpecCode = nor.SpecCode;
                //HeaderContent.MaterialPattern.CoatingCode = nor.CoatingCode;
                //HeaderContent.MaterialPattern.Possession = nor.Possession;
                //HeaderContent.MaterialPattern.Thick = nor.Thick;
                //HeaderContent.MaterialPattern.Width = nor.Width;
                //HeaderContent.MaterialPattern.Length = nor.Length;
                //HeaderContent.MaterialPattern.BussinessType = mat.BussinessType;
            }

            //Get Material that compatible with Machine was select.
            var result = _repo.GetAllMatByFilter(epiSession.PlantID, HeaderContent);
            using (MaterialSelecting frm = new MaterialSelecting(epiSession, result, HeaderContent))
            {
                frm.ShowDialog();
                HeaderContent.Materails = _repo.GetAllMaterial(epiSession.PlantID, HeaderContent.WorkOrderID).ToList();

                //
                if (string.IsNullOrEmpty(HeaderContent.Possession))
                {
                    HeaderContent.Possession = frm._selected.Possession.GetString();
                }
            }

            //Set Material Grid.
            try
            {
                ListMaterialGrid(HeaderContent.Materails);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //Summaries using weight
            HeaderContent.SumInputWeight(HeaderContent);
            HeaderContent.SumUsingWeight(HeaderContent.Materails);
            //Set content and list Material was add from dialog.
            SetHeadContent(HeaderContent);
            ListCuttingGrid(HeaderContent.CuttingDesign);
        }

        private void butWorkOrder_Click(object sender, EventArgs e)
        {
            using (CoilBackRuleDialog frm = new CoilBackRuleDialog(epiSession))
            {
                frm.ShowDialog();
                txtWorkOrderNum.Text = frm.Code;
            }
        }

        private void dptIssueDate_ValueChanged(object sender, EventArgs e)
        {
            HeaderContent.IssueDate = dptIssueDate.Value;
        }

        private void dptDueDate_ValueChanged(object sender, EventArgs e)
        {
            HeaderContent.DueDate = dptDueDate.Value;
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
                        HeaderContent.Materails = _repo.GetAllMaterial(epiSession.PlantID, HeaderContent.WorkOrderID).ToList();
                        SetHeadContent(HeaderContent);

                        //Set Material Grid.
                        try
                        {
                            ListMaterialGrid(HeaderContent.Materails);
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
                    HeaderContent.CuttingLines = _repo.GetCuttingLines(HeaderContent.WorkOrderID).ToList();
                    SetHeadContent(HeaderContent);
                    //if (dgvCutting.Rows.Count == 0) tbutNewCutting.Enabled = true;
                    //Set Cutting Grid.
                    try
                    {
                        ListCuttingGrid(HeaderContent.CuttingLines);
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

        private void dgvMaterial_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (OrderCompleted()) return;
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

            var result = (from item in HeaderContent.Materails
                          where item.TransactionLineID == Convert.ToInt32(transId)
                          select item).First();

            switch (colName)
            {
                case "usingweight":
                    changeState = (result.UsingWeight != Convert.ToDecimal(strVal));
                    HeaderContent.Materails.Where(p => p.TransactionLineID.ToString().Equals(transId)).ToList()
                                                        .ForEach(i => i.UsingWeight = Convert.ToDecimal(strVal));

                    var coilExist = HeaderContent.CoilBacks.Where(p => p.TransactionLineID.ToString().Equals(transId)).ToList();

                    if (result.ValidateToCoilBackAuto(HeaderContent.CoilBackRoles, out risk, out msg) || coilExist.Count > 0)
                    {
                        MessageBox.Show("The remain weight to matched the coil back rule, and then we will Create/Update coil back.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        result.CBSelect = true;
                        CoilBackModel CBack = new CoilBackModel();
                        CBack.WorkOrderID = HeaderContent.WorkOrderID;
                        CBack.TransactionLineID = result.TransactionLineID;
                        //CBack.SeqID
                        //CBack.Serial = result.SerialNo;
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
                        //CBack.BackStep
                        CBack.Status = result.Status;
                        CBack.BussinessType = result.BussinessType;
                        CBack.Possession = result.Possession;
                        CBack.ProductStatus = Convert.ToInt32(result.ProductStatus);
                        CBack.Note = msg;

                        HeaderContent.CoilBacks = _repo.SaveCoilBack(epiSession, CBack).ToList();
                    }
                    else if (coilExist.Count > 0 && result.RemainWeight == 0)
                    {
                        MessageBox.Show("The RemainWeight = 0 still have CoilBack then system will delete coilback!", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HeaderContent.CoilBacks = _repo.DeleteCoilBack(epiSession, HeaderContent.WorkOrderID, result.TransactionLineID).ToList();
                    }
                    result.CalculateUsingLength();
                    break;

                case "usingLengthM":
                    changeState = (result.UsingLengthM != Convert.ToDecimal(strVal));
                    string usingLengthM = dgvMaterial.Rows[e.RowIndex].Cells["usingLengthM"].Value.GetString();
                    string LengthM = dgvMaterial.Rows[e.RowIndex].Cells["LengthM"].Value.GetString();
                    decimal val = (Convert.ToDecimal(weight) * Convert.ToDecimal(usingLengthM)) / Convert.ToDecimal(LengthM);

                    result.UsingWeight = val;
                    result.UsingLengthM = Convert.ToDecimal(usingLengthM);
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
                HeaderContent.SumUsingWeight(HeaderContent.Materails);
                HeaderContent.CuttingDesign = HeaderContent.ReCalculateCuttingLine();
                ListCuttingGrid(HeaderContent.CuttingDesign);
                SetHeadContent(HeaderContent);
                ListCoilBackGrid(HeaderContent.CoilBacks);
            }
        }

        private void cmbPossession_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void cmbPossession_Leave(object sender, EventArgs e)
        {
            if (HeaderContent.Materails.ToList().Count > 0)
            {
                int result = HeaderContent.Materails.Max(p => p.Possession);
                if (cmbPossession.SelectedValue.ToString() != result.ToString())
                {
                    MessageBox.Show("Please select 'Possession' that compatable with material.", "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbPossession.SelectedValue = result.ToString();
                    return;
                }
            }

            if (HeaderContent.CuttingDesign.ToList().Count > 0)
            {
                int result = HeaderContent.CuttingDesign.Max(p => p.Possession);
                if (cmbPossession.SelectedValue.ToString() != result.ToString())
                {
                    MessageBox.Show("Please select 'Possession' that compatable with cutting design.", "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbPossession.SelectedValue = result.ToString();
                    return;
                }
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
                    HeaderContent.SumUsingWeight(HeaderContent.Materails);
                    //Set content and list Material was add from dialog.
                    SetHeadContent(HeaderContent);
                    //Set Material Grid.
                    try
                    {
                        ListMaterialGrid(HeaderContent.Materails);
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

        private void tbutNewCutting_Click(object sender, EventArgs e)
        {
            CutDesignModel model = new CutDesignModel();

            if (HeaderContent.Materails.ToList().Count == 0)
            {
                model.Status = "S";
                model.Stand = 1;
                model.CutDivision = 1;
                model.DeliveryDate = DateTime.Now;
                model.BussinessType = string.IsNullOrEmpty(HeaderContent.BussinessType.GetString()) ? "" : HeaderContent.BussinessType;
            }
            else
            {
                var result = (from item in HeaderContent.Materails
                              select item).First();

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

        private void cmbProcessLine_Leave(object sender, EventArgs e)
        {
            if (HeaderContent.Materails.ToList().Count > 0)
            {
                MessageBox.Show("Please clear all material before change process line.", "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProcessLine.SelectedValue = HeaderContent.ProcessLineDetail.ResourceID;
                return;
            }

            if (HeaderContent.CuttingLines.ToList().Count > 0)
            {
                MessageBox.Show("Please clear all cutting design before change process line.", "Validate data error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProcessLine.SelectedValue = HeaderContent.ProcessLineDetail.ResourceID;
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
                       select item).First();
            switch (colName)
            {
                case "sono":

                    #region Verify S/O No.

                    if (HeaderContent.Materails.ToList().Count == 0) HeaderContent.BussinessType = "";
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
                                       select item).First();
                            dgvCutting.Rows[e.RowIndex].Cells["sono"].Value = result.OrderNumber;
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
                        if (HeaderContent.ProcessLineDetail.ResourceGrpID == "L")
                        {
                            if (resultValue == "F" && !string.IsNullOrEmpty(rowData.NORNum))
                            {
                                dgvCutting.Rows[e.RowIndex].Cells["length1"].ReadOnly = true;
                                //dgvCutting.Rows[e.RowIndex].Cells["calqty"].ReadOnly = true;
                                //dgvCutting.Rows[e.RowIndex].Cells["qtyPack1"].ReadOnly = true;
                            }
                            else
                            {
                                dgvCutting.Rows[e.RowIndex].Cells["length1"].ReadOnly = false;
                                //dgvCutting.Rows[e.RowIndex].Cells["calqty"].ReadOnly = true;
                                //dgvCutting.Rows[e.RowIndex].Cells["qtyPack1"].ReadOnly = true;
                            }
                        }
                        else if (HeaderContent.ProcessLineDetail.ResourceGrpID == "S")
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

                //Set rial time to change wieght by row.
                dgvCutting.Rows[e.RowIndex].Cells["unitweight1"].Value = rowData.UnitWeight;
                dgvCutting.Rows[e.RowIndex].Cells["totalweight"].Value = rowData.TotalWeight;
                dgvCutting.Rows[e.RowIndex].Cells["totallength"].Value = rowData.TotalLength;
                rowData.CompleteRow = validRow;
                HeaderContent.CuttingDesign = _repo.SaveLineCutting(epiSession, HeaderContent, rowData).ToList();
                HeaderContent.CalculationHeader(HeaderContent);
                SetHeadContent(HeaderContent);
                ListMaterialGrid(HeaderContent.Materails);
            }
        }

        private void dgvCutting_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
        }

        private void processingThisLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCutting.Focused == false) return;
            string riskFlag = string.Empty;
            string msg = string.Empty;

            if (HeaderContent.ProcessLineDetail.ResourceGrpID == "S")
            {
                HeaderContent.CuttingDesign = _repo.GenerateCuttingLine(epiSession, HeaderContent, out riskFlag, out msg).ToList();
                if (!string.IsNullOrEmpty(msg))
                {
                    MessageBox.Show(msg, "Data invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (HeaderContent.ProcessLineDetail.ResourceGrpID == "L")
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
            HeaderContent.CalculationHeader(HeaderContent);
            SetHeadContent(HeaderContent);
            ListCuttingGrid(HeaderContent.CuttingDesign);
            ListMaterialGrid(HeaderContent.Materails);
        }

        #endregion Evnet

        #region Method

        private void LevellerResetCutting()
        {
            string riskFlag = string.Empty; string msg = string.Empty;
            foreach (var p in HeaderContent.CuttingDesign)
            {
                decimal bQty = 0;
                var result = HeaderContent.LevSimulateList.Where(i => i.CuttingLineID == p.LineID).ToList();
                if (result.Count != 0)
                {
                    bQty = HeaderContent.LevSimulates.Where(i => i.CuttingLineID == p.LineID).Sum(i => i.Quantity).GetDecimal();
                }

                p.CalQuantity = bQty;
                p.CalculateRows(HeaderContent);
                p.CompleteRow = p.ValidateByRow(HeaderContent, out riskFlag, out msg);
                HeaderContent.CuttingDesign = _repo.SaveLineCutting(epiSession, HeaderContent, p).ToList();
            }
        }

        private void MaterialReset()
        {
            string riskFlag = string.Empty; string msg = string.Empty;
            foreach (var m in HeaderContent.Materails)
            {
                decimal bQty = 0;
                var result = HeaderContent.LevSimulateList.Where(i => i.MaterialTransLineID == m.TransactionLineID).ToList();
                if (result.Count != 0)
                {
                    bQty = HeaderContent.LevSimulates.Where(i => i.MaterialTransLineID == m.TransactionLineID).Min(i => i.LengthM);
                }

                m.WorkDate = HeaderContent.IssueDate;
                m.UsingLengthM = m.LengthM - bQty;
                var res = _repo.SaveMaterial(epiSession, m);
            }

            HeaderContent.Materails = _repo.GetAllMaterial(epiSession.PlantID, HeaderContent.WorkOrderID);
        }

        private void SetDirectionPatter()
        {
            HeaderContent.CurrentClass = new ClassMasterModel();
            if (HeaderContent.CurrentClass == null) HeaderContent.CurrentClass = new ClassMasterModel();

            //Default direction of material pattern by Machine.
            HeaderContent.ProcessLineDetail = _reRes.GetByID(epiSession.PlantID, cmbProcessLine.SelectedValue.ToString());

            //When we seleted materials, need to fix direction of material pattern.
            if (HeaderContent.Materails.ToList().Count > 0)
            {
                var mat = HeaderContent.Materails.FirstOrDefault();

                //Change default direction of material pattern.
                HeaderContent.ProcessLineDetail.ThickMin = mat.Thick;
                HeaderContent.ProcessLineDetail.ThickMax = mat.Thick;
                HeaderContent.ProcessLineDetail.WidthMin = mat.Width;
                HeaderContent.ProcessLineDetail.WidthMax = mat.Width;
                HeaderContent.ProcessLineDetail.LengthMin = mat.Length;
                HeaderContent.ProcessLineDetail.LengthMax = mat.Length;
            }

            //Verify to make sure there already Class pattern.
            if (HeaderContent.ClassID == 0)
            {
                //HeaderContent.CurrentClass.CustomerReq = 1;
                HeaderContent.CurrentClass.ComudityReq = 1;
                HeaderContent.CurrentClass.SpecCodeReq = 1;
                HeaderContent.CurrentClass.PlateCodeReq = 1;
            }
            else
            {
                HeaderContent.CurrentClass = _repoCls.GetByID(epiSession.PlantID, HeaderContent.ClassID);
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

                if (HeaderContent.ProcessLineDetail.ResourceGrpID == "L")
                {
                    dgvMaterial.Columns["usingLengthM"].ReadOnly = false;
                    dgvMaterial.Columns["usingweight"].ReadOnly = true;
                    dgvMaterial.Columns["quantity"].ReadOnly = true;
                }
                else
                {
                    dgvMaterial.Columns["usingLengthM"].ReadOnly = true;
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
                                    , p.CoatingCode, p.Thick, p.Width, p.Length, p.Status, p.Stand, p.CutDivision, p.Note
                                    , p.UnitWeight, p.TotalWeight, p.TotalLength, p.SOWeight, p.SOQuantity, p.CalQuantity, p.QtyPack, p.Pack
                                    , p.BussinessType + " - " + p.BussinessTypeName, Enum.GetName(typeof(Possession), p.Possession), p.CompleteRow);

                if (string.IsNullOrEmpty(p.Status) && p.Status != "F")
                {
                    //this.dgvCutting.Rows[i].Cells["width1"].ReadOnly = false;
                    this.dgvCutting.Rows[i].Cells["length1"].ReadOnly = false;
                }

                if (HeaderContent.ProcessLineDetail.ResourceGrpID == "L")
                {
                    dgvCutting.Rows[i].Cells["width1"].ReadOnly = true;
                    if (string.IsNullOrEmpty(p.NORNum))
                    {                        
                        dgvCutting.Rows[i].Cells["soqty"].ReadOnly = false;
                        //dgvCutting.Rows[i].Cells["calqty"].ReadOnly = false;
                        dgvCutting.Rows[i].Cells["qtyPack1"].ReadOnly = false;
                    }
                    else
                    {
                        //dgvCutting.Rows[i].Cells["calqty"].ReadOnly = true;
                        dgvCutting.Rows[i].Cells["soqty"].ReadOnly = true;
                    }
                }

                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvCutting.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
            //SetPermissCuttingDesign();
            if (HeaderContent.ProcessLineDetail.ResourceGrpID == "S")
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

        private void RecheckCuttingByRow()
        {
            int i = 0;
            foreach (DataGridViewRow row in dgvCutting.Rows)
            {
                dgvCutting.CurrentCell = dgvCutting[1, i];
                i++;
            }
        }

        private List<CutDesignModel> addSOLine(int lineID, string soNumber)
        {
            string risk = string.Empty;
            string msg = string.Empty;
            //Load data from current seleted row to rowData.
            var rowData = (from item in HeaderContent.CuttingDesign
                           where item.LineID == Convert.ToInt32(lineID)
                           select item).First();

            OrderDetailModel resultLine = new OrderDetailModel();
            IEnumerable<OrderDetailModel> resultParam = new List<OrderDetailModel>();
            SetDirectionPatter();
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

            HeaderContent.MaterialPattern = new MaterialModel();
            HeaderContent.ClassID = resultLine.ClassID;
            HeaderContent.Possession = Convert.ToString(resultLine.Possession);
            HeaderContent.BussinessType = resultLine.BussinessType;
            HeaderContent.MaterialPattern.CommodityCode = resultLine.CommodityCode;
            HeaderContent.MaterialPattern.SpecCode = resultLine.SpecCode;
            HeaderContent.MaterialPattern.CoatingCode = resultLine.CoatingCode;
            HeaderContent.MaterialPattern.Thick = resultLine.Thick;
            HeaderContent.MaterialPattern.Width = resultLine.Width;
            HeaderContent.MaterialPattern.Length = resultLine.Length;
            HeaderContent.MaterialPattern.BussinessType = resultLine.BussinessType;
            HeaderContent.MaterialPattern.Possession = resultLine.Possession;

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

        #endregion Method

        private void dgvMaterial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OrderCompleted()) return;
            if (dgvMaterial.Columns[e.ColumnIndex].Name == "SelectCB")//set your checkbox column index instead of 2
            {
                string transId = dgvMaterial.Rows[e.RowIndex].Cells["transactionlineid"].Value.GetString();

                var result = (from item in HeaderContent.Materails
                              where item.TransactionLineID == Convert.ToInt32(transId)
                              select item).First();

                var coilExist = HeaderContent.CoilBacks.Where(p => p.TransactionLineID.ToString().Equals(transId)).ToList();

                bool chk = Convert.ToBoolean(dgvMaterial.Rows[e.RowIndex].Cells["SelectCB"].EditedFormattedValue);

                if (chk)
                {
                    DialogResult diaResulta = MessageBox.Show("Are you sure to add coilback.", "Row validate.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (diaResulta == DialogResult.No)
                    {
                        dgvMaterial.Rows[e.RowIndex].Cells["SelectCB"].Value = false;
                        return;
                    }
                }
                if (chk && result.RemainWeight > 0)
                {
                    bool strVal = Convert.ToBoolean(dgvMaterial.Rows[e.RowIndex].Cells["SelectCB"].Value);
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

                    HeaderContent.CoilBacks = _repo.SaveCoilBack(epiSession, CBack).ToList();
                }
                else if (coilExist.Count > 0)
                {
                    DialogResult diaResult = MessageBox.Show("Are you sure to delete coilback.", "Row validate error!.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (diaResult == DialogResult.Yes)
                    {
                        HeaderContent.CoilBacks = _repo.DeleteCoilBack(epiSession, HeaderContent.WorkOrderID, result.TransactionLineID).ToList();
                    }
                }

                result.CBSelect = chk;
                result.WorkDate = HeaderContent.IssueDate;
                var res = _repo.SaveMaterial(epiSession, result);
                HeaderContent.SumUsingWeight(HeaderContent.Materails);
                SetHeadContent(HeaderContent);
                ListCoilBackGrid(HeaderContent.CoilBacks);
                tbutCalculate_Click(sender, e);
            }
        }

        private void tbutCancelWorkOrder_Click(object sender, EventArgs e)
        {
        }

        private void butSimulate_Click(object sender, EventArgs e)
        {
            if (HeaderContent.ProcessLineDetail.ResourceGrpID == "S")
            {
                SimulateActionModel simModel = new SimulateActionModel();
                simModel.WorkOrderID = HeaderContent.WorkOrderID;
                simModel.WorkOrderNum = HeaderContent.WorkOrderNum;
                simModel.MaterialWeight = HeaderContent.InputWeight;
                simModel.ProductWeight = HeaderContent.OutputWeight;
                simModel.Yield = HeaderContent.Yield;
                simModel.TrimWeight = HeaderContent.CuttingDesign.Where(i => i.Status.Equals("S")).Sum(i => i.TotalWeight);
                simModel.Cuttings = _repo.GetSimulateAll(HeaderContent.WorkOrderID).ToList();
                simModel.Materials = HeaderContent.Materails.ToList();
                using (SimulateEntry frm = new SimulateEntry(epiSession, HeaderContent, simModel))
                {
                    frm.ShowDialog();
                    HeaderContent = frm.HeadModel;
                }
            }

            SetHeadContent(HeaderContent);
            ListMaterialGrid(HeaderContent.Materails);
            ListCuttingGrid(HeaderContent.CuttingDesign);
            //tbutSave_Click(sender, e);
        }

        private void butGenSN_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            IEnumerable<GeneratedSerialModel> serialLines = new List<GeneratedSerialModel>();
            IEnumerable<SimulateModel> simResult = new List<SimulateModel>();
            if (HeaderContent.GenSerialFlag == 0)
            {
                if (HeaderContent.ProcessLineDetail.ResourceGrpID == "S")
                {
                    simResult = _repo.GetSimulateAll(HeaderContent.WorkOrderID);
                }
                else if (HeaderContent.ProcessLineDetail.ResourceGrpID == "L")
                {
                    simResult = _repo.GetSimulateLeveller(HeaderContent.WorkOrderID);
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

        private void butConfirm_Click(object sender, EventArgs e)
        {
            if (HeaderContent.Completed == 1 && HeaderContent.SimulateFlag == 1 && HeaderContent.GenSerialFlag == 0)
            {
                DialogResult diaResulta = MessageBox.Show("Are you sure to unconfirm.", "Question.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (diaResulta == DialogResult.Yes)
                {
                    if (_repo.UnConfirmWork(HeaderContent.WorkOrderID))
                    {
                        MessageBox.Show("Unconfirm Order completed.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HeaderContent = _repo.GetWorkById(HeaderContent.WorkOrderNum, Convert.ToInt32(HeaderContent.ProcessStep), epiSession.PlantID);
                        SetHeadContent(HeaderContent);
                    }
                }
            }
        }

        private void butAddCoilBack_Click(object sender, EventArgs e)
        {
            ListCuttingGrid(HeaderContent.CuttingDesign);
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

        private void SimulateLeveller()
        {
            _repo.ClearSimulateLeveller(HeaderContent.WorkOrderID);
            HeaderContent.LevSimulates = new List<LevellerSimulateModel>();
            decimal matBln = 0;
            foreach (var mat in HeaderContent.Materails)
            {
                decimal matLength = mat.UsingLengthM * 1000;

                foreach (var item in HeaderContent.CuttingDesign)
                {
                    decimal bQty = HeaderContent.LevSimulates.Where(p => p.CuttingLineID == item.LineID).Sum(i => i.Quantity);
                    if (item.SOQuantity != bQty && matLength >= item.Length)
                    {
                        decimal calQty = Math.Floor(matLength / item.Length);

                        if (calQty >= item.SOQuantity && bQty != 0)
                        {
                            item.CalQuantity = item.SOQuantity - bQty;
                            item.CompleteRow = true;
                            matLength = (calQty - (item.CalQuantity - bQty)) * item.Length;
                        }
                        else if (calQty >= item.SOQuantity && bQty == 0)
                        {
                            item.CalQuantity = item.SOQuantity;
                            item.CompleteRow = true;
                            matLength = Math.Floor((calQty - (item.CalQuantity)) * item.Length);
                        }
                        else if (calQty < item.SOQuantity && bQty == 0)
                        {
                            item.CalQuantity = calQty;
                            item.CompleteRow = false;
                            matLength = 0;
                        }
                        else if (calQty < item.SOQuantity && bQty != 0)
                        {
                            var cal = item.SOQuantity - bQty;
                            if (cal > calQty)
                            {
                                item.CalQuantity = calQty;
                                item.CompleteRow = (item.SOQuantity == (item.CalQuantity + bQty)); ;
                                matLength = 0;
                            }
                            else if (cal <= calQty)
                            {
                                item.CalQuantity = cal;
                                item.CompleteRow = true;
                                matLength = (calQty - cal) * item.Length;
                            }
                        }

                        LevellerSimulateModel lev = new LevellerSimulateModel();
                        lev.Plant = epiSession.PlantID;
                        lev.WorkOrderID = HeaderContent.WorkOrderID;
                        lev.CuttingLineID = item.LineID;
                        lev.MaterialTransLineID = mat.TransactionLineID;
                        lev.SOQuantity = Convert.ToInt32(item.SOQuantity);
                        lev.Quantity = Convert.ToInt32(item.CalQuantity);
                        lev.Weight = 0;
                        lev.LengthM = Math.Ceiling(matLength / 1000);
                        matBln = matLength;
                        HeaderContent.LevSimulates = _repo.SaveLevellerSimulate(epiSession, lev).ToList();
                        item.CalculateRows(HeaderContent);
                    }
                }
                //}
            }
        }

        private bool OrderCompleted()
        {
            if (HeaderContent.Completed == 1)
            {
                MessageBox.Show("Work Order has confirmed. if you want to update, please unconfirm before.","Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}