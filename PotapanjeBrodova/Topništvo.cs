using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public enum TaktikaGađanja
    {
        Napipavanje,
        Okruživanje,
        SustavnoUništavanje
    }

    public class Topništvo
    {
        public event EventHandler FlotaPotopljena;

        public Topništvo(int redaka, int stupaca, int[] duljineBrodova)
        {
            mreža = new Mreža(redaka, stupaca);
            this.duljineBrodova = new List<int>(duljineBrodova);
            this.duljineBrodova.Sort((d1, d2) => d2 - d1);
            PromijeniTaktikuUNapipavanje();
        }

        public Polje UputiPucanj()
        {
            return pucač.UputiPucanj();
        }

        public void ObradiGađanje(RezultatGađanja rezultat)
        {
            pucač.EvidentirajRezultat(rezultat);
            switch (rezultat)
            {
                case RezultatGađanja.Promašaj:
                    return;
                case RezultatGađanja.Pogodak:
                    PromijeniTaktikuUSlučajuPogotka();
                    return;
                case RezultatGađanja.Potonuće:
                    ZabilježiPotopljeniBrod();
                    if (duljineBrodova.Count > 0)
                        PromijeniTaktikuUNapipavanje();
                    return;
                default:
                    Debug.Assert(false, string.Format("Nepodržani rezultat gađanja {0}", rezultat.ToString()));
                    return;
            }
        }

        private void PromijeniTaktikuUSlučajuPogotka()
        {
            switch (TrenutnaTaktika)
            {
                case TaktikaGađanja.SustavnoUništavanje:
                    return;
                case TaktikaGađanja.Napipavanje:
                    PromijeniTaktikuUOkruživanje();
                    return;
                case TaktikaGađanja.Okruživanje:
                    PromijeniTaktikuUSustavnoUništavanje();
                    return;
                default:
                    Debug.Assert(false, string.Format("Nepodržana taktika {0}", TrenutnaTaktika.ToString()));
                    return;
            }
        }

        private void PromijeniTaktikuUNapipavanje()
        {
            TrenutnaTaktika = TaktikaGađanja.Napipavanje;
            pucač = new Napipač(mreža, duljineBrodova[0]);
        }

        private void PromijeniTaktikuUOkruživanje()
        {
            TrenutnaTaktika = TaktikaGađanja.Okruživanje;
            pucač = new KružniPucač(pucač.PogođenaPolja.First(), mreža);
        }

        private void PromijeniTaktikuUSustavnoUništavanje()
        {
            TrenutnaTaktika = TaktikaGađanja.SustavnoUništavanje;
            pucač = new SustavniPucač(pucač.PogođenaPolja, mreža);
        }

        private void ZabilježiPotopljeniBrod()
        {
            // makni potopljeni brod iz liste preostalih duljina brodova
            int duljinaPotopljenogBroda = pucač.PogođenaPolja.Count();
            duljineBrodova.Remove(duljinaPotopljenogBroda);
            // akoj je potopljen i zadnji broj, objavi to
            if (duljineBrodova.Count == 0)
                FlotaPotopljena?.Invoke(this, EventArgs.Empty);
        }

        public TaktikaGađanja TrenutnaTaktika
        {
            get; private set;
        }

        IPucač pucač;
        Mreža mreža;
        List<int> duljineBrodova;
    }
}
