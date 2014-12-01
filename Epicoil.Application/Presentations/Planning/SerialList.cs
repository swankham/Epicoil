using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Planning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class SerialList : BaseSession
    {
        private readonly IWorkEntryRepo _repo;

        private IEnumerable<GeneratedSerialModel> snList;
        private PlanningHeadModel workParent;
        public bool GenSNComplete;

        public SerialList(SessionInfo _session = null, IEnumerable<GeneratedSerialModel> model = null, PlanningHeadModel head = null)
        {
            InitializeComponent();
            this._repo = new WorkEntryRepo();
            epiSession = _session;
            snList = model;
            workParent = head;
            GenSNComplete = true;
        }

        private void ListGrid(IEnumerable<GeneratedSerialModel> item)
        {
            int i = 0;
            dgvCutting.Rows.Clear();
            foreach (var p in item)
            {
                dgvCutting.Rows.Add(p.SerialNo, p.Thick, p.Width, p.Length, p.Quantity, p.UnitWeight, p.Status
                                    , p.CommodityCode + " - " + p.CommodityName, p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName
                                    , p.BussinessType + " - " + p.BussinessTypeName, p.PossessionName, p.MCSSNo);
                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvCutting.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void SerialList_Load(object sender, System.EventArgs e)
        {
            ListGrid(snList);
        }

        private void dgvCutting_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            string status = dgvCutting.Rows[e.RowIndex].Cells["status"].Value.GetString();

            if (status == "C")
            {
                nextProcessStepToolStripMenuItem.Enabled = true;
            }
            else
            {
                nextProcessStepToolStripMenuItem.Enabled = false;
            }
        }

        private void dgvCutting_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            string status = dgvCutting.Rows[e.RowIndex].Cells["status"].Value.GetString();

            if (status == "C")
            {
                nextProcessStepToolStripMenuItem.Enabled = true;
            }
            else
            {
                nextProcessStepToolStripMenuItem.Enabled = false;
            }
        }

        private void nextProcessStepToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            PlanningHeadModel newWork = new PlanningHeadModel();

            if (dgvCutting.Rows.Count >= 1)
            {
                int iRow = dgvCutting.CurrentRow.Index;
                string mcssno = dgvCutting.Rows[iRow].Cells["mcssno"].Value.ToString();
                string lotno = dgvCutting.Rows[iRow].Cells["serialNo"].Value.ToString();

                if (!string.IsNullOrEmpty(mcssno))
                {
                    var mat = _repo.GetMaterial(epiSession.PlantID, mcssno, lotno);

                    if (mat == null)
                    {
                        MessageBox.Show("This Material is not available.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    newWork.ProcessStep = _repo.GetStepByWorkOrder(workParent.WorkOrderID);
                    newWork.WorkOrderID = workParent.WorkOrderID;
                    newWork.WorkOrderNum = workParent.WorkOrderNum;
                    newWork.IssueDate = DateTime.Now;
                    newWork.DueDate = DateTime.Now;
                    newWork = _repo.Save(epiSession, newWork);

                    mat.WorkOrderID = newWork.WorkOrderID;
                    mat.WorkOrderNum = newWork.WorkOrderNum;
                    mat.WorkDate = newWork.IssueDate;
                    mat.SetQuantityPack();
                    mat.SetUsingWeight();
                    mat.SetUsingQuantity();
                    var result = _repo.SaveMaterial(epiSession, mat);
                }
                using (WorkEntry frm = new WorkEntry(epiSession, newWork))
                {
                    frm.Show();
                }
                this.Close();
            }
        }

        private void tlbClear_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (workParent.OperationState > 1)
            {
                MessageBox.Show("This work order has to used in Production process, can't delete serials.","Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _repo.ClearSerialInEpicor(epiSession, workParent, out msg);
            _repo.ClearSerialInEpicor(workParent.WorkOrderID);
            GenSNComplete = false;
            this.Close();
            //}
        }
    }
}