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
                if (dajPole(macierz, x, x) != 0) flaga = false;
            }
            if (flaga == false)
            {
                Console.WriteLine("Podany graf ma pętle");
                Console.ReadKey();
                return;
            }

            List<wierzcholek> wierzcholki = new List<wierzcholek>();
            for (int numer = 1; numer <= macierz.Count; numer++)
            {
                wierzcholek nowy = new wierzcholek(numer);
                List<int> zad = macierz.ElementAt((numer - 1));
                for (int i = 1; i <= zad.Count; i++)
                {
                    int a = i - 1;
                    if (zad.ElementAt(a) != 0)
                    {
                        nowy.dodajSasiada(i);
                    }
                }
                wierzcholki.Add(nowy);
            }

            foreach (wierzcholek w in wierzcholki)
            {
                w.napisz();
            }

            List<wierzcholek> odwrocony = new List<wierzcholek>();
            foreach(wierzcholek w in wierzcholki)
            {
                wierzcholek nowy = new wierzcholek(w.dajNumer());
                odwrocony.Add(nowy);
            }
            foreach(wierzcholek w in wierzcholki)
            {
                foreach(int i in w.dajSasiadow())
                {
                    wybierzWierzcholek(odwrocony, i).dodajSasiada(w.dajNumer());
                }
            }

            Console.WriteLine();
            foreach (wierzcholek w in odwrocony)
            {
                w.napisz();
            }

            List<int> stosKosaraju = new List<int>();
            foreach(wierzcholek w in wierzcholki)
            {
                if (w.CzyOdwiedzony() == false) DFS(wierzcholki, w.dajNumer(), stosKosaraju);
            }

            Console.WriteLine();
            string wypis = "Stos: ";
            foreach (int i in stosKosaraju)
            {
                wypis += i;
                wypis += ", ";
            }
            Console.WriteLine(wypis);
            Console.WriteLine();
            int licznik = 1;
            while (stosKosaraju.Count > 0)
            {
                foreach (wierzcholek w in odwrocony)
                {
                    w.odznacz();
                }
                List<int> skladowa = DFS(odwrocony, stosKosaraju.Last());
                string napis = licznik + " składowa spójności składa sie z wierzchołków: ";
                foreach (int i in skladowa) 
                {
                    napis += i;
                    napis += ", ";
                    stosKosaraju.Remove(i);
                    odwrocony.Remove(wybierzWierzcholek(odwrocony, i));
                    foreach(wierzcholek w in odwrocony)
                    {
                        w.usunSasiada(i);
                    }
                }
                licznik++;
                Console.WriteLine(napis);
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
            foreach (wierzcholek w in lista)
            {
                if (w.dajNumer() == a) return w;
            }
            return null;
        }
        
        public static void DFS(List<wierzcholek> lista, int start, List<int> stosKosaraju)
        {
            List<int> stos = new List<int>();
            stos.Add(start);
            wybierzWierzcholek(lista, start).odwiedz();
            while (stos.Count > 0)
            {
                wierzcholek obecny = wybierzWierzcholek(lista, stos.Last());
                List<int> sasiedzi = obecny.dajSasiadow();
                bool znalazl = false;
                foreach (int i in sasiedzi)
                {
                    wierzcholek nowy = wybierzWierzcholek(lista, i);
                    if (nowy.CzyOdwiedzony() == false)
                    {
                        znalazl = true;
                        nowy.odwiedz();
                        stos.Add(nowy.dajNumer());
                        break;
                    }
                }
                if (znalazl == false)
                {
                    stos.Remove(stos.Last());
                    stosKosaraju.Add(obecny.dajNumer());
                }
            }           
        }

        public static List<int> DFS(List<wierzcholek> lista, int start)
        {
            List<int> rozwiazanie = new List<int>();
            List<int> stos = new List<int>();
            stos.Add(start);
            rozwiazanie.Add(start);
            wybierzWierzcholek(lista, start).odwiedz();
            while (stos.Count > 0)
            {
                wierzcholek obecny = wybierzWierzcholek(lista, stos.Last());
                List<int> sasiedzi = obecny.dajSasiadow();
                bool znalazl = false;
                foreach (int i in sasiedzi)
                {
                    wierzcholek nowy = wybierzWierzcholek(lista, i);
                    if (nowy.CzyOdwiedzony() == false)
                    {
                        znalazl = true;
                        nowy.odwiedz();
                        stos.Add(nowy.dajNumer());
                        rozwiazanie.Add(nowy.dajNumer());
                        break;
                    }
                }
                if (znalazl == false)
                {
                    stos.Remove(stos.Last());
                }
            }
            return rozwiazanie;
        }

    }
}