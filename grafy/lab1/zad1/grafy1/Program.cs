using System;
using System.Collections.Generic;
using System.IO;

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
                    for (int x=0; x<values.Length; x++)
                    {
                        zad.Add(Convert.ToInt32(values[x]));
                    }
                    maciez.Add(zad);

                }
            }
            Console.WriteLine("Witam w analizatorze grafów by wardasz");

            while (true)
            {
                switch (menu())
                {
                    case "1":
                        wypisz(maciez);
                        break;
                    case "2":
                        pazyste(maciez);
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Zła liczba");
                        break;
                }
            }
        }

        static void pazyste(List<List<int>> maciez)
        {
            int pazyste = 0;
            int niepazyste = 0;
            foreach (List<int> zad in maciez)
            {
                int suma = 0;
                foreach (int pole in zad)
                {
                    suma += pole;
                }
                if (suma % 2 == 0)
                {
                    pazyste++;
                }
                else
                {
                    niepazyste++;
                }
            }
            Console.WriteLine(pazyste + " wierzchołków ma parzysty stopień, a " + niepazyste + " - nieparzysty.");
        }

        static void wypisz(List<List<int>> maciez)
        {
            foreach(List<int> zad in maciez)
            {
                Console.Write("[");
                foreach (int pole in zad)
                {
                    Console.Write(pole+" ");
                }
                Console.Write("]\n");
            }
        }

        static string menu()
        {
            Console.WriteLine("");
            Console.WriteLine("Co chcesz zrobić?");
            Console.WriteLine("1-wypisz graf");
            Console.WriteLine("2-sprawdź ile wieszchołków jest parzystych a ile nie");

            string x = Console.ReadLine();
            return x;
        }
    }
}
