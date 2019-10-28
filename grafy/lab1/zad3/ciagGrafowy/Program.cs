using System;
using System.Collections.Generic;
using System.Linq;

namespace ciagGrafowy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witam w analizatorze ciągów grafowych by wardasz");
            Console.WriteLine("Podaj ciąg:");
            string ciag = Console.ReadLine();

            List<wierzcholek> elementy = new List<wierzcholek>();
            var czesci = ciag.Split(',');
            for (int x = 0; x < czesci.Length; x++)
            {
                int a = x + 1;
                int b = Convert.ToInt32(czesci[x]);
                elementy.Add(new wierzcholek(a, b));
            }

            while (suma(elementy) > 0)
            {
                elementy.Sort((a, b) => (a.porownaj(b)));
                wierzcholek kasowany = elementy.ElementAt(0);
                int ile = kasowany.dajAktualny();
                kasowany.zeruj();
                int kasowanyNumer = kasowany.dajNumer();

                for(int x = 1; x<=ile; x++)
                {
                    wierzcholek roboczy = elementy.ElementAt(x);
                    int roboczyNumer = roboczy.dajNumer();
                    roboczy.obniz();
                    kasowany.dodajSasiada(roboczyNumer);
                    roboczy.dodajSasiada(kasowanyNumer);
                }               

                foreach(wierzcholek w in elementy)
                {
                    if (w.dajAktualny() < 0)
                    {
                        Console.WriteLine("Podany ciąg nie jest ciągiem grafowym");
                        Console.ReadKey();
                        return;
                    }
                }
            }

            //tworzenie maciezy
            List<List<int>> maciez = new List<List<int>>();
            for(int x = 0; x< elementy.Count; x++)
            {
                List<int> nowa = new List<int>();
                for (int y = 0; y < elementy.Count; y++)
                {
                    nowa.Add(0);
                }
                maciez.Add(nowa);
            }
            
            foreach(wierzcholek w in elementy)
            {
                int numer = w.dajNumer();
                numer--;
                List<int> sasiedzi = w.dajSasiadow();
                foreach(int i in sasiedzi)
                {
                    int n = i - 1;
                    maciez.ElementAt(numer)[n]++;
                }
            }

            //wypisywanie
            int rozmiar = maciez.Count;
            string szczyt = "  | ";
            string linia = "--+";
            for (int x = 1; x <= rozmiar; x++)
            {
                szczyt += x;
                if (x < 10) szczyt += " ";
                szczyt += " ";
                linia += "---";
            }
            Console.WriteLine(szczyt);
            Console.WriteLine(linia);

            int licznik = 1;
            foreach (List<int> zad in maciez)
            {
                string kolejna = licznik.ToString();
                if (licznik < 10) kolejna += " ";
                kolejna += "|";
                foreach (int pole in zad)
                {
                    kolejna += " ";
                    kolejna += pole.ToString();
                    if (pole < 10) kolejna += " ";
                }
                Console.WriteLine(kolejna);
                licznik++;
            }

            Console.ReadKey();
        }

        static int suma(List<wierzcholek> lista)
        {
            int wynik = 0;
            //Console.WriteLine("sumujemy");
            foreach(wierzcholek w in lista)
            {
                //Console.WriteLine(w.dajAktualny());
                wynik = wynik + w.dajAktualny();
            }
            //Console.WriteLine("Suma " + wynik);
            return wynik;
        }
    }
}
