using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Epicoil.Library.Repositories;

namespace Epicoil.Library.Models.Planning
{
    public class PlaningHeadModel
    {
        private readonly IResourceRepo _repoRes;
        private readonly IUserCodeRepo _repoUcd;

        public PlaningHeadModel()
        {
            this._repoRes = new ResourceRepo();
            this._repoUcd = new UserCodeRepo();
        }

        #region Attribute
        public string Company { get; set; }

        public string Plant { get; set; }

        public int WorkOrderID { get; set; }

        public string WorkOrderNum { get; set; }

        public string ProcessLine { get; set; }

        public decimal ProcessStep { get; set; }

        public string OrderType { get; set; }

        public string PIC { get; set; }

        public string PICName { get; set; }

        public string Possession { get; set; }

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

        public IEnumerable<ResourceModel> ResourceList = new List<ResourceModel>();
        public IEnumerable<UserCodeModel> OrderTypeList = new List<UserCodeModel>();
        public IEnumerable<UserCodeModel> PossessionList = new List<UserCodeModel>();
        public IEnumerable<MaterailModel> MaterialList = new List<MaterailModel>();

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

        public List<MaterailModel> Materails
        {
            get { return this.MaterialList.ToList(); }
            set { this.MaterialList = value; }
        }
        #endregion

        #region Method
        public virtual void DataBind(DataRow row)
        {
            this.Company = (string)row["Company"].GetString();
            this.Plant = (string)row["Plant"].GetString();
            this.WorkOrderID = (int)row["WorkOrderID"].GetInt();
            this.WorkOrderNum = (string)row["WorkOrderNum"].GetString();
            this.ProcessLine = (string)row["ProcessLine"].GetString();
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
            this.BT = (string)row["BT"].GetString();
            this.LVTrim = Convert.ToBoolean(row["LVTrim"].GetInt());
            this.PackingPlan = Convert.ToBoolean(row["PackingPlan"].GetInt());
            this.CreationDate = (DateTime)row["CreationDate"].GetDate();
            this.LastUpdateDate = (DateTime)row["LastUpdateDate"].GetDate();
            this.CreatedBy = (string)row["CreatedBy"].GetString();
            this.UpdatedBy = (string)row["UpdatedBy"].GetString();
        }

        public void Load() //0 = Nothing.
        {
            this.FormState = 0;
            this.SimulateFlag = false;
            this.IssueDate = DateTime.Now;
            this.DueDate = DateTime.Now;
            this.ResourceList = new List<ResourceModel>();
            this.OrderTypeList = new List<UserCodeModel>();
            this.PossessionList = new List<UserCodeModel>();
            this.MaterialList = new List<MaterailModel>();
        }

        public void New(string plantId) //1 = New Transaction.
        {
            this.FormState = 1;            
            this.IssueDate = DateTime.Now;
            this.DueDate = DateTime.Now;
            this.ResourceList = _repoRes.GetAll(plantId);
            this.OrderTypeList = _repoUcd.GetAll("OrderType");
            this.PossessionList = _repoUcd.GetAll("Pocessed");
            this.MaterialList = new List<MaterailModel>();
        }

        public void Save() //2 = Transaction was save.
        {
            this.FormState = 2;
        }


        #endregion
    }
}