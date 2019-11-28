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
            
            foreach(wierzcholek w in wierzcholki)
            {
                w.napisz();
            }

            Console.WriteLine();
            List<int> pierwsza = DFS(wierzcholki);
            if(pierwsza.Count == wierzcholki.Count)
            {
                Console.WriteLine("Graf jest spójny");
            }
            else
            {
                List<List<int>> skladowe = new List<List<int>>();
                skladowe.Add(pierwsza);
                foreach(int i in pierwsza)
                {
                    wierzcholek kasowany = wybierzWierzcholek(wierzcholki, i);
                    wierzcholki.Remove(kasowany);
                }
                while (wierzcholki.Count > 0)
                {
                    List<int> kolejna = DFS(wierzcholki);
                    skladowe.Add(kolejna);
                    foreach (int i in kolejna)
                    {
                        wierzcholek kasowany = wybierzWierzcholek(wierzcholki, i);
                        wierzcholki.Remove(kasowany);
                    }
                }

                Console.WriteLine("Graf nie jest spójny");
                int licznik = 1;
                foreach(List<int> skladowa in skladowe)
                {
                    string wynik = licznik + " składowa spójności: ";
                    foreach(int i in skladowa)
                    {
                        wynik += i;
                        wynik += ", ";
                    }
                    Console.WriteLine(wynik);
                    licznik++;
                }
            }
            



            Console.ReadKey();
        }

        public static List<int> DFS(List<wierzcholek> lista)
        {
            List<int> rozwiazanie = new List<int>();

            List<int> stos = new List<int>();
            wierzcholek obecny = lista.ElementAt(0);
            stos.Add(obecny.dajNumer());
            rozwiazanie.Add(obecny.dajNumer());
            while (stos.Count > 0)
            {
                List<int> sasiedzi = obecny.dajSasiadow();
                bool znalazl = false;
                foreach (int i in sasiedzi)
                {
                    wierzcholek nowy = wybierzWierzcholek(lista, i);
                    if (czyWybrany(rozwiazanie, nowy.dajNumer())==false)
                    {
                        znalazl = true;
                        stos.Add(nowy.dajNumer());
                        rozwiazanie.Add(nowy.dajNumer());
                        obecny = nowy;
                        break;
                    }
                }
                if (znalazl == false)
                {
                    stos.Remove(stos.Last());
                    if (stos.Count > 0) obecny = wybierzWierzcholek(lista, stos.Last());
                }
            }

            return rozwiazanie;
        }

        public static bool czyWybrany(List<int> lista, int x)
        {
            foreach(int i in lista)
            {
                if (i == x) return true;
            }
            return false;
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
    }
}