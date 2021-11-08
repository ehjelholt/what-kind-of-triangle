using System;
using System.Linq;

using static System.Console;

#nullable enable

namespace WhatKindOfTriangle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                switch (args.Length)
                {
                    case 1 when string.Equals(args[0], "exit", StringComparison.OrdinalIgnoreCase):
                        return;

                    case 1 when string.Equals(args[0], "showcase", StringComparison.OrdinalIgnoreCase):
                        args = Array.Empty<string>();

                        var triangleCases = new (uint a, uint b, uint c)[]
                        {
                            // These will form different types of triangles
                            (3, 3, 3),
                            (2, 2, 3),
                            (2, 3, 4),
                            
                            // These cannot form triangles and will test the three cases of the triangle inequality theorem
                            (2, 3, 10),
                            (2, 10, 3),
                            (10, 2, 3),

                            // These cannot form triangles, - with a side length of 0 no triangle can be formed
                            (2, 3, 0),
                            (2, 0, 3),
                            (0, 2, 3),
                            (2, 2, 0),
                            (2, 0, 2),
                            (0, 2, 2),
                            (0, 0, 0),
                        };

                        foreach (var triangleCase in triangleCases)
                            Print(TriangleInspector.DetermineTypeOfTriangle(triangleCase));

                        EndOfOutput();

                        break;

                    case 3:
                        var uintArgs = args
                            .Select<string, (bool Parsed, uint Side)>(arg => (uint.TryParse(arg, out var side), side))
                            .ToArray();

                        args = Array.Empty<string>();

                        if (uintArgs.Any(tpl => !tpl.Parsed))
                            WriteLine("One or more of the 3 arguments supplied could not be parsed into an unsigned integer");
                        else
                            Print(TriangleInspector.DetermineTypeOfTriangle((uintArgs[0].Side, uintArgs[1].Side, uintArgs[2].Side)));

                        EndOfOutput();

                        break;

                    default:
                        Write(
                            "---\n" +
                            "Usage:\n" +
                            "  showcase               - runs a predefined showcase of various triangle side combinations\n" +
                            "  exit                   - the program exits\n" +
                            "  <side> <side> <side>   - 3 positive integers denoting the three sides of a potential triangle\n\n" +
                            "> ");

                        args = ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        
                        WriteLine();
                        
                        break;
                }
            }

            static void EndOfOutput() 
            {
                WriteLine("\nPress enter to continue...");
                ReadLine();
            }
        }

        private static void Print(((uint a, uint b, uint c), TriangleType) sidesAndType)
        {
            var ((a, b, c), triangleType) = sidesAndType;

            var message = triangleType switch
            {
                TriangleType.NotTriangle => "cannot form a triangle",
                TriangleType.Equilateral => $"form an - {triangleType} - triangle",
                TriangleType.Isosceles => $"form an - {triangleType} - triangle",
                TriangleType.Scalene => $"form a - {triangleType} - triangle",

                // Due to the unfortunate nature of enums in C# and the shortcomings of the compiler and pattern matching this clause has to exist.
                // Fx a developer can execute a cast of an arbitrary integer to a TriangleType - (TriangleType)37 - which needs to be handled.
                // This unfortunately leads to the problem that in the future if a developer adds another entry in the TriangleType enum then no compile time warning is flagged :(
                _ => throw new ArgumentOutOfRangeException(nameof(triangleType))

            };

            WriteLine($"Sides of lengths {a}, {b} and {c} {message}");
        }
    }
}
