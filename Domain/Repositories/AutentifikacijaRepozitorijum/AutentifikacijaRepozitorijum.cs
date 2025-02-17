using Domain.Enums;
using Domain.Models;

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

        public IEnumerable<Potrosac> DohvatiSveKorisnike()
        {
            return _korisnici;
        }
    }
}
