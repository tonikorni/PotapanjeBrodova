using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotapanjeBrodova;

namespace KonzolnaIgra
{
    class Program
    {
        static void Main(string[] args)
        {
            int redaka = 10;
            int stupaca = 10;
            int[] duljineBrodova = { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            Igra igra = new Igra(redaka, stupaca, duljineBrodova);
            // bacamo novčić tko počinje
            Igra.TkoGađa tkoPrviGađa = (Igra.TkoGađa)new Random().Next(Enum.GetValues(typeof(Igra.TkoGađa)).Length);
            igra.Kreni(tkoPrviGađa);
        }
    }
}
