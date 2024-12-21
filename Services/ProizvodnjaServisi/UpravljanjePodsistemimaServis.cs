using Domain.Enums;
using Domain.Models;
using Domain.Services;

namespace Services.ProizvodnjaServisi
{
    public class UpravljanjePodsistemimaServis : IUpravljanjePodsistemimaProizvodnje
    {

        private static readonly List<PodsistemProizvodnje> _podsistemi = new List<PodsistemProizvodnje>();//static
        private readonly IDostupnaKolicinaEnergije _randomGenerator;
        private readonly ISnabdijevanje _snabdijevanjeServis; // Dodato za smanjenje energije


        public UpravljanjePodsistemimaServis(IDostupnaKolicinaEnergije randomGenerator, ISnabdijevanje snabdijevanjeServis)
        {
            _randomGenerator = randomGenerator;
            _snabdijevanjeServis = snabdijevanjeServis;
            if (!_podsistemi.Any())
            {
                InicijalizujPodsisteme();
            }
        }
        public void InicijalizujPodsisteme()
        {
            _podsistemi.Add(new PodsistemProizvodnje("P001", TipProizvodnje.Hidroelektrana, "Lokacija 1", _randomGenerator.Generate(1000, 5000)));
            _podsistemi.Add(new PodsistemProizvodnje("P002", TipProizvodnje.EcoGreen, "Lokacija 2", _randomGenerator.Generate(1000, 5000)));
            _podsistemi.Add(new PodsistemProizvodnje("P003", TipProizvodnje.CvrstoGorivo, "Lokacija 3", _randomGenerator.Generate(1000, 5000)));
            Console.WriteLine("Podsistemi su uspešno inicijalizovani.");
        }


        public void DodajPodsistem(string sifra, TipProizvodnje tip, string lokacija)
        {
            double preostalaKolicina = _randomGenerator.Generate(1000, 5000);
            var podsistem = new PodsistemProizvodnje(sifra, tip, lokacija, preostalaKolicina);
            _podsistemi.Add(podsistem);
        }

        public List<PodsistemProizvodnje> DohvatiSvePodsisteme()
         {
             return _podsistemi;
         }

        public PodsistemProizvodnje NadjiPodsistemSaNajviseEnergije(double potrebnaEnergija)
        {
            return _podsistemi
                .Where(p => p.PreostalaKolicina >= potrebnaEnergija)
                .OrderByDescending(p => p.PreostalaKolicina)
                .FirstOrDefault();
        }

        public double DohvatiNajvecuDostupnuEnergiju()
        {
            if (!_podsistemi.Any()) // Proverava da li ima elemenata u listi
            {
                Console.WriteLine("Nema dostupnih podsistema.");
                return 0; 
            }
            return _podsistemi.Max(p => p.PreostalaKolicina);
        }

        public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina)
        {
            if (podsistem == null || kolicina <= 0)
                throw new ArgumentException("Podsistem mora biti validan, a količina pozitivna.");

            podsistem.PreostalaKolicina -= kolicina - (kolicina * 0.02);

            Console.WriteLine($"Količina energije u podsistemu '{podsistem.Sifra}' smanjena za {kolicina:F2} kWh.");
        }
        /*
        public PodsistemProizvodnje OdrediPodsistemZaPotrosaca(double potrebnaEnergija)
        {
            // Pronađi podsistem sa najviše dostupne energije
            var odgovarajuciPodsistem = _podsistemi
                .OrderByDescending(p => p.PreostalaKolicina)
                .FirstOrDefault();

            // Ako nema podsistema sa dovoljnim resursima
            if (odgovarajuciPodsistem == null)
            {
                Console.WriteLine("Nema podsistema sa dovoljnom količinom energije.");
                return null;
            }

            double smanjenaEnergija = _snabdijevanjeServis.SmanjenjeKolicine(potrebnaEnergija);



            return;
        }*/
    }
}
