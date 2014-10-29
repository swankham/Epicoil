using Epicoil.Library.Frameworks;
using Epicoil.Library.Models;
using System;
using System.Collections.Generic;

namespace Epicoil.Library.Repositories
{
    public class ClassMasterRepo : IClassMasterRepo
    {
        public IEnumerable<ClassMasterModel> GetAll(string plant)
        {
            IEnumerable<ClassMasterModel> Result = new List<ClassMasterModel>();

            string sql = string.Format(@"SELECT * FROM dbo.ucc_tqa_ClassMaster WHERE plant = '{0}'" + Environment.NewLine, plant);

            Result = Repository.Instance.GetMany<ClassMasterModel>(sql);

            return Result;
        }

        public ClassMasterModel GetByID(string plant, int ClassNo)
        {
            ClassMasterModel result = new ClassMasterModel();

            string sql = string.Format(@"SELECT * FROM dbo.ucc_tqa_ClassMaster WHERE Plant = '{0}' AND ClassNo = {1}" + Environment.NewLine, plant, ClassNo);
            result = Repository.Instance.GetOne<ClassMasterModel>(sql);

            return result;
        }
    }
}