using System;
using System.Collections.Generic;
using System.Text;

namespace ciagGrafowy
{
    class krawedz
    {
        private int a;
        private int b;
        private int waga;

        public krawedz(int x, int y, int z)
        {
            a = x;
            b = y;
            waga = z;
        }

        public int dajA()
        {
            return a;
        }

        public int dajB()
        {
            return b;
        }

        public int dajWage()
        {
            return waga;
        }

        public int porownaj(krawedz a)
        {
            if (waga > a.dajWage())
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public void napisz()
        {
            Console.WriteLine("Krawędź z wierzchołka " + a + " do wierzchołka " + b + " o wadze " + waga + ".");
        }
    }
}
