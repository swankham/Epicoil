using System;
using System.Web;

namespace Epicoil.Library.Models
{
    public class PackingStyleModel
    {
        public string Company { get; set; }

        public string Plant { get; set; }

        public string CodeNum { get; set; }

        public string Code { get; set; }

        public string Num { get; set; }

        public int StyleType { get; set; }
        public string StyleTypeName 
        {
            get
            {
                if (StyleType == 1)
                {
                    return "Sheet";
                }
                else if (StyleType == 2)
                {
                    return "Eye Up";
                }
                else  // 3
                {
                    return "Eye Side";
                }
            } 
        }

        public int CoilSkid { get; set; }
        public string CoilSkidName
        {
            get
            {
                if (CoilSkid == 1)
                {
                    return "Wooden";
                }
                else if (CoilSkid == 2)
                {
                    return "Steel";
                }
                else  // 0
                {
                    return "No Skid";
                }
            }
        }

        public int CoilWrapping { get; set; }
        public string CoilWrappingName
        {
            get
            {
                if (CoilWrapping == 1)
                {
                    return "Polysak";
                }
                else if (CoilWrapping == 2)
                {
                    return "Plastic";
                }
                else if (CoilWrapping == 3)
                {
                    return "Paper";
                }
                else  // 0
                {
                    return "No Wrapping";
                }
            }
        }

        public int CoilStrapping { get; set; }
        public string CoilStrappingName
        {
            get
            {
                if (CoilStrapping == 1)
                {
                    return "Steel band";
                }
                else if (CoilStrapping == 2)
                {
                    return "Plastic band";
                }
                else  // 0
                {
                    return "No Strapping";
                }
            }
        }

        public decimal CoilStrappingSize { get; set; }

        public int CoilDiameter { get; set; }
        public string CoilDiameterName
        {
            get
            {
                if (CoilDiameter == 0)
                {
                    return "OD";
                }
                else  // 1
                {
                    return "ID";
                }
            }
        }

        public decimal CoilDiameterQty { get; set; }

        public decimal CoilDiameterSize { get; set; }

        public int CoilProtector { get; set; }
        public string CoilProtectorName
        {
            get
            {
                if (CoilProtector == 1)
                {
                    return "Top";
                }
                else if (CoilProtector == 2)
                {
                    return "Bottom";
                }
                else if (CoilProtector == 3)
                {
                    return "Top-Bottom";
                }
                else  // 0
                {
                    return "No Protector";
                }
            }
        }

        public decimal CoilProtectorSize { get; set; }

        public int CoilWoodInsert { get; set; }

        public decimal CoilWoodSize { get; set; }

        public int SheetSkid { get; set; }
        public string SheetSkidName
        {
            get
            {
                if (SheetSkid == 1)
                {
                    return "Wooden";
                }
                else if (SheetSkid == 2)
                {
                    return "Steel";
                }
                else  // 0
                {
                    return "No Skid";
                }
            }
        }

        public int SheetWrapping { get; set; }
        public string SheetWrappingName
        {
            get
            {
                if (SheetWrapping == 1)
                {
                    return "Plastic";
                }
                else if (SheetWrapping == 2)
                {
                    return "Paper";
                }
                else  // 0
                {
                    return "No Wrapping";
                }
            }
        }

        public int SheetStrapping { get; set; }
        public string SheetStrappingName
        {
            get
            {
                if (SheetStrapping == 1)
                {
                    return "Steel band";
                }
                else if (SheetStrapping == 2)
                {
                    return "Plastic band";
                }
                else  // 0
                {
                    return "No Strapping";
                }
            }
        }

        public decimal SheetStrappingSteelSize { get; set; }

        public decimal SheetStrappingPlasticSize { get; set; }

        public int SheetProtector { get; set; }
        public string SheetProtectorName
        {
            get
            {
                if (SheetProtector == 1)
                {
                    return "Top";
                }
                else if (SheetProtector == 2)
                {
                    return "Bottom";
                }
                else if (SheetProtector == 3)
                {
                    return "Top-Bottom";
                }
                else  // 0
                {
                    return "Top wood";
                }
            }
        }

        public decimal SheetProtectorSize { get; set; }

        public bool SteelCover { get; set; }

        public string StyleImgPath { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public void DataBind(System.Data.DataRow row)
        {
            this.Company = row["Company"].GetString();
            this.Plant = row["Plant"].GetString();
            this.CodeNum = row["CodeNum"].GetString();
            this.StyleType = row["StyleType"].GetInt();
            this.CoilSkid = row["CoilSkid"].GetInt();
            this.CoilWrapping = row["CoilWrapping"].GetInt();
            this.CoilStrapping = row["CoilStrapping"].GetInt();
            this.CoilStrappingSize = row["CoilStrappingSize"].GetDecimal();
            this.CoilDiameter = row["CoilDiameter"].GetInt();
            this.CoilDiameterQty = row["CoilDiameterQty"].GetDecimal();
            this.CoilDiameterSize = row["CoilDiameterSize"].GetDecimal();
            this.CoilProtector = row["CoilProtector"].GetInt();
            this.CoilProtectorSize = row["CoilProtectorSize"].GetDecimal();
            this.CoilWoodInsert = row["CoilWoodInsert"].GetInt();
            this.CoilWoodSize = row["CoilWoodSize"].GetDecimal();
            this.SheetSkid = row["SheetSkid"].GetInt();
            this.SheetWrapping = row["SheetWrapping"].GetInt();
            this.SheetStrapping = row["SheetStrapping"].GetInt();
            this.SheetStrappingSteelSize = row["SheetStrappingSteelSize"].GetDecimal();
            this.SheetStrappingPlasticSize = row["SheetStrappingPlasticSize"].GetDecimal();
            this.SheetProtector = row["SheetProtector"].GetInt();
            this.SheetProtectorSize = row["SheetProtectorSize"].GetDecimal();
            this.SteelCover = Convert.ToBoolean(row["SteelCover"]); // Original source type is byte
            this.CreationDate = (DateTime)row["CreationDate"].GetDate();
            this.LastUpdateDate = (DateTime)row["LastUpdateDate"].GetDate();
            this.CreatedBy = row["CreatedBy"].GetString();
            this.UpdatedBy = row["UpdatedBy"].GetString();

            this.Code = string.IsNullOrEmpty(this.CodeNum) ? "" : this.CodeNum.Substring(0, 2);
            this.Num = string.IsNullOrEmpty(this.CodeNum) ? "" : this.CodeNum.Substring(3, 4);
        }
    }
}