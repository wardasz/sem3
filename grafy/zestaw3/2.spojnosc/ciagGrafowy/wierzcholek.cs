using System;
using System.Collections.Generic;
using System.Text;

namespace ciagGrafowy
{
    class wierzcholek
    {
        private int numer;
        private List<int> sasiedzi;

        public wierzcholek(int a)
        {
            numer = a;
            sasiedzi = new List<int>();
        }

        public int dajNumer()
        {
            return numer;
        }

        public List<int> dajSasiadow()
        {
            return sasiedzi;
        }

        public void dodajSasiada(int a)
        {
            sasiedzi.Add(a);
        }

        public void napisz()
        {
            string wynik = "Wierzchołek " + numer + " sąsiadujący z wierzchołkami: ";
            foreach(int i in sasiedzi)
            {
                wynik += i;
                wynik += ", ";
            }
            Console.WriteLine(wynik);
        }

    }
}