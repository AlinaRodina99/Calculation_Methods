using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3._1
{
    public class FirstSolve
    {
        private readonly double A;
        private readonly double B;
        private readonly int numberOfNodes;

        public int DegreeOfPolynomial { get; set; }

        public double F { get; set; }

        public FirstSolve(double A, double B, int numberOfNodes)
        {
            this.A = A;
            this.B = B;
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

        public List<(double, double)> SwapTable()
        {
            var table = CalculateNodes();

            for (var i = 0; i < table.Count; ++i)
            {
                table[i] = (table[i].Item2, table[i].Item1);
            }

            return table;
        }

        private List<double> SelectLeastRemoteFromInterpolationPointNodes()
        {
            var listOfNodes = SwapTable().Select(el => el.Item1).ToList();
            var listOfDistancesFromInterpolationPoint = new List<(double, double)>();
            var sortedListOfNodes = new List<double>();

            foreach (var node in listOfNodes)
            {
                listOfDistancesFromInterpolationPoint.Add((Math.Abs(F - node), node));
            }

            listOfDistancesFromInterpolationPoint.Sort();

            for (var i = 0; i <= DegreeOfPolynomial; i++)
            {
                sortedListOfNodes.Add(listOfDistancesFromInterpolationPoint.ToArray()[i].Item2);
            }

            return sortedListOfNodes;
        }

        public double CalculateNewtonFormula()
        {
            var sortedNodes = SelectLeastRemoteFromInterpolationPointNodes();
            var valuesOfNodes = new List<double>();

            foreach (var node in sortedNodes)
            {
                valuesOfNodes.Add(Function(node));
            }

            var dividedDifferencies = new List<List<double>>();

            for (var i = 1; i <= sortedNodes.Count; ++i)
            {
                var differencies_i = new List<double>();

                for (var j = 0; j < sortedNodes.Count - i; ++j)
                {
                    if (i == 1)
                    {
                        differencies_i.Add((valuesOfNodes[j + 1] - valuesOfNodes[j]) / (sortedNodes[j + 1] - sortedNodes[j]));
                    }
                    else
                    {
                        differencies_i.Add((dividedDifferencies[i - 2][j + 1] - dividedDifferencies[i - 2][j]) / (sortedNodes[j + i] - sortedNodes[j]));
                    }
                }

                if (differencies_i.Count != 0)
                {
                    dividedDifferencies.Add(differencies_i);
                }
            }

            double newtonFormula = valuesOfNodes[0];

            for (var i = 1; i <= dividedDifferencies.Count; ++i)
            {
                double summand = 1;
                for (var j = 0; j < i; ++j)
                {
                    summand *= F - sortedNodes[j];
                }

                summand *= dividedDifferencies[i - 1][0];
                newtonFormula += summand;
            }

            return newtonFormula;
        }
    }
}
