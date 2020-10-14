<p align="center">
  <a href="https://github.com/asc-community/AngouriMath">
    <img src="./additional/readme/icon_white.png" alt="AngouriMath logo" width="200" height="200">
  </a>
</p>

<h2 align="center">AngouriMath</h2>

<p align="center">
  <b>New, leading symbolic algebra library in .NET. Everything one would need.</b>
  <br>
  <a href="https://www.nuget.org/packages/AngouriMath" title="Go to NuGet"><b>Download</b></a>
  <b>�</b>
  <a href="#exam"><b>Examples</b></a>
  <b>�</b>
  <a href="#contrib"><b>Contributions</b></a>
  <b>�</b>
  <a href="#license"><b>License</b></a>
  <br>
  <br>
  <br>
  <img src="https://github.com/asc-community/AngouriMath/workflows/Test/badge.svg"/>
<a href="https://www.nuget.org/packages/AngouriMath"><img src="https://img.shields.io/nuget/vpre/AngouriMath?color=blue&label=NuGet"/></a>
<a href="https://discord.gg/YWJEX7a"><img src="https://img.shields.io/discord/642350046213439489?color=orange&label=Discord"/></a>
</p>

## What is it about?

AngouriMath is an open source symbolic algebra library. That is, via AngouriMath,
you can automatically solve equations, systems of equations, work with sets, differentiate,
parse from string, and try many other features.

It is not a CAS, so you can use it in any your project by installing it from 
<a href="https://www.nuget.org/packages/AngouriMath">NuGet</a>. AngouriMath
can be used in calculators, algebra systems, educational/quiz apps, graphics,
TeX rendering applications, etc.

It is free to use even in commercial projects. We work on it a lot, so your requests on 
<a href="https://github.com/asc-community/AngouriMath/issues">issues</a> are likely to
be considered within a few hours.

Finally, if not sure about it, try it on 
<a href="https://dotnetfiddle.net/FIcaDG">.NET Fiddle</a>!

