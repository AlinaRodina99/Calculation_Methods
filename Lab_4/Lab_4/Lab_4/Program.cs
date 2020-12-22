using System;

namespace Lab_4
{
    class Program
    {
        private static double A;
        private static double B;
        private static int m;
        private static double h;

        private static double Y(Func<double, double> function)
        {
            double y = 0;

            for (double i = 1; i < m; ++i)
            {
                y += function(A + i * h);
            }

            return y;
        }

        private static double P(Func<double, double> function)
        {
            double p = 0;

            for (double i = 0; i < m; ++i)
            {
                p += function(A + i * h + h / 2);
            }

            return p;
        }

        private static double LeftRectangles(Func<double, double> function)
        {
            return h * (function(A) + Y(function));
        }

        private static double RightRectangles(Func<double, double> function)
        {
            return h * (function(B) + Y(function));
        }

        private static double MiddleRectangles(Func<double, double> function)
        {
            return h * P(function);
        }

        private static double Trapeze(Func<double, double> function)
        {
            return ((function(A) + function(B)) / 2 + Y(function)) * h;
        }

        private static double Simpson(Func<double, double> function)
        {
            return (2 * Y(function) + 4 * P(function) + function(A) + function(B)) * h / 6.0;
        }

        private static double Const(double x) => 10;

        private static double IntegralForConst(double x) => 10 * x;

        private static double PolynomialFirstDegree(double x) => 6 * x + 10;

        private static double IntegralForPolynomialFirstDegree(double x) => 3 * x * x + 10 * x;

        private static double PolynomialThirdDegree(double x) => 8 * x * x * x + 12 * x * x + 6 * x + 15;

        private static double IntegralForPolynomialThirdDegree(double x) => 2 * x * x * x * x + 4 * x * x * x + 3 * x * x + 15 * x;

        private static double Function1(double x) => Math.Pow(Math.E, x);

        private static double IntegralForFunction1(double x) => Math.Pow(Math.E, x);

        private static double Function2(double x) => 6 * x * x - Math.Cos(x);

        private static double IntegralForFunction2(double x) => 2 * x * x * x - Math.Sin(x);

