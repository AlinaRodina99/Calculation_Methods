using System;
using System.Collections.Generic;

namespace Task3._1
{
    class Program
    {
        private const string function = "exp(-x) - x^2/2";
        private static double A = 0;
        private static double B = 1;
        private static int numberOfNodes = 10;
        private static double accuracy = 1.0e-8;
        private static double F;
        private static int degreeOfPolynomial;

        private static double Function(double x)
        {
            return Math.Exp(-x) - Math.Pow(x, 2) / 2;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("********Задача обратного интерполирования********");
            Console.WriteLine($"Функция: {function}");
            Console.WriteLine("Выберете режим работы: 1 - параметры по умолчанию, 2 - параметры по вводу с клавиатуры");
            var choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 2)
            {
                Console.WriteLine("Введите левый конец отрезка: ");
                A = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите правый конец отрезка: ");
                B = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите количество узлов: ");
                numberOfNodes = Convert.ToInt32(Console.ReadLine());
            }

            var firstSolve = new FirstSolve(A, B, numberOfNodes);
            var table = firstSolve.CalculateNodes();
            Console.WriteLine("Таблица значений для заданной функции: ");

            foreach (var el in table)
            {
                Console.WriteLine($"x = {el.Item1}; f(x) = {el.Item2}");
            }

            var swappedTable = firstSolve.SwapTable();
            Console.WriteLine("Таблица для обратной функции: ");

            foreach (var el in swappedTable)
            {
                Console.WriteLine($"x = {el.Item1}; f(x) = {el.Item2}");
            }

            Console.WriteLine("Введите точку для обратного интерполирования: ");
            F = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"Введите степень многочлена, которая меньше либо равна {numberOfNodes - 1}");
            degreeOfPolynomial = Convert.ToInt32(Console.ReadLine());

            firstSolve.DegreeOfPolynomial = degreeOfPolynomial;
            firstSolve.F = F;

            Console.WriteLine("Выберите способ решения задачи обратного интерполирования: 1 - свести задачу к алгебраическому интерполированию, находя значение " +
                "обратной функции в заданной точке, 2 - свести задачу к решению уравнения P(x) - F = 0 методом бисекции");
            var way = Convert.ToInt32(Console.ReadLine());

            if (way == 1)
            {
                var newtonFormula = firstSolve.CalculateNewtonFormula();
                Console.WriteLine($"Значение аргумента для F: {newtonFormula}");
                var absDiscrepancy = Math.Abs(Function(newtonFormula) - F);
                Console.WriteLine($"Значение модуля невязки: {absDiscrepancy}");
            }
            else
            {
                var secondSolve = new SecondSolve(A, B, numberOfNodes, accuracy);
                secondSolve.F = F;
                var listOfSegments = secondSolve.RootSeparation();
                var list = secondSolve.BisectionMethod(listOfSegments);
                
                foreach (var root in list)
                {
                    Console.WriteLine($"Значение аргумента для F: {root}");
                    var absDiscrepancy = Math.Abs(Function(root) - F);
                    Console.WriteLine($"Значение модуля невязки: {absDiscrepancy}");
                }
            }
        }
    }
}
