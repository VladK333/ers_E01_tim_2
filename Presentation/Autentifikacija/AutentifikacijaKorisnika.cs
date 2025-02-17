using Domain.Models;
using Domain.Services;

namespace Presentation.Autentifikacija
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

            Console.WriteLine("==AUTENTIFIKACIJA==");

            while (!uspjesnaPrijava)
            {
                Console.Write("Ime: ");
                ime = Console.ReadLine() ?? "";

                Console.Write("Sifra: ");
                sifra = Console.ReadLine() ?? "";

                (uspjesnaPrijava, potrosac) = _autentifikacijaServis.Prijava(ime, sifra);

                if (!uspjesnaPrijava)
                {
                    Console.WriteLine("Pogresni podaci, pokusajte ponovo!\n");
                }
            }

            return uspjesnaPrijava;
        }
    }
}
