using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNE03;

namespace TestPNE03
{
    [TestClass]
    public class MathLibTests
    {
        [TestMethod]
        public void MaxItem_ValidArray_ReturnsMax()
        {
            int[] arr = { 1, 3, 5, 2, 4 };
            int? result = MathLib.MaxItem(arr);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void MaxItem_EmptyArray_ReturnsNull()
        {
            int[] arr = { };
            int? result = MathLib.MaxItem(arr);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IsPowOf_ValidPower_ReturnsTrue()
        {
            bool result = MathLib.IsPowOf(8, 2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPowOf_NotPower_ReturnsFalse()
        {
            bool result = MathLib.IsPowOf(10, 2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DecToBin_ValidNumber_ReturnsBinaryString()
        {
            string result = MathLib.DecToBin(10);
            Assert.AreEqual("1010", result);
        }

        [TestMethod]
        public void DecToBin_Zero_ReturnsZero()
        {
            string result = MathLib.DecToBin(0);
            Assert.AreEqual("0", result);
        }
    }
}