using Domain.Models;
using Domain.Enums;

namespace Presentation.GenerisanjePotrosaca
{
    public static class NasumicanPotrosacGenerator
    {
        private static Random random = new Random();

        // Niz sa primjerima imena i prezimena
        private static readonly string[] imena =
        {
            "Marko Jovanović", "Ana Petrović", "Nikola Ilić", "Jelena Savić", "Milan Novaković","Ivana Ivanović", "Petar Petrović",
            "Jovana Kovačević", "Maja Milenković", "Stefan Jovanović", "Vanja Popović", "Nina Tadić", "Luka Jokić", "Tanja Ilić", "Katarina Marković"
        };

        private static readonly TipSnabdijevanja[] tipoviSnabdevanja =
        {
            TipSnabdijevanja.GARANTOVANO,
            TipSnabdijevanja.KOMERCIJALNO
        };

        private static string GenerisiBrojUgovora()
        {
            return "EPS" + random.Next(1000, 9999).ToString() + "K";
        }

        private static double GenerisiPotrosnju()
        {
            return Math.Round(random.NextDouble() * (5000 - 100) + 100, 2);  // Nasumična potrošnja između 100 i 5000 kWh
        }

        private static double GenerisiZaduzenje()
        {
            return Math.Round(random.NextDouble() * (10000 - 100) + 100, 2);  // Nasumično zaduženje između 100 i 10,000
        }

        public static Potrosac GenerisiNasumicanPotrosac()
        {
            string imePrezime = imena[random.Next(imena.Length)];
            string brUgovora = GenerisiBrojUgovora();
            TipSnabdijevanja tipSnabdevanja = tipoviSnabdevanja[random.Next(tipoviSnabdevanja.Length)];
            double ukupnaPotrosnja = GenerisiPotrosnju();
            double trenutnoZaduzenje = GenerisiZaduzenje();

            return new Potrosac
            {
                Id = Guid.NewGuid().ToString(),
                ImePrezime = imePrezime,
                BrUgovora = brUgovora,
                Tip_Snabdevanja = tipSnabdevanja,
                Ukupna_potrosnja_ee = ukupnaPotrosnja,
                Trenutno_zaduzenje = trenutnoZaduzenje
            };
        }
    }
}
