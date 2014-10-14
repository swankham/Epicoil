using Epicoil.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Epicoil.Library.Repositories.Tests
{
    [TestClass()]
    public class BussinessTypeRepoTests
    {
        private BussinessTypeRepo obj = new BussinessTypeRepo();
        private string msg = string.Empty;

        [TestMethod()]
        public void GetAllTest()
        {
            var result = obj.GetAll();
            Assert.IsInstanceOfType(result, typeof(IEnumerable));
        }

        [TestMethod()]
        public void GetByFilterTest()
        {
            BussinessTypeModel model = new BussinessTypeModel();
            var result = obj.GetByFilter(model);

            Assert.IsInstanceOfType(result, typeof(IEnumerable));
        }

        [TestMethod()]
        public void GetByIDTest()
        {
            BussinessTypeModel result = new BussinessTypeModel();

            result = obj.GetByID("1");

            Assert.AreEqual(result, typeof(BussinessTypeModel));
        }
    }
}