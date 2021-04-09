using System;
using System.Collections.Generic;

namespace KR
{
    internal class Program
    {
        private static void Main()
        {
            var count = 0;

            Console.WriteLine("Введите строку");
            var str = Console.ReadLine();
            Console.WriteLine("Введите паттерн");
            var pattern = Console.ReadLine();

            var n = str!.Length;
            var m = pattern!.Length;

            var list = new List<int>();

            const int Q = 999999;

            var sigmaP = 0;
            var sigmaI = 0;


            for (var j = 0; j < m; j++)
            {
                sigmaP += ((int) Math.Pow(2, m - 1 - j) * pattern[j]) % Q;
                sigmaI += ((int) Math.Pow(2, m - 1 - j) * str[j]) % Q;
            }

            for (var i = 0; i < n - m + 1; i++)
            {
                if (sigmaP == sigmaI)
                {
                    var j = 0;
                    while (j < m && pattern[j] == str[i + j])
                    {
                        j++;
                        count++;
                    }

                    if (j == m)
                    {
                        list.Add(i);
                    }
                }
                else
                {
                    // σ(i+1)=(2(Σ(i) – 2^(m - 1) * x[i])+x[i + m]) mod q.
                    sigmaI = ((2 * (sigmaI + Q - (int) Math.Pow(2, m - 1) * str[i]) % Q) % Q + str[i + m]) % Q;
                }
            }

            Console.WriteLine(list.Count == 0 ? "Вхождений не найдено" : $"Индексы вхождений: {string.Join(" ", list)}");
            Console.WriteLine($"Количество сравнений: {count}");
        }
    }
}