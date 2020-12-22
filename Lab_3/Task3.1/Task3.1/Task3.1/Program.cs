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

        static void Main(string[] _)
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

            while (true)
            {
                Console.WriteLine("Введите точку для обратного интерполирования: ");
                F = Convert.ToDouble(Console.ReadLine());
                firstSolve.F = F;
                Console.WriteLine($"Введите степень многочлена, которая меньше либо равна {numberOfNodes - 1}");
                degreeOfPolynomial = Convert.ToInt32(Console.ReadLine());
                firstSolve.DegreeOfPolynomial = degreeOfPolynomial;
                var newtonFormula = firstSolve.CalculateNewtonFormula();
                Console.WriteLine($"Значение аргумента для F первым способом решения: {newtonFormula}");
                var absDiscrepancy1 = Math.Abs(Function(newtonFormula) - F);
                Console.WriteLine($"Значение модуля невязки для первого способа решения: {absDiscrepancy1}");
                var secondSolve = new SecondSolve(A, B, numberOfNodes, accuracy)
                {
                    F = F,
                    DegreeOfPolynomial = degreeOfPolynomial
                };
                var listOfSegments = secondSolve.RootSeparation();
                var list = secondSolve.BisectionMethod(listOfSegments);

                foreach (var root in list)
                {
                    Console.WriteLine($"Значение аргумента для F вторым способом решения: {root}");
                    var absDiscrepancy2 = Math.Abs(Function(root) - F);
                    Console.WriteLine($"Значение модуля невязки для второго способа решения: {absDiscrepancy2}");
                }
                

                Console.WriteLine("Хотите продолжить? Наберите да или нет:");
                var input = Convert.ToString(Console.ReadLine());

                if (input == "да")
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("Программа заканчивает работу.");
                    break;
                }
            }
        }
    }
}
