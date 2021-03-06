﻿using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Planning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Epicoil.Appl.Presentations.Planning
{
    public partial class WorkEntryDialog : BaseSession
    {
        private readonly IUserCodeRepo _repoUcd;
        private readonly IResourceRepo _repoRes;
        private readonly IWorkEntryRepo _repo;

        private PlanningHeadModel Header;
        private IEnumerable<PlanningHeadModel> _model;
        public PlanningHeadModel _selected;

        public WorkEntryDialog(SessionInfo _session, IEnumerable<PlanningHeadModel> param = null)
        {
            InitializeComponent();
            this._repo = new WorkEntryRepo();
            this._repoRes = new ResourceRepo();
            this._repoUcd = new UserCodeRepo();
            this._model = param;
            //this.PatternPara = "";
            //this.StorePerPcsPara = "";
            //this.RemarkPara = "";
            epiSession = _session;
        }

        private void ClearHeaderContent()
        {
            txtWONo.DataBindings.Clear();
            txtPIC.DataBindings.Clear();
            txtProcessStep.DataBindings.Clear();
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

            //DatePicker
            dtWOFrom.Value = DateTime.Now.AddDays(-30);
            dtWOTo.Value = DateTime.Now.AddDays(30);
            dtDueFrom.Value = DateTime.Now.AddDays(-30);
            dtDueTo.Value = DateTime.Now.AddDays(30);
            //Clear Content
            txtWONo.Clear();
            txtPIC.Clear();
            txtProcessStep.Clear();
        }

        private void SetHeadContent(PlanningHeadModel model)
        {
            ClearHeaderContent();
            model.PIC = epiSession.UserID;
            model.PICName = epiSession.UserName;

            //ComboBox
            cmbProcessLine.DataSource = model.Resources.ToList();
            cmbProcessLine.DisplayMember = "ResourceDescription";
            cmbProcessLine.ValueMember = "ResourceID";
            cmbProcessLine.DataBindings.Add("SelectedValue", model, "ProcessLineId", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbOrderType.DataSource = model.OrderTypeList.ToList();
            cmbOrderType.DisplayMember = "CodeDesc";
            cmbOrderType.ValueMember = "CodeID";
            cmbOrderType.DataBindings.Add("SelectedValue", model, "OrderType", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbPossession.DataSource = model.Possessions.ToList();
            cmbPossession.DisplayMember = "CodeDesc";
            cmbPossession.ValueMember = "CodeID";
            cmbPossession.DataBindings.Add("SelectedValue", model, "Possession", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            WorkEntryDialog_Load(sender, e);
        }

        private void WorkEntryDialog_Load(object sender, EventArgs e)
        {
            Header = new PlanningHeadModel();
            Header.Resources = _repoRes.GetAll(epiSession.PlantID).Where(p => p.ResourceGrpID.Equals("L") || p.ResourceGrpID.Equals("R") || p.ResourceGrpID.Equals("S")).ToList();
            Header.OrderTypeList = _repoUcd.GetAll("OrderType").ToList();
            Header.Possessions = _repoUcd.GetAll("Pocessed").ToList();

            SetHeadContent(Header);
            //ClearHeaderContent();

            if (_model == null)
            {
                PlanningHeadModel plan = new PlanningHeadModel();
                plan.Plant = epiSession.PlantID;
                _model = _repo.GetWorkAll(plan);
            }
            SetGrid(_model);
        }

        private void SetGrid(IEnumerable<PlanningHeadModel> data)
        {
            dgvWorkOrder.Rows.Clear();
            foreach (var p in data)
            {
                dgvWorkOrder.Rows.Add(p.WorkOrderID, p.WorkOrderNum, p.ProcessStep, p.ProcessLineId, p.IssueDate, p.DueDate, p.PIC, p.OrderType, p.PossessionName, p.OperationStateName);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var result = _model;

            if (!string.IsNullOrEmpty(txtWONo.Text)) result = result.Where(p => p.WorkOrderNum.ToString().ToUpper().Contains(txtWONo.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(txtProcessStep.Text)) result = result.Where(p => p.ProcessStep.ToString().ToUpper().Contains(txtProcessStep.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(cmbProcessLine.SelectedValue.GetString())) result = result.Where(p => p.ProcessLineId.ToString().ToUpper().Contains(cmbProcessLine.SelectedValue.GetString().ToUpper()));
            if (!string.IsNullOrEmpty(txtPIC.Text)) result = result.Where(p => p.PIC.ToString().ToUpper().Contains(txtPIC.Text.ToString().ToUpper()));
            if (!string.IsNullOrEmpty(cmbOrderType.SelectedValue.GetString())) result = result.Where(p => p.OrderType.ToString().ToUpper().Contains(cmbOrderType.SelectedValue.GetString().ToUpper()));
            if (!string.IsNullOrEmpty(cmbPossession.SelectedValue.GetString())) result = result.Where(p => p.Possession.ToString().ToUpper().Contains(cmbPossession.SelectedValue.GetString().ToUpper()));

            DateTime dtWOFromDT = dtWOFrom.Value.Date;
            DateTime dtWOToDT = dtWOTo.Value.Date;
            DateTime dtDueFromDT = dtDueFrom.Value.Date;
            DateTime dtDueToDT = dtDueTo.Value.Date;

            result = result.Where(p => p.IssueDate.Date >= dtWOFromDT && p.IssueDate.Date <= dtWOToDT);
            result = result.Where(p => p.DueDate.Date >= dtDueFromDT && p.DueDate.Date <= dtDueToDT);

            SetGrid(result);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvWorkOrder.Rows.Count >= 1)
            {
                int iRow = dgvWorkOrder.CurrentRow.Index;
                string WrkNoPara = dgvWorkOrder.Rows[iRow].Cells["workorderid"].Value.ToString();
                string ProcessStepPara = dgvWorkOrder.Rows[iRow].Cells["ProcessStep"].Value.ToString();

                if (!string.IsNullOrEmpty(WrkNoPara))
                {
                    _selected = _repo.GetWorkById(Convert.ToInt32(WrkNoPara), Convert.ToInt32(ProcessStepPara), epiSession.PlantID);
                    _selected.Materials = _repo.GetAllMaterial(epiSession.PlantID, _selected.WorkOrderID).ToList();
                    _selected.CuttingDesign = _repo.GetCuttingLines(_selected.WorkOrderID).ToList();
                    _selected.ProcessLine = _repoRes.GetByID(epiSession.PlantID, _selected.ProcessLineId);
                    this.Close();
                }
            }
        }

        private void dgvWorkOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSelect_Click(sender, e);
        }
    }
}