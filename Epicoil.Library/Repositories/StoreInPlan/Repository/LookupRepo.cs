using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Epicoil.Library.Models.StoreInPlan;
using Epicoil.Library.Frameworks;

namespace Epicoil.Library.Repositories.StoreInPlan
{
    public class LookupRepo : ILookupRepo
    {
        public IEnumerable<MappingLookupModel> GetAll(string typeCode, string supplierCode)
        {
            string sql = string.Format(@"SELECT * FROM ucc_ic_MappingLookUp (NOLOCK) WHERE typeCode = '{0}' AND supplierCode = '{1}'                                                     
                                                    Order by SupCode asc", typeCode, supplierCode);

            return Repository.Instance.GetMany<MappingLookupModel>(sql);
        }

        public IEnumerable<MappingLookupModel> GetByFilter(MappingLookupModel model)
        {
            IEnumerable<MappingLookupModel> query = GetAll(model.TypeCode, model.SupplierCode);

            if (model.SupCode != null) { query = query.Where(p => p.SupCode.Contains(model.SupCode.ToString())); }
            if (model.UCCCode != null) { query = query.Where(p => p.UCCCode.Contains(model.UCCCode.ToString())); }

            return query;
        }

        public MappingLookupModel GetByID(MappingLookupModel model)
        {
            string sql = string.Format(@"SELECT * FROM ucc_ic_MappingLookUp (NOLOCK) WHERE typeCode = '{0}' AND supplierCode = '{1}' AND SupCode = '{2}'                                                 
                                                    Order by SupCode asc", model.TypeCode, model.SupplierCode, model.SupCode);

            return Repository.Instance.GetOne<MappingLookupModel>(sql);
        }

        public IEnumerable<MappingLookupModel> Save(MappingLookupModel model)
        {
            string sql = string.Format(@"IF NOT EXISTS
									    (
										    SELECT * FROM ucc_ic_MappingLookUp (NOLOCK)
										    WHERE  typeCode = '{0}' AND supplierCode = '{1}' AND SupCode = '{2}'
									    )
									    BEGIN
                                            INSERT INTO ucc_ic_MappingLookUp
                                                       (TypeCode
                                                       ,SupplierCode
                                                       ,SupCode
                                                       ,UCCCode
                                                       ,ActiveFlag
                                                       ,UCCCodeForeign)
                                                 VALUES
                                                       ('{0}' --<TypeCode, nvarchar(50),>
                                                       ,N'{1}' --<SupplierCode, nvarchar(100),>
                                                       ,N'{2}' --<SupCode, nvarchar(100),>
                                                       ,N'{3}' --<UCCCode, nvarchar(100),>
                                                       ,{4} --<ActiveFlag, tinyint,>)
                                                       ,N'{5}'
                                                 )
                                        END
                                        ELSE
                                        BEGIN
                                            UPDATE ucc_ic_MappingLookUp
                                               SET UCCCode = N'{3}' --<Quantity, decimal(15,2),>
                                                   , UCCCodeForeign = N'{5}'
                                             WHERE typeCode = '{0}' AND supplierCode = '{1}' AND SupCode = '{2}'
                                        END
                                                " + Environment.NewLine
                                                     , model.TypeCode
                                                     , model.SupplierCode
                                                     , model.SupCode
                                                     , model.UCCCode
                                                     , 1
                                                     , model.UCCCodeForeign
                                                     );

            Repository.Instance.ExecuteWithTransaction(sql, "Update Lookup Mapping");

            return GetAll(model.TypeCode, model.SupplierCode);
        }

        public IEnumerable<MappingLookupModel> Delete(MappingLookupModel model)
        {
            string sql = string.Format(@"DELETE FROM ucc_ic_MappingLookUp
										    WHERE  LookupID = {0} " + Environment.NewLine
                                                     , model.LookupID);

            Repository.Instance.ExecuteWithTransaction(sql, "Delete Lookup Mapping");

            return GetAll(model.TypeCode, model.SupplierCode);
        }
    }
}
