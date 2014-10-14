using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Epicoil.Library.Repositories;
using System.Collections;
namespace Epicoil.Library.Models.Tests
{
    [TestClass()]
    public class BussinessTypeModelTests
    {
        [TestMethod()]
        public void DataBindTest()
        {
            BussinessTypeRepo obj = new BussinessTypeRepo();
            BussinessTypeModel model = new BussinessTypeModel();
            var result = obj.GetByFilter(model);

            Assert.IsInstanceOfType(result, typeof(IEnumerable));
        }
    }
}
