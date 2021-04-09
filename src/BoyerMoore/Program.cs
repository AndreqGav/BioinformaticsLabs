using System;
using System.Collections.Generic;
using System.Linq;

namespace BoyerMoore
{
    internal class Program
    {
        private static readonly Dictionary<string, int> Delta1 = new Dictionary<string, int>();
        private const string NotFoundSymbol = "NotFound";

        private static readonly List<int> Delta2 = new List<int>();

        private static void Main()
        {
            Console.WriteLine("Введите строку");
            var str = Console.ReadLine();

            Console.WriteLine("Введите паттерн");
            var pattern = Console.ReadLine();

            // abaababaabaababaababa
            var (list, count) = FindPattern(str, pattern);

            Console.WriteLine($"Кол-во сравнений: {count}");
            Console.WriteLine(list.Count == 0
                ? "Вхождения не найдены"
                : $"Индексы вхождений: {string.Join(" ", list)}");
        }

        public static (List<int>, int) FindPattern(string x, string p)
        {
            PrepareDelta1(p);
            PrepareDelta2(p);

            var list = new List<int>();
            var count = 0;

            var m = p.Length;
            var n = x.Length;

            var i = m - 1;

            while (i < n)
            {
                var j = m - 1;

                while (j >= 0 && i >= 0)
                {
                    count++;
                    if (x[i] != p[j])
                    {
                        break;
                    }

                    i--;
                    j--;
                }

                if (j == -1)
                {
                    list.Add(i + 1);
                }

                var delta1Value = GetDelta1(x, i);
                var delta2Value = GetDelta2(j);

                i += Math.Max(delta1Value, delta2Value);
            }

            return (list, count);
        }

        private static int GetDelta1(string x, int i)
        {
            var symbol = i >= 0 ? x[i].ToString() : NotFoundSymbol;
            return Delta1.TryGetValue(symbol, out var v) ? v : Delta1[NotFoundSymbol];
        }

        private static int GetDelta2(int j) => j >= 0 ? Delta2[j] : Delta2.First() + 1;

        private static void PrepareDelta1(string p)
        {
            Delta1.Clear();
            foreach (var (c, i) in p.Select(c => c.ToString()).Reverse().WithIndex())
            {
                if (!Delta1.ContainsKey(c))
                {
                    Delta1.Add(c, i);
                }
            }

            Delta1.Add(NotFoundSymbol, p.Length);
            Print(Delta1, "delta1: ");
        }

        private static void PrepareDelta2(string p)
        {
            Delta2.Clear();

            var m = p.Length;

            var transpositionX = p.Reverse().ToStr();

            var beta = BorderArray(p);
            var betaT = BorderArray(transpositionX);
            var ro = betaT.Reverse().ToArray();
            var g1 = GetG1(ro, m);
            var g2 = GetG2(beta, m);

            for (var j = 0; j < m; j++)
            {
                var value = g1[j] != 0 ? m - g1[j] : (m - j) + (m - g2[j] - 1);
                Delta2.Add(value);
            }

            Print(beta, "beta: ");
            Print(betaT, "betaT: ");
            Print(ro, "ro: ");
            Print(g1, "g1: ");
            Print(g2, "g2: ");
            Print(Delta2, "delta2: ");
        }

        private static int[] GetG1(int[] ro, int m)
        {
            //m = ro.Length;
            var g1 = new int[m];
            for (var j = 0; j < m; j++)
            {
                var delta = m - j - 1;
                var i = j;
                var value = 0;
                while (i > 0)
                {
                    if (ro[i] == delta && ro[i - 1] != delta + 1)
                    {
                        value = i;
                        break;
                    }

                    i--;
                }

                g1[j] = value;
            }

            return g1;
        }

        private static int[] GetG2(int[] beta, int m)
        {
            var g2 = new List<int>();
            var sm = new List<int>();

            while (sm.LastOrDefault() != 0 || sm.Count == 0)
            {
                sm.Add(GetPowerBeta(beta, m - 1, sm.Count + 1));
            }

            Print(sm, "Sm: ");

            for (var index = 0; index < sm.Count; index++)
            {
                int a;
                if (index == 0)
                {
                    a = 1;
                }
                else
                {
                    a = m - sm[index - 1] + 1;
                }

                var b = m - sm[index];

                for (var j = a; j <= b; j++)
                {
                    g2.Add(sm[index]);
                }
            }

            return g2.ToArray();
        }

        private static int GetPowerBeta(int[] borderArray, int index, int power)
        {
            var t = borderArray[index];
            for (var i = 1; i < power; i++)
            {
                t = borderArray[t - 1];
            }

            return t;
        }


        private static int[] BorderArray(string x)
        {
            var b = new int[x.Length];
            for (var i = 1; i < x.Length; i++)
            {
                var k = b[i - 1];
                while (k > 0 && x[i] != x[k])
                {
                    k = b[k - 1];
                }

                if (x[i] == x[k])
                {
                    k += 1;
                }

                b[i] = k;
            }

            return b;
        }

        private static void Print<T>(IEnumerable<T> enumerable, string message = "")
        {
            Console.WriteLine($"{message,10} {string.Join(" , ", enumerable)}");
        }
    }
}