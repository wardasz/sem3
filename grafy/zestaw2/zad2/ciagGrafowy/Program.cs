using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            flaga = true;
            foreach (List<int> zad in macierz)
            {
                foreach (int i in zad)
                {
                    if (i == 0 || i == 1)
                    {
                    }
                    else
                    {
                        flaga = false;
                    }
                }
            }
            if (flaga == false)
            {
                Console.WriteLine("Podana macierz reprezentuje graf, który nie jest grafem prostym");
                Console.ReadKey();
                return;
            }

            List<wierzcholek> wierzcholki = new List<wierzcholek>();
            for(int numer = 1; numer<= macierz.Count; numer++)
            {
                wierzcholek nowy = new wierzcholek(numer);
                List<int> zad = macierz.ElementAt((numer - 1));
                for(int i = 1; i <= zad.Count; i++)
                {
                    int a = i-1;
                    if (zad.ElementAt(a) == 1)
                    {
                        nowy.dodajSasiada(i);
                    }
                }
                wierzcholki.Add(nowy);
            }

            while(wierzcholki.Count > 2)
            {
                foreach (wierzcholek w in wierzcholki)
                {
                    w.napisz();
                }
                Console.WriteLine();

                List<wierzcholek> usuwane = new List<wierzcholek>();
                foreach(wierzcholek w in wierzcholki)
                {
                    if (w.dajStopien() == 1)
                    {
                        usuwane.Add(w);
                    }
                }
                foreach (wierzcholek u in usuwane)
                {
                    wierzcholki.Remove(u);
                }
                foreach (wierzcholek u in usuwane)
                {
                    foreach(wierzcholek w in wierzcholki)
                    {
                        if (u.dajNumer() == w.dajNumer())
                        {
                            wierzcholki.Remove(w);
                        }
                        else
                        {
                            w.usunSasiada(u.dajNumer());
                        }
                    }
                }
            }

            if (wierzcholki.Count == 1)
            {
                Console.WriteLine("Centrum drzewa jest wierzchołek " + wierzcholki.ElementAt(0).dajNumer());
            }
            else
            {
                Console.WriteLine("Centrum drzewa są wierzchołki " + wierzcholki.ElementAt(0).dajNumer() + " i " + wierzcholki.ElementAt(1).dajNumer());
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