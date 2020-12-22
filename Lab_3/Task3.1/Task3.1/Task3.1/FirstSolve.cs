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

        private double Function(double x)
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

        public List<(double, double)> SelectLeastRemoteFromInterpolationPointNodes()
        {
            var listOfNodes = SwapTable().ToList();
            var listOfDistancesFromInterpolationPoint = new List<(double, double, double)>();
            var sortedListOfNodes = new List<(double, double)>();

            foreach (var node in listOfNodes)
            {
                listOfDistancesFromInterpolationPoint.Add((Math.Abs(F - node.Item1), node.Item1, node.Item2));
            }

            listOfDistancesFromInterpolationPoint.Sort();

            for (var i = 0; i <= DegreeOfPolynomial; i++)
            {
                sortedListOfNodes.Add((listOfDistancesFromInterpolationPoint.ToArray()[i].Item2, listOfDistancesFromInterpolationPoint.ToArray()[i].Item3));
            }

            return sortedListOfNodes;
        }

        public double CalculateNewtonFormula()
        {
            var sortedNodes = SelectLeastRemoteFromInterpolationPointNodes();
            var valuesOfNodes = new List<double>();

            foreach (var node in sortedNodes)
            {
                valuesOfNodes.Add(node.Item2);
            }

            var dividedDifferencies = new List<List<double>>();
            var nodes = sortedNodes.Select(el => el.Item1).ToList();

            for (var i = 1; i <= sortedNodes.Count; ++i)
            {
                var differencies_i = new List<double>();

                for (var j = 0; j < sortedNodes.Count - i; ++j)
                {
                    if (i == 1)
                    {
                        differencies_i.Add((valuesOfNodes[j + 1] - valuesOfNodes[j]) / (nodes[j + 1] - nodes[j]));
                    }
                    else
                    {
                        differencies_i.Add((dividedDifferencies[i - 2][j + 1] - dividedDifferencies[i - 2][j]) / (nodes[j + i] - nodes[j]));
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
                    summand *= F - nodes[j];
                }

                summand *= dividedDifferencies[i - 1][0];
                newtonFormula += summand;
            }

            return newtonFormula;
        }
    }
}
