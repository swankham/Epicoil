using Epicoil.Library.Frameworks;
using Epicoil.Library.Models.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicoil.Library.Repositories.Production
{
    public class ProductionRepo : IProductionRepo
    {
        public IEnumerable<SerialCuttingModel> GetAllSerialByWorkOrder(int workOrderID)
        {
            string sql = string.Format(@"SELECT mat.MCSSNo, mat.Serial as MaterialSerialNo, cut.SONo, cut.SOLine
	                                            , cut.NORNum, 1 as Quantity, cut.Possession, busi.Key1 as BussinessType, busi.Character01 as BussinessTypeName
	                                            , cmdt.Key1 as CommodityCode, cmdt.Character01 as CommodityName
	                                            , spec.Key1 as SpecCode, spec.Character01 as SpecName, spec.Number01 as Gravity
	                                            , coat.Key1 as CoatingCode, ISNULL(coat.Character01, '') as CoatingName, ISNULL(coat.Number01, 0.00) as FrontPlate, ISNULL(coat.Number02, 0.00) as BackPlate
	                                            , gsn.*, mat.MCSSNo, pln.WorkOrderNum, 0 as ProductionID, 0 as LengthActual, 0 as WeightActual, 0 as NGFlag
                                            FROM ucc_pln_SerialGenerated gsn
	                                            LEFT JOIN ucc_pln_Material mat ON(gsn.MaterialTransLineID = mat.TransactionLineID)
	                                            LEFT JOIN ucc_pln_CuttingDesign cut ON(gsn.CuttingLineID = cut.LineID)
	                                            LEFT JOIN ucc_pln_PlanHead pln ON(gsn.WorkOrderID = pln.WorkOrderID)
	                                            LEFT JOIN UD25 busi ON(cut.BussinessType = busi.Key1)
	                                            LEFT JOIN UD29 cmdt ON(cut.CommodityCode = cmdt.Key1)
	                                            LEFT JOIN UD30 spec ON(cut.CommodityCode = spec.Key2 and cut.SpecCode = spec.Key1)
	                                            LEFT JOIN UD31 coat ON(cut.CoatingCode = coat.Key1)
                                            WHERE gsn.WorkOrderID = {0}
                                            ORDER BY gsn.CutSeq", workOrderID);

            return Repository.Instance.GetMany<SerialCuttingModel>(sql);
        }
    }
}
