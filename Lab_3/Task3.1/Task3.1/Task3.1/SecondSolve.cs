using System;
using System.Collections.Generic;

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

        public SecondSolve(double A, double B, int numberOfNodes, double accuracy = 1.0e-9)
        {
            this.A = A;
            this.B = B;
            this.accuracy = 1.0e-9;
            this.numberOfNodes = numberOfNodes;
        }

        private static double Function(double x)
        {
            return Math.Exp(-x) - Math.Pow(x, 2) / 2;
        }

        public List<(double, double)> CalculateNodes()
        {
            var list = new List<(double, double)>();

            for (var i = 0; i < numberOfNodes; i++)
            {
                var x = A + i * (B - A) / numberOfNodes;
                list.Add((x, Function(x)));
            }

            return list;
        }

        private double CalculateLForEachKNode(double kNode, double x)
        {
            double l_k = 1;
            double denumerator = 1;
            var nodes = CalculateNodes();

            for (var i = 0; i <= DegreeOfPolynomial; ++i)
            {
                if (kNode != nodes[i].Item1)
                {
                    l_k *= x - nodes[i].Item1;
                }
            }

            for (var i = 0; i <= DegreeOfPolynomial; ++i)
            {
                if (kNode != nodes[i].Item1)
                {
                    denumerator *= kNode - nodes[i].Item1;
                }
            }

            return l_k / denumerator;
        }

        private double CalculateLagrangeFormula(double x)
        {
            double lagrFormula = 0;
            var nodes = CalculateNodes();

            for (var i = 0; i <= DegreeOfPolynomial; ++i)
            {
                lagrFormula += CalculateLForEachKNode(nodes[i].Item1, x) * Function(nodes[i].Item1);
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
