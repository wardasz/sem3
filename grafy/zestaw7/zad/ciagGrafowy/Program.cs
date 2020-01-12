using GraphPlanarityTesting.Graphs.DataStructures;
using GraphPlanarityTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GraphPlanarityTesting.PlanarityTesting.BoyerMyrvold;

namespace ciagGrafowy
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> macierz = new List<List<int>>();

            var s = new FileInfo(Directory.GetCurrentDirectory());
            var s2 = s.Directory.Parent.Parent;
            String sciezka = s2.ToString() + "\\dane.csv";

            using (var reader = new StreamReader(sciezka))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    List<int> zad = new List<int>();
                    for (int x = 0; x < values.Length; x++)
                    {
                        zad.Add(Convert.ToInt32(values[x]));
                    }
                    macierz.Add(zad);
                }
            }

            bool flaga = true;
            for (int x = 1; x <= macierz.Count; x++)
            {
                for (int y = x + 1; y <= macierz.Count; y++)
                {
                    int jeden = dajPole(macierz, x, y);
                    int dwa = dajPole(macierz, y, x);
                    if (jeden != dwa) flaga = false;
                }
            }
            if (flaga == false)
            {
                Console.WriteLine("Podana macierz nie jest grafem");
                Console.ReadKey();
                return;
            }

            IGraph<int> graf = new UndirectedAdjacencyListGraph<int>();

            for (int numer = 1; numer <= macierz.Count; numer++)
            {
                graf.AddVertex(numer);
            }

            for (int numer = 1; numer <= macierz.Count; numer++)
            {
                List<int> zad = macierz.ElementAt((numer - 1));
                for(int i = numer; i <= zad.Count; i++)
                {
                    int a = i-1;
                    if (zad.ElementAt(a) != 0)
                    {
                        graf.AddEdge(numer, (a+1));
                    }
                }
            }

            var boyerMyrvold = new BoyerMyrvold<int>();
            if (boyerMyrvold.IsPlanar(graf) == true)
            {
                Console.WriteLine("Graf jest planarny");
            }
            else
            {
                Console.WriteLine("Graf nie jest planarny");
            }
            




            Console.ReadKey();
        }

        public static int dajPole(List<List<int>> macierz, int a, int b)
        {
            List<int> zad = dajWiezcholek(macierz, a);
            b--;
            int i = zad.ElementAt(b);
            return i;
        }

        public static List<int> dajWiezcholek(List<List<int>> macierz, int a)
        {
            a--;
            List<int> zad = macierz.ElementAt(a);
            return zad;
        }
        
    }
}