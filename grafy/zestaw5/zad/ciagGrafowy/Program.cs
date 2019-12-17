using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ciagGrafowy
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> macierz = new List<List<int>>();

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
                    macierz.Add(zad);
                }
            }

            bool flaga = true;
            for (int x = 1; x <= macierz.Count; x++)
            {
                for (int y = x + 1; y <= macierz.Count; y++)
                {
                    int jeden = dajPole(macierz, x, y);
                    int dwa = dajPole(macierz, y, x);
                    if (jeden != dwa) flaga = false;
                }
            }
            if (flaga == false)
            {
                Console.WriteLine("Podana macierz nie jest grafem");
                Console.ReadKey();
                return;
            }

            flaga = true;
            foreach (List<int> zad in macierz)
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
                Console.WriteLine("Podana macierz reprezentuje graf, który nie jest grafem prostym");
                Console.ReadKey();
                return;
            }

            List<wierzcholek> wierzcholki = new List<wierzcholek>();
            for(int numer = 1; numer<= macierz.Count; numer++)
            {
                wierzcholek nowy = new wierzcholek(numer);
                List<int> zad = macierz.ElementAt((numer - 1));
                for(int i = 1; i <= zad.Count; i++)
                {
                    int a = i-1;
                    if (zad.ElementAt(a) == 1)
                    {
                        nowy.dodajSasiada(i);
                    }
                }
                wierzcholki.Add(nowy);
            }
            foreach (wierzcholek w in wierzcholki)
            {
                w.napisz();
            }
            Console.WriteLine();

            foreach (wierzcholek w in wierzcholki)
            {
                if (w.dajKolor() == 0)
                {
                    List<int> kolejka = new List<int>();
                    kolejka.Add(w.dajNumer());
                    w.zaznacz();
                    w.koloruj(1);
                    while (kolejka.Count != 0)
                    {
                        wierzcholek robiony = wybierzWierzcholek(wierzcholki, kolejka.ElementAt(0));
                        List<int> sasiedzi = robiony.dajSasiadow();
                        int obecny = robiony.dajKolor();
                        int nastepny;
                        if (obecny == 1)
                        {
                            nastepny = 2;
                        }
                        else
                        {
                            nastepny = 1;
                        }
                        foreach (int i in sasiedzi)
                        {
                            wierzcholek kolorowany = wybierzWierzcholek(wierzcholki, i);
                            if (kolorowany.koloruj(nastepny) == false)
                            {
                                Console.WriteLine("Graf nie jest dwudzielny");
                                Console.ReadKey();
                                return;
                            }
                            if (kolorowany.dajFlage() == false)
                            {
                                kolorowany.zaznacz();
                                kolejka.Add(i);
                            }
                        }
                        kolejka.Remove(kolejka.ElementAt(0));
                    }
                }
            }

            Console.WriteLine("Graf jest dwudzielny");
            foreach(wierzcholek w in wierzcholki)
            {
                w.napisz();
            }

            List<sciezka> siec = new List<sciezka>();
            sciezka nowa;
            foreach(wierzcholek w in wierzcholki)
            {
                w.odznacz();
                if (w.dajKolor() == 1)
                {
                    foreach(int i in w.dajSasiadow())
                    {
                        nowa = new sciezka(w.dajNumer().ToString(), i.ToString(), 1);
                        siec.Add(nowa);
                    }
                    nowa = new sciezka("z", w.dajNumer().ToString(), 1);
                    siec.Add(nowa);
                }
                else
                {
                    nowa = new sciezka(w.dajNumer().ToString(), "u", 1);
                    siec.Add(nowa);
                }
            }
            Console.WriteLine();
            foreach(sciezka sc in siec)
            {
                sc.napisz();
            }
            Console.WriteLine();
            
            while (true)
            {
                List<sciezka> rezywualna = zrobSiecRezywualna(siec);
                List<string> sciezkaPowiekszajaca = BFS(rezywualna);
                if (sciezkaPowiekszajaca == null) break;                

                int maksimum = 1;
                bool znalazl;             
                for (int x = 0; x < (sciezkaPowiekszajaca.Count) - 1; x++)
                {
                    znalazl = false;
                    foreach (sciezka r in siec)
                    {
                        if (r.skad() == sciezkaPowiekszajaca.ElementAt(x) && r.dokad() == sciezkaPowiekszajaca.ElementAt(x + 1))
                        {
                            znalazl = true;
                            r.powiekszPrzeplyw(maksimum);
                        }
                    }
                    if (znalazl == false)
                    {
                        foreach (sciezka r in siec)
                        {
                            if (r.dokad() == sciezkaPowiekszajaca.ElementAt(x) && r.skad() == sciezkaPowiekszajaca.ElementAt(x + 1))
                            {
                                r.pomniejszPrzeplyw(maksimum);
                            }
                        }
                    }
                }
            }

            int licznik = 0;
            foreach(sciezka sc in siec)
            {
                if(sc.skad()!="z" && sc.dokad()!="u" && sc.dajPrzeplyw() == 1)
                {
                    Console.WriteLine("Krawędz skojarzenia łącząca wierzchołki " + sc.skad() + " i " + sc.dokad());
                    licznik++;
                }
            }
            Console.WriteLine("Maksymalne skojarzenie zawiera " + licznik + " krawędzi.");
            Console.ReadKey();
        }

        public static int dajPole(List<List<int>> macierz, int a, int b)
        {
            List<int> zad = dajWiezcholek(macierz, a);
            b--;
            int i = zad.ElementAt(b);
            return i;
        }

        public static List<int> dajWiezcholek(List<List<int>> macierz, int a)
        {
            a--;
            List<int> zad = macierz.ElementAt(a);
            return zad;
        }

        public static wierzcholek wybierzWierzcholek(List<wierzcholek> lista, int a)
        {
            foreach (wierzcholek w in lista)
            {
                if (w.dajNumer() == a) return w;
            }
            return null;
        }

        public static List<sciezka> zrobSiecRezywualna(List<sciezka> siec)
        {
            List<sciezka> rezywualna = new List<sciezka>();
            foreach (sciezka r in siec)
            {
                int przeplyw = r.dajPrzeplyw();
                int rezerwa = r.wolnyPrzeplyw();
                string a = r.skad();
                string b = r.dokad();

                if (przeplyw != 0)
                {
                    sciezka powrot = new sciezka(b, a, przeplyw);
                    rezywualna.Add(powrot);
                }
                if (rezerwa != 0)
                {
                    sciezka dodatek = new sciezka(a, b, rezerwa);
                    rezywualna.Add(dodatek);
                }
            }
            return rezywualna;
        }

        public static List<string> BFS(List<sciezka> lista)
        {
            List<List<string>> sciezki = new List<List<string>>();
            List<string> pierwsza = new List<string>();
            string start = "z";
            pierwsza.Add(start);
            sciezki.Add(pierwsza);
            while (true)
            {
                List<List<string>> sciezkiNowe = new List<List<string>>();
                foreach (List<string> obecna in sciezki)
                {
                    string koniec = obecna.Last();
                    foreach (sciezka r in lista)
                    {
                        if (r.skad() == koniec)
                        {
                            bool czyJuzJest = false;
                            foreach (string s in obecna)
                            {
                                if (s == r.dokad()) czyJuzJest = true;
                            }
                            if (czyJuzJest == false)
                            {
                                if (r.dokad() == "u")
                                {
                                    obecna.Add("u");
                                    return obecna;
                                }
                                List<string> nowa = new List<string>();
                                foreach (string s in obecna)
                                {
                                    nowa.Add(s);
                                }
                                nowa.Add(r.dokad());
                                sciezkiNowe.Add(nowa);
                            }
                        }
                    }
                }
                if (sciezkiNowe.Count == 0)
                {
                    return null;
                }
                else
                {
                    sciezki = sciezkiNowe;
                }
            }
        }
    }
}