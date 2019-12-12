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
            List<rura> siec = new List<rura>();

            var s = new FileInfo(Directory.GetCurrentDirectory());
            var s2 = s.Directory.Parent.Parent;
            String sciezka = s2.ToString() + "\\dane.csv";
            
            using (var reader = new StreamReader(sciezka))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    rura nowa = new rura(values[0], values[1], Convert.ToInt32(values[2]));
                    siec.Add(nowa);
                }
            }
            
            foreach(rura r in siec)
            {
                r.napisz();
            }
            Console.WriteLine();
            
            bool zrobiono = false;
            int max = 0;

            while (true)
            {
                List<rura> rezywualna = zrobSiecRezywualna(siec);                
                List<string> sciezkaPowiekszajaca = BFS(rezywualna);
                if (sciezkaPowiekszajaca == null)
                {
                    break;
                }
                zrobiono = true;
                string opis = "Ściezka powiększająca: ";
                foreach (string sc in sciezkaPowiekszajaca)
                {
                    opis += sc;
                    opis += ", ";
                }
                int maksimum = -1;
                bool znalazl;
                for (int x = 0; x < (sciezkaPowiekszajaca.Count) - 1; x++)
                {
                    znalazl = false;
                    foreach (rura r in siec)
                    {
                        if (r.skad() == sciezkaPowiekszajaca.ElementAt(x) && r.dokad() == sciezkaPowiekszajaca.ElementAt(x + 1))
                        {
                            znalazl = true;
                            if (maksimum == -1)
                            {
                                maksimum = r.wolnyPrzeplyw();
                                break;
                            }
                            else
                            {
                                if (r.wolnyPrzeplyw() < maksimum)
                                {
                                    maksimum = r.wolnyPrzeplyw();
                                }
                                break;
                            }
                        }
                    }
                    if(znalazl == false)
                    {
                        foreach (rura r in siec)
                        {
                            if (r.dokad() == sciezkaPowiekszajaca.ElementAt(x) && r.skad() == sciezkaPowiekszajaca.ElementAt(x + 1))
                            {
                                if (r.dajPrzeplyw() < maksimum) maksimum = r.dajPrzeplyw();
                                break;
                            }
                        }
                    }
                }
                opis += "przepływ ścieżki równy ";
                opis += maksimum;
                Console.WriteLine(opis);
                for (int x = 0; x < (sciezkaPowiekszajaca.Count) - 1; x++)
                {
                    znalazl = false;
                    foreach (rura r in siec)
                    {
                        if (r.skad() == sciezkaPowiekszajaca.ElementAt(x) && r.dokad() == sciezkaPowiekszajaca.ElementAt(x + 1))
                        {
                            znalazl = true;
                            r.powiekszPrzeplyw(maksimum);
                        }
                    }
                    if (znalazl == false)
                    {
                        foreach (rura r in siec)
                        {
                            if (r.dokad() == sciezkaPowiekszajaca.ElementAt(x) && r.skad() == sciezkaPowiekszajaca.ElementAt(x + 1))
                            {
                                r.pomniejszPrzeplyw(maksimum);
                            }
                        }
                    }
                }
                max += maksimum;
            }

            if (zrobiono == true)
            {
                Console.WriteLine("Brak dalszych ścieżek powiększających.");
                Console.WriteLine("Maksymalny przepływ sieci wynosi " + max);
            }
            else
            {
                Console.WriteLine("Sieć nie ma ścieżki ze źródła do ujścia");
                Console.ReadKey();
                return;
            }
            Console.WriteLine();

            List<rura> rezywualna2 = zrobSiecRezywualna(siec);
            List<string> zrodlowe = BFS2(rezywualna2);
            List<string> ujsciowe = znajdzReszte(siec, zrodlowe);

            List<rura> rozciecie = new List<rura>();
            foreach(rura r in siec)
            {
                if (czyNaLiscie(zrodlowe, r.skad()) == true && czyNaLiscie(ujsciowe, r.dokad()) == true) rozciecie.Add(r);
            }
            Console.WriteLine("Minimalny przekroj:");
            foreach(rura r in rozciecie)
            {
                r.napisz();
            }

            Console.ReadKey();
        }

        public static List<rura> zrobSiecRezywualna(List<rura> siec)
        {
            List<rura> rezywualna = new List<rura>();
            foreach(rura r in siec)
            {            
                int przeplyw = r.dajPrzeplyw();
                int rezerwa = r.wolnyPrzeplyw();
                string a = r.skad();
                string b = r.dokad();

                if (przeplyw != 0)
                {
                    rura powrot = new rura(b, a, przeplyw);
                    rezywualna.Add(powrot);
                }
                if(rezerwa != 0)
                {
                    rura dodatek = new rura(a, b, rezerwa);
                    rezywualna.Add(dodatek);
                }
            }
            return rezywualna;
        }

        public static List<string> BFS(List<rura> lista)
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
                    foreach(rura r in lista)
                    {
                        if (r.skad() == koniec)
                        {
                            bool czyJuzJest = false;
                            foreach(string s in obecna)
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

        public static bool czyNaLiscie(List<string> lista, string element)
        {
            foreach(string s in lista)
            {
                if (s == element) return true;
            }
            return false;
        }

        public static List<string> BFS2(List<rura> lista)
        {
            List<string> rozwiazanie = new List<string>();
            List<string> stos = new List<string>();
            stos.Add("z");
            rozwiazanie.Add("z");
            string obecny;
            while (stos.Count > 0)
            {
                obecny = stos.ElementAt(0);
                foreach (rura r in lista)
                {
                    if (r.skad() == obecny && czyNaLiscie(rozwiazanie, r.dokad())==false)
                    {
                        stos.Add(r.dokad());
                        rozwiazanie.Add(r.dokad());
                    }
                }
                stos.Remove(obecny);
            }
            return rozwiazanie;
        }

        public static List<string> znajdzReszte(List<rura> lista, List<string> odciecie){
            List<string> rozwiazanie = new List<string>();
            foreach(rura r in lista)
            {
                string a = r.dokad();
                string b = r.skad();
                if (czyNaLiscie(odciecie, a) == false && czyNaLiscie(rozwiazanie, a) == false) rozwiazanie.Add(a);
                if (czyNaLiscie(odciecie, b) == false && czyNaLiscie(rozwiazanie, b) == false) rozwiazanie.Add(b);
            }
            return rozwiazanie;
        }

    }
}