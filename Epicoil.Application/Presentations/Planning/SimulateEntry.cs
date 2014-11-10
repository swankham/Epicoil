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

        private PlanningHeadModel HeadModel;
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
                dgvCutting.Rows.Add(p.SimSeq, i + 1, p.CutDiv, p.NORNum, p.Thick, p.Width, p.Length, p.Status, p.Stand, p.CutDiv, p.UnitWeight, p.TotalWeight, p.LengthM
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
            if (rdoWeight.Checked || rdoLength.Checked || rdoDivision.Checked)
            {
                if (rdoWeight.Checked || rdoLength.Checked)
                {
                    if (string.IsNullOrEmpty(cmbCutSeq.Text.GetString()))
                    {
                        MessageBox.Show("Please fill Cut Seq. value.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbCutSeq.Focus();
                        return;
                    }

                    decimal decVal;
                    if (!decimal.TryParse(cmbCutSeq.Text, out decVal))
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

                    int iRow = dgvMaterial.CurrentRow.Index;
                    int id = Convert.ToInt32(dgvMaterial.Rows[iRow].Cells["transactionlineid"].Value.ToString()).GetInt();
                    bool usedflag = Convert.ToBoolean(dgvMaterial.Rows[iRow].Cells["usedflag"].Value.ToString()).GetBoolean();
                    string article = dgvMaterial.Rows[iRow].Cells["article"].Value.ToString().GetString().Trim();

                    var mat = _repo.GetMaterial(id);
                    IEnumerable<SimulateModel> line = new List<SimulateModel>();
                    line = SimModel.Cuttings.Where(i => i.CutDiv.Equals(SimModel.CutSeleted) && i.MaterialSerialNo.GetString() == article && i.CalculatedFlag.GetBoolean() == true).ToList();
                    if (rdoLength.Checked)
                    {
                        SimModel.SimulateOption = 1;
                        SimModel.Expected = Convert.ToDecimal(txtExpected.Text.GetString());
                        decimal usingLengthM = Convert.ToDecimal(dgvMaterial.Rows[iRow].Cells["usingLengthM"].Value.ToString()).GetDecimal();
                        decimal LengthM = Convert.ToDecimal(dgvMaterial.Rows[iRow].Cells["LengthM"].Value.ToString()).GetDecimal();
                        decimal RemainLengthM = Convert.ToDecimal(dgvMaterial.Rows[iRow].Cells["RemainLengthM"].Value.ToString()).GetDecimal();

                        if (usedflag && line.ToList().Count == 0)
                        {
                            if (RemainLengthM < SimModel.Expected)
                            {
                                MessageBox.Show("Expected value must Less than or Equals material length(M).", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtExpected.Focus();
                                txtExpected.SelectAll();
                                return;
                            }
                        }
                        else
                        {
                            if (LengthM < SimModel.Expected)
                            {
                                MessageBox.Show("Expected value must Less than or Equals material length(M).", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtExpected.Focus();
                                txtExpected.SelectAll();
                                return;
                            }
                        }

                        decimal d = Convert.ToDecimal(txtExpected.Text.GetString());
                        SimModel.Expected = (mat.Weight * d) / mat.LengthM;
                    }
                    
                    if (rdoWeight.Checked)
                    {
                        SimModel.SimulateOption = 0;
                        SimModel.Expected = Convert.ToDecimal(txtExpected.Text.GetString());
                    }

                    SimModel.CutSeleted = Convert.ToInt32(cmbCutSeq.Text.GetString());
                    decimal weight = Convert.ToDecimal(dgvMaterial.Rows[iRow].Cells["weight"].Value.ToString()).GetDecimal();
                    decimal remainWeight = Convert.ToDecimal(dgvMaterial.Rows[iRow].Cells["remainWeight"].Value.ToString()).GetDecimal();

                    if (usedflag && line.ToList().Count == 0)
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

                    SimModel.CalculateRowForWeightOption(mat);
                    SimModel.SumTrimmingWeight(HeadModel);
                }
                else if (rdoDivision.Checked)
                {
                    SimModel.SimulateOption = 2;
                }

                ListMaterialGrid(SimModel.Materials);
                ListSimulateGrid(SimModel.Cuttings);
                SetHeader(SimModel);
            }
            else
            {
                MessageBox.Show("Please select option for calculate.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void butConfirm_Click(object sender, EventArgs e)
        {
            if (!SimModel.CheckYeild(HeadModel, SimModel.Yield))
            {
                MessageBox.Show("Cannot confirm becuase Yield invalid.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}