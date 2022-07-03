using System;
using BenchmarkDotNet.Attributes;
using static System.Linq.Enumerable;

namespace RangeForeach
{
    public class LoopBenchmark
    {
        [CLSCompliant(false)]
        [Params(1, 3, 10, 100, 1000)]
        public int LoopCount;

        [Benchmark(Baseline = true)]
        public void LegacyForLoop()
        {
            var sum = 0;
            var loopCount = this.LoopCount;
            for (int num = 0; num < loopCount; num++)
            {
                sum += num;
            }
        }

        [Benchmark]
        public void RangeForeachLoop()
        {
            var sum = 0;
            var loopCount = this.LoopCount;
            foreach (int num in ..loopCount)
            {
                sum += num;
            }
        }

        [Benchmark]
        public void SteppedRangeForeachLoop()
        {
            var sum = 0;
            var loopCount = this.LoopCount;
            foreach (int num in (..loopCount).Step(1))
            {
                sum += num;
            }
        }

        [Benchmark]
        public void EnumerableRangeForeachLoop()
        {
            var sum = 0;
            var loopCount = this.LoopCount;
            foreach (int num in Range(0, loopCount))
            {
                sum += num;
            }
        }
    }
}
