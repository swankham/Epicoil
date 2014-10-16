using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using Epicoil.Library.Models.Planning;
using Epicor.Mfg.BO;
using Epicor.Mfg.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

            var result = Repository.Instance.GetOne<DieModel>(sql);
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

                string whereClause = string.Format(@"UD107.Key1 ='{0}' AND UD107.Key5 = '{1}'", data.DieCode, epiSession.PlantID);
                bool morePages = false;
                bool dataExisting = false;

                try
                {
                    UD107DataSet ds = myUD107.GetByID(data.DieCode, "", "", "", epiSession.PlantID);
                    dataExisting = true;
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Record not found.") dataExisting = false;
                }

                if (dataExisting)
                {
                    dsUD107 = myUD107.GetRows(whereClause, "", "", 0, 1, out morePages);
                }
                else
                {
                    myUD107.GetaNewUD107(dsUD107);
                }

                DataRow drUD107 = dsUD107.Tables[0].Rows[0];
                drUD107.BeginEdit();
                drUD107["Key1"] = data.DieCode;
                drUD107["Key5"] = epiSession.PlantID;
                drUD107["Character01"] = data.DieName;
                drUD107["Character02"] = string.IsNullOrEmpty(data.DieRemark) ? "" : data.DieRemark;
                drUD107["ShortChar01"] = string.IsNullOrEmpty(data.PatternID) ? "" : data.PatternID;
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
            string result = Repository.Instance.GetOne<string>(sql, "Key1").GetString();
            return string.IsNullOrEmpty(result) ? "DIE0000" : result;
        }

        public IEnumerable<DiePatternModel> GetPatternAll()
        {
            string sql = string.Format(@"SELECT * FROM UD36 ORDER BY Key1 ASC");

            return Repository.Instance.GetMany<DiePatternModel>(sql);
        }

        public IEnumerable<DiePatternModel> GetByFilter(DiePatternModel Filter)
        {
            IEnumerable<DiePatternModel> query = this.GetPatternAll();

            if (!string.IsNullOrEmpty(Filter.PatternID)) query = query.Where(p => p.PatternID.ToString().ToUpper().Contains(Filter.PatternID.ToString().ToUpper()));

            return query;
        }


        public void DeleteLine(string dieID)
        {
            string sql = "";
            sql += string.Format(@"DELETE FROM UD107 WHERE Key1 = '{0}'" + Environment.NewLine, dieID);

            Repository.Instance.ExecuteWithTransaction(sql, "Die Master");
        }


        public IEnumerable<DieModel> GetByFilterDie(DieModel Filter)
        {
            IEnumerable<DieModel> query = this.GetDieAll(Filter.PlantID);

            if (!string.IsNullOrEmpty(Filter.DieCode)) query = query.Where(p => p.DieCode.ToString().ToUpper().Contains(Filter.DieCode.ToString().ToUpper()));

            return query;
        }

      
    }
}