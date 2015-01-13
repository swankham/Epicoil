using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories.Common
{
    public class CurrenciesRepo : ICurrenciesRepo
    {
        public IEnumerable<CurrencyModel> GetAll()
        {
            IEnumerable<CurrencyModel> result = new List<CurrencyModel>();
            string sql = string.Format(@"SELECT SourceCurrCode as CurrencyCode, CurrentRate, MAX(EffectiveDate) AS EffectiveDate
		                                            FROM CurrExRate
		                                            WHERE TargetCurrCode = 'THB' and RateGrpCode Like '%BUY%'
		                                            GROUP BY SourceCurrCode, CurrentRate
                                            UNION ALL
                                            SELECT curr.CurrencyCode,
	                                            ISNULL((select top 1 CurrentRate
		                                            from CurrExRate
		                                            where SourceCurrCode = curr.CurrencyCode
		                                            order by EffectiveDate desc),1) as CurrentRate, GETDATE() AS EffectiveDate
		                                            FROM Currency curr
		                                            WHERE curr.Company = 'UCC01' and curr.BaseCurr = 1
		                                            ORDER BY EffectiveDate DESC");

            result = Repository.Instance.GetMany<CurrencyModel>(sql);
            return result;
        }

        public CurrencyModel GetByID(string code)
        {
            string sql = string.Format(@"SELECT SourceCurrCode as CurrencyCode, CurrentRate, MAX(EffectiveDate) AS EffectiveDate
		                                            FROM CurrExRate
		                                            WHERE TargetCurrCode = 'THB' AND RateGrpCode Like '%BUY%'
		                                            AND SourceCurrCode = N'{0}'
		                                            GROUP BY SourceCurrCode, CurrentRate
                                            UNION ALL
                                            SELECT curr.CurrencyCode,
	                                            ISNULL((select top 1 CurrentRate
		                                            from CurrExRate
		                                            where SourceCurrCode = curr.CurrencyCode
		                                            order by EffectiveDate desc),1) as CurrentRate, GETDATE() AS EffectiveDate
		                                            FROM Currency curr
		                                            WHERE curr.Company = 'UCC01' and curr.BaseCurr = 1
		                                            AND curr.CurrencyCode = N'{0}'
		                                            ORDER BY EffectiveDate DESC", code);
            return Repository.Instance.GetOne<CurrencyModel>(sql);
        }
    }
}