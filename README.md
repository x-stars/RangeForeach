# C# Range-Foreach Syntax Extensions

Provides the range-foreach syntax for **C# 9.0 or higher**.

Supported frameworks:

* .NET Framework >= 3.5
* .NET Core >= 1.0
* .NET Standard >= 1.0

## Range-Foreach Syntax

Reference source files or the NuGet package to write foreach loops like this:

``` CSharp
foreach (var index in 0..100)
{
    // loop body...
}
```

which is equivalent to the legacy for loop below:

``` CSharp
for (int index = 0; index < 100; index++)
{
    // loop body...
}
```

**TIPS:** `0` can be omitted in range expressions, e.g. `..100` (equivalent to `0..100`).

**NOTE:** Use `^` to represent negative numbers, e.g. `^100..0` (instead of `-100..0`).

### Stepped Syntax

Use the `Step` method to write foreach loops like this:

``` CSharp
foreach (var index in (99..^1).Step(-2))
{
    // loop body...
}
```

which is equivalent to the legacy for loop below:

``` CSharp
for (int index = 99; index > -1; index -= 2)
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
.NET SDK=7.0.201
  [Host]     : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT
  DefaultJob : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT
```

|                   Method | LoopCount |          Mean |     StdDev | Ratio | Code Size |
|------------------------- |----------:|--------------:|-----------:|------:|----------:|
|             `CounterFor` |         1 |     0.0447 ns |  0.0505 ns |     ? |      20 B |
|           `RangeForeach` |         1 |     0.5299 ns |  0.0747 ns |     ? |      51 B |
|    `SteppedRangeForeach` |         1 |     6.3169 ns |  0.0534 ns |     ? |     163 B |
| `EnumerableRangeForeach` |         1 |    14.2896 ns |  0.5954 ns |     ? |     337 B |
|                          |           |               |            |       |           |
|             `CounterFor` |         3 |     0.4906 ns |  0.0216 ns |  1.00 |      20 B |
|           `RangeForeach` |         3 |     0.7141 ns |  0.0180 ns |  1.46 |      51 B |
|    `SteppedRangeForeach` |         3 |     6.5815 ns |  0.0166 ns | 13.44 |     163 B |
| `EnumerableRangeForeach` |         3 |    18.9336 ns |  0.1653 ns | 38.64 |     337 B |
|                          |           |               |            |       |           |
|             `CounterFor` |        10 |     2.1532 ns |  0.0203 ns |  1.00 |      20 B |
|           `RangeForeach` |        10 |     2.3766 ns |  0.0143 ns |  1.10 |      51 B |
|    `SteppedRangeForeach` |        10 |     9.1535 ns |  0.2358 ns |  4.24 |     163 B |
| `EnumerableRangeForeach` |        10 |    42.6260 ns |  1.1307 ns | 19.76 |     337 B |
|                          |           |               |            |       |           |
|             `CounterFor` |       100 |    27.2106 ns |  0.3413 ns |  1.00 |      20 B |
|           `RangeForeach` |       100 |    28.1094 ns |  0.3127 ns |  1.03 |      51 B |
|    `SteppedRangeForeach` |       100 |    30.6008 ns |  0.6084 ns |  1.13 |     163 B |
| `EnumerableRangeForeach` |       100 |   319.5471 ns |  3.6090 ns | 11.75 |     337 B |
|                          |           |               |            |       |           |
|             `CounterFor` |      1000 |   235.5779 ns |  2.0112 ns |  1.00 |      20 B |
|           `RangeForeach` |      1000 |   238.2080 ns |  0.7198 ns |  1.01 |      51 B |
|    `SteppedRangeForeach` |      1000 |   244.0087 ns |  1.4962 ns |  1.04 |     163 B |
| `EnumerableRangeForeach` |      1000 | 3,029.0070 ns | 17.3052 ns | 12.88 |     337 B |
