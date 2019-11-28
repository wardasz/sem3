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

            List<int> stos = new List<int>();
            wierzcholek obecny = wybierzWierzcholek(wierzcholki, 1);
            obecny.naDrzewo();
            List<sciezka> sciezki = new List<sciezka>();
            stos.Add(1);
            while (stos.Count>0)
            {
                List<int> sasiedzi = obecny.dajSasiadow();
                bool znalazl = false;
                foreach(int i in sasiedzi)
                {
                    wierzcholek nowy = wybierzWierzcholek(wierzcholki, i);
                    if (nowy.czyNaDrzewie() == false)
                    {
                        znalazl = true;
                        nowy.naDrzewo();
                        stos.Add(nowy.dajNumer());
                        sciezki.Add(new sciezka(obecny.dajNumer(), nowy.dajNumer()));
                        obecny = nowy;
                        break;
                    }                   
                }
                if (znalazl == false)
                {
                    stos.Remove(stos.Last());
                    if(stos.Count > 0) obecny = wybierzWierzcholek(wierzcholki, stos.Last());
                }
            }

            Console.WriteLine();
            flaga = true;
            foreach(wierzcholek w in wierzcholki)
            {
                if (w.czyNaDrzewie() == false) flaga = false;
            }

            if(flaga == false)
            {
                Console.WriteLine("Podany graf nie jest spójny");
            }
            else
            {
                Console.WriteLine("Podany graf zawiera następujące drzewo spójności");
                string wynik = "";
                foreach(sciezka x in sciezki)
                {
                    wynik += x.opis();
                    wynik += ", ";
                }
                wynik.Remove(wynik.Last());
                wynik.Remove(wynik.Last());
                Console.WriteLine(wynik);
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
    }
}