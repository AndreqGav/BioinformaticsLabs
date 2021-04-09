using System;
using System.Collections.Generic;

namespace KMP
{
    internal class Program
    {
        public static int count;

        private static void Main()
        {
            Console.WriteLine("Введите строку");
            var str = Console.ReadLine();

            Console.WriteLine("Введите паттерн");
            var pattern = Console.ReadLine();

            var borderArray = BorderArray(pattern);

            var n = str!.Length;
            var m = pattern!.Length;

            var list = new List<int>();

            int i = 0, j = 0;

            while (i < n - m + j + 1)
            {
                if (j == m)
                {
                    list.Add(i - j);
                    j = borderArray[m - 1];
                }
                else
                {
                    count++;

                    if (pattern[j] == str[i])
                    {
                        j++;
                        i++;
                    }
                    else
                    {
                        if (j != 0)
                        {
                            j = borderArray[j - 1];
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
            }

            Console.WriteLine("Массив граней: [" + string.Join(", ", borderArray) + ']');
            Console.WriteLine(list.Count == 0 ? "Вхождения не найдены" : $"Индексы вхождений: {string.Join(" ", list)}");

            Console.WriteLine($"Кол-во сравнений: {count}");
        }


        public static int[] BorderArray(string x)
        {
            var b = new int[x.Length];
            //count++;
            for (var i = 1; i < x.Length; i++)
            {
                var k = b[i - 1];
                while (k > 0 && x[i] != x[k])
                {
                    k = b[k - 1];
                }

                //count++;
                if (x[i] == x[k])
                {
                    k += 1;
                }

                b[i] = k;
            }

            return b;
        }
    }
}