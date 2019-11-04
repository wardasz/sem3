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

            int minStopien = wierzcholki.ElementAt(0).dajStopien();
            foreach(wierzcholek w in wierzcholki)
            {
                if (w.dajStopien() < minStopien)
                {
                    minStopien = w.dajStopien();
                }
            }
            if (minStopien < 2)
            {
                Console.WriteLine("Graf ma za mały minimalny stopień");
                Console.ReadKey();
                return;
            }

            List<lancuch> lancuchy = new List<lancuch>();
            foreach(wierzcholek w in wierzcholki)
            {
                lancuch nowy = new lancuch(w.dajNumer());
                lancuchy.Add(nowy);
            }

            flaga = true;
            while (flaga == true)
            {
                foreach(lancuch l in lancuchy)
                {
                    l.napisz();
                }
                Console.WriteLine();
                flaga = false;
                List<lancuch> nowe = new List<lancuch>();
                foreach(lancuch l in lancuchy)
                {
                    wierzcholek krancowy = dajWierzcholek2(wierzcholki, l.dajOstatni());
                    List<int> sasiednie = krancowy.dajSasiadow();
                    foreach(int i in sasiednie)
                    {
                        if (l.czyW(i) == false)
                        {
                            lancuch nowy = new lancuch(l, i);
                            nowe.Add(nowy);
                            flaga = true;
                        }
                    }
                }
                if(flaga == true) lancuchy = nowe;
            }

            foreach (lancuch l in lancuchy)
            {
                flaga = true;
                while (flaga == true)
                {
                    if (l.dajDlugosc() > minStopien)
                    {
                        int kraniec = l.dajOstatni();
                        wierzcholek ostatni = dajWierzcholek2(wierzcholki, kraniec);
                        int pierwszy = l.dajPierwszy();

                        if (ostatni.czySasiaduje(pierwszy) == true)
                        {
                            Console.WriteLine("Znaleziono cykl o długości większej od minimalnego stopnia grafu. Biegnie on następującymi wierzchołkami:");
                            string wynik = "";
                            foreach(int i in l.dajWierzcholki())
                            {
                                wynik += i;
                                wynik += ", ";
                            }
                            Console.WriteLine(wynik);
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            l.skroc();
                        }
                    }
                    else
                    {
                        flaga = false;
                    }
                }
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

        public static wierzcholek dajWierzcholek2(List<wierzcholek> lista, int a)
        {
            foreach(wierzcholek w in lista)
            {
                if (w.dajNumer() == a)
                {
                    return w;
                }
            }
            return null;
        }
    }
}