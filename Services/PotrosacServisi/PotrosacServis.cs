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
                new Potrosac("Ana Petrović", "EPS5678K", TipSnabdijevanja.KOMERCIJALNO, 340.00, 14626.8),
                new Potrosac("Nikola Ilić", "EPS9101K", TipSnabdijevanja.GARANTOVANO, 250.75, 5697.04),
                new Potrosac("Jelena Savić", "EPS1123K", TipSnabdijevanja.KOMERCIJALNO, 180.20, 7752.2)
            };
        }

        public void DodajPotrosaca(Potrosac potrosac)
        {
            if (potrosac != null)
            {
                _potrosaci.Add(potrosac);
            }
        }

        public Potrosac? PronadjiPotrosaca(string Id)
        {
            var potrosac = _potrosaci.FirstOrDefault(p => p.Id == Id);
            return potrosac;  
        }

        public List<Potrosac> GetPotrosaci()
        {
            return _potrosaci;
        }
    }
}

