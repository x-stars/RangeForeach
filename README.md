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
BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2792/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
```

|                   Method | LoopCount | Mean        | StdDev    | Ratio | Code Size |
|------------------------- |----------:|------------:|----------:|------:|----------:|
|             `CounterFor` |         1 |   0.0037 ns | 0.0032 ns |     ? |      20 B |
|           `RangeForeach` |         1 |   0.0026 ns | 0.0010 ns |     ? |      39 B |
|    `SteppedRangeForeach` |         1 |   0.0051 ns | 0.0022 ns |     ? |      39 B |
| `EnumerableRangeForeach` |         1 |   7.2937 ns | 0.1077 ns |     ? |     593 B |
|                          |           |             |           |       |           |
|             `CounterFor` |         3 |   0.4622 ns | 0.0033 ns |  1.00 |      20 B |
|           `RangeForeach` |         3 |   0.4623 ns | 0.0011 ns |  1.00 |      39 B |
|    `SteppedRangeForeach` |         3 |   0.4639 ns | 0.0034 ns |  1.00 |      39 B |
| `EnumerableRangeForeach` |         3 |   9.4142 ns | 0.1430 ns | 20.37 |     644 B |
|                          |           |             |           |       |           |
|             `CounterFor` |        10 |   2.1077 ns | 0.0098 ns |  1.00 |      20 B |
|           `RangeForeach` |        10 |   2.1039 ns | 0.0019 ns |  1.00 |      39 B |
|    `SteppedRangeForeach` |        10 |   2.1064 ns | 0.0055 ns |  1.00 |      39 B |
| `EnumerableRangeForeach` |        10 |  14.0306 ns | 0.0623 ns |  6.66 |     644 B |
|                          |           |             |           |       |           |
|             `CounterFor` |       100 |  26.3959 ns | 0.0289 ns |  1.00 |      20 B |
|           `RangeForeach` |       100 |  27.8959 ns | 0.0137 ns |  1.06 |      39 B |
|    `SteppedRangeForeach` |       100 |  27.9739 ns | 0.0267 ns |  1.06 |      39 B |
| `EnumerableRangeForeach` |       100 |  64.2927 ns | 0.4154 ns |  2.44 |     644 B |
|                          |           |             |           |       |           |
|             `CounterFor` |      1000 | 230.8943 ns | 0.1496 ns |  1.00 |      20 B |
|           `RangeForeach` |      1000 | 235.6411 ns | 0.1965 ns |  1.02 |      39 B |
|    `SteppedRangeForeach` |      1000 | 235.9903 ns | 0.3364 ns |  1.02 |      39 B |
| `EnumerableRangeForeach` |      1000 | 513.7996 ns | 1.7926 ns |  2.23 |     644 B |
