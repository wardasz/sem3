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
                    int licznik = zad.ElementAt(a);
                    while (licznik > 0)
                    {
                        nowy.dodajSasiada(i);
                        licznik--;
                    }
                }
                wierzcholki.Add(nowy);
            }

            flaga = true;
            foreach(wierzcholek w in wierzcholki)
            {
                if (w.dajStopien() % 2 == 1) flaga = false;
            }
            if (flaga == false)
            {
                Console.WriteLine("Graf nie jest grafem eulerowskim");
                Console.ReadKey();
                return;
            }

            if (czySpojny(wierzcholki) == false)
            {
                Console.WriteLine("Graf nie jest grafem spójnym");
                Console.ReadKey();
                return;
            }

            foreach (wierzcholek w in wierzcholki)
            {
                w.napisz();
            }
            Console.WriteLine();

            List<int> euler = new List<int>();
            euler.Add(1);
            while (wierzcholki.Count > 0)
            {
                int a = euler.Last();
                List<int> sasiedzi = wybierzWierzcholek(wierzcholki, a).dajSasiadow();
                foreach(int b in sasiedzi)
                {
                    usunKrawedz(wierzcholki, a, b);
                    if (czySpojny(wierzcholki) == true)
                    {
                        euler.Add(b);
                        break;
                    }
                    else
                    {
                        dodajKrawedz(wierzcholki, a, b);
                    }
                }
            }

            string wynik = "Cykl Eulera w podanym grafie: ";
            foreach(int i in euler)
            {
                wynik += i;
                wynik += ", ";
            }
            Console.WriteLine(wynik);





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

        public static bool czyNaLiscie(List<int> lista, int obiekt)
        {
            foreach(int i in lista)
            {
                if (i == obiekt) return true; 
            }
            return false;
        }

        public static bool czySpojny(List<wierzcholek> lista)
        {
            if (lista.Count == 0) return true;

            List<int> kolejka = new List<int>();
            List<int> zbadane = new List<int>();
            kolejka.Add(1);
            zbadane.Add(1);
            while (kolejka.Count > 0)
            {              
                wierzcholek obecny = wybierzWierzcholek(lista, kolejka.ElementAt(0));
                List<int> sasiedzi = obecny.dajSasiadow();
                foreach (int i in sasiedzi)
                {
                    if(czyNaLiscie(zbadane, i) == false)
                    {
                        kolejka.Add(i);
                        zbadane.Add(i);
                    }
                }
                kolejka.Remove(kolejka.ElementAt(0));
            }
            if(zbadane.Count == lista.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void dodajKrawedz(List<wierzcholek> lista, int a, int b)
        {
            Console.WriteLine("Dodaje krawędź " + a + "-" + b);
            wierzcholek roboczy = wybierzWierzcholek(lista, a);
            if (roboczy == null)
            {
                roboczy = new wierzcholek(a);
                lista.Add(roboczy);
                Console.WriteLine("Dodaje wierzcholek " + a);
            }
            roboczy.dodajSasiada(b);

            roboczy = wybierzWierzcholek(lista, b);
            if (roboczy == null)
            {
                roboczy = new wierzcholek(b);
                lista.Add(roboczy);
                Console.WriteLine("Dodaje wierzcholek " + b);
            }
            roboczy.dodajSasiada(a);
        }

        public static void usunKrawedz(List<wierzcholek> lista, int a, int b)
        {
            Console.WriteLine("Usuwam krawędź " + a + "-" + b);
            wierzcholek roboczy = wybierzWierzcholek(lista, a);
            roboczy.usunSasiada(b);
            if (roboczy.dajStopien() == 0)
            {
                lista.Remove(roboczy);
                Console.WriteLine("Usuwam wierzcholek " + a);
            }

            roboczy = wybierzWierzcholek(lista, b);
            roboczy.usunSasiada(a);
            if (roboczy.dajStopien() == 0) {
                lista.Remove(roboczy);
                Console.WriteLine("Usuwam wierzcholek " + b);
            }
        }
    }
}