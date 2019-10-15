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
                    case "3":
                        maxMin(maciez);
                        break;
                    case "4":
                        stopien(maciez);
                        break;
                    case "5":
                        stopnie(maciez);
                        break;
                    case "6":
                        dodajWierzcholek(maciez);
                        break;
                    case "7":
                        dodajLuk(maciez);
                        break;
                    case "8":
                        usunWierzcholek(maciez);
                        break;
                    case "9":
                        usunLuk(maciez);
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

        static void usunLuk(List<List<int>> maciez)
        {
            Console.WriteLine("Podaj wierzchołki które usuwana krawędź łączy");
            int a = Convert.ToInt32(Console.ReadLine());
            int b = Convert.ToInt32(Console.ReadLine());
            if(a>maciez.Count || b > maciez.Count)
            {
                Console.WriteLine("Podano niewłaściwe wierzchołki");
                return;
            }
            a--;
            b--;
            if (maciez.ElementAt(a).ElementAt(b) == 0)
            {
                Console.WriteLine("Podano niepołączone wierzcholki");
                return;
            }
            maciez.ElementAt(a)[b]--;
            maciez.ElementAt(b)[a]--;

        }

        static void usunWierzcholek(List<List<int>> maciez)
        {
            Console.WriteLine("Podaj wierzchołek które chcesz usunąć");
            int a = Convert.ToInt32(Console.ReadLine());
            if (a > maciez.Count)
            {
                Console.WriteLine("Podano niewłaściwy wierzchołek");
                return;
            }
            a--;
            maciez.RemoveAt(a);
            foreach (List<int> zad in maciez)
            {
                zad.RemoveAt(a);
            } 
        }

        static void dodajLuk(List<List<int>> maciez)
        {
            Console.WriteLine("Podaj wierzchołki które chcesz połączyć");
            int a = Convert.ToInt32(Console.ReadLine());
            int b = Convert.ToInt32(Console.ReadLine());
            if (a > maciez.Count || b > maciez.Count)
            {
                Console.WriteLine("Podano niewłaściwe wierzchołki");
                return;
            }
            a--;
            b--;
            maciez.ElementAt(a)[b]++;
            maciez.ElementAt(b)[a]++;
        }

        static void dodajWierzcholek(List<List<int>> maciez)
        {
            int ile = maciez.Count;
            ile++;
            foreach(List<int> zad in maciez)
            {
                zad.Add(0);
            }
            List<int> nowy = new List<int>();
            for(int x = 0; x<ile; x++)
            {
                nowy.Add(0);
            }
            maciez.Add(nowy);
            Console.WriteLine("Dodano nowy wierzchołek o numerze " + ile);
        }

        static void stopnie(List<List<int>> maciez)
        {
            List<int> stopnie = new List<int>();
            foreach (List<int> zad in maciez)
            {
                int suma = 0;
                foreach (int pole in zad)
                {
                    suma += pole;
                }
                stopnie.Add(suma);
            }
            stopnie.Sort();
            stopnie.Reverse();
            string wynik = "Ciąg stopni wierzchołków: ";
            foreach(int i in stopnie)
            {
                wynik += i;
                wynik += ", ";
            }
            wynik = wynik.Remove(wynik.Length - 2);
            wynik += ".";
            Console.WriteLine(wynik);
        }

        static void stopien(List<List<int>> maciez)
        {
            Console.WriteLine("Podaj dla którego wierzchołka chcesz policzyć stopień");
            int numer = Convert.ToInt32(Console.ReadLine());
            if (numer > maciez.Count)
            {
                Console.WriteLine("Podano zły numer");
                return;
            }
            numer--;
            List<int> zad = maciez.ElementAt(numer);
            int suma = 0;
            foreach (int pole in zad)
            {
                suma += pole;
            }
            Console.WriteLine("Wierzchołek nr " + (numer + 1) + " ma stopień " + suma);
        }

        static void maxMin(List<List<int>> maciez)
        {
            int max = 0;
            int min = 0;
            foreach (List<int> zad in maciez)
            {
                int ile = 0;
                foreach (int pole in zad)
                {
                    ile += pole;
                }
                if (max == 0)
                {
                    max = ile;
                    min = ile;
                }
                else
                {
                    if (max < ile) max = ile;
                    if (min > ile) min = ile;
                }
            }
            Console.WriteLine("Maksymalny stopień grafu wynosi " + max + " a minimaly " + min);
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
            Console.WriteLine("3-znaleść watość minimalnego i maksymalnego stopnia grafu");
            Console.WriteLine("4-sprawdzić jaki stopień ma dany wierzchołek");
            Console.WriteLine("5-wypisać ciąg stopni wierzchołków");
            Console.WriteLine("6-dodaj nowy wierzchołek");
            Console.WriteLine("7-dodaj nową krawędź");
            Console.WriteLine("8-usuń wierzchołek");
            Console.WriteLine("9-usuń krawędź");

            string x = Console.ReadLine();
            return x;
        }
    }
}
