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
            List<zadanie> zadania = new List<zadanie>();

            var s = new FileInfo(Directory.GetCurrentDirectory());
            var s2 = s.Directory.Parent.Parent;
            String sciezka = s2.ToString() + "\\dane.csv";

            int licznik = 1;
            using (var reader = new StreamReader(sciezka))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    List<int> linia = new List<int>();
                    int masa=0;
                    for (int x = 0; x < values.Length; x++)
                    {
                        int i = Convert.ToInt32(values[x]);
                        if (x == 0)
                        {
                            masa = i;
                        }
                        else
                        {
                            linia.Add(i);
                        }
                    }
                    zadanie nowe = new zadanie(licznik, masa, linia);
                    zadania.Add(nowe);
                    licznik++;
                }
            }

            foreach(zadanie z in zadania)
            {
                z.napisz();
            }

            foreach(zadanie z in zadania)
            {
                List<zadanie> poprzednicy = new List<zadanie>();
                foreach(int i in z.dajPoprzednikow())
                {
                    foreach(zadanie zad in zadania)
                    {
                        if (zad.dajNumer() == i)
                        {
                            poprzednicy.Add(zad);
                            break;
                        }
                    }
                }
                z.ustawPoprzednikow(poprzednicy);
            }

            foreach(zadanie z in zadania)
            {
                List<zadanie> nastepnicy = new List<zadanie>();
                int numer = z.dajNumer();
                foreach(zadanie zad in zadania)
                {
                    foreach(int i in zad.dajPoprzednikow())
                    {
                        if (i == numer)
                        {
                            nastepnicy.Add(zad);
                            break;
                        }
                    }
                }
                z.ustawNastepnikow(nastepnicy);
            }

            foreach (zadanie z in zadania)
            {
                z.policzNatychmiastowe();
            }

            int koniec = 0;
            foreach (zadanie z in zadania)
            {
                if (z.dajKoniec() > koniec) koniec = z.dajKoniec();
            }
            Console.WriteLine();
            Console.WriteLine("Minimalny czas wykonania procedury wynosi " + koniec);
            foreach (zadanie z in zadania)
            {
                if (z.dajKoniec() == koniec) z.dajPozne();
            }

            foreach (zadanie z in zadania)
            {
                z.policzPozne(koniec);
            }

            Console.WriteLine();
            foreach (zadanie z in zadania)
            {
                z.napisz2();
            }

            Console.ReadKey();
        }
    }
}