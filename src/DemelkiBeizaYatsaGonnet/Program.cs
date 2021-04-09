using System;
using System.Collections.Generic;
using System.Linq;

namespace DemelkiBeizaYatsaGonnet
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
            var list = new List<int>();
            var count = 0;

            var alphabet = x.ToHashSet();
            var t = CalculateT(alphabet.ToList(), p);

            var s = new List<int> {0}.Concat(p.Select(_ => 1)).ToArray();

            var n = x.Length;
            var m = p.Length;

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    s[j] = s[j] | t[(x[i], j)];
                }

                s = RightShift(s);

                count++;
                if (s[m] == 0)
                {
                    list.Add(i - m + 1);
                }
            }

            return (list, count);
        }

        public static Dictionary<(char, int), int> CalculateT(List<char> alphabet, string p)
        {
            var t = new Dictionary<(char, int), int>();

            foreach (var c in alphabet)
            {
                for (var j = 0; j < p.Length; j++)
                {
                    //t[i, j] = p[j] == x[i] ? 0 : 1;

                    var v = p[j] == c ? 0 : 1;
                    t.Add((c, j), v);
                }
            }

            return t;
        }

        private static int[] RightShift(IReadOnlyList<int> s)
        {
            var r = new int[s.Count];

            for (var i = 1; i < s.Count; i++)
            {
                r[i] = s[i - 1];
            }

            return r;
        }
    }
}