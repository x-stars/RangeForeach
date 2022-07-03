using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Linq.Enumerable;

namespace RangeForeach
{
    [TestClass]
    public class ValueSumTest
    {
        [TestMethod]
        public void ForEachLoop_PositiveRange_GetExpectedSum()
        {
            var sum = 0;
            foreach (var index in 0..100)
            {
                sum += index;
            }
            var expected = Range(0, 100).Sum();
            Assert.AreEqual(expected, sum);
        }

        [TestMethod]
        public void ForEachLoop_NegativeRange_GetExpectedSum()
        {
            var sum = 0;
            foreach (var index in ^100..0)
            {
                sum += index;
            }
            var expected = Range(-100, 100).Sum();
            Assert.AreEqual(expected, sum);
        }

        [TestMethod]
        public void ForEachLoop_HybridRange_GetExpectedSum()
        {
            var sum = 0;
            foreach (var index in ^100..100)
            {
                sum += index;
            }
            var expected = Range(-100, 200).Sum();
            Assert.AreEqual(expected, sum);
        }

        [TestMethod]
        public void ForEachLoop_InvalidRange_GetZeroSum()
        {
            var sum = 0;
            foreach (var index in 100..^100)
            {
                sum += index;
            }
            var expected = 0;
            Assert.AreEqual(expected, sum);
        }

        [TestMethod]
        public void ForEachLoop_PositiveSteppedRange_GetExpectedSum()
        {
            var sum = 0;
            foreach (var index in (^100..100).Step(2))
            {
                sum += index;
            }
            var expected = Range(-50, 100).Sum() * 2;
            Assert.AreEqual(expected, sum);
        }

        [TestMethod]
        public void ForEachLoop_NegativeSteppedRange_GetExpectedSum()
        {
            var sum = 0;
            foreach (var index in (99..^101).Step(-1))
            {
                sum += index;
            }
            var expected = Range(-50, 100).Sum() * 2;
            Assert.AreEqual(expected, sum);
        }
    }
}
