using Domain.Enums;
using Domain.Models;
using Domain.Services;
using System.Linq;

namespace Services.AutentifikacioniServisi
{
    public class AutentifikacioniServis : IAutentifikacija
    {
        private static readonly List<Potrosac> _potrosaci;

        static AutentifikacioniServis()
        {
            _potrosaci = new List<Potrosac>
            {
                new Potrosac("Maja Adanić", "EPS3345K", TipSnabdijevanja.KOMERCIJALNO, 20, 20),
                new Potrosac("Ana Rabić", "EPS3346K", TipSnabdijevanja.GARANTOVANO, 10, 10),
                new Potrosac("Jovan Petrović", "EPS3347K", TipSnabdijevanja.KOMERCIJALNO, 15, 12),
                new Potrosac("Ivana Nikolić", "EPS3348K", TipSnabdijevanja.GARANTOVANO, 8, 8),
                new Potrosac("Marko Jovanović", "EPS3349K", TipSnabdijevanja.KOMERCIJALNO, 25, 30)
            };
        }

        public (bool, Potrosac) Prijava(string imePrezime, string brojUgovora)
        {
            var potrosac = _potrosaci.FirstOrDefault(p => p.ImePrezime == imePrezime && p.BrUgovora == brojUgovora);
            if (potrosac != null)
            {
                return (true, potrosac);
            }
            return (false, null);
        }

        public bool TryLogin(out Potrosac potrosac)
        {
            potrosac = _potrosaci.FirstOrDefault(); 
            return potrosac != null;
        }
    }
}

