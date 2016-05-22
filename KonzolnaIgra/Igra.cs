using System;
using PotapanjeBrodova;

namespace KonzolnaIgra
{
    class Igra
    {
        public enum TkoGađa
        {
            Komp,
            Ja
        }

        public Igra(int redaka, int stupaca, int[] duljineBrodova)
        {
            Brodograditelj bg = new Brodograditelj();
            kompovaFlota = bg.SložiFlotu(redaka, stupaca, duljineBrodova);
            kompovoTopništvo = new Topništvo(redaka, stupaca, duljineBrodova);
        }

        public void Kreni(TkoGađa tkoPrviGađa)
        {
            tkoGađa = tkoPrviGađa;
            int brojPotopljenihBrodova = 0;
            PočetniIspis();
            do
            {
                switch (tkoGađa)
                {
                    case TkoGađa.Komp:
                        KompGađa();
                        break;
                    case TkoGađa.Ja:
                        JaGađam();
                        break;
                }
                // mijenjamo tko je na redu
                tkoGađa = tkoGađa == TkoGađa.Ja ? TkoGađa.Komp : TkoGađa.Ja;
            } while ((brojPotopljenihBrodova < kompovaFlota.BrojBrodova) && (kompovoTopništvo.BrojPreostalihBrodova > 0));

            Console.WriteLine("IGRA JE GOTOVA!");
            if (kompovoTopništvo.BrojPreostalihBrodova == 0)
                Console.WriteLine("Komp je pobijedio!");
            else
                Console.WriteLine("Ja sam pobijedio!");
        }

        private void PočetniIspis()
        {
            IspišiUputuZaPoljeKojeSeGađa();
            IspišiUputuZaRezultatGađanja();
            switch (tkoGađa)
            {
                case TkoGađa.Komp:
                    Console.WriteLine("Komp prvi gađa!");
                    break;
                case TkoGađa.Ja:
                    Console.WriteLine("Ti prvi gađaš!");
                    break;
            }
            Console.WriteLine();
        }

        private void KompGađa()
        {
            Polje p = kompovoTopništvo.UputiPucanj();
            Console.WriteLine(string.Format("Komp gađa polje: {0}-{1}", p.Stupac.UOznakuStupca(), p.Redak.UOznakuRetka()));
            RezultatGađanja rez = UnosRezultata();
            kompovoTopništvo.ObradiGađanje(rez);
            Console.WriteLine();
        }

        private RezultatGađanja UnosRezultata()
        {
            while (true)
            {
                ConsoleKeyInfo unos = Console.ReadKey();
                switch (unos.KeyChar)
                {
                    case 'f':
                    case 'F':
                        return RezultatGađanja.Promašaj;
                    case 'p':
                    case 'P':
                        return RezultatGađanja.Pogodak;
                    case 't':
                    case 'T':
                        return RezultatGađanja.Potonuće;
                    default:
                        IspišiUputuZaRezultatGađanja();
                        break;
                }
            }
        }

        private void IspišiUputuZaRezultatGađanja()
        {
            Console.WriteLine("Rezultat gađanja unesi pritiskom na tipke (F)ulao, (P)ogodak ili (T)one.");
        }

        private void JaGađam()
        {
            Console.Write("Ti gađaš: ");
            Polje polje = UnosPolja();
            RezultatGađanja rez = kompovaFlota.Gađaj(polje);
            if (rez == RezultatGađanja.Potonuće)
                ++brojPotopljenihBrodova;
            Console.WriteLine(rez.ToString());
        }

        private Polje UnosPolja()
        {
            while (true)
            {
                try
                {
                    string unos = Console.ReadLine();
                    string[] razdvojeno = unos.Split(new char[] { '-' });
                    int redak = razdvojeno[1].UIndeksRetka();
                    int stupac = razdvojeno[0].UIndeksStupca();
                    return new Polje(redak, stupac);
                }
                catch
                {
                    IspišiUputuZaPoljeKojeSeGađa();
                }
            }
        }

        private void IspišiUputuZaPoljeKojeSeGađa()
        {
            Console.WriteLine("Polje koje gađaš upiši u obliku: C-3");
        }

        Flota kompovaFlota;
        Topništvo kompovoTopništvo;
        TkoGađa tkoGađa;
        int brojPotopljenihBrodova;
    }
}
