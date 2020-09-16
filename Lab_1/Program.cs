using System;
using System.Collections.Generic;

namespace Lab_1
{
    class Program
    {
        private const string laboratoryTopic = "Approximate solution of nonlinear equations";
        private const int A = -8;
        private const int B = 2;
        private const string function = "10 * cos(x) - 0.1 * x^2";
        private const double accuracy = 1.0e-5;

        private static double FirstDerivative(double x)
        {
            return -10 * Math.Sin(x) - 0.2 * x;
        }

        private static double SecondDerivative(double x)
        {
            return -10 * Math.Cos(x) - 0.2;
        }

        private static double Function(double x)
        {
            return 10 * Math.Cos(x) - 0.1 * Math.Pow(x, 2);
        }

        private static List<(double, double)> RootSeparation()
        {
            var H = (A - B) / 1000;
            var Counter = 0;
            var X1 = A;
            var X2 = X1 + H;
            var Y1 = Function(X1);
            var listOfSegments = new List<(double, double)>();
            
            while (X2 <= B)
            {
                var Y2 = Function(X2);

                if ((Y1 * Y2) <= 0)
                {
                    Counter++;
                    listOfSegments.Add((X1, X2));
                    Console.WriteLine($"[{X1}, {X2}]");
                }

                X1 = X2;
                X2 = X1 + H;
                Y1 = Y2;
            }

            Console.WriteLine($"Number of segments of the function sign change: {Counter}");
            return listOfSegments;
        }

        private static void BisectionMethod(List<(double, double)> listOfSegments)
        {
            foreach (var segment in listOfSegments)
            {
                var A = segment.Item1;
                var B = segment.Item2;
                var N = 0;

                do
                {
                    N++;
                    var C = (A + B) / 2;

                    if (Function(A) * Function(B) <= 0)
                    {
                        B = C;
                    }

                    A = C;
                } while (B - A > 2 * accuracy);

                var X = (A + B) / 2;
                var Δ = (B - A) / 2;
                var discrepancyAbs = Math.Abs(Function(X));
                Console.WriteLine($"Initial approach to the root: {A}");
                Console.WriteLine($"Number of iterations: {N}");
                Console.WriteLine($"Approximate solution of the equation: {X}");
                Console.WriteLine($"Length of last segment: {Δ}");
                Console.WriteLine($"Absolute length of discrepancy: {discrepancyAbs}");
            }
        }

        private static void NewtonMethod(List<(double, double)> listOfSegments, int p = 1)
        {
            foreach (var segment in listOfSegments)
            {
                var N = 0;
                var X1 = segment.Item1;
                var X2 = segment.Item2;
                var startX = Function(X1) * SecondDerivative(X1) > 0 ? X1 : X2;

                if (FirstDerivative(startX) == 0)
                {
                    NewtonMethod(listOfSegments, p = +2);
                }

                var nextX = startX - p * Function(startX) / FirstDerivative(startX);

                while (Math.Abs(startX - nextX) > accuracy)
                {
                    N++;
                    startX = nextX;
                    var derivative = FirstDerivative(startX);

                    if (derivative == 0)
                    {
                        NewtonMethod(listOfSegments, p++);
                    }

                    nextX = startX - p * Function(startX) / FirstDerivative(startX);
                }

                var Δ = Math.Abs(nextX - startX);
                var discrepancy = Math.Abs(Function(nextX));
                Console.WriteLine($"Number of iterations: {N}");
                Console.WriteLine($"Initial approach to the root: {X1}");
                Console.WriteLine($"Approximate solution of the equation: {nextX}");
                Console.WriteLine($"Length of last segment: {Δ}");
                Console.WriteLine($"Absolute length of discrepancy: {discrepancy}");
            }
        }

        private static void ModifiedNewtonMethod(List<(double, double)> listOfSegments)
        {
            foreach (var segment in listOfSegments)
            {
                var N = 1;
                var X1 = segment.Item1;
                var X2 = segment.Item2;
                var X0 = Function(X1) * SecondDerivative(X1) > 0 ? X1 : X2;

                if (FirstDerivative(X0) == 0)
                {
                    X0 += accuracy;
                }

                var nextX = X0 - Function(X0) / FirstDerivative(X0);
                var startX = X0;

                while (Math.Abs(nextX - startX) > accuracy)
                {
                    N++;
                    startX = nextX;
                    nextX = startX - Function(startX) / FirstDerivative(X0);
                }

                var Δ = Math.Abs(nextX - startX);
                var discrepancy = Math.Abs(Function(nextX));
                Console.WriteLine($"Number of iterations: {N}");
                Console.WriteLine($"Initial approach to the root: {X1}");
                Console.WriteLine($"Approximate solution of the equation: {nextX}");
                Console.WriteLine($"Length of the last segment: {Δ}");
                Console.WriteLine($"Absolute length of discrepancy: {discrepancy}");
            }
        }

        private static void SecantMethod(List<(double, double)> listOfSegments)
        {
            foreach (var segment in listOfSegments)
            {
                var X1 = segment.Item1;
                var X2 = segment.Item2;
                var startX = X1;
                var nextX = startX - Function(startX) / ()
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"******Topic of laboratory work: {laboratoryTopic}******");
            Console.WriteLine("Task parameters:");
            Console.WriteLine($"- Function segment: [A, B] = [{A}; {B}]");
            Console.WriteLine($"- Function: f(x) = {function}");
            Console.WriteLine("- Given accuracy: ε = 10^-5");


        }
    }
}
