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

        #region Properties

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
            txtLossWeight.DataBindings.Add("Text", model, "LossWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0.00");
            txtYield.DataBindings.Add("Text", model, "Yield", true, DataSourceUpdateMode.OnPropertyChanged, 1, "##0");
            txtTotalMaterialAmount.DataBindings.Add("Text", model, "TotalMaterialAmount", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0.00");
            txtTotalWidth.DataBindings.Add("Text", model, "TotalWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0.00");

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

            //DatePicker
            dptIssueDate.Value = model.IssueDate;
            dptDueDate.Value = model.DueDate;
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

        #endregion Properties

        #region Evnet

        private void WorkEntrySlitter_Load(object sender, EventArgs e)
        {
            HeaderContent.PreLoad();
            SetFormState();
        }

        #endregion Evnet

        #region Method

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

            IEnumerable<MaterialModel> model = new List<MaterialModel>();
            HeaderContent.ProcessLineDetail = _reRes.GetByID(epiSession.PlantID, HeaderContent.ProcessLineId);

            var result = HeaderContent.ValidateToSave(model, out objectValid, out messageValid);

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
            HeaderContent.SimulateFlag = true;
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
            HeaderContent.CalculationHeader(HeaderContent);
            SetHeadContent(HeaderContent);
            ListCuttingGrid(HeaderContent.CuttingDesign);
            HeaderContent.FormState = 4;
            SetFormState();
        }

        private void butAddMaterial_Click(object sender, EventArgs e)
        {
            //Initialization
            HeaderContent.FormState = 3;
            SetFormState();
            HeaderContent.MaterialPattern = new MaterialModel();
            HeaderContent.CurrentClass = new ClassMasterModel();
            if (HeaderContent.CurrentClass == null) HeaderContent.CurrentClass = new ClassMasterModel();

            //Get Machine detail from combobox 'Process Line' to finding material.
            HeaderContent.ProcessLineDetail = _reRes.GetByID(epiSession.PlantID, cmbProcessLine.SelectedValue.ToString());

            //If material list > 0 must be assign materail filtering.
            if (HeaderContent.Materails.ToList().Count > 0 && HeaderContent.CuttingDesign.ToList().Count == 0)
            {
                var mat = HeaderContent.Materails.FirstOrDefault();

                HeaderContent.CurrentClass.ComudityReq = 1;
                HeaderContent.MaterialPattern.CommodityCode = mat.CommodityCode;
                HeaderContent.CurrentClass.SpecCodeReq = 1;
                HeaderContent.MaterialPattern.SpecCode = mat.SpecCode;
                HeaderContent.CurrentClass.PlateCodeReq = 1;
                HeaderContent.MaterialPattern.CoatingCode = mat.CoatingCode;
                HeaderContent.MaterialPattern.Possession = mat.Possession;
                //HeaderContent.MaterialPattern.BussinessType = mat.BussinessType;

                //Fix material size
                HeaderContent.ProcessLineDetail.ThickMin = mat.Thick;
                HeaderContent.ProcessLineDetail.ThickMax = mat.Thick;
                HeaderContent.ProcessLineDetail.WidthMin = mat.Width;
                HeaderContent.ProcessLineDetail.WidthMax = mat.Width;
                HeaderContent.ProcessLineDetail.LengthMin = mat.Length;
                HeaderContent.ProcessLineDetail.LengthMax = mat.Length;
            }

            //If material list > 0 must be assign materail filtering.
            if (HeaderContent.Materails.ToList().Count == 0 && HeaderContent.CuttingDesign.ToList().Count > 0)
            {
                var nor = HeaderContent.CuttingDesign.FirstOrDefault();
                HeaderContent.CurrentClass = _repoCls.GetByID(epiSession.PlantID, HeaderContent.ClassID);
                HeaderContent.MaterialPattern.CommodityCode = nor.CommodityCode;
                HeaderContent.MaterialPattern.SpecCode = nor.SpecCode;
                HeaderContent.MaterialPattern.CoatingCode = nor.CoatingCode;
                HeaderContent.MaterialPattern.Possession = nor.Possession;
                HeaderContent.MaterialPattern.Thick = nor.Thick;
                HeaderContent.MaterialPattern.Width = nor.Width;
                HeaderContent.MaterialPattern.Length = nor.Length;
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
                    , Enum.GetName(typeof(MaterialStatus), int.Parse(p.Status)), p.Note, p.BussinessTypeName, Enum.GetName(typeof(ProductStatus), int.Parse(p.ProductStatus)));
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
                                    , p.UnitWeight, p.TotalWeight, p.TotalLength, p.SOWeight, p.SOQuantity, p.CalQuantity, p.QtyPack, p.Pack, p.BussinessType, true);
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

        private void butWorkOrder_Click(object sender, EventArgs e)
        {
            using (CoilBackRuleDialog frm = new CoilBackRuleDialog(epiSession))
            {
                frm.ShowDialog();
                txtWorkOrderNum.Text = frm.Code;
            }
        }

        #endregion Method

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

            switch (colName)
            {
                case "usingweight":
                    HeaderContent.Materails.Where(p => p.TransactionLineID.ToString().Equals(transId)).ToList()
                                                        .ForEach(i => i.UsingWeight = Convert.ToDecimal(strVal));
                    break;

                case "quantity":
                    decimal val = (Convert.ToDecimal(strVal) / Convert.ToDecimal(qtyPack)) * 100;

                    HeaderContent.Materails.Where(p => p.TransactionLineID.ToString().Equals(transId)).ToList()
                                                        .ForEach(i => i.UsingQuantity = Convert.ToDecimal(strVal));

                    HeaderContent.Materails.Where(p => p.TransactionLineID.ToString().Equals(transId)).ToList()
                                                        .ForEach(i => i.UsingWeight = (Convert.ToDecimal(weight) / 100) * val);
                    break;
            }

            var result = (from item in HeaderContent.Materails
                          where item.TransactionLineID == Convert.ToInt32(transId)
                          select item).First();

            dgvMaterial.Rows[e.RowIndex].Cells["usingweight"].Value = result.UsingWeight;
            dgvMaterial.Rows[e.RowIndex].Cells["remainWeight"].Value = result.RemainWeight;
            dgvMaterial.Rows[e.RowIndex].Cells["RemQuantity"].Value = result.RemainQuantity;

            result.WorkDate = dptIssueDate.Value;
            var res = _repo.SaveMaterial(epiSession, result);
            HeaderContent.SumUsingWeight(HeaderContent.Materails);
            SetHeadContent(HeaderContent);
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
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void dgvMaterial_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void tbutNewCutting_Click(object sender, EventArgs e)
        {
            CutDesignModel model = new CutDesignModel();

            if (HeaderContent.Materails.ToList().Count == 0)
            {
                model.Status = "S";
                model.DeliveryDate = DateTime.Now;
                model.BussinessType = string.IsNullOrEmpty(HeaderContent.BT.GetString()) ? "" : HeaderContent.BT;
            }
            else
            {
                var result = (from item in HeaderContent.Materails
                              select item).First();

                model.Status = "S";
                model.DeliveryDate = DateTime.Now;
                model.BussinessType = string.IsNullOrEmpty(HeaderContent.BT.GetString()) ? "" : HeaderContent.BT;
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
            string colName = dgvCutting.Columns[e.ColumnIndex].Name;
            string lineId = dgvCutting.Rows[e.RowIndex].Cells["lineid"].Value.GetString();
            string code = dgvCutting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetString();

            var rowData = (from item in HeaderContent.CuttingDesign
                           where item.LineID == Convert.ToInt32(lineId)
                           select item).First();

            switch (colName)
            {
                case "sono":
                    if (!string.IsNullOrEmpty(code))
                    {
                        var result = _repoSale.GetOrderByID(code);
                        dgvCutting.Rows[e.RowIndex].Cells["soline"].Value = string.Empty;
                        if (result == null)
                        {
                            var soResult = _repoSale.GetOrderHeadAll();
                            using (OrderHeadDialog frm = new OrderHeadDialog(epiSession, soResult))
                            {
                                frm.ShowDialog();
                                dgvCutting.Rows[e.RowIndex].Cells["sono"].Value = frm._selected.OrderNum;
                                dgvCutting.Rows[e.RowIndex].Cells["customer"].Value = frm._selected.CustID;
                            }
                        }
                        else
                        {
                            dgvCutting.Rows[e.RowIndex].Cells["sono"].Value = result.OrderNumber;
                            dgvCutting.Rows[e.RowIndex].Cells["customer"].Value = result.CustID;
                        }
                    }
                    break;

                case "soline":
                    if (!string.IsNullOrEmpty(code))
                    {
                        OrderDetailModel result = new OrderDetailModel();
                        result = _repoSale.GetOrderDtlByID(rowData.SONo, Convert.ToInt32(code));
                        if (result == null)
                        {
                            var soResult = _repoSale.GetOrderDtlAll(rowData.SONo);
                            using (OrderLineDialog frm = new OrderLineDialog(epiSession, soResult))
                            {
                                frm.ShowDialog();
                                result = frm._selected;
                            }
                        }

                        HeaderContent.MaterialPattern = new MaterialModel();
                        HeaderContent.ClassID = result.ClassID;
                        HeaderContent.MaterialPattern.CommodityCode = result.CommodityCode;
                        HeaderContent.MaterialPattern.SpecCode = result.SpecCode;
                        HeaderContent.MaterialPattern.CoatingCode = result.CoatingCode;
                        HeaderContent.MaterialPattern.Thick = result.Thick;
                        HeaderContent.MaterialPattern.Width = result.Width;
                        HeaderContent.MaterialPattern.Length = result.Length;
                        HeaderContent.MaterialPattern.BussinessType = result.BussinessType;
                        HeaderContent.MaterialPattern.Possession = result.Possession;

                        dgvCutting.Rows[e.RowIndex].Cells["soline"].Value = result.OrderLine;
                        dgvCutting.Rows[e.RowIndex].Cells["norno"].Value = result.NORNo;
                        dgvCutting.Rows[e.RowIndex].Cells["commodity1"].Value = result.CommodityCode;
                        dgvCutting.Rows[e.RowIndex].Cells["spec1"].Value = result.SpecCode;
                        dgvCutting.Rows[e.RowIndex].Cells["coating1"].Value = result.CoatingCode;
                        dgvCutting.Rows[e.RowIndex].Cells["thick1"].Value = result.Thick;
                        dgvCutting.Rows[e.RowIndex].Cells["width1"].Value = result.Width;
                        dgvCutting.Rows[e.RowIndex].Cells["length1"].Value = result.Length;
                        //dgvCutting.Rows[e.RowIndex].Cells["status1"].Value = "F";
                        dgvCutting.Rows[e.RowIndex].Cells["soweight"].Value = result.SOWeight;
                        dgvCutting.Rows[e.RowIndex].Cells["soqty"].Value = result.SOQuantity;
                        //dgvCutting.Rows[e.RowIndex].Cells["calqty"].Value = result.so;
                        //dgvCutting.Rows[e.RowIndex].Cells["qtyPack1"].Value = result;
                        dgvCutting.Rows[e.RowIndex].Cells["pack"].Value = result.Pack;
                        dgvCutting.Rows[e.RowIndex].Cells["bt1"].Value = result.BussinessType;
                    }
                    break;
            }
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

            rowData.CalUnitWeight(HeaderContent);
            if (!rowData.ValidateByRow(HeaderContent, out riskFlag, out msg))
            {
                dgvCutting.Rows[e.RowIndex].Cells["rowValidated"].Value = false;
                if (riskFlag == "ERROR")
                {
                    MessageBox.Show(msg, "Row validate error!.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
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

        private void dgvMaterial_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvCutting_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
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
    }
}