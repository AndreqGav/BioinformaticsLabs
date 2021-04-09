using System;
using System.Collections.Generic;

namespace NaiveMethod
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Введите строку");
            var str = Console.ReadLine();

            Console.WriteLine("Введите паттерн");
            var pattern = Console.ReadLine();

            var (list, count) = FindPattern(str, pattern);

            Console.WriteLine($"Кол-во сравнений: {count}");
            Console.WriteLine(list.Count == 0 ? "Вхождения не найдены" : $"Индексы вхождений: {string.Join(" ", list)}");
        }

        public static (List<int>, int) FindPattern(string x, string p)
        {
            var n = x.Length;
            var m = p.Length;
            var list = new List<int>();
            var count = 0;
            for (var i = 0; i < n - m + 1; i++)
            {
                int j;
                for (j = 0; j < m; j++)
                {
                    count++;
                    if (x[i + j] != p[j])
                    {
                        break;
                    }
                }

                if (j == m)
                {
                    list.Add(i);
                }
            }

            return (list, count);
        }
    }
}