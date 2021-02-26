﻿using AngouriMath.Convenience;
using System;

namespace AngouriMath
{
    partial class MathS
    {
        /// <summary>
        /// This class is used for diagnostic and debug of the library itself.
        /// Usually, you do not want to use it in production code.
        /// </summary>
        public static class Diagnostic
        {
            /// <summary>
            /// Explicit output for ToString, that is, no signs or parentheses will be omitted. Useful
            /// for debugging and diagnostic.
            /// </summary>
            public static Setting<bool> OutputExplicit => outputExplicit ??= false;
            [ThreadStatic] private static Setting<bool>? outputExplicit;

            /// <summary>
            /// 
            /// </summary>
            public static Setting<string> CatchOnSimplify => catchOnSimplify ??= "";
            [ThreadStatic] private static Setting<string>? catchOnSimplify;

            /// <summary>
            /// Will only occur in debug mode,
            /// is used if a case defined by Diagnostic settings turns true
            /// (e. g. if you got unexpected result in simplify, change catchOnSimplify to this result
            /// and see, at which point it becomes such)
            /// </summary>
            public sealed class DiagnosticCatchException : Exception
            {
                internal DiagnosticCatchException(string message) : base(message) { }
                internal DiagnosticCatchException() : base() { }
            }
        }
    }
}
