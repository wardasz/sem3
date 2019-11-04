using System;
using System.Collections.Generic;
using System.Text;

namespace ciagGrafowy
{
    class wierzcholek
    {
        private int numer;
        private int stopien;
        private List<int> sasiedzi;

        public wierzcholek(int a)
        {
            numer = a;
            stopien = 0;
            sasiedzi = new List<int>();
        }

        public int dajStopien()
        {
            return stopien;
        }

        public int dajNumer()
        {
            return numer;
        }

        public void dodajSasiada(int a)
        {
            sasiedzi.Add(a);
            stopien++;
        }

        public List<int> dajSasiadow()
        {
            return sasiedzi;
        }

        public bool czySasiaduje(int a)
        {
            foreach(int i in sasiedzi)
            {
                if (a == i) return true;
            }
            return false;
        }


        public void napisz()
        {
            string wynik = "Wierzchołek " + numer + " o stopniu " + stopien + " sąsiadujący z wierzchołkami: ";
            foreach(int i in sasiedzi)
            {
                wynik += i;
                wynik += ", ";
            }
            Console.WriteLine(wynik);
        }

    }
}