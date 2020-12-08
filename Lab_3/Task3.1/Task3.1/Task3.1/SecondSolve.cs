using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3._1
{
    public class SecondSolve
    {
        private readonly double A;
        private readonly double B;
        private readonly double accuracy;
        private readonly int numberOfNodes;

        public int DegreeOfPolynomial { get; set; }

        public double F { get; set; }

        public SecondSolve(double A, double B, int numberOfNodes, double accuracy)
        {
            this.A = A;
            this.B = B;
            this.accuracy = accuracy;
            this.numberOfNodes = numberOfNodes;
        }

        private static double Function(double x)
        {
            return Math.Exp(-x) - Math.Pow(x, 2) / 2;
        }

        private List<double> CalculateNodes()
        {
            var list = new List<double>();

            for (var i = 0; i < numberOfNodes; i++)
            {
                var x = A + i * (B - A) / numberOfNodes;
                list.Add(x);
            }

            return list;
        }

        private double CalculateLForEachKNode(double kNode, double x)
        {
            double l_k = 1;
            double denumerator = 1;
            var nodes = CalculateNodes();

            foreach (var node in nodes)
            {
                if (kNode != node)
                {
                    l_k *= (x - node);
                }
            }

            foreach (var node in nodes)
            {
                if (kNode != node)
                {
                    denumerator *= (kNode - node);
                }
            }

            return l_k / denumerator;
        }

        private double CalculateLagrangeFormula(double x)
        {
            double lagrFormula = 0;
            var nodes = CalculateNodes();

            foreach (var node in nodes)
            {
                lagrFormula += CalculateLForEachKNode(node, x) * Function(node);
            }

            return lagrFormula - F;
        }

        public List<(double, double)> RootSeparation()
        {
            var H = (B - A) / 1000;
            var Counter = 0;
            var X1 = A;
            var X2 = X1 + H;
            var Y1 = CalculateLagrangeFormula(X1);
            var listOfSegments = new List<(double, double)>();

            while (X2 <= B)
            {
                var Y2 = CalculateLagrangeFormula(X2);

                if ((Y1 * Y2) <= 0)
                {
                    Counter++;
                    listOfSegments.Add((X1, X2));
                }

                X1 = X2;
                X2 = X1 + H;
                Y1 = Y2;
            }

            return listOfSegments;
        }

        public List<double> BisectionMethod(List<(double, double)> listOfSegments)
        {
            var results = new List<double>();

            foreach (var segment in listOfSegments)
            {
                var A = segment.Item1;
                var B = segment.Item2;
                var N = 0;

                while (B - A > 2 * accuracy)
                {
                    N++;
                    var C = (A + B) / 2;

                    if (CalculateLagrangeFormula(A) * CalculateLagrangeFormula(C) <= 0)
                    {
                        B = C;
                    }
                    else
                    {
                        A = C;
                    }
                } 

                var X = (A + B) / 2;
                results.Add(X);
            }

            return results;
        }
    }
}
