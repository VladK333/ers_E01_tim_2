using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class PotrosacServis : IPotrosac
    {
        private static List<Potrosac> _potrosaci;

        static PotrosacServis()
        {
            _potrosaci = new List<Potrosac>()
            {
                new Potrosac("Ana Petrović", "EPS5678K", TipSnabdijevanja.KOMERCIJALNO, 3200.00, 500.30),
                new Potrosac("Nikola Ilić", "EPS9101K", TipSnabdijevanja.GARANTOVANO, 2500.75, 1500.90),
                new Potrosac("Jelena Savić", "EPS1123K", TipSnabdijevanja.KOMERCIJALNO, 1800.20, 3000.00)
            };
        }

        public void DodajPotrosaca(Potrosac potrosac)
        {
            if (potrosac != null)
            {
                _potrosaci.Add(potrosac);
            }
        }

        public Potrosac PronadjiPotrosaca(string brUgovora)
        {
            var potrosac = _potrosaci.FirstOrDefault(p => p.BrUgovora == brUgovora);
            return potrosac ?? new Potrosac();  
        }

        public List<Potrosac> GetPotrosaci()
        {
            return _potrosaci;
        }
    }
}

