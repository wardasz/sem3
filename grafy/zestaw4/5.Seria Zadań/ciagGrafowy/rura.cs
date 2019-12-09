using System;
using System.Collections.Generic;
using System.Text;

namespace ciagGrafowy
{
    class rura
    {
        private string Z;
        private string DO;
        private int przepustowosc;
        private int przeplyw;

        public rura(string a, string b, int c)
        {
            Z = a;
            DO = b;
            przepustowosc = c;
            przeplyw = 0;
        }

        public string skad()
        {
            return Z;
        }

        public string dokad()
        {
            return DO;
        }

        public int dajPrzepustowosc()
        {
            return przepustowosc;
        }

        public int dajPrzeplyw()
        {
            return przeplyw;
        }

        public int wolnyPrzeplyw()
        {
            return przepustowosc - przeplyw;
        }

        public void powiekszPrzeplyw(int i)
        {
            przeplyw += i;
        }

        public void pomniejszPrzeplyw(int i)
        {
            przeplyw -= i;
        }

        public void napisz()
        {
            Console.WriteLine("Rura z punkt " + Z + " do " + DO + " o maksymalnej przepustowości równej " + przepustowosc + " i przepływie " + przeplyw);
        }
    }
}
