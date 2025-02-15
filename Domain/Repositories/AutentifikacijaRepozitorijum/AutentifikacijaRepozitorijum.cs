using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Models;
using Domain.Services;

namespace Domain.Repositories.AutentifikacijaRepozitorijum
{
    public class AutentifikacijaRepozitorijum : IAutentifikacijaRepozitorijum
    {
        private static readonly List<Potrosac> _korisnici;

        static AutentifikacijaRepozitorijum()
        {

            _korisnici = new List<Potrosac>
            {
                new Potrosac("Vladana", "123", TipSnabdijevanja.KOMERCIJALNO, 20, 20),
                new Potrosac("Ivana", "123", TipSnabdijevanja.GARANTOVANO, 10, 10),
            };
        }

        public List<Potrosac> DohvatiSveKorisnike()
        {
            return _korisnici;
        }
    }
}
