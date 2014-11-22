using Epicoil.Appl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Epicoil.Library.Models;
using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Common;
using Epicoil.Library.Repositories.TQA;
using Epicoil.Library.Models.TQA;

namespace Epicoil.Appl.Presentations.TQA
{
    public partial class McssMaster : BaseSession
    {
        private readonly IMcssRepo _repo;
        private readonly ICustomerRepo _repoCustomer;
        private readonly ICoatingRepo _repoCoating;
        private readonly ISpecRepo _repoSpec;
        private readonly ICommodity _repoCommodity;
        private readonly ISupplierRepo _repoSupplier;
        private readonly IBussinessTypeRepo _repoBT;
        private readonly IMakerRepo _repoMaker;
        private readonly IMillRepo _repoMill;
        private readonly ICategoryGroupRepo _repoCatg;

        private InitialModel Initail;
        private MCSS McssContent;
        private MCSS ModelClone;

        private Dictionary<int, string> specialRef;

        #region Initialize

        public McssMaster(MCSS model = null, SessionInfo _session = null)
        {
            InitializeComponent();
            this._repo = new McssRepo();
            this._repoCustomer = new CustomerRepo();
            this._repoCoating = new CoatingRepo();
            this._repoSpec = new SpecRepo();
            this._repoCommodity = new Commodity();
            this._repoSupplier = new SupplierRepo();
            this._repoBT = new BussinessTypeRepo();
            this._repoMaker = new MakerRepo();
            this._repoMill = new MillRepo();
            this._repoCatg = new CategoryGroupRepo();
            specialRef = new Dictionary<int, string>();

            #region Dashboard
            if (model == null)
            {
                this.McssContent = new MCSS();
                this.ModelClone = new MCSS();
                this.Initail = new InitialModel();
            }
            else
            {
                this.McssContent = model;
                ModelClone = (MCSS)model.Clone();
            }
            epiSession = _session;
            #endregion

            #region Direct form
            //epiSession = new SessionInfo();
            //if (epiSession.SessionID == null)
            //{
            //    Login frm = new Login();
            //    frm.ShowDialog();
            //}
            //else if (epiSession.SessionID == "x")
            //{
            //    Application.Exit();
            //}
            //else
            //{
            //    this.Text = epiSession.CompanyName;
            //}
            #endregion
        }

        private void McssMaster_Load(object sender, EventArgs e)
        {
            #region Dashboard
            SetContent(McssContent);
            #endregion

            #region Direct form
            //Initial = _repo.GetInitial();
            //if (Initial != null)
            //{
            //    SetMode(Initial);
            //}
            //else
            //{
            //    MCSS model = new MCSS();
            //    model.SpecialRefs = new List<SpecialRef>();
            //    model.InsertState = true;
            //    this.McssContent = model;
            //    ModelClone = (MCSS)model.Clone();
            //    SetContent(McssContent);
            //}
            #endregion
        }

        private void SetMode(InitialModel model)
        {
            McssContent = new MCSS();
            McssContent.SpecialRefs = new List<SpecialRef>();
            McssContent.InsertState = true;
            McssContent.Thick = model.Thick;
            McssContent.Width = model.Width;
            McssContent.Length = model.Length;
            McssContent.SupplierCode = model.SupplierCode;
            McssContent.SupplierName = model.SupplierName;
            McssContent.CustID = model.CustID;
            McssContent.CustomerName = model.CustomerName;
            McssContent.MakerCode = model.MakerCode;
            McssContent.MakerName = model.MakerName;
            McssContent.MillCode = model.MillCode;
            McssContent.MillName = model.MillName;
            McssContent.CommodityCode = model.CommodityCode;
            McssContent.CommodityName = model.CommodityName;
            McssContent.MatSpec1 = model.MatSpec1;
            McssContent.MatSpec2 = model.MatSpec2;

            SetContent(McssContent);

            ModelClone = (MCSS)McssContent.Clone();

            _repo.ClearInitial(model.Key1);
        }

        #endregion Initailize

        #region Control Event

        private void butCustomer_Click(object sender, EventArgs e)
        {
            using (CustomerDailog frm = new CustomerDailog())
            {
                frm.ShowDialog();

                McssContent.CustID = frm.CustId;
                McssContent.CustomerName = frm.CustName;
            }
            SetContent(McssContent);
        }

        private void butSupplier_Click(object sender, EventArgs e)
        {
            using (SupplierDialog frm = new SupplierDialog())
            {
                frm.ShowDialog();

                McssContent.SupplierCode = frm.VendorId;
                McssContent.SupplierName = frm.VendorName;
            }
            SetContent(McssContent);
        }

        private void butMaker_Click(object sender, EventArgs e)
        {
            using (MakerDialog frm = new MakerDialog())
            {
                frm.ShowDialog();
                string code = frm.MakerCode;
                if (code != ModelClone.MakerCode)
                {
                    McssContent.MakerCode = code;
                    McssContent.MakerName = frm.MakerName;
                    ModelClone.MakerCode = code;
                    ModelClone.MakerName = frm.MakerName;
                    McssContent.MillCode = "";
                    McssContent.MillName = "";
                }
            }
            SetContent(McssContent);
        }

        private void butBusinessType_Click(object sender, EventArgs e)
        {
            using (BussinessTypeDialog frm = new BussinessTypeDialog())
            {
                frm.ShowDialog();

                McssContent.BussinessType = frm.Code;
                McssContent.BussinessTypeName = frm.Description;
            }
            SetContent(McssContent);
        }

        private void butCommodity_Click(object sender, EventArgs e)
        {
            using (CommodityDialog frm = new CommodityDialog())
            {
                frm.ShowDialog();
                string code = frm.Code;
                if (code != ModelClone.CommodityCode)
                {
                    McssContent.CommodityCode = code;
                    McssContent.CommodityName = frm.Description;
                    McssContent.CmdtRequireCoating = Convert.ToBoolean(frm.RequireCoating);
                    ModelClone.CommodityCode = code;
                    ModelClone.CommodityName = frm.Description;
                    McssContent.MatSpec1 = "";
                    McssContent.MatSpec2 = "";
                }
            }
            SetContent(McssContent);
        }

        private void butSpec_Click(object sender, EventArgs e)
        {
            using (SpecDialog frm = new SpecDialog(txtCommodityCode.Text.ToString()))
            {
                frm.ShowDialog();

                McssContent.MatSpec1 = frm.SpecID;
                McssContent.MatSpec2 = frm.SpecName;
            }
            SetContent(McssContent);
        }

        private void butMill_Click(object sender, EventArgs e)
        {
            using (MillDialog frm = new MillDialog(txtMakerCode.Text.ToString()))
            {
                frm.ShowDialog();

                McssContent.MillCode = frm.MillCode;
                McssContent.MillName = frm.MillName;
            }
            SetContent(McssContent);
        }

