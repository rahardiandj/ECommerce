using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace ecommerce.core.unittest
{
    public class MergeUnitTest
    {
        public static MergeTest mergeTest1 = new MergeTest()
        {
            value1 = "test1",
            value2 = 1,
        };

        private MergeTest2 mergeTest2 = new MergeTest2()
        {
            value1 = "test2",
            value2 = 2,
        };

        public Collection<MergeTest> mergeList1 = new Collection<MergeTest>();
        public Collection<MergeTest> mergeList2 = new Collection<MergeTest>();
        public Collection<MergeTest> mergeListNull;

        public Collection<MergeTest2> mergeList3 = new Collection<MergeTest2>();

        [SetUp]
        public void Initialization()
        {
            mergeList1.Add(mergeTest1);
        }


        [Test]
        public void MergeTest()
        {
            MergeExtension.Merge(mergeTest1, mergeTest2);
            Assert.AreEqual(mergeTest1.value1, mergeTest2.value1);
            Assert.AreEqual(mergeTest1.value2, mergeTest2.value2);
        }

        [Test]
        public void MergeListTest()
        {
            MergeExtension.MergeList<MergeTest, MergeTest>(mergeList1, mergeList2);
            Assert.AreEqual(mergeList1.FirstOrDefault(), mergeList2.FirstOrDefault());        
        }

        [Test]
        [ExpectedException(typeof(System.ArgumentException))]
        public void MergeListDestinationNullTest()
        {
            MergeExtension.MergeList<MergeTest, MergeTest>(mergeListNull, mergeList1);
        }

        [Test]
        public void MergeListClassNotEqualTest()
        {
            MergeExtension.MergeList<MergeTest, MergeTest2>(mergeList1, mergeList3);
            Assert.AreEqual(mergeList1.FirstOrDefault(), mergeList3.FirstOrDefault());
        }

    }

    #region Class For Testing

    public class MergeTest
    {
        public string value1 { get; set; }
        public int value2 { get; set; }
    }

    public class MergeTest2
    {
        public string value1 { get; set; }
        public int value2 { get; set; }
    }

    #endregion
}
