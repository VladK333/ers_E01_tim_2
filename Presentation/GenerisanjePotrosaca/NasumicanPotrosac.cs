using Domain.Constants;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.SnabdijevanjeServisi;

namespace Presentation.GenerisanjePotrosaca
{
    public static class NasumicanPotrosacGenerator
    {
        private static Random random = new Random();

        // Niz sa primjerima imena i prezimena
        private static readonly string[] imena =
        {
            "Marko Jovanovic", "Ana Petrovic", "Nikola Ilic", "Jelena Savic", "Milan Novakovic", "Ivana Ivanovic", "Petar Petrovic",
            "Jovana Kovačevic", "Maja Milenkovic", "Stefan Jovanovic", "Vanja Popovic", "Nina Tadic", "Luka Jokic", "Tanja Ilic", "Katarina Markovic"
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
            return Math.Round(random.NextDouble() * (5000 - 100) + 100, 2);  // Nasumicna potrosnja između 100 i 5000 kWh
        }
        public static Potrosac GenerisiNasumicanPotrosac(IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis)
        {
            string imePrezime = imena[random.Next(imena.Length)];
            string brUgovora = GenerisiBrojUgovora();
            double ukupnaPotrosnja = GenerisiPotrosnju();
            double trenutnoZaduzenje = 0;

            var odgovarajuciPodsistem = upravljanjePodsistemimaServis.NadjiPodsistemSaNajviseEnergije(ukupnaPotrosnja);
            if (odgovarajuciPodsistem == null)
            {
                throw new InvalidOperationException("Nema podsistema sa dovoljnom kolicinom energije za generisanje potrosaca.");
            }

            TipSnabdijevanja tipSnabdevanja = tipoviSnabdevanja[random.Next(tipoviSnabdevanja.Length)];
            ISnabdijevanje snabdijevanjeServis = tipSnabdevanja == TipSnabdijevanja.GARANTOVANO ? GarantovanoServis.Instance : KomercijalnoServis.Instance;

            if (snabdijevanjeServis is GarantovanoServis garantovanoServis)
            {
                trenutnoZaduzenje = ukupnaPotrosnja * CeneConsts.CenaGarantovano;
                garantovanoServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, ukupnaPotrosnja);
            }
            else if (snabdijevanjeServis is KomercijalnoServis komercijalnoServis)
            {
                trenutnoZaduzenje = ukupnaPotrosnja * CeneConsts.CenaKomercijalno;
                komercijalnoServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, ukupnaPotrosnja);
            }

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
