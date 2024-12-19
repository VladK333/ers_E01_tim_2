using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repozitorijumi.PotrosaciRepozitorijum
{
    public interface IPotrosaciRepozitorijum
    {
        public bool DodajPotrosaca(Potrosac potrosac);


    }
}
