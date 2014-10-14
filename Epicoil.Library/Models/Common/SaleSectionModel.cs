using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicoil.Library.Models
{
    public class SaleSectionModel
    {
        public string Plant { get; set; }
        public string SaleSectCode { get; set; }
        public string SaleSectDesc { get; set; }
        public decimal Capacity { get; set; }


        public void DataBind(DataRow row)
        {
            Plant = string.IsNullOrEmpty(row["Key5"].GetString()) ? "" : row["Key5"].GetString();
            SaleSectCode = (string)row["Key1"].GetString();
            SaleSectDesc = (string)row["Character01"].GetString();
            Capacity = (decimal)row["Number01"].GetDecimal();
        }
    }
}
