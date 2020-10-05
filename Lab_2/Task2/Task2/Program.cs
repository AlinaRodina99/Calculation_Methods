using System;
using System.Collections.Generic;

namespace Task2
{
    class Program
    {
        private const int numberOfOption = 7;
        private static int numberOfNodes;
        private static int maxDegreeOfPolynomial;
        private static double A;
        private static double B;
        private static double pointOfInterpolation;
        private static int degreeOfPolynomial;

        private static double Function(double x)
        {
            return Math.Exp(-x) - Math.Pow(x, 2) / 2;
        }

        private static List<double> CalculateNodes()
        {
            var list = new List<double>();

            for (var i = 0; i < numberOfNodes; i++)
            {
                var x = A + i * (B - A) / numberOfNodes;
                list.Add(x);
            }

            return list;
        }

        private static List<double> SelectLeastRemoteFromInterpolationPointNodes()
        {
            var listOfNodes = CalculateNodes();
            var listOfDistancesFromInterpolationPoint = new List<(double, double)>();
            var sortedListOfNodes = new List<double>();

            foreach (var node in listOfNodes)
            {
                listOfDistancesFromInterpolationPoint.Add((Math.Abs(pointOfInterpolation - node), node));
            }

            listOfDistancesFromInterpolationPoint.Sort();

            for (var i = 0; i <= degreeOfPolynomial; i++)
            {
                sortedListOfNodes.Add(listOfDistancesFromInterpolationPoint.ToArray()[i].Item2);
            }

            return sortedListOfNodes;
        }

        private static double CalculateLForEachKNode(double kNode)
        {
            var sortedNodes = SelectLeastRemoteFromInterpolationPointNodes();
            double l_k = 1;
            double denumerator = 1;

            foreach (var node in sortedNodes)
            {
                if (kNode != node)
                {
                    l_k *= (pointOfInterpolation - node);
                }
            }

            foreach (var node in sortedNodes)
            {
                if (kNode != node)
                {
                    denumerator *= (kNode - node);
                }
            }

            return l_k / denumerator;
        }

        private static double CalculateLagrangeFormula()
        {
            var sortedNodes = SelectLeastRemoteFromInterpolationPointNodes();
            double lagrFormula = 0;

            foreach (var node in sortedNodes)
            {
                lagrFormula += CalculateLForEachKNode(node) * Function(node);
            }

            return lagrFormula;
        }

        static void Main(string[] _)
        {
            Console.WriteLine("******Task of algebraic interpolation******");
            Console.WriteLine($"Task option: {numberOfOption}");
            Console.WriteLine("Enter left border of the segment: ");
            A = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter right border of the segment: ");
            B = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter number of interpolation nodes: ");
            numberOfNodes = Convert.ToInt32(Console.ReadLine());
            maxDegreeOfPolynomial = numberOfNodes - 1;
            Console.WriteLine();

            Console.WriteLine("Table of values: ");
            var listOfNodes = CalculateNodes();

            foreach (var node in listOfNodes)
            {
                Console.WriteLine($"x = {node}; f(x) = {Function(node)}");
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter point of interpolation: ");
                pointOfInterpolation = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine($"Enter degree of polynomial that is equal or less than {maxDegreeOfPolynomial}: ");
                degreeOfPolynomial = Convert.ToInt32(Console.ReadLine());

                while (degreeOfPolynomial > maxDegreeOfPolynomial)
                {
                    Console.WriteLine("You entered degree that is bigger than max degree! Try one more time: ");
                    degreeOfPolynomial = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine();
                Console.WriteLine("List of sorted nodes: ");
                var sortedNodes = SelectLeastRemoteFromInterpolationPointNodes();

                foreach (var node in sortedNodes)
                {
                    Console.WriteLine(node);
                }

                Console.WriteLine();
                var valueOfLagrangianFormula = CalculateLagrangeFormula();
                Console.WriteLine($"Value of Lagrangian formula = {valueOfLagrangianFormula}");
                Console.WriteLine($"Absolute value of actual error: {Math.Abs(Function(pointOfInterpolation) - valueOfLagrangianFormula)}");
                Console.WriteLine("Do you want to continue working of program? Enter yes or no: ");
                var input = Convert.ToString(Console.ReadLine());

                if (input == "yes")
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("You have terminated the program. Goodbye!");
                    break;
                }
            }
        }
    }
}
