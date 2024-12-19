using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repozitorijumi.PotrosaciRepozitorijum
{
    public class PotrosaciRepozitorijum : IPotrosaciRepozitorijum
    {
        private static readonly List<Potrosac> svi_potrosaci;

        public bool DodajPotrosaca(Potrosac potrosac)
        {
            svi_potrosaci.Add(potrosac);
            return true;
        }
    }
}
