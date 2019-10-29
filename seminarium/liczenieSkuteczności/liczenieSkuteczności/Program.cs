using System;
using System.Collections.Generic;
using System.IO;

namespace liczenieSkuteczności
{
    class Program
    {

        //public static String adresTekst = "D:\\studia\\magisterka\\sem3\\seminarium\\oczyszczony.txt";    //pc
        public static String adresTekst = "D:\\studia\\sem3\\seminarium\\oczyszczony.txt";                  //laptop

        static void Main(string[] args)
        {
            List<znak> znaki = new List<znak>();
            Console.SetWindowSize(120, 30);

            var s = new FileInfo(Directory.GetCurrentDirectory());
            var s2 = s.Directory.Parent.Parent;
            String sciezka = s2.ToString() + "\\wikipedia.csv";
            //String sciezka = s2.ToString() + "\\discord.csv";

            using (var reader = new StreamReader(sciezka))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    znak z = new znak(values[0], values[1]);
                    znaki.Add(z);
                }
            }

            int ileZnakow = 0;
            int ileInnych = 0;
            int dlugoscNorma = 0;
            int dlugoscKrotka = 0;
            int dlugoscHuffman = 0;

            string tekst = System.IO.File.ReadAllText(adresTekst);
            foreach(char c in tekst)
            {
                ileZnakow++;
                string a = c.ToString();
                bool czy = false;
                
                foreach(znak z in znaki){
                    if (z.dajZnak() == a)
                    {
                        czy = true;
                        dlugoscNorma += 8;
                        dlugoscKrotka += 5;
                        dlugoscHuffman += z.dajKod().Length;
                        break;
                    }
                }
                if (czy == false)
                {
                    ileInnych++;
                }
            }

            double procentNorma = ((double)dlugoscHuffman / (double)dlugoscNorma)*100;
            double procentKrotka = ((double)dlugoscHuffman / (double)dlugoscKrotka)*100;

            Console.WriteLine("Tekst zawierał " + ileZnakow + " znaków, z czego " + ileInnych + " nie zostało rozpoznanych.");
            Console.WriteLine("W normalnych warunkach rozpoznane znaki zostały by zapisane na " + dlugoscNorma + " bitach, w wersji skróconej na " + dlugoscKrotka + ".");
            Console.WriteLine("Dzięki konwersji ta liczba spadła do " + dlugoscHuffman + " bitów.");
            Console.WriteLine("Liczba ta stanowi odpowiednio " + procentNorma + " i " + procentKrotka + " procent poprzedniego wyniku.");

            Console.ReadKey();
        }
    }
}
