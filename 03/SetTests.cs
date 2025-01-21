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
    public class SetTests
    {
        [TestMethod]
        public void Add_ContainsElement_ReturnsTrue()
        {
            Set set = new Set();
            set.Add(5);
            Assert.IsTrue(set.Contains(5));
        }

        [TestMethod]
        public void Remove_ElementRemoved_ReturnsFalse()
        {
            Set set = new Set();
            set.Add(5);
            set.Remove(5);
            Assert.IsFalse(set.Contains(5));
        }

        [TestMethod]
        public void Size_EmptySet_ReturnsZero()
        {
            Set set = new Set();
            Assert.AreEqual(0, set.Size());
        }

        [TestMethod]
        public void Union_SetsMergedCorrectly()
        {
            Set set1 = new Set();
            set1.Add(1);
            set1.Add(2);

            Set set2 = new Set();
            set2.Add(3);

            Set unionSet = set1.Union(set2);
            Assert.AreEqual(3, unionSet.Size());
            Assert.IsTrue(unionSet.Contains(1));
            Assert.IsTrue(unionSet.Contains(2));
            Assert.IsTrue(unionSet.Contains(3));
        }

        [TestMethod]
        public void Intersection_SetsIntersectedCorrectly()
        {
            Set set1 = new Set();
            set1.Add(1);
            set1.Add(2);

            Set set2 = new Set();
            set2.Add(2);
            set2.Add(3);

            Set intersectSet = set1.Intersection(set2);
            Assert.AreEqual(1, intersectSet.Size());
            Assert.IsTrue(intersectSet.Contains(2));
        }

        [TestMethod]
        public void Subtract_SetsSubtractedCorrectly()
        {
            Set set1 = new Set();
            set1.Add(1);
            set1.Add(2);

            Set set2 = new Set();
            set2.Add(2);
            set2.Add(3);

            Set subtractSet = set1.Subtract(set2);
            Assert.AreEqual(1, subtractSet.Size());
            Assert.IsTrue(subtractSet.Contains(1));
        }

        [TestMethod]
        public void IsSubsetOf_CorrectlyIdentifiesSubset()
        {
            Set set1 = new Set();
            set1.Add(1);
            set1.Add(2);

            Set set2 = new Set();
            set2.Add(1);
            set2.Add(2);
            set2.Add(3);

            Assert.IsTrue(set1.IsSubsetOf(set2));
            Assert.IsFalse(set2.IsSubsetOf(set1));
        }
    }

}
