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
            butConfirm.Visible = Convert.ToBoolean(model.Completed);
            butConfirm.Text = model.CompletedStr;
            if (model.Completed == 1)
            {
                butConfirm.BackColor = Color.Green;
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

            //DatePicker
            dptIssueDate.Value = model.IssueDate;
            dptDueDate.Value = model.DueDate;

            if (model.CheckYeild(model.Yield))
            {
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
                    butConfirm.BackColor = Color.Green;
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
            dgvCutting.Columns["customer"].ReadOnly = true;
            dgvCutting.Columns["commodity1"].ReadOnly = true;
            dgvCutting.Columns["spec1"].ReadOnly = true;
            dgvCutting.Columns["coating1"].ReadOnly = true;
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
            }
            else if (HeaderContent.ProcessLineDetail.ResourceGrpID == "L")
            {
                dgvCutting.Columns["length1"].ReadOnly = false;
                dgvCutting.Columns["status1"].ReadOnly = false;
            }
            else if (HeaderContent.ProcessLineDetail.ResourceGrpID == "R")
            {
                dgvCutting.Columns["width1"].ReadOnly = false;
                dgvCutting.Columns["length1"].ReadOnly = false;
                dgvCutting.Columns["status1"].ReadOnly = false;
            }

            if (dgvCutting.Rows.Count != 0) tbutNewCutting.Enabled = false;
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
            string risk = string.Empty;
            string msg = string.Empty;
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

                    break;

                case "quantity":
                    decimal val = (Convert.ToDecimal(strVal) / Convert.ToDecimal(qtyPack)) * 100;

                    HeaderContent.Materails.Where(p => p.TransactionLineID.ToString().Equals(transId)).ToList()
                                                        .ForEach(i => i.UsingQuantity = Convert.ToDecimal(strVal));

                    HeaderContent.Materails.Where(p => p.TransactionLineID.ToString().Equals(transId)).ToList()
                                                        .ForEach(i => i.UsingWeight = (Convert.ToDecimal(weight) / 100) * val);
                    break;

                //case "SelectCB":
                //    result.CBSelect = Convert.ToBoolean(dgvMaterial.Rows[e.RowIndex].Cells["SelectCB"].Value);
                //    break;
            }

            dgvMaterial.Rows[e.RowIndex].Cells["usingweight"].Value = result.UsingWeight;
            dgvMaterial.Rows[e.RowIndex].Cells["remainWeight"].Value = result.RemainWeight;
            dgvMaterial.Rows[e.RowIndex].Cells["RemQuantity"].Value = result.RemainQuantity;
            dgvMaterial.Rows[e.RowIndex].Cells["SelectCB"].Value = result.CBSelect;

            result.WorkDate = dptIssueDate.Value;
            var res = _repo.SaveMaterial(epiSession, result);
            HeaderContent.SumUsingWeight(HeaderContent.Materails);
            SetHeadContent(HeaderContent);
            ListCoilBackGrid(HeaderContent.CoilBacks);
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

            HeaderContent.CuttingLines = _repo.SaveLineCutting(epiSession, HeaderContent, model);
            ListCuttingGrid(HeaderContent.CuttingLines);
            SetPermissCuttingDesign();
            dgvCutting.Columns["customer"].ReadOnly = false;
            dgvCutting.Columns["commodity1"].ReadOnly = false;
            dgvCutting.Columns["spec1"].ReadOnly = false;
            dgvCutting.Columns["coating1"].ReadOnly = false;
            dgvCutting.Columns["thick1"].ReadOnly = false;
            dgvCutting.Columns["width1"].ReadOnly = false;
            dgvCutting.Columns["length1"].ReadOnly = false;
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
            OrderHeadModel result = new OrderHeadModel();
            string colName = dgvCutting.Columns[e.ColumnIndex].Name;
            string lineId = dgvCutting.Rows[e.RowIndex].Cells["lineid"].Value.GetString();
            string code = dgvCutting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetString();

            if (!string.IsNullOrEmpty(dgvCutting.Rows[e.RowIndex].Cells["soline"].Value.GetString()))
            {
                //Set grid after select S/O Line.
                if (!string.IsNullOrEmpty(dgvCutting.Rows[e.RowIndex].Cells["norno"].Value.GetString()))
                {
                    dgvCutting.Rows[e.RowIndex].Cells["norno"].ReadOnly = true;
                    dgvCutting.Rows[e.RowIndex].Cells["customer"].ReadOnly = true;
                    dgvCutting.Rows[e.RowIndex].Cells["commodity1"].ReadOnly = true;
                    dgvCutting.Rows[e.RowIndex].Cells["spec1"].ReadOnly = true;
                    dgvCutting.Rows[e.RowIndex].Cells["coating1"].ReadOnly = true;
                    dgvCutting.Rows[e.RowIndex].Cells["thick1"].ReadOnly = true;
                    dgvCutting.Rows[e.RowIndex].Cells["width1"].ReadOnly = true;
                    dgvCutting.Rows[e.RowIndex].Cells["length1"].ReadOnly = true;
                }
                return;
            }

            switch (colName)
            {
                case "sono":
                    //Load data from current seleted row to rowData.
                    var rowData = (from item in HeaderContent.CuttingDesign
                                   where item.LineID == Convert.ToInt32(lineId)
                                   select item).First();

                    //Verify S/O No.
                    if (!string.IsNullOrEmpty(code))
                    {
                        result = _repoSale.GetOrderByID(code);
                        dgvCutting.Rows[e.RowIndex].Cells["soline"].Value = string.Empty;
                        if (result == null)
                        {
                            var soResult = _repoSale.GetOrderHeadAll();
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

                        //Set header value.
                        HeaderContent.OrderType = result.OrderType;
                        dgvCutting.Rows[e.RowIndex].Cells["sono"].Value = result.OrderNumber;
                        dgvCutting.Rows[e.RowIndex].Cells["customer"].Value = result.CustID;

                        OrderDetailModel resultLine = new OrderDetailModel();
                        IEnumerable<OrderDetailModel> resultParam = new List<OrderDetailModel>();
                        SetDirectionPatter();
                        resultParam = _repoSale.GetOrderDtlByFilter(rowData.SONo, HeaderContent);
                        using (OrderLineDialog frm = new OrderLineDialog(epiSession, resultParam))
                        {
                            frm.ShowDialog();
                            resultLine = frm._selected;
                            if (resultLine.OrderNum == null)
                            {
                                dgvCutting.Rows[e.RowIndex].Cells["sono"].Value = string.Empty;
                                dgvCutting.Rows[e.RowIndex].Cells["soline"].Value = string.Empty;
                                return;
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

                        dgvCutting.Rows[e.RowIndex].Cells["soline"].Value = resultLine.OrderLine;
                        dgvCutting.Rows[e.RowIndex].Cells["norno"].Value = resultLine.NORNo;
                        dgvCutting.Rows[e.RowIndex].Cells["commodity1"].Value = resultLine.CommodityCode;
                        dgvCutting.Rows[e.RowIndex].Cells["spec1"].Value = resultLine.SpecCode;
                        dgvCutting.Rows[e.RowIndex].Cells["coating1"].Value = resultLine.CoatingCode;
                        dgvCutting.Rows[e.RowIndex].Cells["thick1"].Value = resultLine.Thick;
                        dgvCutting.Rows[e.RowIndex].Cells["width1"].Value = resultLine.Width;
                        dgvCutting.Rows[e.RowIndex].Cells["length1"].Value = resultLine.Length;
                        //dgvCutting.Rows[e.RowIndex].Cells["status1"].Value = "F";
                        dgvCutting.Rows[e.RowIndex].Cells["soweight"].Value = resultLine.SOWeight;
                        dgvCutting.Rows[e.RowIndex].Cells["soqty"].Value = resultLine.SOQuantity;
                        //dgvCutting.Rows[e.RowIndex].Cells["calqty"].Value = result.so;
                        //dgvCutting.Rows[e.RowIndex].Cells["qtyPack1"].Value = result;
                        dgvCutting.Rows[e.RowIndex].Cells["pack"].Value = resultLine.Pack;
                        dgvCutting.Rows[e.RowIndex].Cells["bt1"].Value = resultLine.BussinessType;
                    }
                    break;
            }

            //Set grid after select S/O Line.
            if (!string.IsNullOrEmpty(dgvCutting.Rows[e.RowIndex].Cells["norno"].Value.GetString()))
            {
                dgvCutting.Rows[e.RowIndex].Cells["norno"].ReadOnly = true;
                dgvCutting.Rows[e.RowIndex].Cells["customer"].ReadOnly = true;
                dgvCutting.Rows[e.RowIndex].Cells["commodity1"].ReadOnly = true;
                dgvCutting.Rows[e.RowIndex].Cells["spec1"].ReadOnly = true;
                dgvCutting.Rows[e.RowIndex].Cells["coating1"].ReadOnly = true;
                dgvCutting.Rows[e.RowIndex].Cells["thick1"].ReadOnly = true;
                dgvCutting.Rows[e.RowIndex].Cells["width1"].ReadOnly = true;
                dgvCutting.Rows[e.RowIndex].Cells["length1"].ReadOnly = true;
            }

            HeaderContent.CalculationHeader(HeaderContent);
            SetHeadContent(HeaderContent);
        }

        private void dgvCutting_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvCutting.Rows.Count == 0) return;
            //Fix bug CutLines is null
            if (HeaderContent.CuttingDesign.ToList().Count == 0) return;

            CutDesignModel rowData = new CutDesignModel();
            int i;
            decimal d;
            string riskFlag = string.Empty; //Set error level WARNNING/ERROR
            string msg = string.Empty;
            string strVal = e.FormattedValue.ToString();// dgvCutting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetString();
            string lineId = dgvCutting.Rows[e.RowIndex].Cells["lineid"].Value.GetString();
            string colName = dgvCutting.Columns[e.ColumnIndex].Name;

            if (HeaderContent.Materails.ToList().Count != 0)
            {
                HeaderContent.MaterialPattern = (from item in HeaderContent.Materails select item).First();
            }

            try
            {
                rowData = (from item in HeaderContent.CuttingDesign
                           where item.LineID == Convert.ToInt32(lineId)
                           select item).First();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    return;
                }
                else
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //Update current row
            rowData.SONo = dgvCutting.Rows[e.RowIndex].Cells["sono"].Value.GetString();
            rowData.SOLine = Convert.ToInt32(string.IsNullOrEmpty(dgvCutting.Rows[e.RowIndex].Cells["soline"].Value.GetString()) ? "0" :
                                                    dgvCutting.Rows[e.RowIndex].Cells["soline"].Value.GetString());
            rowData.NORNum = dgvCutting.Rows[e.RowIndex].Cells["norno"].Value.GetString();
            rowData.CustID = dgvCutting.Rows[e.RowIndex].Cells["customer"].Value.GetString();
            rowData.CommodityCode = dgvCutting.Rows[e.RowIndex].Cells["commodity1"].Value.GetString();
            rowData.SpecCode = dgvCutting.Rows[e.RowIndex].Cells["spec1"].Value.GetString();
            rowData.CoatingCode = dgvCutting.Rows[e.RowIndex].Cells["coating1"].Value.GetString();
            rowData.Thick = Convert.ToDecimal(string.IsNullOrEmpty(dgvCutting.Rows[e.RowIndex].Cells["thick1"].Value.GetString()) ? "0M" :
                                                    dgvCutting.Rows[e.RowIndex].Cells["thick1"].Value.GetString());
            rowData.Width = Convert.ToDecimal(string.IsNullOrEmpty(dgvCutting.Rows[e.RowIndex].Cells["width1"].Value.GetString()) ? "0M" :
                                                    dgvCutting.Rows[e.RowIndex].Cells["width1"].Value.GetString());
            rowData.Length = Convert.ToDecimal(string.IsNullOrEmpty(dgvCutting.Rows[e.RowIndex].Cells["length1"].Value.GetString()) ? "0M" :
                                                    dgvCutting.Rows[e.RowIndex].Cells["length1"].Value.GetString());
            rowData.Status = dgvCutting.Rows[e.RowIndex].Cells["status1"].Value.GetString();
            rowData.Stand = dgvCutting.Rows[e.RowIndex].Cells["stand"].Value.GetInt();
            rowData.CutDivision = dgvCutting.Rows[e.RowIndex].Cells["cutdiv"].Value.GetInt();

            switch (colName)
            {
                case "sono":
                    rowData.SONo = strVal;
                    break;

                case "soline":
                    rowData.SOLine = Convert.ToInt32(string.IsNullOrEmpty(strVal) ? "0" : strVal);
                    break;

                case "norno":
                    break;

                case "customer":
                    rowData.CustID = strVal;
                    break;

                case "commodity1":
                    rowData.CommodityCode = strVal;
                    break;

                case "spec1":
                    rowData.SpecCode = strVal;
                    break;

                case "coating1":
                    rowData.CoatingCode = strVal;
                    break;

                case "thick1":
                    if (!decimal.TryParse(strVal, out d))
                    {
                        e.Cancel = true;
                        return;
                    }
                    rowData.Thick = Convert.ToDecimal(string.IsNullOrEmpty(strVal) ? "0M" : strVal);
                    break;

                case "width1":
                    if (!decimal.TryParse(strVal, out d))
                    {
                        e.Cancel = true;
                        return;
                    }
                    rowData.Width = Convert.ToDecimal(string.IsNullOrEmpty(strVal) ? "0M" : strVal);
                    break;

                case "length1":
                    if (!decimal.TryParse(strVal, out d))
                    {
                        e.Cancel = true;
                        return;
                    }
                    rowData.Length = Convert.ToDecimal(string.IsNullOrEmpty(strVal) ? "0M" : strVal);
                    break;

                case "status1":
                    rowData.Status = strVal;
                    break;

                case "stand":
                    if (!int.TryParse(strVal, out i))
                    {
                        e.Cancel = true;
                        return;
                    }
                    rowData.Stand = Convert.ToInt32(strVal);
                    break;

                case "cutdiv":
                    if (!int.TryParse(strVal, out i))
                    {
                        e.Cancel = true;
                        return;
                    }
                    rowData.CutDivision = Convert.ToInt32(strVal);
                    break;

                case "note1":
                    rowData.Note = strVal;
                    break;
            }

            rowData.CalculateRow(HeaderContent);
            if (!rowData.ValidateByRow(HeaderContent, out riskFlag, out msg))
            {
                dgvCutting.Rows[e.RowIndex].Cells["rowValidated"].Value = false;
                if (riskFlag == "ERROR")
                {
                    DialogResult diaResult = MessageBox.Show(msg, "Row validate error!.", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (diaResult == DialogResult.Retry)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
                return;
            }
            else
            {
                dgvCutting.Rows[e.RowIndex].Cells["rowValidated"].Value = true;
                dgvCutting.Rows[e.RowIndex].Cells["unitweight1"].Value = rowData.UnitWeight;
                dgvCutting.Rows[e.RowIndex].Cells["totalweight"].Value = rowData.TotalWeight;
                dgvCutting.Rows[e.RowIndex].Cells["totallength"].Value = rowData.TotalLength;
                HeaderContent.CuttingDesign = _repo.SaveLineCutting(epiSession, HeaderContent, rowData).ToList();
            }
        }

        private void processingThisLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCutting.Focused == false) return;

            string riskFlag = string.Empty;
            string msg = string.Empty;

            //var rows = from DataGridViewRow row in dgvCutting.Rows
            //           where row.Selected
            //           select row;

            //if (rows != null)
            //{
            //    MessageBox.Show("Please complete data in cutting design list.", "Data invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            HeaderContent.CuttingDesign = _repo.GenerateCuttingLine(epiSession, HeaderContent, out riskFlag, out msg).ToList();
            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, "Data invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ListCuttingGrid(HeaderContent.CuttingDesign);
        }

        #endregion Evnet

        #region Method

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
                dgvMaterial.Rows.Add(p.TransactionLineID, p.MCSSNo, i + 1, p.SerialNo, p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName, p.Thick, p.Width, p.Length
                    , p.Weight, p.UsingWeight
                    , p.RemainWeight, p.LengthM
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
                                    , p.BussinessType + " - " + p.BussinessTypeName, Enum.GetName(typeof(Possession), p.Possession), true);

                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvCutting.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
            SetPermissCuttingDesign();
            if (dgvCutting.Rows.Count == 0)
            {
                tbutNewCutting.Enabled = true;
            }
            else
            {
                tbutNewCutting.Enabled = false;
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

        #endregion Method

        private void dgvMaterial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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
            ListMaterialGrid(HeaderContent.Materails);
            ListCuttingGrid(HeaderContent.CuttingDesign);
            SetHeadContent(HeaderContent);
            tbutSave_Click(sender, e);
        }

        private void butGenSN_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            IEnumerable<GeneratedSerialModel> serialLines = new List<GeneratedSerialModel>();
            if (HeaderContent.GenSerialFlag == 0)
            {
                var simResult = _repo.GetSimulateAll(HeaderContent.WorkOrderID);
                serialLines = _repo.GenerateSerial(epiSession, simResult, HeaderContent.WorkOrderID);
                _repo.ImportSerialToEpicor(epiSession, HeaderContent, out msg);
                HeaderContent.GenSerialFlag = 1;
            }
            else
            {
                serialLines = _repo.GetSerialAllByWorkOrder(HeaderContent.WorkOrderID);
            }

            using (SerialList frm = new SerialList(epiSession, serialLines))
            {
                frm.ShowDialog();                
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
                        MessageBox.Show("Unconfirm Order completed.","Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HeaderContent = _repo.GetWorkById(HeaderContent.WorkOrderNum, Convert.ToInt32(HeaderContent.ProcessStep), epiSession.PlantID);
                        SetHeadContent(HeaderContent);
                    }
                }
            }
        }
    }
}