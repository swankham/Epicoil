using Epicoil.Library.Repositories;
using Epicoil.Library.Repositories.Planning;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Epicoil.Library.Models.Planning
{
    public class PlanningHeadModel
    {
        private readonly IWorkEntryRepo _repo;
        private readonly IUserCodeRepo _repoUcd;
        private readonly IResourceRepo _repoRes;

        public PlanningHeadModel()
        {
            this._repo = new WorkEntryRepo();
            this._repoUcd = new UserCodeRepo();
            this._repoRes = new ResourceRepo();
        }

        #region Attribute

        public string Company { get; set; }

        public string Plant { get; set; }

        public int WorkOrderID { get; set; }

        public string WorkOrderNum { get; set; }

        public string ProcessLineId { get; set; }

        public decimal ProcessStep { get; set; }

        public string OrderType { get; set; }

        public string PIC { get; set; }

        public string PICName { get; set; }

        public string Possession { get; set; }

        public string PossessionName
        {
            get
            {
                return Enum.GetName(typeof(Possession), Convert.ToInt32(Possession));
            }
        }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public decimal UsingWeight { get; set; }

        public decimal InputWeight { get; set; }

        public decimal RewindWeight { get; set; }

        public decimal OutputWeight { get; set; }

        public decimal LossWeight { get; set; }

        public decimal Yield { get; set; }

        public decimal TotalMaterialAmount { get; set; }

        public decimal TotalWeight { get; set; }

        public string BT { get; set; }

        public bool LVTrim { get; set; }

        public bool PackingPlan { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        /// <summary>
        /// 0 = Nothing.
        /// 1 = New Transaction.
        /// 2 = Transaction was save.
        /// 3 = Selected materail.
        /// 4 = Calculated.
        /// </summary>
        public int FormState { get; set; }

        public bool SimulateFlag { get; set; }

        public MaterialModel MaterialPattern { get; set; }

        public ClassMasterModel CurrentClass = new ClassMasterModel();
        public ResourceModel ProcessLineDetail = new ResourceModel();
        public IEnumerable<ResourceModel> ResourceList = new List<ResourceModel>();
        public IEnumerable<UserCodeModel> OrderTypeList = new List<UserCodeModel>();
        public IEnumerable<UserCodeModel> PossessionList = new List<UserCodeModel>();

        public ClassMasterModel Class
        {
            get { return this.CurrentClass; }
            set { this.CurrentClass = value; }
        }

        public ResourceModel ProcessLine
        {
            get { return this.ProcessLineDetail; }
            set { this.ProcessLineDetail = value; }
        }

        public List<ResourceModel> Resources
        {
            get { return this.ResourceList.ToList(); }
            set { this.ResourceList = value; }
        }

        public List<UserCodeModel> UserCodes
        {
            get { return this.OrderTypeList.ToList(); }
            set { this.OrderTypeList = value; }
        }

        public List<UserCodeModel> Possessions
        {
            get { return this.PossessionList.ToList(); }
            set { this.PossessionList = value; }
        }

        public IEnumerable<MaterialModel> Materails = new List<MaterialModel>();

        public int ClassID { get; set; }

        #endregion Attribute

        #region Method

        public virtual void DataBind(DataRow row)
        {
            this.Company = (string)row["Company"].GetString();
            this.Plant = (string)row["Plant"].GetString();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.WorkOrderNum = (string)row["WorkOrderNum"].GetString();
            this.ProcessLineId = (string)row["ProcessLine"].GetString();
            this.ProcessStep = (decimal)row["ProcessStep"].GetDecimal();
            this.OrderType = (string)row["OrderType"].GetString();
            this.PIC = (string)row["PIC"].GetString();
            this.PICName = (string)row["PICName"].GetString();
            this.Possession = (string)row["Possession"].GetString();
            this.IssueDate = (DateTime)row["IssueDate"].GetDate();
            this.DueDate = (DateTime)row["DueDate"].GetDate();
            this.UsingWeight = (decimal)row["UsingWgt"].GetDecimal();
            this.InputWeight = (decimal)row["InputWgt"].GetDecimal();
            this.RewindWeight = (decimal)row["RewindWgt"].GetDecimal();
            this.OutputWeight = (decimal)row["OutputWgt"].GetDecimal();
            this.LossWeight = (decimal)row["LossWgt"].GetDecimal();
            this.Yield = (decimal)row["Yield"].GetDecimal();
            this.TotalMaterialAmount = (decimal)row["TotalMatAmount"].GetDecimal();
            this.TotalWeight = (decimal)row["TotalWidth"].GetDecimal();
            this.BT = (string)row["BT"].GetString();
            this.LVTrim = Convert.ToBoolean(row["LVTrim"].GetInt());
            this.PackingPlan = Convert.ToBoolean(row["PackingPlan"].GetInt());
            this.CreationDate = (DateTime)row["CreationDate"].GetDate();
            this.LastUpdateDate = (DateTime)row["LastUpdateDate"].GetDate();
            this.CreatedBy = (string)row["CreatedBy"].GetString();
            this.UpdatedBy = (string)row["UpdatedBy"].GetString();
            this.ClassID = (int)row["ClassID"].GetInt();
        }

        public void PreLoad() //0 = Nothing.
        {
            this.FormState = 0;
            this.SimulateFlag = false;
            this.IssueDate = DateTime.Now;
            this.DueDate = DateTime.Now;
            this.ResourceList = new List<ResourceModel>();
            this.OrderTypeList = new List<UserCodeModel>();
            this.PossessionList = new List<UserCodeModel>();
            //this.MaterialList = new List<MaterialModel>();
            this.MaterialPattern = new MaterialModel();
            this.CurrentClass = new ClassMasterModel();
        }

        public void New(string plantId) //1 = New Transaction.
        {
            this.FormState = 1;
            this.IssueDate = DateTime.Now;
            this.DueDate = DateTime.Now;
            this.ResourceList = _repoRes.GetAll(plantId).Where(p => p.ResourceGrpID.Equals("L") || p.ResourceGrpID.Equals("R") || p.ResourceGrpID.Equals("S"));
            this.OrderTypeList = _repoUcd.GetAll("OrderType");
            this.PossessionList = _repoUcd.GetAll("Pocessed");
            //this.ProcessLineDetail.ResourceID = "R08";
            //this.MaterialList = new List<MaterialModel>();
            this.ProcessStep = _repo.GetLastStep(WorkOrderID);
        }

        public void Saved() //2 = Transaction was save.
        {
            this.FormState = 2;
            this.MaterialPattern = new MaterialModel();
            this.CurrentClass = new ClassMasterModel();
        }

        public void SumUsingWeight(IEnumerable<MaterialModel> materialList)
        {
            UsingWeight = materialList.Sum(p => p.UsingWeight).GetDecimal();
        }

        public bool ValidateToSave(IEnumerable<MaterialModel> materialList, out string invalidObject, out string msg)
        {
            invalidObject = "";
            msg = "";
            bool valid = true;

            //Validate Process Line Selected.
            if (ProcessLineId == null)
            {
                invalidObject = "ProcessLine";
                msg = "Please select Process Line.";
                return false;
            }
            else
            {
                if (ProcessLineDetail.ResourceGrpID != "S" && PackingPlan == true)
                {
                    invalidObject = "ProcessLine";
                    msg = "Packing Plan can be select for Sliter only.";
                    return false;
                }
            }

            //Validate compatible between Machine and Materail.
            if (Materails.ToList().Count() > 0)
            {
                //Validate Possession.
                if (Possession == null)
                {
                    invalidObject = "Possession";
                    msg = "Please select Possession.";
                    return false;
                }

                decimal valmin = Materails.Min(i => i.Width);
                if (ProcessLineDetail.WidthMin > valmin)
                {
                    invalidObject = "ProcessLine";
                    msg = string.Format(@"Machine and Materail is not compatible for Machine width min = {0} and Materail width = {1}.",
                                        ProcessLineDetail.WidthMin.ToString("#,##0.00"), valmin.ToString("#,##0.00"));
                    return false;
                }

                decimal valmax = Materails.Max(i => i.Width);
                if (ProcessLineDetail.WidthMax < valmax)
                {
                    invalidObject = "ProcessLine";
                    msg = string.Format(@"Machine and Materail is not compatible for Machine width max = {0} and Materail width = {1}.",
                                        ProcessLineDetail.WidthMin.ToString("#,##0.00"), valmax.ToString("#,##0.00"));
                    return false;
                }
            }

            //Validate OrderType Selected.
            if (OrderType == null)
            {
                invalidObject = "OrderType";
                msg = "Please select Order Type.";
                return false;
            }

            //Compare date between Issue date and Due date.
            if (IssueDate > DueDate)
            {
                invalidObject = "IssueDate";
                msg = "Please change Due Date to be greater than Issue Date.";
                return false;
            }

            return valid;
        }

        public bool ValidateToDelMaterial(MaterialModel material, out string msg)
        {
            bool valid = true;
            msg = "";
            //TODO : Work around method.
            /*
             * Condition statement...
            */
            return valid;
        }

        #endregion Method
    }
}