        private void butCoating_Click(object sender, EventArgs e)
        {
            using (CoatingDialog frm = new CoatingDialog())
            {
                frm.ShowDialog();

                McssContent.Coating1 = frm.Coating;
                McssContent.Coating2 = frm.CoatingName;
                McssContent.CoatingWeight1 = frm.FrontPlate;
                McssContent.CoatingWeight2 = frm.BackPlate;
            }
            SetContent(McssContent);
        }

        private void butCategory_Click(object sender, EventArgs e)
        {
            using (CategoryGroupDialog frm = new CategoryGroupDialog())
            {
                frm.ShowDialog();

                McssContent.CategoryGroupHead1 = frm.Code;
                McssContent.CategoryGroupHead2 = frm.Name;
            }
            SetContent(McssContent);
        }

        private void txtSupplierCode_Leave(object sender, EventArgs e)
        {
            string code = txtSupplierCode.Text.Trim();
            if (code != ModelClone.SupplierCode)
            {
                var result = _repoSupplier.GetByID(code);

                if (result == null)
                {
                    butSupplier_Click(sender, e);
                }
                else
                {
                    McssContent.SupplierCode = result.VendorID;
                    McssContent.SupplierName = result.VendorName;
                }
                SetContent(McssContent);
            }
        }

        private void txtCustID_Leave(object sender, EventArgs e)
        {
            string code = txtCustID.Text.Trim();
            if (code != ModelClone.CustID)
            {
                var result = _repoCustomer.GetCustomerByID(code);

                if (result == null)
                {
                    butCustomer_Click(sender, e);
                }
                else
                {
                    McssContent.CustID = result.CustId;
                    McssContent.CustomerName = result.CustName;
                }
                SetContent(McssContent);
            }
        }

        private void txtMakerCode_Leave(object sender, EventArgs e)
        {
            string code = txtMakerCode.Text.Trim();
            if (code != ModelClone.MakerCode)
            {
                var result = _repoMaker.GetByID(code);

                if (result == null)
                {
                    butMaker_Click(sender, e);
                }
                else
                {
                    McssContent.MakerCode = result.MakerCode;
                    McssContent.MakerName = result.MakerName;
                }
                SetContent(McssContent);
            }
        }

        private void txtMillCode_Leave(object sender, EventArgs e)
        {
            string code = txtMillCode.Text.Trim();
            if (code != ModelClone.MillCode)
            {
                var result = _repoMill.GetByID(code, McssContent.MakerCode);

                if (result == null)
                {
                    butMill_Click(sender, e);
                }
                else
                {
                    McssContent.MillCode = result.MillCode;
                    McssContent.MillName = result.MillName;
                }
                SetContent(McssContent);
            }
        }

        private void txtBussinessType_Leave(object sender, EventArgs e)
        {
            string code = txtBussinessType.Text.Trim();
            if (code != ModelClone.BussinessType)
            {
                var result = _repoBT.GetByID(code);

                if (result == null)
                {
                    butBusinessType_Click(sender, e);
                }
                else
                {
                    McssContent.BussinessType = result.BussinessCode;
                    McssContent.BussinessTypeName = result.BussinessName;
                }
                SetContent(McssContent);
            }
        }

        private void txtCommodityCode_Leave(object sender, EventArgs e)
        {
            string code = txtCommodityCode.Text.Trim();
            if (code != ModelClone.CommodityCode)
            {
                var result = _repoCommodity.GetByID(code);

                if (result == null)
                {
                    butCommodity_Click(sender, e);
                }
                else
                {
                    McssContent.CommodityCode = result.CommodityCode;
                    McssContent.CommodityName = result.CommodityName;
                }
                SetContent(McssContent);
            }
        }

        private void txtMatSpec1_Leave(object sender, EventArgs e)
        {
            string code = txtMatSpec1.Text.Trim();
            if (code != ModelClone.MatSpec1)
            {
                var result = _repoSpec.GetByID(code, McssContent.CommodityCode);

                if (result == null)
                {
                    butSpec_Click(sender, e);
                }
                else
                {
                    McssContent.MatSpec1 = result.SpecID;
                    McssContent.MatSpec2 = result.SpecName;
                }
                SetContent(McssContent);
            }
        }

        private void txtCoating1_Leave(object sender, EventArgs e)
        {
            string code = txtCoating1.Text.Trim();
            if (code != ModelClone.Coating1)
            {
                var result = _repoCoating.GetByID(code);

                if (result == null)
                {
                    butCoating_Click(sender, e);
                }
                else
                {
                    McssContent.Coating1 = result.CoatingPlate;
                    McssContent.Coating2 = result.CoatingName;
                    McssContent.CoatingWeight1 = result.FrontPlate;
                    McssContent.CoatingWeight2 = result.BackPlate;
                }
                SetContent(McssContent);
            }
        }

        private void txtCategoryGroupHead1_Leave(object sender, EventArgs e)
        {
            string code = txtCategoryGroupHead1.Text.Trim();
            if (code != ModelClone.CategoryGroupHead1)
            {
                var result = _repoCatg.GetByID(code);

                if (result == null)
                {
                    butCategory_Click(sender, e);
                }
                else
                {
                    McssContent.CategoryGroupHead1 = result.CategoryGroupCode;
                    McssContent.CategoryGroupHead2 = result.CategoryGroupName;
                }
                SetContent(McssContent);
            }
        }

