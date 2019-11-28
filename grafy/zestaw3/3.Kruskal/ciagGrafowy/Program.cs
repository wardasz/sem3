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
            for(int x =1; x<=macierz.Count; x++)
            {
                if (dajPole(macierz, x, x) != 0) flaga = false;
            }
            if (flaga == false)
            {
                Console.WriteLine("Podany graf ma pętle");
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
                    if (zad.ElementAt(a) != 0)
                    {
                        nowy.dodajSasiada(i);
                    }
                }
                wierzcholki.Add(nowy);
            }
            
            foreach(wierzcholek w in wierzcholki)
            {
                w.napisz();
            }
            Console.WriteLine();

            List<krawedz> krawedzie = new List<krawedz>();
            for (int x = 1; x <= macierz.Count; x++)
            {
                for (int y = x + 1; y <= macierz.Count; y++)
                {
                    if (dajPole(macierz, x, y) != 0)
                    {
                        krawedz nowa = new krawedz(x, y, dajPole(macierz, x, y));
                        krawedzie.Add(nowa);
                    }
                }
            }
            krawedzie.Sort((a, b) => a.porownaj(b));
            foreach(krawedz k in krawedzie)
            {
                k.napisz();
            }

            List<wierzcholek> drzewo = new List<wierzcholek>();
            int max = wierzcholki.Count();
            max--;
            int ile = 0;

            Console.WriteLine();
            while (ile < max)
            {
                krawedz dodawana = krawedzie.ElementAt(0);               
                dodaj(drzewo, dodawana);
                if(czyCykl(drzewo, dodawana.dajA()) == true)
                {
                    usun(drzewo, dodawana);
                    Console.WriteLine("Nie dodano krawędzi o masie " + dodawana.dajWage());
                }
                else
                {
                    ile++;
                    Console.WriteLine("Dodano krawędź o masie " + dodawana.dajWage());
                }
                krawedzie.Remove(dodawana);
            }
            Console.WriteLine();
            Console.WriteLine("Minimalne drzewo rozpinające:");
            foreach(wierzcholek w in drzewo)
            {
                w.napisz();
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

        public static wierzcholek wybierzWierzcholek(List<wierzcholek> lista, int a)
        {
            foreach(wierzcholek w in lista)
            {
                if (w.dajNumer() == a) return w;
            }
            return null;
        }

        public static void dodaj(List<wierzcholek> drzewo, krawedz k)
        {
            int a = k.dajA();
            int b = k.dajB();
            wierzcholek A = wybierzWierzcholek(drzewo, a);
            wierzcholek B = wybierzWierzcholek(drzewo, b);
            if (A == null)
            {
                A = new wierzcholek(a);
                drzewo.Add(A);
            }
            if (B == null)
            {
                B = new wierzcholek(b);
                drzewo.Add(B);
            }
            A.dodajSasiada(b);
            B.dodajSasiada(a);
        }

        public static void usun(List<wierzcholek> drzewo, krawedz k)
        {
            int a = k.dajA();
            int b = k.dajB();
            wierzcholek A = wybierzWierzcholek(drzewo, a);
            wierzcholek B = wybierzWierzcholek(drzewo, b);
            A.usunSasiada(b);
            B.usunSasiada(a);
            if (A.dajSasiadow().Count==0)
            {
                drzewo.Remove(A);
            }
            if (B.dajSasiadow().Count == 0)
            {
                drzewo.Remove(B);
            }         
        }

        public static bool czyCykl(List<wierzcholek> drzewo, int i)
        {
            foreach(wierzcholek w in drzewo)
            {
                w.odznacz();
            }
            List<int> kolejka = new List<int>();
            kolejka.Add(i);
            int ileWiezcholkow = 0;
            int ileKrawedzi = 0;
            while (kolejka.Count != 0)
            {
                wierzcholek sprawdzany = wybierzWierzcholek(drzewo, kolejka.ElementAt(0));
                if(sprawdzany.CzyOdwiedzony() == false)
                {
                    ileWiezcholkow++;
                    ileKrawedzi += sprawdzany.dajSasiadow().Count;
                    foreach(int x in sprawdzany.dajSasiadow())
                    {
                        kolejka.Add(x);
                    }
                    sprawdzany.odwiedz();
                }
                kolejka.Remove(kolejka.ElementAt(0));
            }
            ileKrawedzi = ileKrawedzi / 2;
            ileWiezcholkow--;
            if (ileWiezcholkow == ileKrawedzi)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}