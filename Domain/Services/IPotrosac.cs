using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPotrosac
    {
        void DodajPotrosaca(Potrosac potrosac);
        Potrosac PronadjiPotrosaca(string id);
        void AzurirajPotrosaca(Potrosac potrosac);
        void ObrisiPotrosaca(string id);

        public List<Potrosac> GetPotrosaci();

    }
}
