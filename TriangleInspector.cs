using System.Collections.Generic;

namespace WhatKindOfTriangle
{
    public static class TriangleInspector
    {
        public static ((uint a, uint b, uint c), TriangleType) DetermineTypeOfTriangle((uint a, uint b, uint c) sides)
        {
            var (a, b, c) = sides;

            var distinctSideLengths = new HashSet<uint> { a, b, c };

            var triangleType = distinctSideLengths.Count switch
            {
                _ when distinctSideLengths.Contains(0) => TriangleType.NotTriangle, // No sides can have length 0 for ot to be a triangle
                1 => TriangleType.Equilateral,
                _ when a + b < c || b + c < a || c + a < b => TriangleType.NotTriangle, // The triangle inequality theorem - any 2 sides combined cannot be less than the 3rd side
                2 => TriangleType.Isosceles,
                _ => TriangleType.Scalene, // distinctSideLengths.Count can ONLY be 1, 2 or 3 but the C# compiler can't tell so we use a final '_' (discard) to catch the last case and appease the compiler
            };

            return (sides, triangleType);
        }
    }
}