        static void Main(string[] _)
        {
            Console.WriteLine("******Приближенное вычисление интеграла по составным квадратурным формулам******");
            while (true)
            {
                Console.WriteLine("Введите левый конец отрезка интегрирования:");
                A = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите правый конец отрезка интегрирования:");
                B = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введите число промежутков деления отрезка интегрирования:");
                m = Convert.ToInt32(Console.ReadLine());
                h = (B - A) / m;
                Console.WriteLine();
                Console.WriteLine($"f(x) = 10");
                var integral1 = IntegralForConst(B) - IntegralForConst(A);
                Console.WriteLine($"Точное значение интеграла: { integral1 }");
                var integral1WithLeftRectangles = LeftRectangles(Const);
                var integral1WithRightRectangles = RightRectangles(Const);
                var integral1WithMiddleRectangles = MiddleRectangles(Const);
                var integral1WithTrapeze = Trapeze(Const);
                var integra1WithSimpson = Simpson(Const);
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы левых прямоугольников:{ integral1WithLeftRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для левых прямоугольников: { Math.Abs(integral1 - integral1WithLeftRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы правых прямоугольников:{ integral1WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для правых прямоугольников: { Math.Abs(integral1 - integral1WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы средних прямоугольников:{ integral1WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для средних прямоугольников: { Math.Abs(integral1 - integral1WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы трапеций:{ integral1WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для трапеций: { Math.Abs(integral1 - integral1WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы Симпсона:{ integral1WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для Симпсона: { Math.Abs(integral1 - integral1WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"f(x) = 6 * x + 10");
                var integral2 = IntegralForPolynomialFirstDegree(B) - IntegralForPolynomialFirstDegree(A);
                Console.WriteLine($"Точное значение интеграла: { integral2 }");
                var integral2WithLeftRectangles = LeftRectangles(PolynomialFirstDegree);
                var integral2WithRightRectangles = RightRectangles(PolynomialFirstDegree);
                var integral2WithMiddleRectangles = MiddleRectangles(PolynomialFirstDegree);
                var integral2WithTrapeze = Trapeze(PolynomialFirstDegree);
                var integra2WithSimpson = Simpson(PolynomialFirstDegree);
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы левых прямоугольников:{ integral2WithLeftRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для левых прямоугольников: { Math.Abs(integral2 - integral2WithLeftRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы правых прямоугольников:{ integral2WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для правых прямоугольников: { Math.Abs(integral2 - integral2WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы средних прямоугольников:{ integral2WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для средних прямоугольников: { Math.Abs(integral2 - integral2WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы трапеций:{ integral2WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для трапеций: { Math.Abs(integral2 - integral2WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы Симпсона:{ integral2WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для Симпсона: { Math.Abs(integral2 - integral2WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"f(x) = 8 * x^3 - 12 * x^2 + 6 * x - 15");
                var integral3 = IntegralForPolynomialThirdDegree(B) - IntegralForPolynomialThirdDegree(A);
                Console.WriteLine($"Точное значение интеграла: { integral3 }");
                var integral3WithLeftRectangles = LeftRectangles(PolynomialThirdDegree);
                var integral3WithRightRectangles = RightRectangles(PolynomialThirdDegree);
                var integral3WithMiddleRectangles = MiddleRectangles(PolynomialThirdDegree);
                var integral3WithTrapeze = Trapeze(PolynomialThirdDegree);
                var integra3WithSimpson = Simpson(PolynomialThirdDegree);
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы левых прямоугольников:{ integral3WithLeftRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для левых прямоугольников: { Math.Abs(integral3 - integral3WithLeftRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы правых прямоугольников:{ integral3WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для правых прямоугольников: { Math.Abs(integral3 - integral3WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы средних прямоугольников:{ integral3WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для средних прямоугольников: { Math.Abs(integral3 - integral3WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы трапеций:{ integral3WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для трапеций: { Math.Abs(integral3 - integral3WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы Симпсона:{ integral3WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для Симпсона: { Math.Abs(integral3 - integral3WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"f(x) = 6 * x^2 - cos(x)");
                var integral4 = IntegralForFunction2(B) - IntegralForFunction2(A);
                Console.WriteLine($"Точное значение интеграла: { integral4 }");
                var integral4WithLeftRectangles = LeftRectangles(Function2);
                var integral4WithRightRectangles = RightRectangles(Function2);
                var integral4WithMiddleRectangles = MiddleRectangles(Function2);
                var integral4WithTrapeze = Trapeze(Function2);
                var integra4WithSimpson = Simpson(Function2);
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы левых прямоугольников:{ integral4WithLeftRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для левых прямоугольников: { Math.Abs(integral4 - integral4WithLeftRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы правых прямоугольников:{ integral4WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для правых прямоугольников: { Math.Abs(integral4 - integral4WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы средних прямоугольников:{ integral4WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для средних прямоугольников: { Math.Abs(integral4 - integral4WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы трапеций:{ integral4WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для трапеций: { Math.Abs(integral4 - integral4WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы Симпсона:{ integral4WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для Симпсона: { Math.Abs(integral4 - integral4WithRightRectangles) }");
                Console.WriteLine();
                Console.WriteLine($"f(x) = e^x");
                var integral5 = IntegralForFunction1(B) - IntegralForFunction1(A);
                Console.WriteLine($"Точное значение интеграла: { integral5 }");
                var integral5WithLeftRectangles = LeftRectangles(Function1);
                var integral5WithRightRectangles = RightRectangles(Function1);
                var integral5WithMiddleRectangles = MiddleRectangles(Function1);
                var integral5WithTrapeze = Trapeze(Function1);
                var integra5WithSimpson = Simpson(Function1);
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы левых прямоугольников:{ integral5WithLeftRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для левых прямоугольников: { Math.Abs(integral5 - integral5WithLeftRectangles) }");
                Console.WriteLine($"Теоретическая погрешность для левых прямоугольников: { (double)1 / 2 * (B - A) * m * Math.Exp(B) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы правых прямоугольников:{ integral5WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для правых прямоугольников: { Math.Abs(integral5 - integral5WithRightRectangles) }");
                Console.WriteLine($"Теоретическая погрешность для левых прямоугольников: { (double)1 / 2 * (B - A) * m * Math.Exp(B) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы средних прямоугольников:{ integral5WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для средних прямоугольников: { Math.Abs(integral5 - integral5WithRightRectangles) }");
                Console.WriteLine($"Теоретическая погрешность для левых прямоугольников: { (double)1 / 24 * (B - A) * Math.Pow(m, 2) * Math.Exp(B) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы трапеций:{ integral5WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для трапеций: { Math.Abs(integral5 - integral5WithRightRectangles) }");
                Console.WriteLine($"Теоретическая погрешность для левых прямоугольников: { (double)1 / 12 * (B - A) * Math.Pow(m, 2) * Math.Exp(B) }");
                Console.WriteLine();
                Console.WriteLine($"Значение интеграла, посчитанное с помощью формулы Симпсона:{ integral5WithRightRectangles }");
                Console.WriteLine($"Абсолютная фактическая погрешность для Симпсона: { Math.Abs(integral5 - integral5WithRightRectangles) }");
                Console.WriteLine($"Теоретическая погрешность для левых прямоугольников: { (double)1 / 2880 * (B - A) * Math.Pow(m, 4) * Math.Exp(B) }");

                Console.WriteLine("Хотите ввести новые параметры и продолжить работу? Введите да или нет:");
                var choice = Console.ReadLine();

                if (choice == "да")
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
