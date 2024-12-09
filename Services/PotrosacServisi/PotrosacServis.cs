using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.Services;

namespace Services.PotrosacServisi
{
    public class PotrosacServis : IPotrosac
    {
        private readonly List<Potrosac> _potrosaci;

        public PotrosacServis()
        {
            _potrosaci = new List<Potrosac>();
        }

        // Dodavanje novog potrošača
        public void DodajPotrosaca(Potrosac potrosac)
        {
            _potrosaci.Add(potrosac);
            Console.WriteLine($"Potrošač {potrosac.ImePrezime} je uspešno dodat.");
        }

        // Pretraga potrošača prema ID-u
        public Potrosac PronadjiPotrosaca(string id)
        {
            var potrosac = _potrosaci.Find(p => p.Id == id);
            if (potrosac == null)
            {
                Console.WriteLine($"Potrošač sa ID {id} nije pronađen.");
            }
            return potrosac;
        }

        // Ažuriranje informacija o potrošaču
        public void AzurirajPotrosaca(Potrosac potrosac)
        {
            var index = _potrosaci.FindIndex(p => p.Id == potrosac.Id);
            if (index != -1)
            {
                _potrosaci[index] = potrosac;
                Console.WriteLine($"Podaci o potrošaču {potrosac.ImePrezime} su ažurirani.");
            }
            else
            {
                Console.WriteLine($"Potrošač sa ID {potrosac.Id} nije pronađen.");
            }
        }

        // Brisanje potrošača
        public void ObrisiPotrosaca(string id)
        {
            var potrosac = PronadjiPotrosaca(id);
            if (potrosac != null)
            {
                _potrosaci.Remove(potrosac);
                Console.WriteLine($"Potrošač {potrosac.ImePrezime} je obrisan.");
            }
        }
    }

}
