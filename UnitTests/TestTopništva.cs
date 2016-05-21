using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotapanjeBrodova;

namespace UnitTests
{
    [TestClass]
    public class TestTopništva
    {
        int[] duljineBrodova = { 1, 2, 3 };
        [TestMethod]
        public void Topništvo_PočetnaTaktikaGađanjaJeNapipavanje()
        {
            Topništvo t = new Topništvo(10, 10, duljineBrodova);
            Assert.AreEqual(TaktikaGađanja.Napipavanje, t.TrenutnaTaktika);
        }

        [TestMethod]
        public void Topništvo_TaktikaGađanjaNakonPrvogPogotkaJeOkruživanje()
        {
            Topništvo t = new Topništvo(10, 10, duljineBrodova);
            t.UputiPucanj();
            t.ObradiGađanje(RezultatGađanja.Pogodak);
            Assert.AreEqual(TaktikaGađanja.Okruživanje, t.TrenutnaTaktika);
        }

        [TestMethod]
        public void Topništvo_TaktikaGađanjaNakonDrugogPogotkaJeSustavnoUništavanje()
        {
            Topništvo t = new Topništvo(10, 10, duljineBrodova);
            t.UputiPucanj();
            t.ObradiGađanje(RezultatGađanja.Pogodak);
            t.UputiPucanj();
            t.ObradiGađanje(RezultatGađanja.Pogodak);
            Assert.AreEqual(TaktikaGađanja.SustavnoUništavanje, t.TrenutnaTaktika);
        }

        [TestMethod]
        public void Topništvo_TaktikaGađanjaNakonPotonućaJeNapipavanje()
        {
            Topništvo t = new Topništvo(10, 10, duljineBrodova);
            t.UputiPucanj();
            t.ObradiGađanje(RezultatGađanja.Potonuće);
            Assert.AreEqual(TaktikaGađanja.Napipavanje, t.TrenutnaTaktika);
        }

        [TestMethod]
        public void Topništvo_TaktikaGađanjaNakonPromašajaSeNeMijenja()
        {
            Topništvo t = new Topništvo(10, 10, duljineBrodova);
            t.UputiPucanj();
            // inicijalno je Napipavanje
            t.ObradiGađanje(RezultatGađanja.Promašaj);
            Assert.AreEqual(TaktikaGađanja.Napipavanje, t.TrenutnaTaktika);
            t.UputiPucanj();
            t.ObradiGađanje(RezultatGađanja.Pogodak);
            // nakon prvog pogotka je Okruživanje
            t.UputiPucanj();
            t.ObradiGađanje(RezultatGađanja.Promašaj);
            Assert.AreEqual(TaktikaGađanja.Okruživanje, t.TrenutnaTaktika);
            t.UputiPucanj();
            t.ObradiGađanje(RezultatGađanja.Pogodak);
            // nakon drugog pogotka je SustavnoUništavanje
            t.UputiPucanj();
            t.ObradiGađanje(RezultatGađanja.Promašaj);
            Assert.AreEqual(TaktikaGađanja.SustavnoUništavanje, t.TrenutnaTaktika);
        }

        [TestMethod]
        public void Topništvo_NapipavanjeDajeBiloKojePoljeNaPraznojMreži()
        {
            Topništvo t = new Topništvo(10, 10, duljineBrodova);
            Assert.IsTrue(new Mreža(10,10).DajSlobodnaPolja().Contains(t.UputiPucanj()));
        }

    }
}
