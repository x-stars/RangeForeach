# C# Range-Foreach Syntax Extensions

Provides the range-foreach syntax for **C# 9.0 or higher**.

Supported frameworks:

* .NET Framework >= 4.0
* .NET Core >= 1.0
* .NET Standard >= 1.0

## Range-Foreach Syntax

Reference source files or the NuGet package to write foreach-loops like this:

``` CSharp
foreach (var index in 0..100)
{
    // loop body...
}
```

which is equivalent to the legacy for-loop below:

``` CSharp
for (int index = 0; index < 100; index++)
{
    // loop body...
}
```

**NOTE:** Use `^` to represent negative numbers, e.g. `^100..0` (instead of `-100..0`).

### Stepped Syntax

If `STEPPED_RANGE` is defined, this syntax can also be used:

``` CSharp
foreach (var index in (99..^1).Step(-2))
{
    // loop body...
}
```

which is equivalent to the legacy for-loop below:

``` CSharp
for (int index = 99; index >= 0; index -= 2)
{
    // loop body...
}
```

## Performance Benchmark

``` PlainText
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.302
  [Host]     : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT
  DefaultJob : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT
```

|                       Method | LoopCount |          Mean |    StdDev |  Ratio | RatioSD |
|----------------------------- |----------:|--------------:|----------:|-------:|--------:|
|              `LegacyForLoop` |         1 |     0.0237 ns | 0.0096 ns |   1.00 |    0.00 |
|           `RangeForeachLoop` |         1 |     0.2197 ns | 0.0056 ns |  10.54 |    3.87 |
|    `SteppedRangeForeachLoop` |         1 |     6.5912 ns | 0.0174 ns | 317.22 |  118.54 |
| `EnumerableRangeForeachLoop` |         1 |    13.6163 ns | 0.1818 ns | 653.25 |  246.38 |
|                              |           |               |           |        |         |
|              `LegacyForLoop` |         3 |     0.4609 ns | 0.0097 ns |   1.00 |    0.00 |
|           `RangeForeachLoop` |         3 |     0.6935 ns | 0.0064 ns |   1.50 |    0.03 |
|    `SteppedRangeForeachLoop` |         3 |     6.7897 ns | 0.0347 ns |  14.74 |    0.28 |
| `EnumerableRangeForeachLoop` |         3 |    19.3017 ns | 0.1484 ns |  41.84 |    0.90 |
|                              |           |               |           |        |         |
|              `LegacyForLoop` |        10 |     2.1043 ns | 0.0124 ns |   1.00 |    0.00 |
|           `RangeForeachLoop` |        10 |     2.5624 ns | 0.0194 ns |   1.22 |    0.01 |
|    `SteppedRangeForeachLoop` |        10 |     8.2221 ns | 0.0250 ns |   3.91 |    0.02 |
| `EnumerableRangeForeachLoop` |        10 |    39.3786 ns | 0.2443 ns |  18.71 |    0.16 |
|                              |           |               |           |        |         |
|              `LegacyForLoop` |       100 |    26.4636 ns | 0.0874 ns |   1.00 |    0.00 |
|           `RangeForeachLoop` |       100 |    27.4956 ns | 0.0970 ns |   1.04 |    0.01 |
|    `SteppedRangeForeachLoop` |       100 |    31.3107 ns | 1.0000 ns |   1.20 |    0.04 |
| `EnumerableRangeForeachLoop` |       100 |   292.9096 ns | 1.0386 ns |  11.06 |    0.04 |
|                              |           |               |           |        |         |
|              `LegacyForLoop` |      1000 |   230.1425 ns | 0.2999 ns |   1.00 |    0.00 |
|           `RangeForeachLoop` |      1000 |   234.6219 ns | 0.3923 ns |   1.02 |    0.00 |
|    `SteppedRangeForeachLoop` |      1000 |   246.3007 ns | 3.6063 ns |   1.07 |    0.02 |
| `EnumerableRangeForeachLoop` |      1000 | 2,780.5681 ns | 5.4667 ns |  12.08 |    0.02 |
