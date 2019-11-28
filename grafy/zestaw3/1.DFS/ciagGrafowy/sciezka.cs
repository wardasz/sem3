using System;
using System.Collections.Generic;
using System.Text;

namespace ciagGrafowy
{
    class sciezka
    {
        private int a;
        private int b;

        public sciezka(int x, int y)
        {
            a = x;
            b = y;
        }

        public string opis()
        {
            string wynik = "{" + a + "," + b + "}";
            return wynik;
        }
    }
}
