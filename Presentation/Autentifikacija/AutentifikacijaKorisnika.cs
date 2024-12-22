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
            bool uspesnaPrijava = false;
            string imePrezime = "", brojUgovora = "";

            while (!uspesnaPrijava)
            {
                Console.Write("Korisničko ime: ");
                imePrezime = Console.ReadLine() ?? "";

                Console.Write("Broj ugovora: ");
                brojUgovora = Console.ReadLine() ?? "";

                (uspesnaPrijava, potrosac) = _autentifikacijaServis.Prijava(imePrezime.Trim(), brojUgovora.Trim());

                if (!uspesnaPrijava)
                {
                    Console.WriteLine("Pogrešni podaci, pokušajte ponovo.");
                }
            }

            return uspesnaPrijava;
        }
    }
}
