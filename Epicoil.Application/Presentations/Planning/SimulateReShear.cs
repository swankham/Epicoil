using Epicoil.Library;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Planning;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class SimulateReShear : BaseSession
    {
        private readonly IWorkEntryRepo _repo;

        public PlanningHeadModel HeadModel;
        public SimulateReshearHeadModel ReshearHead;

        public SimulateReShear(SessionInfo _session = null, PlanningHeadModel model = null, SimulateReshearHeadModel data = null)
        {
            InitializeComponent();
            this._repo = new WorkEntryRepo();
            HeadModel = model;
            ReshearHead = data;
            epiSession = _session;
        }

        private void SimulateReShear_Load(object sender, EventArgs e)
        {
            ListMaterialGrid(ReshearHead.Materials.ToList());
            ListCuttingGrid(ReshearHead.Cuttings.ToList());
        }

        private void ListMaterialGrid(List<MaterialModel> item)
        {
            int i = 0;
            dgvMaterial.Rows.Clear();
            foreach (var p in item)
            {
                //p.CalculateUsingLength();
                dgvMaterial.Rows.Add(p.TransactionLineID, p.MCSSNo, p.SerialNo, p.Thick, p.Width, p.Length, p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName);
                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvMaterial.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void ListCuttingGrid(List<CutDesignModel> item)
        {
            int i = 0;
            if (dgvFinishGood.Rows.Count != 0) dgvFinishGood.Rows.Clear();
            foreach (var p in item)
            {
                dgvFinishGood.Rows.Add(p.LineID, p.Thick, p.Width, p.Length, p.CommodityCode, p.SpecCode
                                    , p.CoatingCode, p.BussinessType + " - " + p.BussinessTypeName, Enum.GetName(typeof(Possession), p.Possession)
                                    , p.CustID, p.SONo, (p.SOLine == 0) ? "" : p.SOLine.ToString(), p.NORNum);

                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvFinishGood.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void ListOption1Grid(IEnumerable<SimulateReshearModel> item)
        {
            int i = 0;
            if (dgvOption1.Rows.Count != 0) dgvOption1.Rows.Clear();
            foreach (var p in item)
            {
                dgvOption1.Rows.Add(p.LineID, p.WidthSuggsQty, p.WidthActualQty, p.WidthActualRemain);

                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvOption1.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void ListOption1QtyGrid(IEnumerable<SimulateReshearModel> item)
        {
            int i = 0;
            if (dgvOption1Qty.Rows.Count != 0) dgvOption1Qty.Rows.Clear();
            foreach (var p in item)
            {
                dgvOption1Qty.Rows.Add(p.LineID, p.LengthSuggsQty, p.LengthActualQty, p.LengthActualRemain, p.Quantity);

                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvOption1Qty.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }


        private void ListOption2rid(IEnumerable<SimulateReshearModel> item)
        {
            int i = 0;
            if (dgvOption2.Rows.Count != 0) dgvOption2.Rows.Clear();
            foreach (var p in item)
            {
                dgvOption2.Rows.Add(p.LineID, p.WidthSuggsQty, p.WidthActualQty, p.WidthActualRemain);

                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvOption2.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void ListOption2QtyGrid(IEnumerable<SimulateReshearModel> item)
        {
            int i = 0;
            if (dgvOption2Qty.Rows.Count != 0) dgvOption2Qty.Rows.Clear();
            foreach (var p in item)
            {
                dgvOption2Qty.Rows.Add(p.LineID, p.LengthSuggsQty, p.LengthActualQty, p.LengthActualRemain, p.Quantity);

                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvOption2Qty.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void tbutSimulate_Click(object sender, EventArgs e)
        {
            int transactionLineID = Convert.ToInt32(dgvMaterial.Rows[dgvMaterial.CurrentRow.Index].Cells["transactionlineid"].Value.ToString());
            var mat = HeadModel.Materials.Where(i => i.TransactionLineID == transactionLineID).First();
            ReshearHead.SetMaterialRemain(mat);

            int cutLineID = Convert.ToInt32(dgvFinishGood.Rows[dgvFinishGood.CurrentRow.Index].Cells["lineid"].Value.ToString());
            var cut = HeadModel.CuttingDesign.Where(i => i.LineID == cutLineID).First();

            SimulateReshearModel sim = new SimulateReshearModel();
            sim.WorkOrderID = HeadModel.WorkOrderID;
            sim.MaterialTransLineID = transactionLineID;
            sim.CuttingLineID = cutLineID;
            sim.OptionNum = 1;
            sim.WidthSuggsQty = mat.Width / cut.Width;
            sim.WidthActualQty = sim.WidthSuggsQty;
            sim.WidthSuggsRemain = ReshearHead.RemainWidthOpt1 - (sim.WidthSuggsQty * cut.Width);
            sim.WidthActualRemain = ReshearHead.RemainWidthOpt1 - (sim.WidthActualQty * cut.Width);
            sim.LengthSuggsQty = mat.Length / cut.Length;
            sim.LengthActualQty = sim.LengthSuggsQty;
            sim.LengthSuggsRemain = ReshearHead.RemainLengthOpt1 - (sim.LengthSuggsQty * cut.Length);
            sim.LengthActualRemain = ReshearHead.RemainLengthOpt1 - (sim.LengthActualQty * cut.Length);

            var result = _repo.SaveReshearSimulation(epiSession, sim).Where(i => i.OptionNum == 1);

            ListOption1Grid(result);
            ListOption1QtyGrid(result);

            ReshearHead.SetMaterialRemain(mat);
            SimulateReshearModel simll = new SimulateReshearModel();
            simll.WorkOrderID = HeadModel.WorkOrderID;
            simll.MaterialTransLineID = transactionLineID;
            simll.CuttingLineID = cutLineID;
            simll.OptionNum = 2;
            simll.WidthSuggsQty = mat.Width / cut.Length;
            simll.WidthActualQty = mat.Width / cut.Length;
            simll.WidthSuggsRemain = ReshearHead.RemainWidthOpt1 - (simll.WidthSuggsQty * cut.Length);
            simll.WidthActualRemain = ReshearHead.RemainWidthOpt1 - (simll.WidthActualQty * cut.Length);
            simll.LengthSuggsQty = mat.Length / cut.Width;
            simll.LengthActualQty = mat.Length / cut.Width;
            simll.LengthSuggsRemain = ReshearHead.RemainLengthOpt1 - (simll.LengthSuggsQty * cut.Width);
            simll.LengthActualRemain = ReshearHead.RemainLengthOpt1 - (simll.LengthActualQty * cut.Width);

            var resultLen = _repo.SaveReshearSimulation(epiSession, simll).Where(i => i.OptionNum == 2);

            ListOption2rid(resultLen);
            ListOption2QtyGrid(resultLen);
        }
    }
}