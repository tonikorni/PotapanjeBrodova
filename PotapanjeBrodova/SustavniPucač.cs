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
            pogođenaPolja = new List<Polje>(pogođena);
            pogođenaPolja.Sort((a, b) => a.Redak - b.Redak + a.Stupac - b.Stupac);
            this.mreža = mreža;
        }

        public Polje UputiPucanj()
        {
            Orijentacija o = DajOrijentaciju();
            var liste = DajPoljaUNastavku(o);
            if (liste.Count() == 1)
                return liste.First().First();
            int indeks = slučajni.Next(liste.Count());
            return liste.ElementAt(indeks).First();
        }

        private Orijentacija DajOrijentaciju()
        {
            if (pogođenaPolja[0].Redak == pogođenaPolja[1].Redak)
                return Orijentacija.Horizontalno;
            return Orijentacija.Vertikalno;
        }

        private IEnumerable<IEnumerable<Polje>> DajPoljaUNastavku(Orijentacija orijentacija)
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

        private IEnumerable<IEnumerable<Polje>> DajPoljaUNastavku(Smjer smjer1, Smjer smjer2)
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

        List<Polje> pogođenaPolja;
        Mreža mreža;
        Random slučajni = new Random();
    }
}
