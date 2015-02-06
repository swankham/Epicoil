using Epicoil.Library.Models;
using Epicoil.Library.Models.Production;
using Epicoil.Library.Repositories.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Production
{
    public partial class ProductionPlan : BaseSession
    {
        #region Fields

        private readonly IProductionPlanRepo _repo;
        private ProductionPlanModel domain;

        #endregion Fields

        #region Constructers

        public ProductionPlan(SessionInfo _session, ProductionPlanModel model = null)
        {
            InitializeComponent();
            this._repo = new ProductionPlanRepo();
            this.domain = new ProductionPlanModel();
            epiSession = _session;
        }

        #endregion Constructers

        #region Binding data with control

        private void BindContent(ProductionPlanModel model)
        {
            ClearBinding();
            txtWorkOrderNumForm.DataBindings.Add("Text", model, "WorkOrderNumForm", false, DataSourceUpdateMode.OnPropertyChanged);
            txtWorkOrderNumTo.DataBindings.Add("Text", model, "WorkOrderNumTo", false, DataSourceUpdateMode.OnPropertyChanged);

            dtpWorkDateFrom.DataBindings.Add("Value", model, "WorkDateFrom", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpWorkDateTo.DataBindings.Add("Value", model, "WorkDateTo", true, DataSourceUpdateMode.OnPropertyChanged);

            dtpDueDateFrom.DataBindings.Add("Value", model, "DueDateFrom", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpDueDateTo.DataBindings.Add("Value", model, "DueDateTo", true, DataSourceUpdateMode.OnPropertyChanged);

            cboMachineGroup.DataSource = model.Resources;
            cboMachineGroup.DisplayMember = "ResourceDescription";
            cboMachineGroup.ValueMember = "ResourceID";
            cboMachineGroup.DataBindings.Add("SelectedValue", model, "MachineCode", true, DataSourceUpdateMode.OnPropertyChanged);

            cboMachine.DataSource = model.Resources;
            cboMachine.DisplayMember = "ResourceDescription";
            cboMachine.ValueMember = "ResourceID";
            cboMachine.DataBindings.Add("SelectedValue", model, "MachineCode", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void ClearBinding()
        {
            txtWorkOrderNumForm.DataBindings.Clear();
            txtWorkOrderNumTo.DataBindings.Clear();

            txtWorkOrderNumForm.Clear();
            txtWorkOrderNumTo.Clear();

            dtpWorkDateFrom.DataBindings.Clear();
            dtpWorkDateTo.DataBindings.Clear();

            dtpWorkDateFrom.Value = DateTime.Now;
            dtpWorkDateTo.Value = DateTime.Now;

            dtpDueDateFrom.DataBindings.Clear();
            dtpDueDateTo.DataBindings.Clear();

            dtpDueDateFrom.Value = DateTime.Now;
            dtpDueDateTo.Value = DateTime.Now;

            cboMachineGroup.DataBindings.Clear();
            cboMachine.DataBindings.Clear();

            rdoAllMachine.Checked = true;
        }

        #endregion Binding data with control

        #region Binding forms event

        private void ProductionPlan_Load(object sender, EventArgs e)
        {
            domain = _repo.Get(epiSession);
            BindContent(domain);
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            domain = _repo.Get(epiSession);
            BindContent(domain);
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            ValidationContext context = new ValidationContext(domain, null, null);
            IList<ValidationResult> errors = new List<ValidationResult>();

            if (!Validator.TryValidateObject(domain, context, errors, true))
            {
                foreach (ValidationResult result in errors)
                    MessageBox.Show(result.ErrorMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetGrid(domain);
        }

        private void butRight_Click(object sender, EventArgs e)
        {
            var rowSelected = domain.WorkOrders.Where(i => i.WorkOrderId.Equals(Convert.ToInt32(dgvWorkOrder.CurrentRow.Cells["id"].Value.GetString()))).FirstOrDefault();
            int n = dgvPlanned.Rows.Count == 0 ? 0 : dgvPlanned.CurrentRow.Index;
            domain.WorkOrdersPlanned.Insert(n, rowSelected);
            domain.WorkOrders.Remove(rowSelected);

            SetPlannedGrid(domain);
            SetGrid(domain);

            dgvPlanned.ClearSelection();
            dgvPlanned.Rows[n].Selected = true;
        }

        private void butLeft_Click(object sender, EventArgs e)
        {
            var rowSelected = domain.WorkOrdersPlanned.Where(i => i.WorkOrderId.Equals(Convert.ToInt32(dgvPlanned.CurrentRow.Cells["idd"].Value.GetString()))).FirstOrDefault();
            domain.WorkOrdersPlanned.Remove(rowSelected);
            domain.WorkOrders.Insert(0, rowSelected);

            SetPlannedGrid(domain);
            SetGrid(domain);
        }

        #endregion Binding forms event

        #region Methods

        private void SetGrid(ProductionPlanModel model)
        {
            int i = 0;
            if (model.WorkOrders.Count == 0) return;
            dgvWorkOrder.Rows.Clear();
            foreach (var p in model.WorkOrders)
            {
                dgvWorkOrder.Rows.Add(p.WorkOrderId, p.WorkOrderNum, p.ProcessStep, p.ProcessLine, p.IssueDate, p.DueDate, p.PossessionName, p.PICName, p.Yield);

                if (i % 2 == 1)
                {
                    this.dgvWorkOrder.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void SetPlannedGrid(ProductionPlanModel model)
        {
            int i = 0;
            if (model.WorkOrders.Count == 0) return;
            dgvPlanned.Rows.Clear();
            foreach (var p in model.WorkOrdersPlanned)
            {
                dgvPlanned.Rows.Add(i + 1, p.WorkOrderId, p.WorkOrderNum, p.ProcessStep, p.ProcessLine, p.IssueDate, p.DueDate, p.PossessionName, p.PICName, p.Yield);

                if (i % 2 == 1)
                {
                    this.dgvPlanned.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        #endregion Methods
    }
}