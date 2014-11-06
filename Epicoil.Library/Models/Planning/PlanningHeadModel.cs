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

        public decimal TotalWidth { get; set; }

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

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

        public MaterialModel matSet = new MaterialModel();
        public ClassMasterModel CurrentClass = new ClassMasterModel();
        public ResourceModel ProcessLineDetail = new ResourceModel();
        public IEnumerable<ResourceModel> ResourceList = new List<ResourceModel>();
        public IEnumerable<UserCodeModel> OrderTypeList = new List<UserCodeModel>();
        public IEnumerable<UserCodeModel> PossessionList = new List<UserCodeModel>();

        public MaterialModel MaterialPattern
        {
            get { return this.matSet; }
            set { this.matSet = value; }
        }

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
        public IEnumerable<CutDesignModel> CuttingLines = new List<CutDesignModel>();
        public IEnumerable<CoilBackModel> CoilBackList = new List<CoilBackModel>();

        public IEnumerable<CoilBackRuleModel> CoilBackRoleList = new List<CoilBackRuleModel>();
        

        public List<CutDesignModel> CuttingDesign
        {
            get { return this.CuttingLines.ToList(); }
            set { this.CuttingLines = value; }
        }

        public List<CoilBackModel> CoilBacks
        {
            get { return this.CoilBackList.ToList(); }
            set { this.CoilBackList = value; }
        }

        public List<CoilBackRuleModel> CoilBackRoles
        {
            get { return this.CoilBackRoleList.ToList(); }
            set { this.CoilBackRoleList = value; }
        }

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
            this.TotalWidth = (decimal)row["TotalWidth"].GetDecimal();
            this.BussinessType = string.IsNullOrEmpty((string)row["BT"].GetString()) ? "" : (string)row["BT"].GetString();
            this.BussinessTypeName = (string)row["BussinessTypeName"].GetString();
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

        public void CalculationHeader(PlanningHeadModel model)
        {
            SumUsingWeight(model.Materails);
            SumInputWeight(model);
            SumRewindWeight(model);
            SumOutputWeight(model);
            SumLossWeight(model);
            SumYeild(model);
            SumProductWidth(model);
        }

        public void SumProductWidth(PlanningHeadModel model)
        {
            TotalWidth = (from item in model.CuttingDesign
                          where item.Status != "S"
                          select item).Sum(i => i.Width * i.Stand);
        }

        /// <summary>
        /// Sum Using weight on header
        /// </summary>
        /// <param name="materialList"></param>
        public void SumUsingWeight(IEnumerable<MaterialModel> materialList)
        {
            UsingWeight = materialList.Sum(p => p.UsingWeight).GetDecimal();
        }

        /// <summary>
        /// Sum Input Weight on header
        /// </summary>
        /// <param name="model"></param>
        public void SumInputWeight(PlanningHeadModel model)
        {
            if (model.Materails.ToList().Count != 0)
            {
                InputWeight = Math.Round(model.Materails.Sum(p => p.Weight), 0);
            }
            else
            {
                InputWeight = 0;
            }
        }

        /// <summary>
        /// Sum Rewind weight on header
        /// </summary>
        /// <param name="model"></param>
        public void SumRewindWeight(PlanningHeadModel model)
        {
            if (model.CoilBacks.ToList().Count != 0)
            {
                RewindWeight = Math.Round(model.CoilBacks.Sum(p => p.Weight), 0);
            }
            else
            {
                RewindWeight = 0;
            }
        }

        /// <summary>
        /// Sum Output weight on header
        /// </summary>
        /// <param name="model"></param>
        public void SumOutputWeight(PlanningHeadModel model)
        {
            if (model.CuttingLines.ToList().Count != 0)
            {
                OutputWeight = Math.Round(model.CuttingLines.Where(p => p.Status != "S").Sum(i => i.TotalWeight), 0);
            }
            else
            {
                OutputWeight = 0;
            }
        }

        /// <summary>
        /// Sum Loss weight on header
        /// </summary>
        /// <param name="model"></param>
        public void SumLossWeight(PlanningHeadModel model)
        {
            LossWeight = InputWeight.GetDecimal() - RewindWeight.GetDecimal() - OutputWeight.GetDecimal();
        }

        public decimal CalYeildPercent(decimal WgtFG, decimal WgtMaterial, decimal WgtCoilBack)
        {
            decimal YieldPer = 0;
            WgtMaterial = (WgtMaterial == 0) ? 1 : WgtMaterial;
            YieldPer = Math.Round(Math.Round(WgtFG, 0) / (Math.Round(WgtMaterial, 0) - Math.Round(WgtCoilBack, 0)) * 100, 2);
            return YieldPer;
        }

        /// <summary>
        /// Sum Yield percent on header
        /// </summary>
        /// <param name="model"></param>
        public void SumYeild(PlanningHeadModel model)
        {
            Yield = CalYeildPercent(Math.Round(OutputWeight, 0), Math.Round(InputWeight, 0), Math.Round(RewindWeight, 0));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="YeildValue"></param>
        /// <returns>True = In of range/False = Out of range</returns>
        public bool CheckYeild(decimal YeildValue)
        {
            decimal YieldMin = ProcessLineDetail.YieldPercentMin;
            decimal YieldMax = ProcessLineDetail.YieldPercentMax;

            bool FlagYield = true;
            if (YeildValue < YieldMin)
            {
                FlagYield = false;
            }
            if (YeildValue > YieldMax)
            {
                FlagYield = false;
            }
            return FlagYield;
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

                if (ProcessLineDetail.ResourceGrpID != "L" && LVTrim == true)
                {
                    invalidObject = "ProcessLine";
                    msg = "Trim can be select for Leveller only.";
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