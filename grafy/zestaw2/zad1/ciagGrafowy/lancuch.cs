using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ciagGrafowy
{
    class lancuch
    {
        int dlugosc;
        List<int> wierzcholki;

        public lancuch(int a)
        {
            dlugosc = 1;
            wierzcholki = new List<int>();
            wierzcholki.Add(a);            
        }

        public lancuch(lancuch a, int b)
        {
            dlugosc = a.dajDlugosc();
            dlugosc++;
            wierzcholki = new List<int>();
            foreach(int i in a.dajWierzcholki())
            {
                wierzcholki.Add(i);
            }
            wierzcholki.Add(b);

        }
        
        public List<int> dajWierzcholki()
        {
            return wierzcholki;
        }

        public int dajDlugosc()
        {
            return dlugosc;
        }

        public bool czyW(int a)
        {
            foreach(int i in wierzcholki)
            {
                if (i == a) return true;
            }
            return false;
        }

        public int dajOstatni()
        {
            int ile = wierzcholki.Count;
            ile--;
            return wierzcholki.ElementAt(ile);
        }

        public int dajPierwszy()
        {
            return wierzcholki.ElementAt(0);
        }

        public void napisz()
        {
            string wynik = "Łańcuch o długości " + dlugosc + " zawierający wierzchołki: ";
            foreach(int i in wierzcholki)
            {
                wynik += i;
                wynik += ", ";
            }
            Console.WriteLine(wynik);
        }

        public void skroc()
        {
            wierzcholki.Remove(wierzcholki.ElementAt((dlugosc - 1)));
            dlugosc--;
        }

    }
}
