using System;
using System.Collections.Generic;
using System.Text;

namespace ciagGrafowy
{
    class wierzcholek
    {
        private int numer;
        private int stopienMax;
        private int stopienAkt;
        private List<int> sasiedzi;

        public wierzcholek(int a, int b)
        {
            numer = a;
            stopienMax = b;
            stopienAkt = b;
            sasiedzi = new List<int>();
        }

        public int dajAktualny()
        {
            return stopienAkt;
        }

        public int dajMaksymalny()
        {
            return stopienMax;
        }

        public int dajNumer()
        {
            return numer;
        }

        public void dodajSasiada(int a)
        {
            sasiedzi.Add(a);
        }

        public List<int> dajSasiadow()
        {
            return sasiedzi;
        }

        public void zeruj()
        {
            stopienAkt = 0;
        }

        public void obniz()
        {
            stopienAkt--;
        }

        public int porownaj(wierzcholek a)
        {
            if (stopienAkt > a.dajAktualny())
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
