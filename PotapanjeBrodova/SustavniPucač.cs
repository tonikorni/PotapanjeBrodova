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
            this.mreža = mreža;
        }

        public Polje UputiPucanj()
        {
            throw new NotImplementedException();
        }

        List<Polje> pogođenaPolja;
        Mreža mreža;
    }
}
