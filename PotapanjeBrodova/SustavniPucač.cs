using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class SustavniPucač : IPucač
    {
        public SustavniPucač(IEnumerable<Polje> pogođena, Mreža mreža)
        {
            pogođenaPolja.AddRange(pogođena);
            pogođenaPolja.Sort((a, b) => a.Redak - b.Redak + a.Stupac - b.Stupac);
            this.mreža = mreža;
        }

        #region Implementacija sučelja IPucač

        public Polje UputiPucanj()
        {
            Orijentacija o = DajOrijentaciju();
            var liste = DajPoljaUNastavku(o);
            // sortiraj dobivene liste po duljinama te ih grupiraj
            liste.Sort((lista1, lista2) => lista2.Count() - lista1.Count());
            var grupe = liste.GroupBy(lista => lista.Count());
            // uzmi najdulju listu, a ako ih ima više onda slučajni odabir
            var najdulji = grupe.First();
            int indeks = najdulji.Count() == 1 ? 0 : slučajni.Next(najdulji.Count());
            zadnjeGađano = najdulji.ElementAt(indeks).First();
            mreža.EliminirajPolje(zadnjeGađano);

            return zadnjeGađano;
        }

        public void EvidentirajRezultat(RezultatGađanja rezultat)
        {
            if (rezultat == RezultatGađanja.Promašaj)
                return;
            pogođenaPolja.Add(zadnjeGađano);
            pogođenaPolja.Sort((a, b) => a.Redak - b.Redak + a.Stupac - b.Stupac);
        }

        public IEnumerable<Polje> PogođenaPolja
        {
            get { return pogođenaPolja; }
        }

        #endregion Implementacija sučelja IPucač

        private Orijentacija DajOrijentaciju()
        {
            if (pogođenaPolja[0].Redak == pogođenaPolja[1].Redak)
                return Orijentacija.Horizontalno;
            return Orijentacija.Vertikalno;
        }

        private List<IEnumerable<Polje>> DajPoljaUNastavku(Orijentacija orijentacija)
        {
            switch (orijentacija)
            {
                case Orijentacija.Vertikalno:
                    return DajPoljaUNastavku(Smjer.Gore, Smjer.Dolje);
                case Orijentacija.Horizontalno:
                    return DajPoljaUNastavku(Smjer.Lijevo, Smjer.Desno);
                default:
                    throw new NotImplementedException();
            }
        }

        private List<IEnumerable<Polje>> DajPoljaUNastavku(Smjer smjer1, Smjer smjer2)
        {
            List<IEnumerable<Polje>> liste = new List<IEnumerable<Polje>>();
            int redak0 = pogođenaPolja[0].Redak;
            int stupac0 = pogođenaPolja[0].Stupac;
            var l1 = mreža.DajPoljaUZadanomSmjeru(redak0, stupac0, smjer1);
            if (l1.Count() > 0)
                liste.Add(l1);
            int n = pogođenaPolja.Count() - 1;
            int redakN = pogođenaPolja[n].Redak;
            int stupacN = pogođenaPolja[n].Stupac;
            var l2 = mreža.DajPoljaUZadanomSmjeru(redakN, stupacN, smjer2);
            if (l2.Count() > 0)
                liste.Add(l2);
            return liste;
        }

        List<Polje> pogođenaPolja = new List<Polje>();
        private Polje zadnjeGađano;
        Mreža mreža;
        Random slučajni = new Random();

    }
}
