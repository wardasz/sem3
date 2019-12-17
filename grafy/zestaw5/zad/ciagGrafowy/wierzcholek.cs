using System;
using System.Collections.Generic;
using System.Text;

namespace ciagGrafowy
{
    class wierzcholek
    {
        private int numer;
        private int kolor;
        private bool flaga;
        private List<int> sasiedzi;

        public wierzcholek(int a)
        {
            numer = a;
            kolor = 0;
            flaga = false;
            sasiedzi = new List<int>();
        }

        public int dajNumer()
        {
            return numer;
        }

        public bool dajFlage()
        {
            return flaga;
        }

        public void zaznacz()
        {
            flaga = true;
        }

        public void odznacz()
        {
            flaga = false;
        }

        public List<int> dajSasiadow()
        {
            return sasiedzi;
        }

        public void dodajSasiada(int a)
        {
            sasiedzi.Add(a);
        }

        public int dajKolor()
        {
            return kolor;
        }

        public bool koloruj(int a)
        {
            if(kolor == 0)
            {
                kolor = a;
                return true;
            }
            else
            {
                if(kolor == a)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void napisz()
        {
            string wynik = "Wierzchołek " + numer + " o kolorze " + kolor + " sąsiadujący z wierzchołkami: ";
            foreach(int i in sasiedzi)
            {
                wynik += i;
                wynik += ", ";
            }
            Console.WriteLine(wynik);
        }
    }
}