/*
 * This file was auto-generated by TestGenerator
 * Do not modify it; modify TestGenerator.java and rerun it instead.
 */

/*
 * It's super important to test all following cases because they test replacements for Trigonometric functions
 * so if one is wrong your result might be wrong at all
 */


using AngouriMath;
using static AngouriMath.Entity.Number;
using System;
using System.Linq;
using Xunit;
using AngouriMath.Core.Exceptions;

namespace UnitTests.PatternsTest
{
    public sealed class TestTrigTableConsts
    {
        // TODO: Remove when we implement extra precision for rounding
        internal static void AssertEqualWithoutLast3Digits(Real expected, Real actual) =>
            Assert.Equal(expected.EDecimal.RoundToExponent(expected.EDecimal.Ulp().Exponent + 3),
                         actual.EDecimal.RoundToExponent(expected.EDecimal.Ulp().Exponent + 3));

        // For MemberData to show up as individual test cases, all arguments must be serializable:
        // https://github.com/xunit/xunit/issues/1473#issuecomment-333226539
        public static readonly System.Collections.Generic.IEnumerable<object[]> TrigTestData =
            new[] { nameof(MathS.Sin), nameof(MathS.Cos), nameof(MathS.Tan), nameof(MathS.Cotan) }
            .SelectMany(_ => Enumerable.Range(1, 29), (func, i) => new object[] { func, i });

        [Theory]
        [MemberData(nameof(TrigTestData))]
        public void TrigTest(string trigFunc, int twoPiOver)
        {
            var toSimplify =
                (Entity?)typeof(MathS).GetMethod(trigFunc)?.Invoke(null, new object[] { 2 * MathS.pi / twoPiOver })
                ?? throw new Exception($"{trigFunc} not found.");
            var expected = Assert.IsAssignableFrom<Real>(toSimplify.EvalNumerical());
            var actual = Assert.IsAssignableFrom<Real>(toSimplify.InnerSimplified.EvalNumerical());
            AssertEqualWithoutLast3Digits(expected, actual);
        }
    }
}
