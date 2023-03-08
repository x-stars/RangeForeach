# C# Range-Foreach Syntax Extensions

Provides the range-foreach syntax for **C# 9.0 or higher**.

Supported frameworks:

* .NET Framework >= 2.0
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

Use the `Step` method to write foreach-loops like this:

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

## Dependency Type Polyfill

This syntax requires the `System.Range` type (and also the `System.Index` type).
Considering that early frameworks do not provide this type, this project includes the polyfill source.

If a third party package that includes the `System.Range` type (such as `IndexRange`, etc.) is referenced,
define the `INDEX_RANGE` constant in the project file to avoid duplicate definitions:

``` XML
<DefineConstants>$(DefineConstants);INDEX_RANGE</DefineConstants>
```

## Performance Benchmark

``` PlainText
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22621
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT
  DefaultJob : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT
```

|                       Method | LoopCount |          Mean |     StdDev | Ratio | RatioSD |
|----------------------------- |----------:|--------------:|-----------:|------:|--------:|
|              `LegacyForLoop` |         1 |     0.1038 ns |  0.0880 ns |     ? |       ? |
|           `RangeForeachLoop` |         1 |     0.2240 ns |  0.0930 ns |     ? |       ? |
|    `SteppedRangeForeachLoop` |         1 |     7.1447 ns |  0.1860 ns |     ? |       ? |
| `EnumerableRangeForeachLoop` |         1 |    17.9865 ns |  0.7955 ns |     ? |       ? |
|                              |           |               |            |       |         |
|              `LegacyForLoop` |         3 |     0.7047 ns |  0.1365 ns |  1.00 |    0.00 |
|           `RangeForeachLoop` |         3 |     0.9819 ns |  0.1322 ns |  1.45 |    0.35 |
|    `SteppedRangeForeachLoop` |         3 |     7.7502 ns |  0.1300 ns | 12.16 |    1.89 |
| `EnumerableRangeForeachLoop` |         3 |    26.5011 ns |  0.4834 ns | 41.49 |    5.99 |
|                              |           |               |            |       |         |
|              `LegacyForLoop` |        10 |     2.6473 ns |  0.1568 ns |  1.00 |    0.00 |
|           `RangeForeachLoop` |        10 |     3.2285 ns |  0.1636 ns |  1.22 |    0.09 |
|    `SteppedRangeForeachLoop` |        10 |     9.7268 ns |  0.3551 ns |  3.66 |    0.27 |
| `EnumerableRangeForeachLoop` |        10 |    53.3254 ns |  2.4620 ns | 20.16 |    1.38 |
|                              |           |               |            |       |         |
|              `LegacyForLoop` |       100 |    29.1309 ns |  0.8345 ns |  1.00 |    0.00 |
|           `RangeForeachLoop` |       100 |    31.0612 ns |  0.6702 ns |  1.07 |    0.04 |
|    `SteppedRangeForeachLoop` |       100 |    33.9128 ns |  0.5381 ns |  1.17 |    0.04 |
| `EnumerableRangeForeachLoop` |       100 |   350.0842 ns |  6.8264 ns | 12.07 |    0.40 |
|                              |           |               |            |       |         |
|              `LegacyForLoop` |      1000 |   262.0913 ns |  7.5677 ns |  1.00 |    0.00 |
|           `RangeForeachLoop` |      1000 |   265.2877 ns |  5.8342 ns |  1.01 |    0.03 |
|    `SteppedRangeForeachLoop` |      1000 |   275.1638 ns |  6.9428 ns |  1.05 |    0.04 |
| `EnumerableRangeForeachLoop` |      1000 | 3,577.8530 ns | 31.8177 ns | 13.76 |    0.38 |
