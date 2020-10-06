﻿using AngouriMath;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static AngouriMath.Entity;
using static AngouriMath.Entity.Set;
using static AngouriMath.MathS;
using static AngouriMath.MathS.Sets;

namespace UnitTests.Core.Sets
{
    public class IntervalAndInterval
    {
        private readonly Set A = Interval(2, true, 5, true);
        private readonly Set B = Interval(2, true, 3, true);
        private readonly Set C = Interval(5, true, 7, true);
        private readonly Set C1 = Interval(5, false, 7, true);
        private readonly Set D = Interval(2, true, 3, false);
        private readonly Set E = Interval(3, false, 4, false);
        private readonly Set F = Interval(3, true, 6, true);
        private readonly Set F1 = Interval(3, false, 6, false);

        private void TestInt(Entity act, Interval expected)
        {
            var inter = Assert.IsType<Interval>(act.InnerSimplified);
            Assert.Equal(expected, inter);
        }

        private void TestArb(Entity actual, Entity expected)
        {
            Assert.Equal(expected, actual);
        }

        private void CannotSimplify(Entity act)
            => Assert.Equal(act, act.InnerSimplified);

        [Fact] public void Union1() => TestInt(A.Unite(B), Interval(2, true, 5, true));
        [Fact] public void Union2() => TestInt(B.Unite(A), Interval(2, true, 5, true));
        [Fact] public void Union3() => TestInt(A.Unite(C1), Interval(2, true, 7, true));
        [Fact] public void Union4() => TestInt(C1.Unite(A), Interval(2, true, 7, true));
        [Fact] public void Union5() => CannotSimplify(E.Unite(D));
        [Fact] public void Union6() => CannotSimplify(D.Unite(E));
        [Fact] public void Union7() => CannotSimplify(A.Unite(A));
        [Fact] public void Union8() => CannotSimplify(B.Unite(B));
        [Fact] public void Union9() => CannotSimplify(C.Unite(C));
        [Fact] public void Union10() => CannotSimplify(D.Unite(D));

        [Fact] public void Intersection1() => TestInt(A.Intersect(B), Interval(2, true, 3, true));
        [Fact] public void Intersection2() => TestInt(B.Intersect(A), Interval(2, true, 3, true));
        [Fact] public void Intersection3() => TestArb(A.Intersect(C), new FiniteSet(5));
        [Fact] public void Intersection4() => TestArb(C.Intersect(A), new FiniteSet(5));
        [Fact] public void Intersection5() => CannotSimplify(A.Intersect(A));
        [Fact] public void Intersection6() => CannotSimplify(B.Intersect(B));
        [Fact] public void Intersection7() => TestArb(E.Intersect(D), Set.Empty);
        [Fact] public void Intersection8() => TestArb(D.Intersect(E), Set.Empty);
        [Fact] public void Intersection9() => TestArb(E.Intersect(B), Set.Empty);
        [Fact] public void Intersection10() => TestArb(B.Intersect(E), Set.Empty);
        [Fact] public void Intersection11() => TestInt(A.Intersect(F), Interval(3, 5));
        [Fact] public void Intersection12() => TestInt(F.Intersect(A), Interval(3, 5));
        [Fact] public void Intersection13() => TestInt(A.Intersect(F1), Interval(3, false, 5, true));
        [Fact] public void Intersection14() => TestInt(F1.Intersect(A), Interval(3, false, 5, true));

        [Fact] public void Subtraction1() => TestInt(A.SetSubtract(B), Interval(3, false, 5, true));
        [Fact] public void Subtraction2() => TestArb(B.SetSubtract(A), Set.Empty);
        [Fact] public void Subtraction3() => CannotSimplify(E.SetSubtract(D));
        [Fact] public void Subtraction4() => CannotSimplify(D.SetSubtract(E));
        [Fact] public void Subtraction5() => TestInt(A.SetSubtract(F), Interval(2, true, 3, false));
        [Fact] public void Subtraction6() => TestInt(F.SetSubtract(A), Interval(5, false, 6, true));
    }
}
