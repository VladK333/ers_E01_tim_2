using Domain.Models;
using Domain.Services;
using Domain.Enums;

namespace Services.AutentifikacioniServisi
{
    public class AutentifikacioniServis : IAutentifikacija
    {
        private static readonly List<Potrosac> _korisnici;

        static AutentifikacioniServis()
        {
            _korisnici = new List<Potrosac>
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
        public static List<Potrosac> GetPotrosaci()
        {
            return _korisnici;
        }
        public (bool, Potrosac) Prijava(string ImePrezime, string BrUgovora)
        {
            foreach (Potrosac korisnik in _korisnici)
            {
                if (korisnik.ImePrezime.Equals(ImePrezime) && korisnik.BrUgovora.Equals(BrUgovora))
                {
                    return (true, korisnik);
                }
            }

            return (false, new Potrosac());
        }
        //Dodato////////////////
        public void DodajPotrosaca(Potrosac potrosac)
        {
            _korisnici.Add(potrosac);  // Dodaje novog potrošača u statičku listu
        }

        public Potrosac PronadjiPotrosaca(string brUgovora)
        {
            return _korisnici.FirstOrDefault(p => p.BrUgovora.Equals(brUgovora));  // Traži potrošača prema broju ugovora
        }

    }
}