        private void tlbSave_Click(object sender, EventArgs e)
        {
            bool IsSucces = false;
            string msg = "";
            MCSS model = new MCSS();

            //model.McssNum = txtMcssNum.Text.Trim();
            model.Thick = Convert.ToDecimal(txtThick.Text);
            model.Width = Convert.ToDecimal(txtWidth.Text);
            model.Length = Convert.ToDecimal(txtLength.Text);

            McssContent.Pocession = (rdoPocession0.Checked) ?  0 : 1;

            var dupRecord = _repo.GetByFilter(DateTime.Now, DateTime.Now, model, false);
            int i = 1;

            if (dupRecord.ToList().Count > 0)
            {
                foreach (var p in dupRecord)
                {
                    if (p.McssNum != txtMcssNum.Text.Trim())
                    {
                        msg += i + ". " + p.McssNum + "\t (" + p.CommodityCode + " : " + p.CommodityName + ") \t" + p.Size + Environment.NewLine;
                        i++;
                    }
                }
                if (i > 1)
                {
                    if (MessageBox.Show("Already exists Spec in table." + Environment.NewLine + msg + "Do you want to save?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            if (ValidatingContent(out msg) == false)
            {
                var result = _repo.Save(McssContent, epiSession, out IsSucces, out msg);

                if (IsSucces)
                {
                    SpecialRef modelRef = new SpecialRef();
                    modelRef.McssNum = McssContent.McssNum;
                    result.SpecialRefs = _repo.SaveSpecailRef(modelRef, epiSession, specialRef);
                    McssContent = result;
                    specialRef.Clear();
                    SetContent(McssContent);
                }
                else
                {
                    MessageBox.Show(msg, "Warnning.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("List of invalid field." + Environment.NewLine + msg, "Invalid field.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tlbInactive_Click(object sender, EventArgs e)
        {
            //SetMode(Initail);
        }

        private void dgvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string id = dgvList.Rows[e.RowIndex].Cells["id"].Value.GetString();
            int iRow = e.RowIndex + 1;
            if (string.IsNullOrEmpty(id))
            {
                dgvList.Rows[e.RowIndex].Cells["no"].Value = iRow;
            }

            if (specialRef.ContainsKey(iRow))
            {
                specialRef[iRow] = dgvList.Rows[e.RowIndex].Cells["description"].Value.GetString();
            }
            else
            {
                specialRef.Add(iRow, dgvList.Rows[e.RowIndex].Cells["description"].Value.GetString());
            }
        }

        private void dgvList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Console.WriteLine(e.RowIndex.ToString());
        }

        private void butDeleteLine_Click(object sender, EventArgs e)
        {
            if (dgvList.Rows.Count == 1) return;
            int iRow = dgvList.CurrentRow.Index;
            SpecialRef m = new SpecialRef();
            m.RefId = int.Parse(dgvList.Rows[iRow].Cells["id"].Value.GetString());
            m.McssNum = McssContent.McssNum;
            McssContent.SpecialRefs = _repo.DeleteSpecailRef(m);
            specialRef.Clear();
            SetGrid(McssContent.SpecialRefs);
        }
        
        #endregion Control Event

        #region CheckBox CheckChanged

        private void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.Other = c.Checked;
            SetContent(McssContent);
        }

        private void chkRoHS_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.RoHS = c.Checked;
            SetContent(McssContent);
        }

        private void chkPFOS_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.PFOS = c.Checked;
            SetContent(McssContent);
        }

        private void chkSOC_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.SOC = c.Checked;
            SetContent(McssContent);
        }

        private void chkELV_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.ELV = c.Checked;
            SetContent(McssContent);
        }

        private void chkREACH_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.REACH = c.Checked;
            SetContent(McssContent);
        }

        private void chkWelding_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.Welding = c.Checked;
            SetContent(McssContent);
        }

