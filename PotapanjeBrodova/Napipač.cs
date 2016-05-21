using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class Napipač : IPucač
    {
        public Napipač(Mreža mreža, int duljinaBroda)
        {
            this.mreža = mreža;
            this.duljinaBroda = duljinaBroda;
        }

        #region Implementacija sučelja IPucač

        public Polje UputiPucanj()
        {
            List<Polje> polja = DajKandidateZaHorizontalniBrod().ToList();
            polja.AddRange(DajKandidateZaVertikalniBrod());

            int indeks = slučajni.Next(0, polja.Count());
            zadnjeGađano = polja[indeks];
            mreža.EliminirajPolje(zadnjeGađano);
            return zadnjeGađano;
        }

        public void EvidentirajRezultat(RezultatGađanja rezultat)
        {
        }

        public IEnumerable<Polje> PogođenaPolja
        {
            get
            {
                Debug.Assert(zadnjeGađano != null);
                return new List<Polje> { zadnjeGađano };
            }
        }

        #endregion Implementacija sučelja IPucač

        public IEnumerable<Polje> DajKandidateZaHorizontalniBrod()
        {
            List<Polje> kandidati = new List<Polje>();
            var slobodna = mreža.DajSlobodnaPolja();
            for (int r = 0; r < mreža.Redaka; ++r)
            {
                int brojačPolja = 0;
                for (int s = 0; s < mreža.Stupaca; ++s)
                {
                    Polje p = new Polje(r, s);
                    if (slobodna.Contains(p))
                        ++brojačPolja;
                    else
                        brojačPolja = 0;

                    if (brojačPolja >= duljinaBroda)
                    {
                        for (int ss = s - duljinaBroda + 1; ss <= s; ++ss)
                            kandidati.Add(new Polje(r, ss));
                    }
                }
            }
            return kandidati;
        }

        public IEnumerable<Polje> DajKandidateZaVertikalniBrod()
        {
            List<Polje> kandidati = new List<Polje>();
            var slobodna = mreža.DajSlobodnaPolja();
            for (int s = 0; s < mreža.Stupaca; ++s)
            {
                int brojačPolja = 0;
                for (int r = 0; r < mreža.Redaka; ++r)
                {
                    Polje p = new Polje(r, s);
                    if (slobodna.Contains(p))
                        ++brojačPolja;
                    else
                        brojačPolja = 0;

                    if (brojačPolja >= duljinaBroda)
                    {
                        for (int rr = r - duljinaBroda + 1; rr <= r; ++rr)
                            kandidati.Add(new Polje(rr, s));
                    }
                }
            }
            return kandidati;
        }

        private Mreža mreža;
        private Polje zadnjeGađano;
        private int duljinaBroda;
        private Random slučajni = new Random();
    }
}
