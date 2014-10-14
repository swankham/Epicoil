using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicoil.Library.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Diagnostics;

namespace Epicoil.Library.Repositories.Tests
{
    [TestClass()]
    public class MakerRepoTests
    {
        private readonly MakerRepo obj = new MakerRepo();
        [TestMethod()]
        public void GetAllTest()
        {
           var result = obj.GetAll();
           Assert.AreNotEqual(0, result.Count());
            //Assert.IsInstanceOfType(result.Count, typeof(IEnumerable));
        }
    }
}
