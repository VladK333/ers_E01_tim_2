using Domain.Models;
using Domain.Services;

namespace Presentation.Authentifikacija
{
    public class AutentifikacijaKorisnika
    {
        private readonly IAutentifikacija _autentifikacijaServis;

        public AutentifikacijaKorisnika(IAutentifikacija autentifikacijaServis)
        {
            _autentifikacijaServis = autentifikacijaServis;
        }

        public bool TryLogin(out Potrosac potrosac)
        {
            potrosac = new Potrosac();
            bool uspjesnaPrijava = false;
            string? ime = "", sifra = "";

            while (!uspjesnaPrijava)
            {
                Console.Write("Ime: ");
                ime = Console.ReadLine() ?? "";

                Console.Write("Sifra: ");
                sifra = Console.ReadLine() ?? "";

                (uspjesnaPrijava, potrosac) = _autentifikacijaServis.Prijava(ime.Trim(), sifra.Trim());

                if (!uspjesnaPrijava)
                {
                    Console.WriteLine("Pogrešni podaci, pokušajte ponovo.");
                }
            }

            return uspjesnaPrijava;
        }
    }
}
