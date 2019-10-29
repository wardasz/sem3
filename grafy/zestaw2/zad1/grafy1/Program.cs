using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace grafy1
{
    class Program
    {

        static void Main(string[] args)
        {
            List<List<int>> maciez = new List<List<int>>();

            var s = new FileInfo(Directory.GetCurrentDirectory());
            var s2 = s.Directory.Parent.Parent;
            String sciezka = s2.ToString() + "\\dane.csv";

            using (var reader = new StreamReader(sciezka))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    List<int> zad = new List<int>();
                    for (int x = 0; x < values.Length; x++)
                    {
                        zad.Add(Convert.ToInt32(values[x]));
                    }
                    maciez.Add(zad);
                }
            }

            bool flaga = true;
            foreach (List<int> zad in maciez)
            {
                foreach (int i in zad)
                {
                    if (i == 0 || i == 1)
                    {
                    }
                    else
                    {
                        flaga = false;
                    }
                }
            }

            if (flaga == false)
            {
                Console.WriteLine("Złe dane wejściowe (graf z pętlami lub wielokrotnymi krawędziami)");
            }
            else
            {
                Boolean byly = false;
                for (int x = 1; x <= maciez.Count; x++)
                {
                    List<int> l = dajWiezcholek(maciez, x);
                    l = dajSasiadow(l);
                    foreach(int i in l)
                    {
                        List<int> t = znajdzTrzecich(maciez, x, i);
                        foreach(int a in t)
                        {
                            List<int> p = dajWiezcholek(maciez, a);
                            p = dajSasiadow(p);
                            if(naLiscie(p, x) == true)
                            {
                                Console.WriteLine("Podgraf izomorficzny do C3 na wierzchołkach " + x + ", " + i + " i " + a);
                                byly = true;
                            }
                        }
                    }
                }
                if (byly == false) Console.WriteLine("Brak podgrafów izomorficznych do C3 w grafie");
            }





            Console.ReadKey();
        }
        public static List<int> dajWiezcholek(List<List<int>> maciez, int a)
        {
            a--;
            List<int> zad = maciez.ElementAt(a);
            return zad;
        }

        public static List<int> dajSasiadow(List<int> zad)
        {
            List<int> wynik = new List<int>();
            for (int x = 1; x <= zad.Count; x++)
            {
                int i = x - 1;
                if (zad.ElementAt(i) == 1)
                {

                    wynik.Add(x);
                }
            }
            return wynik;
        }

        public static List<int> znajdzTrzecich(List<List<int>> maciez, int pierwszy, int drugi)
        {
            List<int> wynik = new List<int>();
            List<int> l = dajWiezcholek(maciez, drugi);
            l = dajSasiadow(l);

            foreach(int i in l)
            {
                if (i != pierwszy)
                {
                    if(naLiscie(wynik, i) == false)
                    {
                        wynik.Add(i);
                    }
                }
            }

            return wynik;
        }

        public static Boolean naLiscie(List<int> lista, int element)
        {
            foreach(int i in lista)
            {
                if (i == element) return true;
            }
            return false;
        }

    }    
}
