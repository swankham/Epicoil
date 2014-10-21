using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Repositories.Planning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Epicoil.Library.Repositories.Planning.Tests
{
    [TestClass()]
    public class WorkEntryRepoTests
    {
        private readonly IWorkEntryRepo _repo;

        public WorkEntryRepoTests()
        {
            _repo = new WorkEntryRepo();
        }

        [TestMethod()]
        public void GenWorkOrderFixFormatTest()
        {
            string yy = DateTime.Now.ToString("yy");
            string mm = Enum.GetName(typeof(Month), int.Parse(DateTime.Now.ToString("MM")));
            int id = 1;
            string expected = "K" + yy + mm + "0001";
            string actual = _repo.GenWorkOrderFixFormat(id);

            Assert.AreEqual(expected, actual);            
        }
    }
}
