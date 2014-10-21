using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class MaterialSelecting : BaseSession
    {
        private IEnumerable<MaterialModel> model;
        private PlaningHeadModel baseOrder;

        public MaterialSelecting(SessionInfo _session, IEnumerable<MaterialModel> data, PlaningHeadModel workOrder)
        {
            InitializeComponent();
            epiSession = _session;
            this.model = data;
            this.baseOrder = workOrder;
        }

        private void ListMaterialGrid(IEnumerable<MaterialModel> item)
        {
            int i = 0;
            dgvMaterial.Rows.Clear();
            foreach (var p in item)
            {
                dgvMaterial.Rows.Add(p.MCSSNo, p.SerialNo, p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName.GetString(), p.Thick, p.Width, p.Length
                                     , p.Weight, p.LengthM, p.QuantityPack, p.Note, p.BussinessType + " - " + p.BussinessTypeName.GetString(), p.SupplierCode + " - " + p.SupplierName.GetString()
                                     , p.CustID + " - " + p.CustomerName, p.MakerCode + " - " + p.MakerName.GetString(), p.MillCode + " - " + p.MillName.GetString());

                if (i % 2 == 1)
                {
                    this.dgvMaterial.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void MaterialSelecting_Load(object sender, EventArgs e)
        {
            ListMaterialGrid(this.model);
            SetNumricText();
        }

        private void SetNumricText()
        {
            numThickMin.Minimum = baseOrder.ProcessLineSpec.ThickMin;
            numThickMin.Maximum = baseOrder.ProcessLineSpec.ThickMax;
            numThickMin.Value = baseOrder.ProcessLineSpec.ThickMin;
            numThickMax.Minimum = baseOrder.ProcessLineSpec.ThickMin;
            numThickMax.Maximum = baseOrder.ProcessLineSpec.ThickMax;
            numThickMax.Value = baseOrder.ProcessLineSpec.ThickMax;

            numWidthMin.Minimum = baseOrder.ProcessLineSpec.WidthMin;
            numWidthMin.Maximum = baseOrder.ProcessLineSpec.WidthMax;
            numWidthMin.Value = baseOrder.ProcessLineSpec.WidthMin;
            numWidthMax.Minimum = baseOrder.ProcessLineSpec.WidthMin;
            numWidthMax.Maximum = baseOrder.ProcessLineSpec.WidthMax;
            numWidthMax.Value = baseOrder.ProcessLineSpec.WidthMax;

            numLengthMin.Minimum = baseOrder.ProcessLineSpec.LengthMin;
            numLengthMin.Maximum = baseOrder.ProcessLineSpec.LengthMax;
            numLengthMin.Value = baseOrder.ProcessLineSpec.LengthMin;
            numLengthMax.Minimum = baseOrder.ProcessLineSpec.LengthMin;
            numLengthMax.Maximum = baseOrder.ProcessLineSpec.LengthMax;
            numLengthMax.Value = baseOrder.ProcessLineSpec.LengthMax;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            var result = this.model;

            if (!string.IsNullOrEmpty(txtCustID.Text)) result = result.Where(p => p.CustID.ToString().ToUpper().Contains(txtCustID.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(txtCommodityCode.Text)) result = result.Where(p => p.CommodityCode.ToString().ToUpper().Contains(txtCommodityCode.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(txtSpecCode.Text)) result = result.Where(p => p.SpecCode.ToString().ToUpper().Contains(txtSpecCode.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(txtCoatingCode.Text)) result = result.Where(p => p.CoatingCode.ToString().ToUpper().Contains(txtCoatingCode.Text.ToString().ToUpper()));
            //if (!string.IsNullOrEmpty(txtCategory.Text)) result = result.Where(p => p.SpecCode.ToString().ToUpper().Contains(txtCategory.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(txtBT.Text)) result = result.Where(p => p.BussinessType.ToString().ToUpper().Contains(txtBT.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(txtMakerCode.Text)) result = result.Where(p => p.MakerCode.ToString().ToUpper().Contains(txtMakerCode.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(txtMillCode.Text)) result = result.Where(p => p.MillCode.ToString().ToUpper().Contains(txtMillCode.Text.ToString().ToUpper()));

            if (numThickMin.Value != 0) result = result.Where(p => p.Thick >= numThickMin.Value);
            if (numThickMax.Value != 0) result = result.Where(p => p.Thick <= numThickMax.Value);
            if (numWidthMin.Value != 0) result = result.Where(p => p.Width >= numWidthMin.Value);
            if (numWidthMax.Value != 0) result = result.Where(p => p.Width <= numWidthMax.Value);
            if (numLengthMin.Value != 0) result = result.Where(p => p.Length >= numLengthMin.Value);
            if (numLengthMax.Value != 0) result = result.Where(p => p.Length <= numLengthMax.Value);

            ListMaterialGrid(result);
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            txtCustID.Clear();
            txtCommodityCode.Clear();
            txtSpecCode.Clear();
            txtCoatingCode.Clear();
            txtCategory.Clear();
            txtBT.Clear();
            txtMakerCode.Clear();
            txtMillCode.Clear();
            SetNumricText();
            ListMaterialGrid(this.model);
        }
    }
}