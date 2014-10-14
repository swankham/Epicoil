using Epicor.Mfg.BO;
using Epicor.Mfg.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using System.Data;

namespace Epicoil.Library.Repositories.Planning
{
    public class DieMasterRepo : IDieMasterRepo
    {
        public IEnumerable<DieModel> GetDieAll(string plant)
        {
            string sql = string.Format(@"SELECT * FROM UD107 WHERE Key5 = N'{0}' ORDER BY Key1 ASC", plant);

            return Repository.Instance.GetMany<DieModel>(sql);
        }

        public DieModel GetByID(string plant, string dieID)
        {
            string sql = string.Format(@"SELECT * FROM UD107 WHERE Key5 = N'{0}' and Key1 = N'{1}' ", plant, dieID);

            var result= Repository.Instance.GetOne<DieModel>(sql);
            if (result != null)
            {
                result.Pattern = GetDiePattern(result.PatternID);
            }
            return result;
        }

        public DiePatternModel GetDiePattern(string patternID)
        {
            string sql = string.Format(@"SELECT * FROM UD36 WHERE Key1 = N'{0}' ", patternID);

            return Repository.Instance.GetOne<DiePatternModel>(sql);
        }


        public IEnumerable<DieModel> Save(DieModel data, SessionInfo epiSession)
        {
            try
            {
                Session currSession = new Session(epiSession.UserID, epiSession.UserPassword, epiSession.AppServer, Session.LicenseType.Default);
                UD107 myUD107 = new UD107(currSession.ConnectionPool);
                UD107DataSet dsUD107 = new UD107DataSet();
                myUD107.GetaNewUD107(dsUD107);

                DataRow drUD107 = dsUD107.Tables[0].Rows[0];
                drUD107.BeginEdit();
                drUD107["Key1"] = data.DieCode;
                drUD107["Key5"] = epiSession.PlantID;
                drUD107["Character01"] = data.DieName;
                drUD107["Character02"] = string.IsNullOrEmpty(data.DieRemark) ? "" : data.DieRemark;
                drUD107["ShortChar01"] =  string.IsNullOrEmpty(data.PatternID) ? "" : data.PatternID;
                drUD107.EndEdit();
                myUD107.Update(dsUD107);
                currSession.Dispose();    
                return GetDieAll(epiSession.PlantID);
            }
            catch (Exception ex)
            {
                return null;
            }
           
      

        }
        public string MaxID()
        {
            string sql = string.Format(@"select top 1 * from UD107 
                                         order by cast(SUBSTRING (key1,4,4) as int) desc ");

            return Repository.Instance.GetOne<string>(sql, "Key1");
        }
    }
}
