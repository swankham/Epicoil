using Epicoil.Library;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Planning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class SimulateEntry : BaseSession
    {
        private readonly IWorkEntryRepo _repo;

        public PlanningHeadModel HeadModel;
        private SimulateActionModel SimModel;

        public SimulateEntry(SessionInfo _session = null, PlanningHeadModel model = null, SimulateActionModel simModel = null)
        {
            InitializeComponent();
            this._repo = new WorkEntryRepo();
            HeadModel = model;
            SimModel = simModel;
            epiSession = _session;
        }

        private void ClearContent()
        {
            txtWorkOrderNo.DataBindings.Clear();
            txtWorkOrderNo.Clear();
            txtMaterialWeight.Clear();
            txtMaterialWeight.DataBindings.Clear();
            txtProductWeight.Clear();
            txtProductWeight.DataBindings.Clear();
            txtTrimingWeight.Clear();
            txtTrimingWeight.DataBindings.Clear();
            txtYield.Clear();
            txtYield.DataBindings.Clear();
            txtRewindWeight.Clear();
            txtRewindWeight.DataBindings.Clear();
        }

        private void SetOption()
        {
            cmbCutSeq.Enabled = false;
            txtExpected.ReadOnly = true;

            if (rdoWeight.Checked)
            {
                cmbCutSeq.Enabled = true;
                txtExpected.ReadOnly = false;
            }
            else if (rdoLength.Checked)
            {
                cmbCutSeq.Enabled = true;
                txtExpected.ReadOnly = false;
            }
            else if (rdoDivision.Checked)
            {
                txtExpected.ReadOnly = false;
            }
        }

        private void SetHeader(SimulateActionModel model)
        {
            ClearContent();
            txtWorkOrderNo.DataBindings.Add("Text", model, "WorkOrderNum", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMaterialWeight.DataBindings.Add("Text", model, "MaterialWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0");
            txtProductWeight.DataBindings.Add("Text", model, "ProductWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0");
            txtTrimingWeight.DataBindings.Add("Text", model, "TrimWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0");
            txtYield.DataBindings.Add("Text", model, "Yield", true, DataSourceUpdateMode.OnPropertyChanged, 1, "##0.00");
            txtRewindWeight.DataBindings.Add("Text", model, "RewindWeight", true, DataSourceUpdateMode.OnPropertyChanged, 1, "#,###,##0");

            if (model.CheckYeild(HeadModel, model.Yield))
            {
                txtYield.BackColor = SystemColors.Control;
            }
            else
            {
                txtYield.BackColor = Color.Yellow;
            }
        }

        private void rdoWeight_CheckedChanged(object sender, EventArgs e)
        {
            SetOption();
        }

        private void SimulateEntry_Load(object sender, EventArgs e)
        {
            SetHeader(SimModel);
            ListMaterialGrid(SimModel.Materials);
            ListSimulateGrid(SimModel.Cuttings);
        }

        private void ListMaterialGrid(IEnumerable<MaterialModel> item)
        {
            int i = 0;
            dgvMaterial.Rows.Clear();
            foreach (var p in item)
            {
                dgvMaterial.Rows.Add(p.TransactionLineID, p.MCSSNo, i + 1, p.SerialNo, p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName, p.Thick, p.Width, p.Length
                    , p.Weight, p.UsingWeight
                    , p.RemainWeight, p.LengthM, p.UsingLengthM, p.RemainLengthM
                    , (p.Length == 0) ? 1 : p.QuantityPack, (p.UsingQuantity == 0) ? ((p.Length == 0) ? 1 : p.QuantityPack) : p.UsingQuantity
                    , p.RemainQuantity, Enum.GetName(typeof(MaterialStatus), int.Parse(p.Status)), p.Note, p.BussinessType + " - " + p.BussinessTypeName, Enum.GetName(typeof(ProductStatus)
                    , int.Parse(p.ProductStatus)), p.UsedFlag);
                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvMaterial.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void ListSimulateGrid(IEnumerable<SimulateModel> item)
        {
            int i = 0;
            dgvCutting.Rows.Clear();
            foreach (var p in item)
            {
                dgvCutting.Rows.Add(p.SimLineID, i + 1, p.CutDiv, p.NORNum, p.Thick, p.Width, p.Length, p.Status, p.Stand, p.CutDiv, p.UnitWeight, p.TotalWeight, p.LengthM
                                   , p.CalculatedFlag, p.MaterialSerialNo, p.SOLine, p.SONo, p.CommodityCode + " - " + p.CommodityName, p.SpecCode + " - " + p.SpecName
                                   , p.CoatingCode + " - " + p.CoatingName, p.BussinessType + " - " + p.BussinessTypeName, p.PossessionName);
                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvCutting.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void rdoLength_CheckedChanged(object sender, EventArgs e)
        {
            SetOption();
        }

        private void rdoDivision_CheckedChanged(object sender, EventArgs e)
        {
            SetOption();
        }

        private void butCalculate_Click(object sender, EventArgs e)
        {
            //Validate check box is checked.
            if (rdoWeight.Checked || rdoLength.Checked || rdoDivision.Checked)
            {
                //When weight or length checked.
                if (rdoWeight.Checked || rdoLength.Checked)
                {
                    #region Common validation data within weight and length checked. 
                    if (string.IsNullOrEmpty(cmbCutSeq.Text.GetString()))
                    {
                        MessageBox.Show("Please fill Cut Seq. value.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbCutSeq.Focus();
                        return;
                    }

                    int decVal;
                    if (!int.TryParse(cmbCutSeq.Text, out decVal))
                    {
                        MessageBox.Show("Data type invalid.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbCutSeq.Focus();
                        cmbCutSeq.SelectAll();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtExpected.Text.GetString()))
                    {
                        MessageBox.Show("Please fill Expected value.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtExpected.Focus();
                        return;
                    }

                    decimal decExpVal;
                    if (!decimal.TryParse(txtExpected.Text, out decExpVal))
                    {
                        MessageBox.Show("Data type invalid.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtExpected.Focus();
                        txtExpected.SelectAll();
                        return;
                    }

                    SimModel.CutSeleted = Convert.ToInt32(cmbCutSeq.Text.GetString());
                    string msg = string.Empty;
                    if (!SimModel.ValidateToCal(out msg))
                    {
                        MessageBox.Show(msg, "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbCutSeq.Focus();
                        cmbCutSeq.SelectAll();
                        return;
                    }
                    #endregion

                    /*
                     * Important for case Weight and Length checked.
                     * - Length checked will be convert length expected to weight done and then call CalculateRowForWeightOption(MaterialModel).
                     * - Weight checked can be call CalculateRowForWeightOption(MaterialModel) immediately.
                     */
                    //Get index on current row.
                    int iRow = dgvMaterial.CurrentRow.Index;
                    //Get current material transaction line id.
                    int id = Convert.ToInt32(dgvMaterial.Rows[iRow].Cells["transactionlineid"].Value.ToString()).GetInt();
                    //Get using status on current row.
                    bool usedflag = Convert.ToBoolean(dgvMaterial.Rows[iRow].Cells["usedflag"].Value.ToString()).GetBoolean();
                    //Get material serial No on current row. 
                    string article = dgvMaterial.Rows[iRow].Cells["article"].Value.ToString().GetString().Trim();

                    //Get Material Row by selected.
                    var mat = _repo.GetMaterial(id);

                    //New object instant.
                    //IEnumerable<SimulateModel> line = new List<SimulateModel>();
                    //Get calculated cutting line by [CutDivision, MaterialSerialNo, CalculateFlag == true] parameter, this line will be to verify weight/length remain balance.
                    //line = SimModel.Cuttings.Where(i => i.CutDiv.Equals(SimModel.CutSeleted) && i.MaterialSerialNo.GetString() == article && i.CalculatedFlag.GetBoolean() == true).ToList();

                    //***Length option checked.
                    if (rdoLength.Checked)
                    {
                        //Simulate option = 1 is calculate by Length.
                        SimModel.SimulateOption = 1;
                        //Keep expected value into 'SimModel.Expected' from TextBox.
                        SimModel.Expected = Convert.ToDecimal(txtExpected.Text.GetString());

                        //decimal usingLengthM = Convert.ToDecimal(dgvMaterial.Rows[iRow].Cells["usingLengthM"].Value.ToString()).GetDecimal();
                        decimal LengthM = Convert.ToDecimal(dgvMaterial.Rows[iRow].Cells["LengthM"].Value.ToString()).GetDecimal();
                        //Get Length remaining on current row selected.
                        decimal RemainLengthM = Convert.ToDecimal(dgvMaterial.Rows[iRow].Cells["RemainLengthM"].Value.ToString()).GetDecimal();

                        #region Validation Length remaining for material partial used.
                        //If current material selected is used and 
                        if (usedflag) // && line.ToList().Count == 0)
                        {
                            if (RemainLengthM < SimModel.Expected)
                            {
                                MessageBox.Show("Expected value must Less than or Equals material length(M).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtExpected.Focus();
                                txtExpected.SelectAll();
                                return;
                            }
                        }
                        else
                        {
                            if (LengthM < SimModel.Expected)
                            {
                                MessageBox.Show("Expected value must Less than or Equals material length(M).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtExpected.Focus();
                                txtExpected.SelectAll();
                                return;
                            }
                        }
                        #endregion

                        //Keep variable expected value and convert value to decimal.
                        decimal d = Convert.ToDecimal(txtExpected.Text.GetString());

                        //Calculate all rows for material and cutting grid by Simulate header.
                        //SimModel.CalculateRowForLegnthOption(mat);
                        //Convert Length to Weight and keep [Weight value] for calculate same weight option checked.
                        SimModel.Expected = (mat.Weight * d) / mat.LengthM;                        
                    }

                    //***Weight option checked.
                    if (rdoWeight.Checked)
                    {
                        //Simulate option = 1 is calculate by Weight.
                        SimModel.SimulateOption = 0;
                        //Keeping [Weight value] for calculate.
                        SimModel.Expected = Convert.ToDecimal(txtExpected.Text.GetString());
                    }

                    //Keeping [Cutting value] for calculate.
                    SimModel.CutSeleted = Convert.ToInt32(cmbCutSeq.Text.GetString());

                    //Keeping weight value from material grid.
                    decimal weight = Convert.ToDecimal(dgvMaterial.Rows[iRow].Cells["weight"].Value.ToString()).GetDecimal();
                    //Keeping remain weight value from material grid.
                    decimal remainWeight = Convert.ToDecimal(dgvMaterial.Rows[iRow].Cells["remainWeight"].Value.ToString()).GetDecimal();

                    #region Validation Weight remaining for material partial used.
                    if (usedflag)// && line.ToList().Count == 0)
                    {
                        if (remainWeight < SimModel.Expected)
                        {
                            MessageBox.Show("Expected value must Less than or Equals material weight.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtExpected.Focus();
                            txtExpected.SelectAll();
                            return;
                        }
                    }
                    else
                    {
                        if (weight < SimModel.Expected)
                        {
                            MessageBox.Show("Expected value must Less than or Equals material weight.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtExpected.Focus();
                            txtExpected.SelectAll();
                            return;
                        }
                    }
                    #endregion

                    //Calculate after data preparation in case Weight and Length checked.
                    SimModel.CalculateRowForWeightOption(mat);
                    SimModel.SumTrimmingWeight(HeadModel);
                }
                else if (rdoDivision.Checked)
                {
                    SimModel.SimulateOption = 2;
                    SimModel.Expected = Convert.ToDecimal(txtExpected.Text.GetString());
                    if (_repo.ClearSimulateLines(HeadModel.WorkOrderID))
                    {
                        SimulateActionModel simModel = new SimulateActionModel();
                        simModel.WorkOrderID = HeadModel.WorkOrderID;
                        simModel.WorkOrderNum = HeadModel.WorkOrderNum;
                        simModel.MaterialWeight = HeadModel.InputWeight;
                        simModel.ProductWeight = HeadModel.OutputWeight;
                        simModel.Yield = HeadModel.Yield;
                        simModel.TrimWeight = HeadModel.CuttingDesign.Where(i => i.Status.Equals("S")).Sum(i => i.TotalWeight);
                        simModel.Cuttings.Where(i => i.WorkOrderID.Equals(HeadModel.WorkOrderID));

                        SimModel.Cuttings = _repo.InsertSimulate(epiSession, HeadModel, Convert.ToInt32(SimModel.Expected)).ToList();
                        SimModel.Materials = HeadModel.Materails.ToList();
                    }
                }

                ListMaterialGrid(SimModel.Materials);
                ListSimulateGrid(SimModel.Cuttings);
                SetHeader(SimModel);
            }
            else
            {
                MessageBox.Show("Please select option for calculate.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void butConfirm_Click(object sender, EventArgs e)
        {
            int workComplete = 1; string riskFlag = string.Empty; string msg = string.Empty;

            if (HeadModel.GenSerialFlag == 1)
            {
                MessageBox.Show("This work order has generated serial, can't re-confirm.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!SimModel.CheckYeild(HeadModel, SimModel.Yield))
            {
                DialogResult diaResult = MessageBox.Show("Yield invalid, are you sure to hold work order.", "Question.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (diaResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    workComplete = 2;
                }
            }

            //string msg = string.Empty;
            if (!SimModel.ValidateToConfirm(out msg))
            {
                MessageBox.Show(msg, "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = _repo.UpdateSimulateByWorkOrder(epiSession, SimModel.Cuttings, workComplete);
            HeadModel.Completed = workComplete;
            HeadModel.WorkOrderID = SimModel.WorkOrderID;
            HeadModel.WorkOrderNum = SimModel.WorkOrderNum;
            HeadModel.InputWeight = SimModel.MaterialWeight;
            HeadModel.OutputWeight = SimModel.ProductWeight;
            HeadModel.Yield = SimModel.Yield;
            HeadModel.CuttingDesign = _repo.UpdateCuttingByWorkOrder(epiSession, SimModel.Cuttings, HeadModel.WorkOrderID).ToList();
            HeadModel.Materails = _repo.UpdateMaterialByWorkOrder(epiSession, SimModel.Materials, HeadModel.WorkOrderID);
            this.Close();
        }
    }
}