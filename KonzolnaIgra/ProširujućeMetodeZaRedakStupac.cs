using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonzolnaIgra

{
    static class ProširujućeMetodeZaRedakStupac
    {
        public static string UOznakuRetka(this int x)
        {
            return (x + 1).ToString();
        }

        public static string UOznakuStupca(this int x)
        {
            return ((char)(x + 'A')).ToString();
        }

        public static int UIndeksRetka(this string s)
        {
            return int.Parse(s) - 1;
        }

        public static int UIndeksStupca(this string s)
        {
            return (int)(s.ToUpper()[0]) - 'A';
        }
    }
}
