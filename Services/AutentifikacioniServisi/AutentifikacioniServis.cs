using Domain.Enums;
using Domain.Models;
using Domain.Services;

namespace Services.AutentifikacioniServisi
{
    public class AutentifikacioniServis : IAutentifikacija
    {
        // Statička lista potrošača sa inicijalnim podacima
        private static List<Potrosac> _potrosaci;

        // Staticki konstruktor koji inicijalizuje listu potrošača
        static AutentifikacioniServis()
        {
            _potrosaci = new List<Potrosac>
            {
                new Potrosac("Maja Adanić", "EPS3345K", TipSnabdijevanja.KOMERCIJALNO, 20, 20),
                new Potrosac("Ana Rabić", "EPS3346K", TipSnabdijevanja.GARANTOVANO, 10, 10),
                new Potrosac("Jovan Petrović", "EPS3347K", TipSnabdijevanja.KOMERCIJALNO, 15, 12),
                new Potrosac("Ivana Nikolić", "EPS3348K", TipSnabdijevanja.GARANTOVANO, 8, 8),
                new Potrosac("Marko Jovanović", "EPS3349K", TipSnabdijevanja.KOMERCIJALNO, 25, 30),
                new Potrosac("Petar Milić", "EPS3350K", TipSnabdijevanja.GARANTOVANO, 5, 7),
                new Potrosac("Sara Ilić", "EPS3351K", TipSnabdijevanja.KOMERCIJALNO, 18, 18),
                new Potrosac("Nikola Đorđević", "EPS3352K", TipSnabdijevanja.GARANTOVANO, 20, 25),
                new Potrosac("Katarina Stojanović", "EPS3353K", TipSnabdijevanja.KOMERCIJALNO, 30, 20),
                new Potrosac("Luka Savić", "EPS3354K", TipSnabdijevanja.GARANTOVANO, 12, 15)
            };
        }

        // Prijava korisnika na osnovu imena i broja ugovora
        public (bool, Potrosac) Prijava(string ImePrezime, string BrUgovora)
        {
            var potrosac = _potrosaci.FirstOrDefault(p => p.ImePrezime == ImePrezime && p.BrUgovora == BrUgovora);
            if (potrosac != null)
            {
                return (true, potrosac);
            }
            return (false, null);
        }

        public static void DodajPotrosaca(Potrosac potrosac)
        {
            _potrosaci.Add(potrosac);
        }

        public Potrosac PronadjiPotrosaca(string brUgovora)
        {
            return _potrosaci.FirstOrDefault(p => p.BrUgovora == brUgovora);
        }

        public static List<Potrosac> GetPotrosaci()
        {
            return _potrosaci;
        }
    }
}
