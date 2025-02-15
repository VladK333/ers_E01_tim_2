using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories.AutentifikacijaRepozitorijum
{
    public interface IAutentifikacijaRepozitorijum
    {
        List<Potrosac> DohvatiSveKorisnike();
    }
}
