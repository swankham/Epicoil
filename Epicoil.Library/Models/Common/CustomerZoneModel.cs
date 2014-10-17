using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicoil.Library.Models
{
    public class CustomerZoneModel
    {
        //public string Plant { get; set; }
        public string CustomerZoneCode { get; set; }
        public string CustomerZoneDesc { get; set; }
        public string CustomerZoneProvince { get; set; }
        public bool CustomerZoneActive { get; set; }
        public string CustomerZoneRemark { get; set; }


        public void DataBind(DataRow row)
        {
            //Plant = string.IsNullOrEmpty(row["Key5"].GetString()) ? "" : row["Key5"].GetString();
            //SaleSectCode = (string)row["Key1"].GetString();
            //SaleSectDesc = (string)row["Character01"].GetString();
            //Capacity = (decimal)row["Number01"].GetDecimal();

            CustomerZoneCode = string.IsNullOrEmpty(row["Key1"].GetString())  
            //CustomerZoneCode = (string)row["Key1".GetString();
            CustomerZoneDesc = (string)row["Character01"].GetString();
            CustomerZoneProvince = (string)row["Character02"].GetString();
            CustomerZoneActive = (bool)row["CheckBox01"].GetBoolean();
            CustomerZoneRemark = (string)row["Character03"].GetString();        
        }
    }
}
