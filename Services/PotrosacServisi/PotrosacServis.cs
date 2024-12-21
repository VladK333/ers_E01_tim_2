using Domain.Models;
using Domain.Services;
using Services.AutentifikacioniServisi;

namespace Services.PotrosacServisi
{
    public class PotrosacServis : IPotrosac
    {
        private readonly AutentifikacioniServis _autentifikacioniServis;
        private readonly IPotrosnja _potrosnjaServis;

        public PotrosacServis(AutentifikacioniServis autentifikacioniServis, IPotrosnja potrosnjaServis)
        {
            _autentifikacioniServis = autentifikacioniServis;
            _potrosnjaServis = potrosnjaServis;
        }

        public void DodajPotrosaca(Potrosac potrosac)
        {
            AutentifikacioniServis.DodajPotrosaca(potrosac); // Dodaje potrošača u statičku listu
            Console.WriteLine($"Potrosac {potrosac.ImePrezime} je uspesno dodat.\n");
        }

        public Potrosac PronadjiPotrosaca(string brUgovora)
        {
            return _autentifikacioniServis.PronadjiPotrosaca(brUgovora);  // Koristi metodu iz Autentifikacionog servisa za pronalaženje
        }

        // Ažuriranje informacija o potrošaču
        public void AzurirajPotrosaca(Potrosac potrosac)
        {
            var postojeciPotrosac = _autentifikacioniServis.PronadjiPotrosaca(potrosac.BrUgovora);
            if (postojeciPotrosac != null)
            {
                var index = AutentifikacioniServis.GetPotrosaci().FindIndex(p => p.BrUgovora == potrosac.BrUgovora);
                if (index != -1)
                {
                    AutentifikacioniServis.GetPotrosaci()[index] = potrosac;
                    Console.WriteLine($"Podaci o potrošaču {potrosac.ImePrezime} su azurirani.");
                }
            }
            else
            {
                Console.WriteLine($"Potrošač sa brojem ugovora {potrosac.BrUgovora} nije pronadjen.");
            }
        }

        // Brisanje potrošača
        public void ObrisiPotrosaca(string brUgovora)
        {
            var potrosac = PronadjiPotrosaca(brUgovora);
            if (potrosac != null)
            {
                AutentifikacioniServis.GetPotrosaci().Remove(potrosac);
                Console.WriteLine($"Potrosac {potrosac.ImePrezime} je obrisan.");
            }
            else
            {
                Console.WriteLine($"Potrosac sa brojem ugovora {brUgovora} nije pronadjen.");
            }
        }

        // Prosljedjivanje zahtjeva servisu potrošnje
        public void ObradiZahtevZaPotrosnju(string brUgovora)
        {
            var potrosac = PronadjiPotrosaca(brUgovora);
            if (potrosac != null)
            {
                _potrosnjaServis.ProvjeriPotrosnju(potrosac);
            }
            else
            {
                Console.WriteLine("Zahtev ne moze biti obradjen jer potrosac nije pronadjen.");
            }
        }

        // Vraćanje svih potrošača
        public List<Potrosac> GetPotrosaci()
        {
            return AutentifikacioniServis.GetPotrosaci();  // Vraća listu potrošača iz Autentifikacionog servisa
        }
    }
}
