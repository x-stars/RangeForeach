using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RangeForeach
{
    [TestClass]
    public class InstantiationTest
    {
        [TestMethod]
        public void GetEnumerator_StartEqualToEnd_DoNothing()
        {
            foreach (var index in ..)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetEnumerator_StartGreaterThanEnd_DoNothing()
        {
            foreach (var index in 10..0)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetEnumerator_NegativeIndex_CatchOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => (..-1).GetEnumerator());
        }

        [TestMethod]
        public void SteppedRange_ZeroStep_CatchOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => (0..10).Step(0).GetEnumerator());
        }
    }
}
