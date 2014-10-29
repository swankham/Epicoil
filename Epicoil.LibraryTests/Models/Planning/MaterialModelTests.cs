using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Epicoil.Library.Models.Planning;

namespace Epicoil.Library.Models.Planning.Tests
{
    [TestClass()]
    public class MaterialModelTests
    {
        private MaterialModel model = new MaterialModel();

        [TestMethod()]
        public void CalculateLengthMeterTest()
        {
            decimal weight = 4000M;
            decimal width = 1190M;
            decimal thick = 2.5M;
            decimal gravity = 7.85M;
            decimal frontPlate = 0M;
            decimal backPlate = 0M;

            var result = model.CalculateLengthMeter(weight, width, thick, gravity, frontPlate, backPlate);
            Assert.AreEqual(171, Math.Round(result,0));
        }

        [TestMethod()]
        public void CalculateLengthMeterTestByZero()
        {
            decimal weight = 0M;
            decimal width = 0M;
            decimal thick = 0M;
            decimal gravity = 0M;
            decimal frontPlate = 0M;
            decimal backPlate = 0M;

            var result = model.CalculateLengthMeter(weight, width, thick, gravity, frontPlate, backPlate);
            Assert.AreEqual(0, Math.Round(result, 0));
        }

        [TestMethod()]
        public void ValidateUsingTest()
        {

            Assert.Fail();
        }
    }
}