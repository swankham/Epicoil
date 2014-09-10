using System.Collections.Generic;
using System.Linq;
using Epicoil.Library.Frameworks;

using Epicoil.Library.Models;

namespace Epicoil.Library.Repositories
{
    public class PORepo : IPORepo
    {
        public IEnumerable<POLineModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<POLineModel> GetByPO(int PONum)
        {
            string sql = string.Format(@"SELECT cmdt.Character01 AS CommodityName, spec.Character01 AS SpecName
	                                            , coat.Character01 AS CoatingName, eusr.Name as EndUserName, acteusr.Name as ActlEndUserName
	                                            ,pod.* 
                                            FROM POdetail pod 
	                                            LEFT JOIN UD29 cmdt ON(pod.ShortChar01 = cmdt.Key1)
	                                            LEFT JOIN UD30 spec ON(pod.ShortChar02 = spec.Key1 AND cmdt.Key1 = spec.Key2)
	                                            LEFT JOIN UD31 coat ON(pod.ShortChar03 = coat.Key1)
	                                            LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
	                                            LEFT JOIN Customer acteusr ON(pod.Character03 = acteusr.CustID)
                                            WHERE pod.PONum = {0} ORDER BY pod.POLine ASC", PONum);
            return Repository.Instance.GetMany<POLineModel>(sql);
        }

        public POLineModel GetByID(int PONum, int POLine)
        {
            string sql = string.Format(@"SELECT cmdt.Character01 AS CommodityName, spec.Character01 AS SpecName
	                                            , coat.Character01 AS CoatingName, eusr.Name as EndUserName, acteusr.Name as ActlEndUserName
	                                            ,pod.* 
                                            FROM POdetail pod 
	                                            LEFT JOIN UD29 cmdt ON(pod.ShortChar01 = cmdt.Key1)
	                                            LEFT JOIN UD30 spec ON(pod.ShortChar02 = spec.Key1 AND cmdt.Key1 = spec.Key2)
	                                            LEFT JOIN UD31 coat ON(pod.ShortChar03 = coat.Key1)
	                                            LEFT JOIN Customer eusr ON(pod.Character02 = eusr.CustID)
	                                            LEFT JOIN Customer acteusr ON(pod.Character03 = acteusr.CustID)
                                            WHERE pod.PONum = {0} and pod.POLine = {1}", PONum, POLine);
            return Repository.Instance.GetOne<POLineModel>(sql);
        }
    }
}
