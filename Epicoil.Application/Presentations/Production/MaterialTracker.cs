using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicoil.Library.Repositories.Production;
using Epicoil.Library.Models.Production;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Planning;

namespace Epicoil.Appl.Presentations.Production
{
    public partial class MaterialTracker : BaseSession
    {
        private readonly IWorkEntryRepo _repoWork;
        private readonly IMaterialFindingRepo _repo;
        private IEnumerable<MaterialFindingModel> list;
        private IEnumerable<UserCodeModel> possessionList;
        private IEnumerable<ResourceModel> resourceList;
        private readonly IResourceRepo _repoResrc;
        private readonly IUserCodeRepo _repoUcode;

        MaterialFindingModel modelFilter;

        public MaterialTracker(SessionInfo _session = null, string str = null)
        {
            InitializeComponent();
            epiSession = _session;
            _repoWork = new WorkEntryRepo();
            _repo = new MaterialFindingRepo();
            _repoResrc = new ResourceRepo();
            _repoUcode = new UserCodeRepo();

            list = new List<MaterialFindingModel>();
            possessionList = new List<UserCodeModel>();
            resourceList = new List<ResourceModel>();
            modelFilter = new MaterialFindingModel();
        }

        private void MaterialTracker_Load(object sender, EventArgs e)
        {
            ClearText();
            SetListInComboBox();
            list = _repo.GetAllMaterailTracker(epiSession.PlantID);
            ListMaterialGrid(list);
            ClearText();
        }

        private void ListMaterialGrid(IEnumerable<MaterialFindingModel> item)
        {
            int i = 0;
            dgvList.Rows.Clear();
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.WorkOrderID, p.TransactionLineID, p.FoundFlag, p.UnPackFlag, p.ProcessFlag, p.ProcessLineCode + " - " + p.ProcessLineName, p.WorkOrderNum
                                ,p.SerialNo, p.Thick, p.Width, p.Length, p.Weight, p.CommodityCode + " - " + p.CommodityName, p.SpecCode + " - " + p.SpecName
                                , p.CoatingCode + " - " + p.CoatingName, p.BussinessType + " - " + p.BussinessTypeName, p.Possession + " - " + p.PossessionName, p.MCSSNo);

                //Fill color rows for even number.
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            ClearText();            
        }

        private void SetListInComboBox()
        {
            cmbProcessLine.DataSource = _repoResrc.GetAll(epiSession.PlantID).Where(p => p.ResourceGrpID.Equals("L") || p.ResourceGrpID.Equals("R") || p.ResourceGrpID.Equals("S")).ToList();
            cmbProcessLine.DisplayMember = "ResourceDescription";
            cmbProcessLine.ValueMember = "ResourceID";


            cmbPossession.DataSource = _repoUcode.GetAll("Pocessed").ToList();
            cmbPossession.DisplayMember = "CodeDesc";
            cmbPossession.ValueMember = "CodeID";
        }

        private void ClearText()
        {
            modelFilter = new MaterialFindingModel();
            txtWorkOrderNum.Text = string.Empty;
            txtSpec.Text = string.Empty;
            txtCommodity.Text = string.Empty;
            txtCoating.Text = string.Empty;
            chkFound.Checked = false;
            chkUnpacked.Checked = false;
            chkProcess.Checked = false;
            cmbProcessLine.Text = "";
            cmbPossession.Text = "";
        }

        private void butSearch_Click(object sender, EventArgs e)
        {            

            modelFilter.WorkOrderNum = txtWorkOrderNum.Text;
            modelFilter.SpecCode = txtSpec.Text;
            modelFilter.CommodityCode = txtCommodity.Text;
            modelFilter.CategoryCode = txtCoating.Text;
            
            if(!string.IsNullOrEmpty(cmbProcessLine.Text)) modelFilter.ProcessLineCode = cmbProcessLine.SelectedValue.ToString();

            modelFilter.FoundFlag = chkFound.Checked;
            modelFilter.UnPackFlag = chkUnpacked.Checked;
            modelFilter.ProcessFlag = chkProcess.Checked;

            list = _repo.GetAllMaterailTrackerByFilter(epiSession.PlantID, modelFilter);
            ListMaterialGrid(list);
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MaterialFindingModel currentRow = new MaterialFindingModel();

            string workOrderId = dgvList.Rows[e.RowIndex].Cells["workid"].Value.GetString();
            string materialTransID = dgvList.Rows[e.RowIndex].Cells["TransactionLineID"].Value.GetString();

            currentRow = list.Where(i => i.TransactionLineID == Convert.ToInt32(materialTransID)).First();

            if (dgvList.Columns[e.ColumnIndex].Name == "found")//set your check box column index instead of 2
            {
                dgvList.EndEdit();
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dgvList.Rows[dgvList.CurrentRow.Index].Cells["found"];
                bool checkFound = Convert.ToBoolean(ch1.Value.ToString());
                var result = _repoWork.GetWorkById(epiSession.PlantID, Convert.ToInt32(workOrderId));

                if (result.OperationState == 2 && !checkFound)
                {
                    MessageBox.Show("This material has used to production", "Warning!");
                }
                else if (checkFound)
                {
                    currentRow.FoundFlag = checkFound;
                }
                else if (!checkFound)
                {
                    currentRow.FoundFlag = checkFound;
                }

                var saveResult = _repo.SaveMaterial(epiSession, currentRow);
                list = _repo.GetAllMaterailTrackerByFilter(epiSession.PlantID, modelFilter, list);
                ListMaterialGrid(list);
            }
            else if (dgvList.Columns[e.ColumnIndex].Name == "unpack")
            {
                dgvList.EndEdit();
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dgvList.Rows[dgvList.CurrentRow.Index].Cells["unpack"];
                bool checkFound = Convert.ToBoolean(ch1.Value.ToString());
                var result = _repoWork.GetWorkById(epiSession.PlantID, Convert.ToInt32(workOrderId));

                if (result.OperationState == 2 && !checkFound)
                {
                    MessageBox.Show("This material has used to production", "Warning!");
                }
                else if (checkFound)
                {
                    currentRow.UnPackFlag = checkFound;
                }
                else if (!checkFound)
                {
                    currentRow.UnPackFlag = checkFound;
                }

                var saveResult = _repo.SaveMaterial(epiSession, currentRow);
                list = _repo.GetAllMaterailTrackerByFilter(epiSession.PlantID, modelFilter, list);
                ListMaterialGrid(list);
            }
        }
    }
}