## README navigation:
- [Installation](#inst)
- [Examples](#exam)
  - [Computations](#computations)
  - [Algebra](#algebra)
  - [Calculus](#calculus)
  - [Sets](#sets)
  - [Neat syntax](#syntax)
- [I want to contribute](#contrib)

If you are new to AM, we suggest you checking out some samples instead of reading boring 
documentation. If you want to contribute, we would be happy to welcome you in our
community.

For any questions, feel free to contact us via <a href="https://discord.gg/YWJEX7a">Discord</a>.

## Computations

Use as a simple calculator:
```cs
Entity expr = "1 + 2 * log(3, 9)";
Console.WriteLine(expr.EvalNumerical());
```
<img src="https://render.githubusercontent.com/render/math?math=5">

```cs
Console.WriteLine("2 / 3 + sqrt(-16)".EvalNumerical());
>>> 2 / 3 + 4i
```
<img src="https://render.githubusercontent.com/render/math?math=\frac{2}{3} %2B 4i">

```cs
Console.WriteLine("(-2) ^ 3".EvalNumerical());
```
<img src="https://render.githubusercontent.com/render/math?math=-8">

Build expressions with variables and substitute them:
```cs
Entity expr = "2x + sin(x) / sin(2 ^ x)";
var subs = expr.Substitute("x", 0.3m);
Console.WriteLine(subs);
```
<img src="https://render.githubusercontent.com/render/math?math=2\times \frac{3}{10}%2B\frac{\sin\left(\frac{3}{10}\right)}{\sin\left(\sqrt[10]{2}^{3}\right)}">

Simplify complicated expressions:
```cs
Console.WriteLine("2x + x + 3 + (4 a * a^6) / a^3 / 5".Simplify());
```
<img src="https://render.githubusercontent.com/render/math?math=3%2B\frac{4}{5}\times {a}^{4}%2B3\times x">

```cs
var expr = "1/2 + sin(pi / 4) + (sin(3x)2 + cos(3x)2)";
Console.WriteLine(expr.Simplify());
```
<img src="https://render.githubusercontent.com/render/math?math=\frac{1}{2}\times \left(1%2B\sqrt{2}\right)%2B1">

Compiled functions work 15x+ faster
```cs
var x = MathS.Variable("x");
var expr = MathS.Sin(x) + MathS.Sqrt(x) / (MathS.Sqrt(x) + MathS.Cos(x)) + MathS.Pow(x, 3);
var func = expr.Compile(x);
Console.WriteLine(func.Substitute(3));
```

```cs
var expr = "sin(x) + sqrt(x) / (sqrt(x) + cos(x)) + x3";
var compiled = expr.Compile("x");
Console.WriteLine(compiled.Substitute(4));
```

## Algebra

Start with boolean algebra:
```cs
// Those are equal
Entity expr1 = "a and b or c";
Entity expr2 = "a & b | c";

// as well as those
Entity expr3 = "a -> b";
Entity expr3 = "a implies b";
```

```cs
Entity expr = "a -> true";
Console.WriteLine(MathS.SolveBooleanTable(expr, "a"));
```

```
>>> Matrix[2 x 1]
>>> False
>>> True
```

Next, solve some equations:
```cs
Console.WriteLine("x2 + x + a".SolveEquation("x"));
```
<img src="https://render.githubusercontent.com/render/math?math=\left\{\frac{-1-\sqrt{1-4\times a}}{2},\frac{-1%2B\sqrt{1-4\times a}}{2}\right\}">

Under developing now and forever (always available)
```cs
Entity expr = "(sin(x)2 - sin(x) + a)(b - x)((-3) * x + 2 + 3 * x ^ 2 + (x + (-3)) * x ^ 3)";
Console.WriteLine(expr.SolveEquation("x").Latexise());
```
<img src="https://render.githubusercontent.com/render/math?math=\left\{-\left(-\arcsin\left(\frac{1-\sqrt{1-4\times a}}{2}\right)-2\times \pi\times n_{1}\right),-\left(-\pi--\arcsin\left(\frac{1-\sqrt{1-4\times a}}{2}\right)-2\times \pi\times n_{1}\right),-\left(-\arcsin\left(\frac{1%2B\sqrt{1-4\times a}}{2}\right)-2\times \pi\times n_{1}\right),-\left(-\pi--\arcsin\left(\frac{1%2B\sqrt{1-4\times a}}{2}\right)-2\times \pi\times n_{1}\right),\frac{-b}{-1},-i,i,1,2\right\}">

Try some inequalities:
```cs
Console.WriteLine("(x - 6)(x + 9) >= 0".Solve("x"));
```
<img src="https://render.githubusercontent.com/render/math?math=\left\{-9,6\right\}\cup\left(-\infty%3B-9\right)\cup\left(6%3B\infty\right)">

Systems of equations:
```cs
var system = MathS.Equations(
    "x2 + y + a",
    "y - 0.1x + b"
);
Console.WriteLine(system);
var solutions = system.Solve("x", "y");
Console.WriteLine(solutions);
```
System:

<img src="https://render.githubusercontent.com/render/math?math=\begin{cases}{x}^{2}%2By%2Ba = 0\\y-\frac{1}{10}\times x%2Bb = 0\\\end{cases}">

Result:

<img src="additional/readme/pic1.PNG">

```cs
var system = MathS.Equations(
    "cos(x2 + 1)^2 + 3y",
    "y * (-1) + 4cos(x2 + 1)"
);
Console.WriteLine(system.Latexise());
var solutions = system.Solve("x", "y");
Console.WriteLine(solutions);
```
<img src="https://render.githubusercontent.com/render/math?math=\begin{cases}{\cos\left({x}^{2}%2B1\right)}^{2}%2B3\times y = 0\\y\times -1%2B4\times \cos\left({x}^{2}%2B1\right) = 0\\\end{cases}">
(solution matrix is too complicated to show)

## Calculus

Find derivatives:
```cs
var func = "x2 + ln(cos(x) + 3) + 4x";
var derivative = func.Derive("x");
Console.WriteLine(derivative.Simplify());
```
<img src="https://render.githubusercontent.com/render/math?math=4%2B\frac{\sin\left(x\right)}{{\ln\left(\cos\left(x\right)%2B3\right)}^{2}\times \left(\cos\left(x\right)%2B3\right)}%2B2\times x">

Find limits:
```cs
WriteLine("(a x2 + b x) / (e x - h x2 - 3)".Limit("x", "+oo").InnerSimplified);
```
<img src="https://render.githubusercontent.com/render/math?math=\frac{a}{-h}">

Find integrals:
```cs
WriteLine("x2 + a x".Integrate("x").InnerSimplified);
```
<img src="https://render.githubusercontent.com/render/math?math=\frac{{x}^{3}}{3}%2Ba\times \frac{{x}^{2}}{2}">

## Sets

There are four types of sets:
```cs
WriteLine("{ 1, 2 }".Latexise());
WriteLine("[3; +oo)".Latexise());
WriteLine("RR".Latexise());
WriteLine("{ x : x8 + a x < 0 }".Latexise());
```

<img src="https://render.githubusercontent.com/render/math?math=\left\{ 1, 2 \right\}">
<img src="https://render.githubusercontent.com/render/math?math=\left[3%3B \infty \right)">
<img src="https://render.githubusercontent.com/render/math?math=\mathbb{R}">
<img src="https://render.githubusercontent.com/render/math?math=\left\{ x %3A {x}^{8}%2B a\times x < 0 \right\}">

And there operators:
```cs
WriteLine(@"A \/ B".Latexise());
WriteLine(@"A /\ B".Latexise());
WriteLine(@"A \ B".Latexise());
```

<img src="https://render.githubusercontent.com/render/math?math=A\cup B">
<img src="https://render.githubusercontent.com/render/math?math=A\cap B">
<img src="https://render.githubusercontent.com/render/math?math=A\setminus B">

## Syntax

You can build LaTeX with AngouriMath:
```cs
var expr = "x ^ y + sqrt(x) + integral(sqrt(x) / a, x, 1) + derive(sqrt(x) / a, x, 1) + limit(sqrt(x) / a, x, +oo)";
Console.WriteLine(expr.Latexise());
>>> {x}^{y}+\sqrt{x}+\int \left[\frac{\sqrt{x}}{a}\right] dx+\frac{d\left[\frac{\sqrt{x}}{a}\right]}{dx}+\lim_{x\to \infty } \left[\frac{\sqrt{x}}{a}\right]
```
<img src="https://render.githubusercontent.com/render/math?math={x}^{y}%2B\sqrt{x}%2B\int\left[\frac{\sqrt{x}}{a}\right]dx%2B\frac{d\left[\frac{\sqrt{x}}{a}\right]}{dx}%2B\lim_{x\to\infty}\left[\frac{\sqrt{x}}{a}\right]">

You can parse `Entity` from string with
```cs
var expr = MathS.FromString("x + 2 + sqrt(x)");
Entity expr = "x + 2 + sqrt(x)";
```

A few convenient features: `x2` => `x^2`, `a x` => `a * x`, `(...)2` => `(...)^2`, `2(...)` => `2 * (...)`


## <a name="contrib"></a>Contribution

We appreciate and welcome any contributors to AngouriMath. Current tasks can be tracked
on <a href="https://github.com/asc-community/AngouriMath/projects">this page</a>.

Use pull requests to contribute to it. We also appreciate early pull requests so that we know what you are improving and
can help you with something.

Documentation for contributors and developers is <a href="./AngouriMath/Docs/Contributing/README.md">here</a>.

## License

<a href="https://github.com/asc-community/AngouriMath/blob/master/LICENSE.md"><img src="https://img.shields.io/github/license/AngouriSoft/MathS?color=purple"/></a>

The project is open source, but can be used in closed commercial projects. There is no restriction on it
with the only requirement to keep the MIT license with all distributives of AngouriMath.