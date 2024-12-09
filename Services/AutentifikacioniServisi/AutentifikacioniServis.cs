using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services;
using Domain.Enums;

namespace Services.AutentifikacioniServisi
{
    public class AutentifikacioniServis : IAutentifikacija
    {
        private static readonly List<Potrosac> _korisnici;

        static AutentifikacioniServis()
        {
            _korisnici =
            [
                new("maja", "123", TipSnabdijevanja.KOMERCIJALNO, 20, 20),
                new("ana", "321", TipSnabdijevanja.GARANTOVANO, 10, 10)
            ];
        }

        public (bool, Potrosac) Prijava(string ImePrezime, string Id)
        {
            foreach (Potrosac korisnik in _korisnici)
            {
                if (korisnik.ImePrezime.Equals(ImePrezime) && korisnik.Id.Equals(Id))
                {
                    return (true, korisnik);
                }
            }

            return (false, new Potrosac());
        }
    }
}
