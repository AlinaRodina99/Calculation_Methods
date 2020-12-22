using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class DifferentialEquations
    {
        private readonly double h;
        private readonly int m;
        private readonly double a;

        public DifferentialEquations(double h, int m, double a)
        {
            this.h = h;
            this.m = m;
            this.a = a;
        }

        private double Function(double x) =>
            Math.Exp(1.5 * 3 * x);
        

        private double FirstDerivative(double x) =>
            1.5 * 3 * Math.Exp(1.5 * 3 * x);
        

        private double SecondDerivative(double x) =>
            2.25 * 9 * Math.Exp(1.5 * 3 * x);
       
        public List<(double, double)> CalculateNodes()
        {
            var list = new List<(double, double)>();

            for (var i = 0; i <= m; ++i)
            {
                var x = a + i * h;
                var f = Function(x);
                list.Add((x, f));
            }

            return list;
        }

        public void PrintTable()
        {
            var list = CalculateNodes();
            Console.WriteLine(string.Format("|{1,25}|{1,25}|{2,25}|{3,25}|{4,25}|{5,25}|", "xi", "f(xi)", "f'(xi)nd", "f'(xi)e - f'(xi)nd", "f''(xi)nd", "f''(xi)e - f''(xi)nd"));

            for (var i = 0; i < list.Count; ++i)
            {
                if (i == 0)
                {
                    var x = list[i].Item1;
                    var f = list[i].Item2;
                    var der1 = (-3 * f + 4 * list[i + 1].Item2 - list[i + 2].Item2) / (2 * h);
                    var variance1 = Math.Abs(FirstDerivative(x) - der1);
                    Console.WriteLine(string.Format("|{0,25}|{1,25}|{2,25}|{3,25}", x, f, der1, variance1));
                }
                else if (i == list.Count - 1)
                {
                    var x = list[i].Item1;
                    var f = list[i].Item2;
                    var der1 = (3 * f - 4 * list[i - 1].Item2 + list[i - 2].Item2) / (2 * h);
                    var variance1 = Math.Abs(FirstDerivative(x) - der1);
                    Console.WriteLine(string.Format("|{0,25}|{1,25}|{2,25}|{3,25}", x, f, der1, variance1));
                }
                else
                {
                    var x = list[i].Item1;
                    var f = list[i].Item2;
                    var der1 = (list[i + 1].Item2 - list[i - 1].Item2) / (2 * h);
                    var variance1 = Math.Abs(FirstDerivative(x) - der1);
                    var der2 = (list[i + 1].Item2 - 2 * f + list[i - 1].Item2) / (h * h);
                    var variance2 = Math.Abs(SecondDerivative(x) - der2);
                    Console.WriteLine(string.Format("|{0,25}|{1,25}|{2,25}|{3,25}|{4,25}|{5,25}|", x, f, der1, variance1, der2, variance2));
                }
            }
        }
    }
}
