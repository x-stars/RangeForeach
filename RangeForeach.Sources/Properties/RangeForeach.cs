﻿// Copyright (c) 2022 XstarS
// This file is released under the MIT License.
// https://opensource.org/licenses/MIT

#nullable disable
#pragma warning disable

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[DebuggerNonUserCode, ExcludeFromCodeCoverage]
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This type supports the range-foreach syntax " +
          "and should not be used directly in user code.")]
internal static class RangeEnumerable
{
    public static Enumerator GetEnumerator(this Range range)
    {
        return new Enumerator(in range);
    }

    [DebuggerNonUserCode, ExcludeFromCodeCoverage]
    public struct Enumerator
    {
        private int CurrentIndex;

        private readonly int EndIndex;

        private readonly long Padding;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(in Range range)
        {
            this.CurrentIndex = range.Start.GetOffset(0) - 1;
            this.EndIndex = range.End.GetOffset(0);
            this.Padding = default(long);
        }

        public int Current => this.CurrentIndex;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++this.CurrentIndex < this.EndIndex;
    }

#if STEPPED_RANGE
    public static Stepped Step(this Range range, int step)
    {
        return new Stepped(range, step);
    }

    [DebuggerNonUserCode, ExcludeFromCodeCoverage]
    public readonly struct Stepped
    {
        public readonly Range Range;

        public readonly int Step;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Stepped(Range range, int step)
        {
            if (step == 0) { throw Stepped.StepOutOfRange(); }
            this.Range = range; this.Step = step;
        }

        public Enumerator GetEnumerator() => new Enumerator(in this);

        private static ArgumentOutOfRangeException StepOutOfRange() =>
            new ArgumentOutOfRangeException("step", "Non-zero number required.");

        [DebuggerNonUserCode, ExcludeFromCodeCoverage]
        public struct Enumerator
        {
            private int CurrentIndex;

            private readonly int EndIndex;

            private readonly int StepSign;

            private readonly int StepValue;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal Enumerator(in Stepped stepped)
            {
                var range = stepped.Range;
                int start = range.Start.GetOffset(0);
                int end = range.End.GetOffset(0);
                int step = stepped.Step;
                int sign = step >> 31;
                this.CurrentIndex = (start - step) ^ sign;
                this.EndIndex = end ^ sign;
                this.StepSign = sign;
                this.StepValue = (step ^ sign) - sign;
            }

            public int Current => this.CurrentIndex ^ this.StepSign;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() =>
                (this.CurrentIndex += this.StepValue) < this.EndIndex;
        }
    }
#endif
}

#if !(EXCLUDE_FROM_CODE_COVERAGE_ATTRIBUTE || NETCOREAPP3_0_OR_GREATER)
#if !(NET40_OR_GREATER || NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER)
namespace System.Diagnostics.CodeAnalysis
{
    // Excludes the attributed code from code coverage information.
    internal sealed partial class ExcludeFromCodeCoverageAttribute : Attribute
    {
    }
}
#endif
#endif
