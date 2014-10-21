using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Planning;
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

        private PlaningHeadModel HeaderContent;
        private ClassMasterModel _class;

        public WorkEntry(SessionInfo _session = null, PlaningHeadModel model = null)
        {
            InitializeComponent();
            this._repoRes = new ResourceRepo();
            this._repoUcd = new UserCodeRepo();
            this._repo = new WorkEntryRepo();
            this._reRes = new ResourceRepo();

            this.HeaderContent = new PlaningHeadModel();
            this._class = new ClassMasterModel();

            //Initial Session and content
            this.HeaderContent = new PlaningHeadModel();
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
                    tbutNewCoilBack.Enabled = false;
                    tbutSave.Enabled = false;
                    tbutCalculate.Enabled = false;
                    tbutSimulate.Enabled = false;
                    tbutCreateSerial.Enabled = false;
                    tlbClear.Enabled = false;
                    tbutCancelWorkOrder.Enabled = false;
                    break;

                case 1: /// 1 = New Transaction.
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = false;
                    tbutNewCoilBack.Enabled = false;
                    tbutSave.Enabled = true;
                    tbutCalculate.Enabled = false;
                    tbutSimulate.Enabled = false;
                    tbutCreateSerial.Enabled = false;
                    tlbClear.Enabled = true;
                    tbutCancelWorkOrder.Enabled = false;
                    ResetMaterialGrid();
                    ResetCoilBackGrid();
                    ResetCuttingGrid();
                    break;

                case 2: /// 2 = Transaction was save.
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = true;
                    tlbClear.Enabled = true;
                    break;

                case 3: /// 3 = Selected materail.
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = true;
                    tbutNewCoilBack.Enabled = false;
                    tbutSave.Enabled = true;
                    tbutCalculate.Enabled = true;
                    tbutSimulate.Enabled = false;
                    tbutCreateSerial.Enabled = false;
                    tlbClear.Enabled = true;
                    tbutCancelWorkOrder.Enabled = true;
                    break;

                case 4: /// 4 = Calculated.
                    tbutNewWork.Enabled = false;
                    tbutNewMaterial.Enabled = true;
                    tbutNewCoilBack.Enabled = true;
                    tbutSave.Enabled = true;
                    tbutCalculate.Enabled = true;
                    tbutSimulate.Enabled = true;
                    tbutCreateSerial.Enabled = true;
                    tlbClear.Enabled = true;
                    tbutCancelWorkOrder.Enabled = true;
                    break;
            }
            butAddMaterial.Enabled = tbutNewMaterial.Enabled;
            butAddCoilBack.Enabled = tbutNewCoilBack.Enabled;
        }

        private void SetHeadContent(PlaningHeadModel model)
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
            txtYield.DataBindings.Add("Text", model, "Yield", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0.00");
            txtTotalMaterialAmount.DataBindings.Add("Text", model, "TotalMaterialAmount", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0.00");
            txtTotalWidth.DataBindings.Add("Text", model, "TotalWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0.00");

            //ComboBox
            cmbProcessLine.DataSource = model.ResourceList.ToList();
            cmbProcessLine.DisplayMember = "ResourceDescription";
            cmbProcessLine.ValueMember = "ResourceID";
            cmbProcessLine.DataBindings.Add("SelectedValue", model, "ProcessLineSpec.ResourceID", false, DataSourceUpdateMode.OnPropertyChanged);
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
            HeaderContent.PackingPlan = false;
            HeaderContent.LVTrim = false;

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
            var result = HeaderContent.ValidateModel(model, out objectValid, out messageValid);

            if (!result)
            {
                MessageBox.Show(messageValid, "a", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        private void tbutCalculate_Click(object sender, EventArgs e)
        {
            //Simulated Complate.
            HeaderContent.FormState = 4;
            SetFormState();
        }

        private void butAddMaterial_Click(object sender, EventArgs e)
        {
            //Selected Complate.
            HeaderContent.FormState = 3;
            SetFormState();

            HeaderContent.MaterialPattern = new MaterialModel();
            HeaderContent.ProcessLineSpec = _reRes.GetByID(epiSession.PlantID, cmbProcessLine.SelectedValue.ToString());
            var result = _repo.GetAllMatByFilter(epiSession.PlantID, HeaderContent);
            using (MaterialSelecting frm = new MaterialSelecting(epiSession, result, HeaderContent))
            {
                frm.ShowDialog();
            }
        }

        private void ListMaterialGrid(IEnumerable<MaterialModel> item)
        {
            int i = 0;
            foreach (var p in item)
            {
                dgvMaterial.Rows.Add(p.MCSSNo, p.SerialNo, p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName, p.Thick, p.Width, p.Length
                                     , p.Weight, p.UsingWeight, p.RemainWeight, p.LengthM, p.Quantity, p.RemainQty, p.QuantityPack, p.CBSelect
                                     , p.Status, p.Note, p.BussinessTypeName, p.ProductStatus);

                if (i % 2 == 1)
                {
                    this.dgvMaterial.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
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

        private void cmbProcessLine_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        #endregion Method
    }
}