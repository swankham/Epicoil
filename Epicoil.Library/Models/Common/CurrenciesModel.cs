using System.Data;

namespace Epicoil.Library.Models
{
    public class CurrenciesModel
    {
        public string CurrencyCode { get; set; }

        public decimal ExchangeRate { get; set; }

        public virtual void DataBind(DataRow row)
        {
            this.CurrencyCode = (string)row["CurrencyCode"];
            this.ExchangeRate = (decimal)row["CurrentRate"];
        }
    }
}