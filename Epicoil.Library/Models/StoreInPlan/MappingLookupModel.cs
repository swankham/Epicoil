using System.Data;

namespace Epicoil.Library.Models.StoreInPlan
{
    public class MappingLookupModel
    {
        public int LookupID { get; set; }

        public string TypeCode { get; set; }

        public string TypeName
        {
            get
            {
                if (TypeCode == "CMDTY")
                {
                    return "Commodity";
                }
                else if (TypeCode == "SPEC")
                {
                    return "Specification";
                }
                else if (TypeCode == "COATING")
                {
                    return "Coating";
                }
                else
                {
                    return "";
                }
            }
        }

        public string SupplierCode { get; set; }

        public string SupplierName { get; set; }

        public string SupCode { get; set; }

        public string UCCCode { get; set; }

        public string UCCCodeForeign { get; set; }

        public int ActiveFlag { get; set; }

        public void DataBind(DataRow row)
        {
            this.LookupID = (int)row["LookupID"];
            this.TypeCode = (string)row["TypeCode"];
            this.SupplierCode = (string)row["SupplierCode"];
            this.SupCode = (string)row["SupCode"];
            this.UCCCode = (string)row["UCCCode"];
            this.ActiveFlag = (int)row["ActiveFlag"].GetInt();
            this.UCCCodeForeign =  string.IsNullOrEmpty(row["UCCCodeForeign"].GetString()) ? "" : (string)row["UCCCodeForeign"];
        }
    }
}