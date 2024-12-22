using Domain.Models;

namespace Domain.Services
{
    public class PotrosacServis : IPotrosac
    {
        private readonly IAutentifikacija _autentifikacija;  // readonly jer se ne mijenja nakon inicijalizacije
        private static List<Potrosac> _potrosaci = new List<Potrosac>();  // Lista može da se mijenja

        public PotrosacServis(IAutentifikacija autentifikacija)
        {
            _autentifikacija = autentifikacija;
        } 

        public void DodajPotrosaca(Potrosac potrosac)
        {
            if (potrosac == null)
            {
                Console.WriteLine("Potrosac ne može biti null.");
                return;
            }

            if (string.IsNullOrEmpty(potrosac.ImePrezime))
            {
                Console.WriteLine("Ime i prezime potrošača je obavezno.");
                return;
            }

            if (_potrosaci.Any(p => p.BrUgovora == potrosac.BrUgovora))
            {
                Console.WriteLine($"Potrosac sa brojem ugovora {potrosac.BrUgovora} već postoji.");
            }
            else
            {
                _potrosaci.Add(potrosac);
                Console.WriteLine($"Potrosac {potrosac.ImePrezime} je uspešno dodat.");
            }
        }

        public Potrosac PronadjiPotrosaca(string brUgovora)
        {
            return _potrosaci.FirstOrDefault(p => p.BrUgovora == brUgovora);
        }

        public void AzurirajPotrosaca(Potrosac potrosac)
        {
            var existingPotrosac = PronadjiPotrosaca(potrosac.BrUgovora);
            if (existingPotrosac != null)
            {
                existingPotrosac.ImePrezime = potrosac.ImePrezime;
                existingPotrosac.Tip_Snabdevanja = potrosac.Tip_Snabdevanja;
                existingPotrosac.Ukupna_potrosnja_ee = potrosac.Ukupna_potrosnja_ee;
                existingPotrosac.Trenutno_zaduzenje = potrosac.Trenutno_zaduzenje;
                Console.WriteLine("Podaci o potrošaču su ažurirani.");
            }
            else
            {
                Console.WriteLine("Potrošač nije pronađen.");
            }
        }

        public void ObrisiPotrosaca(string id)
        {
            var potrosac = _potrosaci.FirstOrDefault(p => p.BrUgovora == id);
            if (potrosac != null)
            {
                _potrosaci.Remove(potrosac);
                Console.WriteLine($"Potrosac sa brojem ugovora {id} je obrisan.");
            }
            else
            {
                Console.WriteLine("Potrošač nije pronađen.");
            }
        }

        public List<Potrosac> GetPotrosaci()
        {
            return _potrosaci;
        }
    }
}

