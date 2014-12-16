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
    public partial class PackingOrder : BaseSession
    {
        #region Fields

        private readonly IPackingOrderRepo _repo;
        private PackingOrderModel HeaderContent;

        #endregion Fields

        #region Constructors

        public PackingOrder(SessionInfo _session = null, PackingOrderModel model = null)
        {
            InitializeComponent();
            epiSession = _session;
            this._repo = new PackingOrderRepo();
            this.HeaderContent = new PackingOrderModel();
        }

        #endregion Constructors

        #region Forms events

        private void butWorkOrder_Click(object sender, EventArgs e)
        {
            HeaderContent = new PackingOrderModel();
            PlanningHeadModel plan = new PlanningHeadModel();
            plan.Plant = epiSession.PlantID;
            plan.Completed = 1; //Completed.
            plan.GenSerialFlag = 1;  //Serial has created.
            plan.OperationState = 1; //Stay on production operation.

            var result = _repo.GetWorkAll(plan);
            using (WorkEntryDialog frm = new WorkEntryDialog(epiSession, result))
            {
                frm.ShowDialog();
                if (frm._selected != null)
                {
                    HeaderContent.WorkOrderId = frm._selected.WorkOrderID;
                    HeaderContent.WorkOrderNum = frm._selected.WorkOrderNum;
                    if (!_repo.PackOrderExist(HeaderContent.WorkOrderId))
                    {
                        PackingOrderModel model = new PackingOrderModel();
                        model.WorkOrderId = HeaderContent.WorkOrderId;
                        model.PackOrderNum = "PKG-" + HeaderContent.WorkOrderNum;
                        model.Remark = "";
                        model.CompleteFlag = 0;

                        if (_repo.SavePackOrder(epiSession, model, out HeaderContent))
                        {
                            var styles = _repo.GetPackStyleByWorkOrder(HeaderContent.WorkOrderId, HeaderContent.Id);
                            if (_repo.SavePackStyles(epiSession, styles))
                            {
                                var resultStyle = _repo.GetPackStyleByPackOrder(HeaderContent.WorkOrderId).ToList();
                                foreach (var item in resultStyle)
                                {
                                    var savingSn = _repo.GetSerialForFirstDefault(item);
                                    _repo.SaveSerialByStyle(epiSession, savingSn);
                                }
                            }
                        }
                    }

                    HeaderContent = _repo.GetPackOrderByID(HeaderContent.WorkOrderId);
                }
            }

            BindingContentsHeader(HeaderContent);
            ListPackingStyleToGrid(HeaderContent.PackStyles);
            string styleId = dgvPackStyle.CurrentRow.Cells["styleid"].Value.GetString();
            ListSerialCuttingToGrid(HeaderContent.SerialLines.Where(i => i.PackStyleId == Convert.ToInt32(styleId)));
            ListSkidWithinSerial(HeaderContent.SkidPacks.Where(i => i.PackStyleId == Convert.ToInt32(styleId)));
        }

        private void dgvPackStyle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //var grouper = new DataGridViewGrouper(dataGridView1);
            if (e.RowIndex > -1)
            {
                var dgv = (DataGridView)sender;
                string rowId = dgv.Rows[e.RowIndex].Cells["styleid"].Value.GetString();
                var result = HeaderContent.PackStyles.Where(i => i.Id == Convert.ToInt32(rowId)).FirstOrDefault();
                BindingStyleContent(result);

                ListSerialCuttingToGrid(HeaderContent.SerialLines.Where(i => i.PackStyleId == Convert.ToInt32(rowId)));
                ListSkidWithinSerial(HeaderContent.SkidPacks.Where(i => i.PackStyleId == Convert.ToInt32(rowId)));
            }
        }

        private void dgvCutting_DragDrop(object sender, DragEventArgs e)
        {
            var dgv = (DataGridView)sender;
            //DataGridViewRow row = dataGridView.Rows.GetFirstRow();
        }

        private void butAddPacking_Click(object sender, EventArgs e)
        {
            string styleId = dgvPackStyle.CurrentRow.Cells["styleid"].Value.GetString();
            if (string.IsNullOrEmpty(styleId))
            {
                MessageBox.Show("Please select pack style.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SkidPackingModel model = new SkidPackingModel();
            model.HeadLineID = HeaderContent.Id;
            model.PackStyleId = Convert.ToInt32(styleId);
            var seqResult = HeaderContent.SkidPacks.Where(i => i.PackStyleId == Convert.ToInt32(styleId)).ToList();
            model.Seq = seqResult.Count + 1;

            HeaderContent.SkidPacks = _repo.AddSkidPacking(epiSession, model).ToList();
            ListSkidWithinSerial(HeaderContent.SkidPacks.Where(i => i.PackStyleId == Convert.ToInt32(styleId)));
        }

        private void butDeletePacking_Click(object sender, EventArgs e)
        {
            string styleId = dgvPackStyle.CurrentRow.Cells["styleid"].Value.GetString();
            string id = dgvSkidNumber.CurrentRow.Cells["id"].Value.GetString();
            string serialId = dgvSkidNumber.CurrentRow.Cells["serialId"].Value.GetString();
            int snId = string.IsNullOrEmpty(serialId) ? 0 : Convert.ToInt32(serialId);

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Please select packing design.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HeaderContent.SkidPacks = _repo.DeleteSkidPacking(epiSession, HeaderContent.Id, Convert.ToInt32(id), snId).ToList();
            ListSkidWithinSerial(HeaderContent.SkidPacks.Where(i => i.PackStyleId == Convert.ToInt32(styleId)));
            ListSerialCuttingToGrid(HeaderContent.SerialLines.Where(i => i.PackStyleId == Convert.ToInt32(styleId)));
        }

        private void butRight_Click(object sender, EventArgs e)
        {
            string id = string.Empty;
            try
            {
                id = dgvSkidNumber.CurrentRow.Cells["id"].Value.GetString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string styleId = dgvPackStyle.CurrentRow.Cells["styleid"].Value.GetString();
            string serialId = dgvSkidNumber.CurrentRow.Cells["serialId"].Value.GetString();
            int snId = string.IsNullOrEmpty(serialId) ? 0 : Convert.ToInt32(serialId);

            Int32 selectedCellCount = dgvCutting.GetCellCount(DataGridViewElementStates.Selected);
            string serialCollection = string.Empty;

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Please select packing design.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedCellCount > 0)
            {
                for (int i = 0; i < selectedCellCount; i++)
                {
                    if (dgvCutting.SelectedCells[i].OwningColumn.Name.ToString() == "seriallineid")
                    {
                        serialCollection += string.Format(@"{0},", dgvCutting.SelectedCells[i].Value.GetString());
                    }
                }
            }

            if (!string.IsNullOrEmpty(serialCollection))
            {
                SkidPackingModel model = new SkidPackingModel();
                HeaderContent.SkidPacks = _repo.AddSerialToSkid(epiSession, HeaderContent.Id, Convert.ToInt32(id), serialCollection.Substring(0, serialCollection.Length - 1)).ToList();
                ListSkidWithinSerial(HeaderContent.SkidPacks.Where(i => i.PackStyleId == Convert.ToInt32(styleId)));
                ListSerialCuttingToGrid(HeaderContent.SerialLines.Where(i => i.PackStyleId == Convert.ToInt32(styleId)));
            }
        }

        #endregion Forms events

        #region Binding controls

        private void BindingContentsHeader(PackingOrderModel model)
        {
            ClearBindingContensHead();
            txtWorkOrderNum.DataBindings.Add("Text", model, "WorkOrderNum", false, DataSourceUpdateMode.OnPropertyChanged);
            txtRemark.DataBindings.Add("Text", model, "Remark", false, DataSourceUpdateMode.OnPropertyChanged);

            //Set value to DatePicker
            //dptIssueDate.Value = model.IssueDate;
            //dptDueDate.Value = model.DueDate;
        }

        private void BindingStyleContent(PackStyleOrderModel model)
        {
            ClearBindingByPackStyle();
            txtCustomer.Text = model.CustId + " - " + model.CustomerName;
            txtPackingStyle.Text = model.StyleCode;
            txtTotalQty.Text = model.TotalQuantity.ToString("0.00");
            txtPackingRemark.Text = model.Remarks;
        }

        private void ClearBindingContensHead()
        {
            txtWorkOrderNum.Clear();
            txtWorkOrderNum.DataBindings.Clear();
            txtRemark.Clear();
            txtRemark.DataBindings.Clear();
            dptIssueDate.Value = DateTime.Now;
            dptDueDate.Value = DateTime.Now;

            dgvPackStyle.Rows.Clear();
            ClearBindingByPackStyle();
        }

        private void ClearBindingByPackStyle()
        {
            dgvCutting.Rows.Clear();
            dgvSkidNumber.Rows.Clear();
            txtCustomer.Clear();
            txtPackingStyle.Clear();
            txtTotalQty.Clear();
            txtPackingRemark.Clear();
        }

        private void ListSerialCuttingToGrid(IEnumerable<SerialsPackingModel> item)
        {
            int i = 0;
            dgvCutting.Rows.Clear();
            foreach (var p in item)
            {
                dgvCutting.Rows.Add(p.Id, p.SerialLineId, p.DesignedPack, p.SerialNo, p.Thick, p.Width, p.Length, p.UnitWeight
                                             , p.CommodityCode + " - " + p.CommodityName
                                             , p.SpecCode + " - " + p.SpecName, p.CoatingCode + " - " + p.CoatingName
                                             , p.BussinessType + " - " + p.BussinessTypeName, p.PossessionName);
                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvCutting.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void ListPackingStyleToGrid(IEnumerable<PackStyleOrderModel> item)
        {
            int i = 0;
            dgvPackStyle.Rows.Clear();
            foreach (var p in item)
            {
                dgvPackStyle.Rows.Add(p.Id, p.CustId + " - " + p.CustomerName, p.StyleCode, p.Size, p.TotalQuantity
                                     , p.CoilWeigthPackMin, p.CoilWeigthPackMax, p.CoilPerPackMin, p.CoilPerPackMax);
                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvPackStyle.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void ListSkidWithinSerial(IEnumerable<SkidPackingModel> item)
        {
            int i = 0;
            dgvSkidNumber.Rows.Clear();
            Font font = new Font(dgvSkidNumber.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold);

            HeaderContent.SerialLines = _repo.GetSerialByStyleID(HeaderContent.Id).ToList();
            foreach (var p in item.OrderBy(x => x.Seq))
            {
                dgvSkidNumber.Rows.Add(p.Id, "", p.Seq, "Coil = " + HeaderContent.SerialLines.Where(n => n.PackingDesignId == p.Id).Count()
                                       , "Total = " + HeaderContent.SerialLines.Where(n => n.PackingDesignId == p.Id).Sum(n => n.UnitWeight).ToString("#,##0.00"));

                this.dgvSkidNumber.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                this.dgvSkidNumber.Rows[i].DefaultCellStyle.Font = font;
                i++;
                foreach (var x in HeaderContent.SerialLines.Where(n => n.PackingDesignId == p.Id).OrderBy(m => m.SerialNo))
                {
                    dgvSkidNumber.Rows.Add(p.Id, x.SerialLineId, "", x.SerialNo, x.UnitWeight.ToString("#,##0.00"));
                    this.dgvSkidNumber.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    i++;
                }
            }
        }

        #endregion Binding controls
    }
}