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

        public static MergeTest mergeTest1NotNull = new MergeTest()
        {
            value1 = "test3",
            value2 = 3,
        };

        public static MergeTest mergeTest1AddRange = new MergeTest()
        {
            value1 = "testAddRange",
            value2 = 1,
        };

        private MergeTest2 mergeTest2 = new MergeTest2()
        {
            value1 = "test2",
            value2 = 2,
        };

        public Collection<MergeTest> mergeList1 = new Collection<MergeTest>();
        public Collection<MergeTest> mergeList2 = new Collection<MergeTest>();
        public Collection<MergeTest> mergeListAddrangeNotNull = new Collection<MergeTest>();
        public Collection<MergeTest> mergeListAddrangeNotNull2 = new Collection<MergeTest>();
        public Collection<MergeTest> mergeListAddrangeNotNull3 = new Collection<MergeTest>();
        public Collection<MergeTest> mergeListListNotNull = new Collection<MergeTest>();
        public Collection<MergeTest> mergeListAddrangeNull = null;
        public Collection<MergeTest> mergeListNull;

        public Collection<string> words = new Collection<string>();

        public Collection<MergeTest2> mergeList3 = new Collection<MergeTest2>();

        [SetUp]
        public void Initialization()
        {
            mergeList1.Add(mergeTest1);
            mergeListListNotNull.Add(mergeTest1NotNull);
            
            
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
        public void MergeListNotNullTest()
        {
            MergeExtension.MergeList<MergeTest, MergeTest>(mergeListListNotNull,mergeList1);
            Assert.AreEqual(mergeListListNotNull.FirstOrDefault().value1, mergeList1.FirstOrDefault().value1);
        }

        [Test]
        [ExpectedException(typeof(System.ArgumentException))]
        public void MergeListDestinationNullTest()
        {
            MergeExtension.MergeList<MergeTest, MergeTest>(mergeListNull, mergeList1);
        }

        [Test]
        public void MergeListSourceNullTest()
        {
            MergeExtension.MergeList<MergeTest, MergeTest>(mergeList1, mergeListNull);
            Assert.IsNotNull(mergeList1);
        }

        [Test]
        public void MergeListClassNotEqualTest()
        {
            MergeExtension.MergeList<MergeTest, MergeTest2>(mergeList1, mergeList3);
            Assert.AreEqual(mergeList1.FirstOrDefault(), mergeList3.FirstOrDefault());
        }

        [Test]
        public void AddRangeTest()
        {
            mergeListAddrangeNotNull.Add(mergeTest1AddRange);
            mergeListAddrangeNotNull2.Add(mergeTest1);
            MergeExtension.AddRange<MergeTest>(mergeListAddrangeNotNull, mergeListAddrangeNotNull2);
            Assert.AreEqual(2, mergeListAddrangeNotNull.Count);
        }

        [Test]
        [ExpectedException(typeof(System.ArgumentException))]
        public void AddRangeDestinationNullTest()
        {
            MergeExtension.AddRange<MergeTest>(mergeListAddrangeNull, mergeListAddrangeNotNull);
        }

        [Test]
        public void AddRangeSourceNullTest()
        {
            mergeListAddrangeNotNull3.Add(mergeTest1);
            MergeExtension.AddRange<MergeTest>(mergeListAddrangeNotNull3, mergeListAddrangeNull);
            Assert.AreEqual(1, mergeListAddrangeNotNull3.Count);
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
