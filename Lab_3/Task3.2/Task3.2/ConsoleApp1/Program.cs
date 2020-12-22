using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] _)
        {
            Console.WriteLine("******Задача численного дифференцирования******");
            Console.WriteLine("Функция: e^(4,5 * x)");
            while (true)
            {
                Console.WriteLine("Введите левый конец отрезка дифференцирования:");
                var a = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите шаг, с которым будут генерироваться точки:");
                var h = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите количество точек:");
                var m = Convert.ToInt32(Console.ReadLine());
                var equations = new DifferentialEquations(h, m, a);

                var list = equations.CalculateNodes();
                Console.WriteLine("Таблица значений для функции:");
                foreach (var node in list)
                {
                    Console.WriteLine($"x = {node.Item1}; f(x) = {node.Item2}");
                }

                equations.PrintTable();
                Console.WriteLine("Хотите ввести новые параметры? Введите да или нет:");
                var choice = Convert.ToString(Console.ReadLine());

                if (choice == "да")
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
