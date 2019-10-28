using System;
using System.Collections.Generic;
using System.Text;

namespace liczenieSkuteczności
{
    class znak
    {
        private string symbol;
        private string kod; 

        public znak(string a, string b)
        {
            symbol = a;
            kod = b;
        }

        public string dajZnak()
        {
            return symbol;
        }

        public string dajKod()
        {
            return kod;
        }

        public void napisz()
        {
            Console.WriteLine("znak " + symbol + " o kodzie " + kod);
        }
    }
}
