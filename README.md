# C# Range-Foreach Syntax Extensions

Provides the range-foreach syntax for **C# 9.0 or higher**.

Supported frameworks:

* .NET Framework >= 4.0
* .NET Core >= 1.0
* .NET Standard >= 1.0

## Range-Foreach Syntax

Reference source files or the NuGet package to write foreach-loops like this:

``` C#
foreach (var index in 0..100)
{
    // loop body...
}
```

which is equivalent to the legacy for-loop below:

``` C#
for (int index = 0; index < 100; index++)
{
    // loop body...
}
```

**NOTE:** Use `^` to represent negative numbers, e.g. `^100..0` (instead of `-100..0`).

### Stepped Syntax

If `STEPPED_RANGE` is defined, this syntax can also be used:

``` C#
foreach (var index in (99..^1).Step(-2))
{
    // loop body...
}
```

Which is equivalent to the legacy for-loop below:

``` C#
for (int index = 99; index >= 0; index -= 2)
{
    // loop body...
}
```
