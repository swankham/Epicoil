using Epicoil.Library;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Planning;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            HeadModel = model;
            SimModel = simModel;
        }

        private void ClearContent()
        {
            txtWorkOrderNo.Clear();
            txtWorkOrderNo.DataBindings.Clear();
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
            txtMaterialWeight.DataBindings.Add("Text", model, "MaterialWeight", false, DataSourceUpdateMode.OnPropertyChanged);
            txtProductWeight.DataBindings.Add("Text", model, "ProductWeight", false, DataSourceUpdateMode.OnPropertyChanged);
            txtTrimingWeight.DataBindings.Add("Text", model, "TrimWeight", false, DataSourceUpdateMode.OnPropertyChanged);
            txtYield.DataBindings.Add("Text", model, "Yield", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void rdoWeight_CheckedChanged(object sender, EventArgs e)
        {
            SetOption();
        }

        private void SimulateEntry_Load(object sender, EventArgs e)
        {
            SetHeader(SimModel);
            ListMaterialGrid(SimModel.Materials);
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
                    , p.RemainQuantity, Enum.GetName(typeof(MaterialStatus), int.Parse(p.Status)), p.Note, p.BussinessType + " - " + p.BussinessTypeName, Enum.GetName(typeof(ProductStatus), int.Parse(p.ProductStatus)));
                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvMaterial.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }
    }
}