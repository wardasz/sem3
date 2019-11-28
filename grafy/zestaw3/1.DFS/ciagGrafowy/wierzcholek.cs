using System;
using System.Collections.Generic;
using System.Text;

namespace ciagGrafowy
{
    class wierzcholek
    {
        private int numer;
        bool wDrzewie;
        private List<int> sasiedzi;

        public wierzcholek(int a)
        {
            numer = a;
            wDrzewie = false;
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

        public bool czyNaDrzewie()
        {
            return wDrzewie;
        }

        public void naDrzewo()
        {
            wDrzewie = true;
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