        private void chkPainting_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.Painting = c.Checked;
            SetContent(McssContent);
        }

        private void chkProcessOther_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.ProcessOther = c.Checked;
            SetContent(McssContent);
        }

        private void chkDegreasing_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.Degreasing = c.Checked;
            SetContent(McssContent);
        }

        private void chkBlanking_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.Blanking = c.Checked;
            SetContent(McssContent);
        }

        private void chkCommercial_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.Commercial = c.Checked;
            SetContent(McssContent);
        }

        private void chkDrawing_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.Drawing = c.Checked;
            SetContent(McssContent);
        }

        private void chkDeepDrawing_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.DeepDrawing = c.Checked;
            SetContent(McssContent);
        }

        private void chkExtraDeep_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.ExtraDeep = c.Checked;
            SetContent(McssContent);
        }

        private void chkFolding_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.Folding = c.Checked;
            SetContent(McssContent);
        }

        private void chkFormingOther_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            McssContent.FormingOther = c.Checked;
            SetContent(McssContent);
        }

        #endregion CheckBox CheckChanged

        #region RadioButton CheckedChanged

        private void rdoEdgeWave0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoEdgeWave1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoCenterWave0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoCenterWave1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoPackingStyle0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoPackingStyle1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoCustomerType0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoCustomerType1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoCustomerType2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoQuantityPerPlant0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoQuantityPerPlant1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoQuantityPerPlant2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoStandardRef0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoStandardRef1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoStandardRef2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoThicknessTolerance0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoThicknessTolerance1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoThicknessTolerance4_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoThicknessTolerance2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoThicknessTolerance3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoWidthStandard0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoWidthStandard1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoOilling0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoOilling1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistCR0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistCR1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistCR2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistCR3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistHR0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistHR1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistHR2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistHR3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistGI4_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistGI3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistGI2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistGI1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoDistGI0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoChemPersent0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoChemPersent1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoChemPersent2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoYield0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoYield1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoYield2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoTensile0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoTensile1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoTensile2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoElongation0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoElongation1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoElongation2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoHardness0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoHardness1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoHardness2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoCoreLoss0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoCoreLoss1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoCoreLoss2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoMagnatic0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoMagnatic1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoMagnatic2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoOriented0_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoOriented1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        private void rdoOriented2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked) { SetRadioValue((RadioButton)sender); }
        }

        #endregion RadioButton CheckedChanged

        #region Methods

        private void SetContent(MCSS model)
        {
            txtMcssNum.DataBindings.Clear();
            txtMcssNum.DataBindings.Add("Text", model, "McssNum", false, DataSourceUpdateMode.OnPropertyChanged);

            txtRevision.DataBindings.Clear();
            txtRevision.DataBindings.Add("Text", model, "Revision", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.Pocession == 0)
            {
                rdoPocession0.Checked = true;
            }
            else { rdoPocession1.Checked = true; }

            txtSupplierCode.DataBindings.Clear();
            txtSupplierCode.DataBindings.Add("Text", model, "SupplierCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSupplierName.DataBindings.Clear();
            txtSupplierName.DataBindings.Add("Text", model, "SupplierName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtCustID.DataBindings.Clear();
            txtCustID.DataBindings.Add("Text", model, "CustID", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCustomerName.DataBindings.Clear();
            txtCustomerName.DataBindings.Add("Text", model, "CustomerName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtMakerCode.DataBindings.Clear();
            txtMakerCode.DataBindings.Add("Text", model, "MakerCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMakerName.DataBindings.Clear();
            txtMakerName.DataBindings.Add("Text", model, "MakerName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtMillCode.DataBindings.Clear();
            txtMillCode.DataBindings.Add("Text", model, "MillCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMillName.DataBindings.Clear();
            txtMillName.DataBindings.Add("Text", model, "MillName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtPOAllowance.DataBindings.Clear();
            txtPOAllowance.DataBindings.Add("Text", model, "POAllowance", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.###");

            txtCommodityCode.DataBindings.Clear();
            txtCommodityCode.DataBindings.Add("Text", model, "CommodityCode", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCommodityName.DataBindings.Clear();
            txtCommodityName.DataBindings.Add("Text", model, "CommodityName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtMatSpec1.DataBindings.Clear();
            txtMatSpec1.DataBindings.Add("Text", model, "MatSpec1", false, DataSourceUpdateMode.OnPropertyChanged);
            txtMatSpec2.DataBindings.Clear();
            txtMatSpec2.DataBindings.Add("Text", model, "MatSpec2", false, DataSourceUpdateMode.OnPropertyChanged);

            txtCoating1.DataBindings.Clear();
            txtCoating1.DataBindings.Add("Text", model, "Coating1", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCoating2.DataBindings.Clear();
            txtCoating2.DataBindings.Add("Text", model, "Coating2", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCoatingWeight1.DataBindings.Clear();
            txtCoatingWeight1.DataBindings.Add("Text", model, "CoatingWeight1", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.###");
            txtCoatingWeight2.DataBindings.Clear();
            txtCoatingWeight2.DataBindings.Add("Text", model, "CoatingWeight2", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.###");

            txtCategoryGroupHead1.DataBindings.Clear();
            txtCategoryGroupHead1.DataBindings.Add("Text", model, "CategoryGroupHead1", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCategoryGroupHead2.DataBindings.Clear();
            txtCategoryGroupHead2.DataBindings.Add("Text", model, "CategoryGroupHead2", false, DataSourceUpdateMode.OnPropertyChanged);

            txtThick.DataBindings.Clear();
            txtThick.DataBindings.Add("Text", model, "Thick", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.000");

            txtWidth.DataBindings.Clear();
            txtWidth.DataBindings.Add("Text", model, "Width", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.000");

            txtLength.DataBindings.Clear();
            txtLength.DataBindings.Add("Text", model, "Length", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.000");

            if (model.CustomerType == 0)
            {
                rdoCustomerType0.Checked = true;
                txtCustomerTypeRemark.Enabled = false;
            }
            else if (model.CustomerType == 1)
            {
                rdoCustomerType1.Checked = true;
                txtCustomerTypeRemark.Enabled = false;
            }
            else
            {
                rdoCustomerType2.Checked = true;
                txtCustomerTypeRemark.Enabled = true;
            }

            txtTISINo.DataBindings.Clear();
            txtTISINo.DataBindings.Add("Text", model, "TISINo", false, DataSourceUpdateMode.OnPropertyChanged);
            txtLicenseNo.DataBindings.Clear();
            txtLicenseNo.DataBindings.Add("Text", model, "LicenseNo", false, DataSourceUpdateMode.OnPropertyChanged);

            //Condition Tab
            txtCustomerTypeRemark.DataBindings.Clear();
            txtCustomerTypeRemark.DataBindings.Add("Text", model, "CustomerTypeRemark", false, DataSourceUpdateMode.OnPropertyChanged);
            //txtCustomerTypeRemark.DataBindings.Add("Text", model, "CustomerTypeRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            txtBussinessType.DataBindings.Clear();
            txtBussinessType.DataBindings.Add("Text", model, "BussinessType", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBussinessTypeName.DataBindings.Clear();
            txtBussinessTypeName.DataBindings.Add("Text", model, "BussinessTypeName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtQuantityPerMonth.DataBindings.Clear();
            txtQuantityPerMonth.DataBindings.Add("Text", model, "QuantityPerMonth", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.###");

            if (model.QuantityPerPlant == 0)
            {
                rdoQuantityPerPlant0.Checked = true;
            }
            else if (model.QuantityPerPlant == 1)
            {
                rdoQuantityPerPlant1.Checked = true;
            }
            else
            {
                rdoQuantityPerPlant2.Checked = true;
            }

            txtBusinessRoute.DataBindings.Clear();
            txtBusinessRoute.DataBindings.Add("Text", model, "BusinessRoute", false, DataSourceUpdateMode.OnPropertyChanged);

            txtBusinessRemark.DataBindings.Clear();
            txtBusinessRemark.DataBindings.Add("Text", model, "BusinessRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.StandardRef == 0)
            {
                rdoStandardRef0.Checked = true;
                txtStandardRefRemark.Enabled = false;
            }
            else if (model.StandardRef == 1)
            {
                rdoStandardRef1.Checked = true;
                txtStandardRefRemark.Enabled = false;
            }
            else
            {
                rdoStandardRef2.Checked = true;
                txtStandardRefRemark.Enabled = true;
            }

            txtStandardRefRemark.DataBindings.Clear();
            txtStandardRefRemark.DataBindings.Add("Text", model, "StandardRefRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            txtNumber.DataBindings.Clear();
            txtNumber.DataBindings.Add("Text", model, "Number", false, DataSourceUpdateMode.OnPropertyChanged);

            txtName.DataBindings.Clear();
            txtName.DataBindings.Add("Text", model, "Name", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.ThicknessTolerance == 0)
            {
                rdoThicknessTolerance0.Checked = true;
                txtThicknessTolerValPos.Enabled = false;
                txtThicknessTolerValNeg.Enabled = false;
            }
            else if (model.ThicknessTolerance == 1)
            {
                rdoThicknessTolerance1.Checked = true;
                txtThicknessTolerValPos.Enabled = false;
                txtThicknessTolerValNeg.Enabled = false;
            }
            else if (model.ThicknessTolerance == 2)
            {
                rdoThicknessTolerance2.Checked = true;
                txtThicknessTolerValPos.Enabled = false;
                txtThicknessTolerValNeg.Enabled = false;
            }
            else if (model.ThicknessTolerance == 3)
            {
                rdoThicknessTolerance3.Checked = true;
                txtThicknessTolerValPos.Enabled = false;
                txtThicknessTolerValNeg.Enabled = false;
            }
            else
            {
                rdoThicknessTolerance4.Checked = true;
                txtThicknessTolerValPos.Enabled = true;
                txtThicknessTolerValNeg.Enabled = true;
            }

            txtThicknessTolerValPos.DataBindings.Clear();
            txtThicknessTolerValPos.DataBindings.Add("Text", model, "ThicknessTolerValPos", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.###");

            txtThicknessTolerValNeg.DataBindings.Clear();
            txtThicknessTolerValNeg.DataBindings.Add("Text", model, "ThicknessTolerValNeg", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.###");

            if (model.WidthStandard == 0)
            {
                rdoWidthStandard0.Checked = true;
                txtWidthStdPos.Enabled = true;
                txtWidthStdNeg.Enabled = true;
            }
            else
            {
                rdoWidthStandard1.Checked = true;
                txtWidthStdPos.Enabled = false;
                txtWidthStdNeg.Enabled = false;
            }

            txtWidthStdPos.DataBindings.Clear();
            txtWidthStdPos.DataBindings.Add("Text", model, "WidthStdPos", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.###");

            txtWidthStdNeg.DataBindings.Clear();
            txtWidthStdNeg.DataBindings.Add("Text", model, "WidthStdNeg", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.###");

            if (model.Oilling == 0)
            {
                rdoOilling0.Checked = true;
                txtOillingVal.Enabled = false;
            }
            else
            {
                rdoOilling1.Checked = true;
                txtOillingVal.Enabled = true;
            }

            txtOillingVal.DataBindings.Clear();
            txtOillingVal.DataBindings.Add("Text", model, "OillingVal", false, DataSourceUpdateMode.OnPropertyChanged);

            txtBaseMaterial.DataBindings.Clear();
            txtBaseMaterial.DataBindings.Add("Text", model, "BaseMaterial", false, DataSourceUpdateMode.OnPropertyChanged);

            txtCountry.DataBindings.Clear();
            txtCountry.DataBindings.Add("Text", model, "Country", false, DataSourceUpdateMode.OnPropertyChanged);

            txtRemark.DataBindings.Clear();
            txtRemark.DataBindings.Add("Text", model, "Remark", false, DataSourceUpdateMode.OnPropertyChanged);

            //Chemical, Mechanical, Critiria Tab
            if (model.DistCR == 0)
            {
                rdoDistCR0.Checked = true;
                txtDistCRRemark.Enabled = false;
            }
            else if (model.DistCR == 1)
            {
                rdoDistCR1.Checked = true;
                txtDistCRRemark.Enabled = false;
            }
            else if (model.DistCR == 2)
            {
                rdoDistCR2.Checked = true;
                txtDistCRRemark.Enabled = false;
            }
            else
            {
                rdoDistCR3.Checked = true;
                txtDistCRRemark.Enabled = true;
            }

            txtDistCRRemark.DataBindings.Clear();
            txtDistCRRemark.DataBindings.Add("Text", model, "DistCRRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.DistHR == 0)
            {
                rdoDistHR0.Checked = true;
                txtDistHRRemark.Enabled = false;
            }
            else if (model.DistHR == 1)
            {
                rdoDistHR1.Checked = true;
                txtDistHRRemark.Enabled = false;
            }
            else if (model.DistHR == 2)
            {
                rdoDistHR2.Checked = true;
                txtDistHRRemark.Enabled = false;
            }
            else
            {
                rdoDistHR3.Checked = true;
                txtDistHRRemark.Enabled = true;
            }

            txtDistHRRemark.DataBindings.Clear();
            txtDistHRRemark.DataBindings.Add("Text", model, "DistHRRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.DistGI == 0)
            {
                rdoDistGI0.Checked = true;
                txtDistGIRemark.Enabled = false;
            }
            else if (model.DistGI == 1)
            {
                rdoDistGI1.Checked = true;
                txtDistGIRemark.Enabled = false;
            }
            else if (model.DistGI == 2)
            {
                rdoDistGI2.Checked = true;
                txtDistGIRemark.Enabled = false;
            }
            else if (model.DistGI == 3)
            {
                rdoDistGI3.Checked = true;
                txtDistGIRemark.Enabled = false;
            }
            else
            {
                rdoDistGI4.Checked = true;
                txtDistGIRemark.Enabled = true;
            }

            txtDistGIRemark.DataBindings.Clear();
            txtDistGIRemark.DataBindings.Add("Text", model, "DistGIRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            txtStainless.DataBindings.Clear();
            txtStainless.DataBindings.Add("Text", model, "Stainless", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.ChemPersent == 0)
            {
                rdoChemPersent0.Checked = true;
                txtChemPersentRemark.Enabled = false;
            }
            else if (model.ChemPersent == 1)
            {
                rdoChemPersent1.Checked = true;
                txtChemPersentRemark.Enabled = false;
            }
            else
            {
                rdoChemPersent2.Checked = true;
                txtChemPersentRemark.Enabled = true;
            }

            txtChemPersentRemark.DataBindings.Clear();
            txtChemPersentRemark.DataBindings.Add("Text", model, "ChemPersentRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.Yield == 0)
            {
                rdoYield0.Checked = true;
                txtYieldRemark.Enabled = false;
            }
            else if (model.Yield == 1)
            {
                rdoYield1.Checked = true;
                txtYieldRemark.Enabled = false;
            }
            else
            {
                rdoYield2.Checked = true;
                txtYieldRemark.Enabled = true;
            }

            txtYieldRemark.DataBindings.Clear();
            txtYieldRemark.DataBindings.Add("Text", model, "YieldRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.Tensile == 0)
            {
                rdoTensile0.Checked = true;
                txtTensileRemark.Enabled = false;
            }
            else if (model.Tensile == 1)
            {
                rdoTensile1.Checked = true;
                txtTensileRemark.Enabled = false;
            }
            else
            {
                rdoTensile2.Checked = true;
                txtTensileRemark.Enabled = true;
            }

            txtTensileRemark.DataBindings.Clear();
            txtTensileRemark.DataBindings.Add("Text", model, "TensileRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.Elongation == 0)
            {
                rdoElongation0.Checked = true;
                txtElongationRemark.Enabled = false;
            }
            else if (model.Elongation == 1)
            {
                rdoElongation1.Checked = true;
                txtElongationRemark.Enabled = false;
            }
            else
            {
                rdoElongation2.Checked = true;
                txtElongationRemark.Enabled = true;
            }

            txtElongationRemark.DataBindings.Clear();
            txtElongationRemark.DataBindings.Add("Text", model, "ElongationRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.Hardness == 0)
            {
                rdoHardness0.Checked = true;
                txtHardnessRemark.Enabled = false;
            }
            else if (model.Hardness == 1)
            {
                rdoHardness1.Checked = true;
                txtHardnessRemark.Enabled = false;
            }
            else
            {
                rdoHardness2.Checked = true;
                txtHardnessRemark.Enabled = true;
            }

            txtHardnessRemark.DataBindings.Clear();
            txtHardnessRemark.DataBindings.Add("Text", model, "HardnessRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.CoreLoss == 0)
            {
                rdoCoreLoss0.Checked = true;
                txtCoreLossRemark.Enabled = false;
            }
            else if (model.CoreLoss == 1)
            {
                rdoCoreLoss1.Checked = true;
                txtCoreLossRemark.Enabled = false;
            }
            else
            {
                rdoCoreLoss2.Checked = true;
                txtCoreLossRemark.Enabled = true;
            }

            txtCoreLossRemark.DataBindings.Clear();
            txtCoreLossRemark.DataBindings.Add("Text", model, "CoreLossRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.Magnatic == 0)
            {
                rdoMagnatic0.Checked = true;
                txtMagnaticRemark.Enabled = false;
            }
            else if (model.Magnatic == 1)
            {
                rdoMagnatic1.Checked = true;
                txtMagnaticRemark.Enabled = false;
            }
            else
            {
                rdoMagnatic2.Checked = true;
                txtMagnaticRemark.Enabled = true;
            }

            txtMagnaticRemark.DataBindings.Clear();
            txtMagnaticRemark.DataBindings.Add("Text", model, "MagnaticRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.Oriented == 0)
            {
                rdoOriented0.Checked = true;
            }
            else if (model.Oriented == 1)
            {
                rdoOriented1.Checked = true;
            }
            else
            {
                rdoOriented2.Checked = true;
            }
            //Cust Process Tab
            if (model.Welding == true)
            {
                chkWelding.Checked = true;
            }
            else { chkWelding.Checked = false; }

            if (model.Painting == true)
            {
                chkPainting.Checked = true;
            }
            else { chkPainting.Checked = false; }

            if (model.ProcessOther == true)
            {
                chkProcessOther.Checked = true;
                txtProcessOtherRemark.Enabled = true;
            }
            else { chkProcessOther.Checked = false; txtProcessOtherRemark.Enabled = false; }

            txtProcessOtherRemark.DataBindings.Clear();
            txtProcessOtherRemark.DataBindings.Add("Text", model, "ProcessOtherRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.Degreasing == true)
            {
                chkDegreasing.Checked = true;
            }
            else { chkDegreasing.Checked = false; }

            if (model.Blanking == true)
            {
                chkBlanking.Checked = true;
            }
            else { chkBlanking.Checked = false; }

            if (model.Commercial == true)
            {
                chkCommercial.Checked = true;
            }
            else { chkCommercial.Checked = false; }

            if (model.Drawing == true)
            {
                chkDrawing.Checked = true;
            }
            else { chkDrawing.Checked = false; }

            if (model.DeepDrawing == true)
            {
                chkDeepDrawing.Checked = true;
            }
            else { chkDeepDrawing.Checked = false; }

            if (model.ExtraDeep == true)
            {
                chkExtraDeep.Checked = true;
            }
            else { chkExtraDeep.Checked = false; }

            if (model.Folding == true)
            {
                chkFolding.Checked = true;
            }
            else { chkFolding.Checked = false; }

            if (model.FormingOther == true)
            {
                chkFormingOther.Checked = true;
                txtFormingOtherRemark.Enabled = true;
            }
            else { chkFormingOther.Checked = false; txtFormingOtherRemark.Enabled = false; }

            txtFormingOtherRemark.DataBindings.Clear();
            txtFormingOtherRemark.DataBindings.Add("Text", model, "FormingOtherRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            txtPartName.DataBindings.Clear();
            txtPartName.DataBindings.Add("Text", model, "PartName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtProductName.DataBindings.Clear();
            txtProductName.DataBindings.Add("Text", model, "ProductName", false, DataSourceUpdateMode.OnPropertyChanged);

            txtCusProcessRemark.DataBindings.Clear();
            txtCusProcessRemark.DataBindings.Add("Text", model, "CusProcessRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            txtEndUser1.DataBindings.Clear();
            txtEndUser1.DataBindings.Add("Text", model, "EndUser1", false, DataSourceUpdateMode.OnPropertyChanged);

            txtEndUser2.DataBindings.Clear();
            txtEndUser2.DataBindings.Add("Text", model, "EndUser2", false, DataSourceUpdateMode.OnPropertyChanged);

            txtEndUser3.DataBindings.Clear();
            txtEndUser3.DataBindings.Add("Text", model, "EndUser3", false, DataSourceUpdateMode.OnPropertyChanged);

            txtEndUser4.DataBindings.Clear();
            txtEndUser4.DataBindings.Add("Text", model, "EndUser4", false, DataSourceUpdateMode.OnPropertyChanged);

            txtCategoryGroup1.DataBindings.Clear();
            txtCategoryGroup1.DataBindings.Add("Text", model, "CategoryGroup1", false, DataSourceUpdateMode.OnPropertyChanged);

            txtCategoryGroup2.DataBindings.Clear();
            txtCategoryGroup2.DataBindings.Add("Text", model, "CategoryGroup2", false, DataSourceUpdateMode.OnPropertyChanged);

            txtCategoryGroup3.DataBindings.Clear();
            txtCategoryGroup3.DataBindings.Add("Text", model, "CategoryGroup3", false, DataSourceUpdateMode.OnPropertyChanged);

            txtCategoryGroup4.DataBindings.Clear();
            txtCategoryGroup4.DataBindings.Add("Text", model, "CategoryGroup4", false, DataSourceUpdateMode.OnPropertyChanged);
            //Hardous substance, Packing, Flatness Tab
            if (model.EdgeWave == 0)
            {
                rdoEdgeWave0.Checked = true;
                txtEdgeWaveRemark.Enabled = false;
            }
            else
            {
                rdoEdgeWave1.Checked = true;
                txtEdgeWaveRemark.Enabled = true;
            }

            txtEdgeWaveRemark.DataBindings.Clear();
            txtEdgeWaveRemark.DataBindings.Add("Text", model, "EdgeWaveRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.CenterWave == 0)
            {
                rdoCenterWave0.Checked = true;
                txtCenterWaveRemark.Enabled = false;
            }
            else
            {
                rdoCenterWave1.Checked = true;
                txtCenterWaveRemark.Enabled = true;
            }

            txtCenterWaveRemark.DataBindings.Clear();
            txtCenterWaveRemark.DataBindings.Add("Text", model, "CenterWaveRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.PackingStyle == 0)
            {
                rdoPackingStyle0.Checked = true;
            }
            else
            {
                rdoPackingStyle1.Checked = true;
            }

            txtWeightPerCoilMin.DataBindings.Clear();
            txtWeightPerCoilMin.DataBindings.Add("Text", model, "WeightPerCoilMin", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.###");
            txtWeightPerCoilMin.Refresh();

            txtWeightPerCoilMax.DataBindings.Clear();
            txtWeightPerCoilMax.DataBindings.Add("Text", model, "WeightPerCoilMax", true, DataSourceUpdateMode.OnPropertyChanged, 0, "#,##0.###");

            txtIDCoil.DataBindings.Clear();
            txtIDCoil.DataBindings.Add("Text", model, "IDCoil", false, DataSourceUpdateMode.OnPropertyChanged);

            txtODCoil.DataBindings.Clear();
            txtODCoil.DataBindings.Add("Text", model, "ODCoil", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.RoHS == true)
            {
                chkRoHS.Checked = true;
            }
            else { chkRoHS.Checked = false; }

            if (model.PFOS == true)
            {
                chkPFOS.Checked = true;
            }
            else { chkPFOS.Checked = false; }

            if (model.SOC == true)
            {
                chkSOC.Checked = true;
            }
            else { chkSOC.Checked = false; }

            if (model.ELV == true)
            {
                chkELV.Checked = true;
            }
            else { chkELV.Checked = false; }

            if (model.REACH == true)
            {
                chkREACH.Checked = true;
            }
            else { chkREACH.Checked = false; }

            if (model.Other == true)
            {
                chkOther.Checked = true;
                txtOtherRemark.Enabled = true;
            }
            else { chkOther.Checked = false; txtOtherRemark.Enabled = false; }

            txtOtherRemark.DataBindings.Clear();
            txtOtherRemark.DataBindings.Add("Text", model, "OtherRemark", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model.InsertState == false)
            {
                txtSupplierCode.ReadOnly = true;
                txtCustID.ReadOnly = true;
                txtMakerCode.ReadOnly = true;
                txtMillCode.ReadOnly = true;
                txtCommodityCode.ReadOnly = true;
                txtMatSpec1.ReadOnly = true;

                butSupplier.Enabled = false;
                butCustomer.Enabled = false;
                butMaker.Enabled = false;
                butMill.Enabled = false;
                butCommodity.Enabled = false;
                butSpec.Enabled = false;
            }
            else
            {
                txtSupplierCode.ReadOnly = false;
                txtCustID.ReadOnly = false;
                txtMakerCode.ReadOnly = false;
                txtMillCode.ReadOnly = false;
                txtCommodityCode.ReadOnly = false;
                txtMatSpec1.ReadOnly = false;

                butSupplier.Enabled = true;
                butCustomer.Enabled = true;
                butMaker.Enabled = true;
                butMill.Enabled = true;
                butCommodity.Enabled = true;
                butSpec.Enabled = true;
            }

            if (!McssContent.CmdtRequireCoating)
            {
                butCoating.Enabled = false;
                txtCoating1.Enabled = false;
            }
            else
            {
                butCoating.Enabled = true;
                txtCoating1.Enabled = true;
            }
            SetGrid(model.SpecialRefs);
        }

        private void SetRadioValue(RadioButton rdo)
        {
            string obj = rdo.Name;
            switch (obj)
            {
                case "rdoCustomerType0": McssContent.CustomerType = 0; break;
                case "rdoCustomerType1": McssContent.CustomerType = 1; break;
                case "rdoCustomerType2": McssContent.CustomerType = 2; break;

                case "rdoQuantityPerPlant0": McssContent.QuantityPerPlant = 0; break;
                case "rdoQuantityPerPlant1": McssContent.QuantityPerPlant = 1; break;
                case "rdoQuantityPerPlant2": McssContent.QuantityPerPlant = 2; break;

                case "rdoStandardRef0": McssContent.StandardRef = 0; break;
                case "rdoStandardRef1": McssContent.StandardRef = 1; break;
                case "rdoStandardRef2": McssContent.StandardRef = 2; break;

                case "rdoThicknessTolerance0": McssContent.ThicknessTolerance = 0; break;
                case "rdoThicknessTolerance1": McssContent.ThicknessTolerance = 1; break;
                case "rdoThicknessTolerance2": McssContent.ThicknessTolerance = 2; break;
                case "rdoThicknessTolerance3": McssContent.ThicknessTolerance = 3; break;
                case "rdoThicknessTolerance4": McssContent.ThicknessTolerance = 4; break;

                case "rdoWidthStandard0": McssContent.WidthStandard = 0; break;
                case "rdoWidthStandard1": McssContent.WidthStandard = 1; break;

                case "rdoDistCR0": McssContent.DistCR = 0; break;
                case "rdoDistCR1": McssContent.DistCR = 1; break;
                case "rdoDistCR2": McssContent.DistCR = 2; break;
                case "rdoDistCR3": McssContent.DistCR = 3; break;

                case "rdoDistHR0": McssContent.DistHR = 0; break;
                case "rdoDistHR1": McssContent.DistHR = 1; break;
                case "rdoDistHR2": McssContent.DistHR = 2; break;
                case "rdoDistHR3": McssContent.DistHR = 3; break;

                case "rdoDistGI0": McssContent.DistGI = 0; break;
                case "rdoDistGI1": McssContent.DistGI = 1; break;
                case "rdoDistGI2": McssContent.DistGI = 2; break;
                case "rdoDistGI3": McssContent.DistGI = 3; break;
                case "rdoDistGI4": McssContent.DistGI = 4; break;

                case "rdoChemPersent0": McssContent.ChemPersent = 0; break;
                case "rdoChemPersent1": McssContent.ChemPersent = 1; break;
                case "rdoChemPersent2": McssContent.ChemPersent = 2; break;

                case "rdoYield0": McssContent.Yield = 0; break;
                case "rdoYield1": McssContent.Yield = 1; break;
                case "rdoYield2": McssContent.Yield = 2; break;

                case "rdoTensile0": McssContent.Tensile = 0; break;
                case "rdoTensile1": McssContent.Tensile = 1; break;
                case "rdoTensile2": McssContent.Tensile = 2; break;

                case "rdoElongation0": McssContent.Elongation = 0; break;
                case "rdoElongation1": McssContent.Elongation = 1; break;
                case "rdoElongation2": McssContent.Elongation = 2; break;

                case "rdoHardness0": McssContent.Hardness = 0; break;
                case "rdoHardness1": McssContent.Hardness = 1; break;
                case "rdoHardness2": McssContent.Hardness = 2; break;

                case "rdoCoreLoss0": McssContent.CoreLoss = 0; break;
                case "rdoCoreLoss1": McssContent.CoreLoss = 1; break;
                case "rdoCoreLoss2": McssContent.CoreLoss = 2; break;

                case "rdoMagnatic0": McssContent.Magnatic = 0; break;
                case "rdoMagnatic1": McssContent.Magnatic = 1; break;
                case "rdoMagnatic2": McssContent.Magnatic = 2; break;

                case "rdoOriented0": McssContent.Oriented = 0; break;
                case "rdoOriented1": McssContent.Oriented = 1; break;
                case "rdoOriented2": McssContent.Oriented = 2; break;

                case "rdoOilling0": McssContent.Oilling = 0; break;
                case "rdoOilling1": McssContent.Oilling = 1; break;

                case "rdoEdgeWave0": McssContent.EdgeWave = 0; break;
                case "rdoEdgeWave1": McssContent.EdgeWave = 1; break;

                case "rdoCenterWave0": McssContent.CenterWave = 0; break;
                case "rdoCenterWave1": McssContent.CenterWave = 1; break;

                case "rdoPackingStyle0": McssContent.PackingStyle = 0; break;
                case "rdoPackingStyle1": McssContent.PackingStyle = 1; break;
            }
            SetContent(McssContent);
        }

        private bool ValidatingContent(out string msgerror)
        {
            bool errorFlag = false;
            string msg = "";

            errorProvider1.Clear();
            if (rdoPocession0.Checked == false && rdoPocession1.Checked == false)
            {
                errorProvider1.SetError(rdoPocession0, "Please fill the required field.");
                errorFlag = true;
                msg = "- " + "Possestion" + Environment.NewLine;
            }

            if (txtSupplierCode.Text == "")
            {
                errorProvider1.SetError(txtSupplierCode, "Please fill the required field.");
                errorFlag = true;
                msg = "- " + "Supplier/Vendor" + Environment.NewLine;
            }

            if (txtCustID.Text == "")
            {
                errorProvider1.SetError(txtCustID, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "Customer" + Environment.NewLine;
            }

            if (txtMakerCode.Text == "")
            {
                errorProvider1.SetError(txtMakerCode, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "Maker" + Environment.NewLine;
            }

            if (txtMillCode.Text == "")
            {
                errorProvider1.SetError(txtMillCode, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "Mill" + Environment.NewLine;
            }

            if (txtCommodityCode.Text == "")
            {
                errorProvider1.SetError(txtCommodityCode, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "Commodity" + Environment.NewLine;
            }

            /*
            if (txtBussinessType.Text == "")
            {
                errorProvider1.SetError(txtBussinessType, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "BT" + Environment.NewLine;
            }
            */
            if (txtMatSpec1.Text == "")
            {
                errorProvider1.SetError(txtMatSpec1, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "Spec" + Environment.NewLine;
            }

            if (McssContent.CmdtRequireCoating)
            {
                if (txtCoating1.Text == "")
                {
                    errorProvider1.SetError(txtCoating1, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Coating" + Environment.NewLine;
                }
            }

            if (txtThick.Text == "")
            {
                errorProvider1.SetError(txtThick, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "Thick" + Environment.NewLine;
            }
            else if (Convert.ToDecimal(txtThick.Text) <= 0)
            {
                errorProvider1.SetError(txtThick, "Must be greater than zero.");
                errorFlag = true;
                msg += "- " + "Thick" + Environment.NewLine;
            }

            if (txtWidth.Text == "")
            {
                errorProvider1.SetError(txtWidth, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "Width" + Environment.NewLine;
            }
            else if (Convert.ToDecimal(txtWidth.Text) <= 0)
            {
                errorProvider1.SetError(txtWidth, "Must be greater than zero.");
                errorFlag = true;
                msg += "- " + "Width" + Environment.NewLine;
            }

            if (txtLength.Text == "")
            {
                errorProvider1.SetError(txtLength, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "Length" + Environment.NewLine;
            }

            if (rdoCustomerType2.Checked)
            {
                if (txtCustomerTypeRemark.Text == "")
                {
                    errorProvider1.SetError(txtCustomerTypeRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Customer Type" + Environment.NewLine;
                }
            }

            if (rdoStandardRef2.Checked)
            {
                if (txtStandardRefRemark.Text == "")
                {
                    errorProvider1.SetError(txtStandardRefRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Standard Referrence" + Environment.NewLine;
                }
            }

            if (rdoThicknessTolerance4.Checked)
            {
                if (txtThicknessTolerValPos.Text == "")
                {
                    errorProvider1.SetError(txtThicknessTolerValPos, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Thickness tolerance" + Environment.NewLine;
                }
                if (txtThicknessTolerValNeg.Text == "")
                {
                    errorProvider1.SetError(txtThicknessTolerValNeg, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Thickness tolerance" + Environment.NewLine;
                }
            }

            if (rdoWidthStandard1.Checked)
            {
                if (txtWidthStdPos.Text == "")
                {
                    errorProvider1.SetError(txtWidthStdPos, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Width standard" + Environment.NewLine;
                }
                if (txtWidthStdNeg.Text == "")
                {
                    errorProvider1.SetError(txtWidthStdNeg, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Width standard" + Environment.NewLine;
                }
            }

            if (rdoOilling1.Checked)
            {
                if (txtOillingVal.Text == "")
                {
                    errorProvider1.SetError(txtOillingVal, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Oilling" + Environment.NewLine;
                }
            }

            if (txtNumber.Text == "")
            {
                errorProvider1.SetError(txtNumber, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "Number" + Environment.NewLine;
            }

            if (txtWeightPerCoilMin.Text == "")
            {
                errorProvider1.SetError(txtWeightPerCoilMin, "Please fill the required field.");
                errorFlag = true;
                msg += "- " + "Packing (Weight/Coil)" + Environment.NewLine;
            }

            if (rdoDistCR3.Checked)
            {
                if (txtDistCRRemark.Text == "")
                {
                    errorProvider1.SetError(txtDistCRRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Distinction surface findsh (CR)" + Environment.NewLine;
                }
            }

            if (rdoDistHR3.Checked)
            {
                if (txtDistHRRemark.Text == "")
                {
                    errorProvider1.SetError(txtDistHRRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Distinction surface findsh (HR)" + Environment.NewLine;
                }
            }

            if (rdoDistGI4.Checked)
            {
                if (txtDistGIRemark.Text == "")
                {
                    errorProvider1.SetError(txtDistGIRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Distinction surface findsh (GI, GA, GL, CG)" + Environment.NewLine;
                }
            }

            if (rdoChemPersent2.Checked)
            {
                if (txtChemPersentRemark.Text == "")
                {
                    errorProvider1.SetError(txtChemPersentRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Chemical composition" + Environment.NewLine;
                }
            }

            if (rdoYield2.Checked)
            {
                if (txtYieldRemark.Text == "")
                {
                    errorProvider1.SetError(txtYieldRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Chemical composition (Tield Point)" + Environment.NewLine;
                }
            }

            if (rdoTensile2.Checked)
            {
                if (txtTensileRemark.Text == "")
                {
                    errorProvider1.SetError(txtTensileRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Chemical composition (Tensile)" + Environment.NewLine;
                }
            }

            if (rdoElongation2.Checked)
            {
                if (txtElongationRemark.Text == "")
                {
                    errorProvider1.SetError(txtElongationRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Chemical composition (Elongation)" + Environment.NewLine;
                }
            }

            if (rdoHardness2.Checked)
            {
                if (txtHardnessRemark.Text == "")
                {
                    errorProvider1.SetError(txtHardnessRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Chemical composition (Hardness)" + Environment.NewLine;
                }
            }

            if (rdoCoreLoss2.Checked)
            {
                if (txtCoreLossRemark.Text == "")
                {
                    errorProvider1.SetError(txtCoreLossRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Chemical composition (For silocon core less)" + Environment.NewLine;
                }
            }

            if (rdoMagnatic2.Checked)
            {
                if (txtMagnaticRemark.Text == "")
                {
                    errorProvider1.SetError(txtMagnaticRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Chemical composition (For slicon magnatic fulx)" + Environment.NewLine;
                }
            }

            if (chkProcessOther.Checked)
            {
                if (txtProcessOtherRemark.Text == "")
                {
                    errorProvider1.SetError(txtProcessOtherRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Customer Process (For silicon oriented)" + Environment.NewLine;
                }
            }

            if (chkFormingOther.Checked)
            {
                if (txtFormingOtherRemark.Text == "")
                {
                    errorProvider1.SetError(txtFormingOtherRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Customer forming" + Environment.NewLine;
                }
            }

            if (rdoEdgeWave1.Checked)
            {
                if (txtEdgeWaveRemark.Text == "")
                {
                    errorProvider1.SetError(txtEdgeWaveRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Flatness edge wave" + Environment.NewLine;
                }
            }

            if (rdoCenterWave1.Checked)
            {
                if (txtCenterWaveRemark.Text == "")
                {
                    errorProvider1.SetError(txtCenterWaveRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Flatness center wave" + Environment.NewLine;
                }
            }

            if (chkOther.Checked)
            {
                if (txtOtherRemark.Text == "")
                {
                    errorProvider1.SetError(txtOtherRemark, "Please fill the required field.");
                    errorFlag = true;
                    msg += "- " + "Hardous substance contain" + Environment.NewLine;
                }
            }

            msgerror = msg;
            return errorFlag;
        }

        private void SetGrid(IEnumerable<SpecialRef> item)
        {
            int i = 0;
            dgvList.Rows.Clear();
            foreach (var p in item)
            {
                dgvList.Rows.Add(p.RefId, p.RefNo, i + 1, p.Description, false);
                if (i % 2 == 1)
                {
                    this.dgvList.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                }
                i++;
            }
        }

        #endregion Methods
    }
}