using Epicoil.Library.Models.Sales;
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
        #region Fields

        private readonly IWorkEntryRepo _repo;
        private readonly IResourceRepo _repoRes;
        private readonly IUserCodeRepo _repoUcd;

        #endregion Fields

        #region Constructors

        public PlanningHeadModel()
        {
            _repo = new WorkEntryRepo();
            _repoUcd = new UserCodeRepo();
            _repoRes = new ResourceRepo();

            LevSimulates = new List<LevellerSimulateModel>();
            Class = new ClassMasterModel();
            CoilBacks = new List<CoilBackModel>();
            CoilBackRoles = new List<CoilBackRuleModel>();
            CuttingDesign = new List<CutDesignModel>();
            Materials = new List<MaterialModel>();
            OrderTypeList = new List<UserCodeModel>();
            Possessions = new List<UserCodeModel>();
            ProcessLine = new ResourceModel();
            Resources = new List<ResourceModel>();
        }

        #endregion Constructors

        #region Properties

        public string BussinessType { get; set; }

        public string BussinessTypeName { get; set; }

        public ClassMasterModel Class { get; set; }

        public int ClassID { get; set; }

        public IList<CoilBackRuleModel> CoilBackRoles { get; set; }

        public IList<CoilBackModel> CoilBacks { get; set; }

        public string Company { get; set; }

        public int Completed { get; set; }

        public string CompletedStr
        {
            get
            {
                return Enum.GetName(typeof(CompleteStatus), Completed);
            }
        }

        public string CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public IList<CutDesignModel> CuttingDesign { get; set; }

        public DateTime DueDate { get; set; }

        /// <summary>
        /// 0 = Nothing.
        /// 1 = New Transaction.
        /// 2 = Transaction was save.
        /// 3 = Selected materail.
        /// 4 = Calculated.
        /// </summary>
        public int FormState { get; set; }

        public int GenSerialFlag { get; set; }

        public string GenSerialFlagStr
        {
            get
            {
                return (Enum.GetName(typeof(GenerateSNStatus), GenSerialFlag).Replace("_", " "));
            }
        }

        public decimal InputWeight { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public IList<LevellerSimulateModel> LevSimulates { get; set; }

        public decimal LossWeight { get; set; }

        public bool LVTrim { get; set; }

        //public MaterialModel MaterialPattern { get; set; }

        public IList<MaterialModel> Materials { get; set; }
        public int OpenFlag { get; set; }

        public int OperationState { get; set; }

        public string OperationStateName
        {
            get
            {
                return Enum.GetName(typeof(OperationState), OperationState);
            }
        }

        public string OrderType { get; set; }

        public IList<UserCodeModel> OrderTypeList { get; set; }

        public decimal OutputWeight { get; set; }

        public int PackingOrderFlag { get; set; }

        public bool PackingPlan { get; set; }

        public int PackingPlanFlag { get; set; }
        public string PIC { get; set; }

        public string PICName { get; set; }

        public string Plant { get; set; }

        public string Possession { get; set; }

        public string PossessionName
        {
            get
            {
                return Enum.GetName(typeof(Possession), Convert.ToInt32(Possession));
            }
        }

        public IList<UserCodeModel> Possessions { get; set; }

        public ResourceModel ProcessLine { get; set; }

        public string ProcessLineId { get; set; }

        public decimal ProcessStep { get; set; }

        public IList<ResourceModel> Resources { get; set; }

        public decimal RewindWeight { get; set; }

        public int SimulateFlag { get; set; }

        public string SimulateFlagStr
        {
            get
            {
                return Enum.GetName(typeof(SimulateStatus), SimulateFlag);
            }
        }

        public decimal TotalMaterialAmount { get; set; }

        public decimal TotalWidth { get; set; }

        public string UpdatedBy { get; set; }
        public decimal UsingWeight { get; set; }

        public int WorkOrderID { get; set; }

        public string WorkOrderNum { get; set; }

        public decimal Yield { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void CalculationHeader(PlanningHeadModel model)
        {
            SumUsingWeight(model.Materials);
            SumInputWeight(model);
            SumRewindWeight(model);
            SumOutputWeight(model);
            SumLossWeight(model);
            SumYeild(model);
            SumProductWidth(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WgtFG"></param>
        /// <param name="WgtMaterial"></param>
        /// <param name="WgtCoilBack"></param>
        /// <returns></returns>
        public decimal CalYeildPercent(decimal WgtFG, decimal WgtMaterial, decimal WgtCoilBack)
        {
            decimal YieldPer = 0;
            WgtMaterial = (WgtMaterial == 0) ? 1 : WgtMaterial;
            YieldPer = Math.Round(Math.Round(WgtFG, 0) / (Math.Round(WgtMaterial, 0) - Math.Round(WgtCoilBack, 0)) * 100, 2);
            return YieldPer;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="YeildValue"></param>
        /// <returns>True = In of range/False = Out of range</returns>
        public bool CheckYeild(decimal YeildValue)
        {
            decimal YieldMin = ProcessLine.YieldPercentMin;
            decimal YieldMax = ProcessLine.YieldPercentMax;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
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
            this.Completed = (int)row["Completed"].GetInt();
            this.SimulateFlag = (int)row["SimulateFlag"].GetInt();
            this.GenSerialFlag = (int)row["GenSerialFlag"].GetInt();
            this.OpenFlag = (int)row["OpenFlag"].GetInt();
            this.OperationState = (int)row["OperationState"].GetInt();
            this.PackingPlanFlag = (int)row["PackingPlanFlag"].GetInt();
            this.PackingOrderFlag = (int)row["PackingOrderFlag"].GetInt();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plantId"></param>
        public void New(string plantId)
        {
            this.FormState = 1;
            this.IssueDate = DateTime.Now;
            this.DueDate = DateTime.Now;
            this.Resources = _repoRes.GetAll(plantId).Where(p => p.ResourceGrpID.Equals("L") || p.ResourceGrpID.Equals("R") || p.ResourceGrpID.Equals("S")).ToList();
            this.OrderTypeList = _repoUcd.GetAll("OrderType").ToList();
            this.Possessions = _repoUcd.GetAll("Pocessed").ToList();
            this.ProcessStep = _repo.GetLastStep(WorkOrderID);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PreLoad()
        {
            this.FormState = 0;
            this.SimulateFlag = 0;
            this.IssueDate = DateTime.Now;
            this.DueDate = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<CutDesignModel> ReCalculateCuttingLine()
        {
            foreach (var v in CuttingDesign)
            {
                v.CalculateRows(this);
            }
            return CuttingDesign;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Saved()
        {
            this.FormState = 2;
            //this.MaterialPattern = new MaterialModel();
            //this.CurrentClass = new ClassMasterModel();
        }

        /// <summary>
        /// Sum Input Weight on header
        /// </summary>
        /// <param name="model"></param>
        public void SumInputWeight(PlanningHeadModel model)
        {
            if (model.Materials.ToList().Count != 0)
            {
                InputWeight = Math.Round(model.Materials.Sum(p => p.Weight), 0);
            }
            else
            {
                InputWeight = 0;
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

        /// <summary>
        /// Sum Output weight on header
        /// </summary>
        /// <param name="model"></param>
        public void SumOutputWeight(PlanningHeadModel model)
        {
            if (model.CuttingDesign.ToList().Count != 0)
            {
                OutputWeight = Math.Round(model.CuttingDesign.Where(p => p.Status != "S").Sum(i => i.TotalWeight), 0);
            }
            else
            {
                OutputWeight = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void SumProductWidth(PlanningHeadModel model)
        {
            TotalWidth = (from item in model.CuttingDesign
                          where item.Status != "S"
                          select item).Sum(i => i.Width * i.Stand);
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
        /// Sum Using weight on header
        /// </summary>
        /// <param name="materialList"></param>
        public void SumUsingWeight(IEnumerable<MaterialModel> materialList)
        {
            UsingWeight = materialList.Sum(p => p.UsingWeight).GetDecimal();
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
        /// <param name="ordDtl"></param>
        /// <param name="lineID"></param>
        /// <returns></returns>
        public IList<CutDesignModel> UpdateCuttingByLine(OrderDetailModel ordDtl, int lineID)
        {
            for (int i = 0; i < CuttingDesign.Count; i++)
            {
                if (CuttingDesign[i].LineID == lineID)
                {
                    CuttingDesign[i].SOLine = ordDtl.OrderLine;
                    CuttingDesign[i].NORNum = ordDtl.NORNo;
                    CuttingDesign[i].CommodityCode = ordDtl.CommodityCode;
                    CuttingDesign[i].SpecCode = ordDtl.SpecCode;
                    CuttingDesign[i].CoatingCode = ordDtl.CoatingCode;
                    CuttingDesign[i].Thick = ordDtl.Thick;
                    CuttingDesign[i].Width = ordDtl.Width;
                    CuttingDesign[i].Length = ordDtl.Length;
                    CuttingDesign[i].SOWeight = ordDtl.SOWeight;
                    CuttingDesign[i].SOQuantity = ordDtl.SOQuantity;
                    CuttingDesign[i].QtyPack = ordDtl.QtyPack;
                    CuttingDesign[i].Pack = ordDtl.Pack;
                    CuttingDesign[i].BussinessType = ordDtl.BussinessType;
                }
            }

            return CuttingDesign;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="invalidObject"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ValidateToSave(PlanningHeadModel model, out string invalidObject, out string msg)
        {
            invalidObject = "";
            msg = "";
            bool valid = true;

            #region Validate Process Line Selected.

            if (ProcessLineId == null)
            {
                invalidObject = "ProcessLine";
                msg = "Please select Process Line.";
                return false;
            }
            else
            {
                if (ProcessLine.ResourceGrpID != "S" && PackingPlan == true)
                {
                    invalidObject = "ProcessLine";
                    msg = "Packing Plan can be select for Slitter only.";
                    return false;
                }

                if (ProcessLine.ResourceGrpID != "L" && LVTrim == true)
                {
                    invalidObject = "ProcessLine";
                    msg = "Trim can be select for Leveller only.";
                    return false;
                }
            }

            #endregion Validate Process Line Selected.

            #region Validate compatible between Machine and Material.

            if (Materials.ToList().Count() > 0)
            {
                //Validate Possession.
                if (Possession == null)
                {
                    invalidObject = "Possession";
                    msg = "Please select Possession.";
                    return false;
                }

                decimal valmin = Materials.Min(i => i.Width);
                if (ProcessLine.WidthMin > valmin)
                {
                    invalidObject = "ProcessLine";
                    msg = string.Format(@"Machine and Material is not compatible for Machine width min = {0} and Material width = {1}.",
                                        ProcessLine.WidthMin.ToString("#,##0.00"), valmin.ToString("#,##0.00"));
                    return false;
                }

                decimal valmax = Materials.Max(i => i.Width);
                if (ProcessLine.WidthMax < valmax)
                {
                    invalidObject = "ProcessLine";
                    msg = string.Format(@"Machine and Material is not compatible for Machine width max = {0} and Material width = {1}.",
                                        ProcessLine.WidthMin.ToString("#,##0.00"), valmax.ToString("#,##0.00"));
                    return false;
                }

                var resSumUsingWgt = Materials.Sum(i => i.UsingWeight);
                var resSumRem = Materials.Sum(i => i.RemainWeight);
                if (Math.Round((resSumUsingWgt + resSumRem), 0) != Math.Round(InputWeight, 0))
                {
                    invalidObject = "InputWeight";
                    msg = @"Input weight invalid.";
                    return false;
                }
            }

            #endregion Validate compatible between Machine and Material.

            #region Cutting line dose existing.

            if (CuttingDesign.ToList().Count > 0)
            {
                //Line from Sale Order
                if (CuttingDesign.Where(i => i.SONo != "").ToList().Count > 0)
                {
                    //Validate OrderType Selected.
                    if (OrderType == null)
                    {
                        invalidObject = "OrderType";
                        msg = "Please select Order Type.";
                        return false;
                    }
                }

                var resSumTotalWgt = CuttingDesign.Where(i => i.Status != "S").Sum(i => i.TotalWeight);
                if (Math.Round(OutputWeight, 0) != Math.Round(resSumTotalWgt, 0))
                {
                    invalidObject = "OutputWeight";
                    msg = @"Output weight invalid.";
                    return false;
                }
            }

            #endregion Cutting line dose existing.

            #region Compare date between Issue date and Due date.

            if (IssueDate > DueDate)
            {
                invalidObject = "IssueDate";
                msg = "Please change Due Date to be greater than Issue Date.";
                return false;
            }

            #endregion Compare date between Issue date and Due date.

            return valid;
        }

        #endregion Method
    }
}