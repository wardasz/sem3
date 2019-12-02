using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ciagGrafowy
{
    class zadanie
    {
        private int numer;
        private int czas;
        private int natychmiastowyStart;
        private int poznyStart;
        private int natychmiastowyKoniec;
        private int poznyKoniec;
        private List<int> poprzednicy;
        private List<zadanie> popLink;
        private List<zadanie> nastLink;

        public zadanie(int a, int b, List<int> lista)
        {
            numer = a;
            czas = b;
            poprzednicy = lista;
            natychmiastowyStart = -1;
            natychmiastowyKoniec = -1;
            poznyStart = -1;
            poznyKoniec = -1;
        }

        public int dajNumer()
        {
            return numer;
        }
        public int dajCzas()
        {
            return czas;
        }
        public List<int> dajPoprzednikow()
        {
            return poprzednicy;
        }

        public void ustawPoprzednikow(List<zadanie> a)
        {
            popLink = a;
        }

        public void ustawNastepnikow(List<zadanie> a)
        {
            nastLink = a;
        }

        public void napisz()
        {
            string wynik = "Zadanie nr " + numer + " o czasie wykonania " + czas + ", wymagające zakończenia zadań: ";
            foreach(int i in poprzednicy)
            {
                wynik += i;
                wynik += ", ";
            }
            Console.WriteLine(wynik);
        }

        public void policzNatychmiastowe()
        {
            if (natychmiastowyStart == -1)
            {
                int najpozniejsze = 0;
                foreach (zadanie z in popLink)
                {
                    if (z.dajKoniec() > najpozniejsze) najpozniejsze = z.dajKoniec();
                }
                natychmiastowyStart = najpozniejsze;
                natychmiastowyKoniec = natychmiastowyStart + czas;
            }
        }

        public void dajPozne()
        {
            poznyKoniec = natychmiastowyKoniec;
            poznyStart = natychmiastowyStart;
        }

        public void policzPozne(int koniec)
        {
            if (poznyKoniec == -1)
            {
                int najwczesniejsze = koniec;
                foreach(zadanie z in nastLink)
                {
                    if (z.dajStart(koniec) < najwczesniejsze) najwczesniejsze = z.dajStart(koniec);
                }
                poznyKoniec = najwczesniejsze;
                poznyStart = najwczesniejsze - czas;
            }
        }

        public void napisz2()
        {
            Console.WriteLine("Zadanie numer " + numer + " może rozpocząć się już w  " + natychmiastowyStart + " a zakończyć w " + natychmiastowyKoniec + " jednostce czasu.");
            Console.WriteLine("Musi być wykonane przed " + poznyKoniec + " jc, więc trzeba je rozpocząc najpóźniej w " + poznyStart + ".");
        }

        public int dajKoniec()
        {
            if (natychmiastowyKoniec == -1) policzNatychmiastowe();
            return natychmiastowyKoniec;
        }

        public int dajStart(int koniec)
        {
            if (poznyStart == -1) policzPozne(koniec);
            return poznyStart;
        }
    }
}
