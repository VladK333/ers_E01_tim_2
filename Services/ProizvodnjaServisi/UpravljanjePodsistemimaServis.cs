using Domain.Enums;
using Domain.Models;
using Domain.Services;

namespace Services.ProizvodnjaServisi
{
    public class UpravljanjePodsistemimaServis : IUpravljanjePodsistemimaProizvodnje
    {

        private readonly List<PodsistemProizvodnje> _podsistemi = new List<PodsistemProizvodnje>();
        private readonly IDostupnaKolicinaEnergije _randomGenerator;


        public UpravljanjePodsistemimaServis(IDostupnaKolicinaEnergije randomGenerator, ISnabdijevanje snabdijevanjeServis)
        {
            _randomGenerator = randomGenerator;
            
            if (_podsistemi.Count == 0)
            {
                InicijalizujPodsisteme();
            }
        }
        public void InicijalizujPodsisteme()
        {
            _podsistemi.Add(new PodsistemProizvodnje("PP221-NS1", TipProizvodnje.Hidroelektrana, "Lokacija 1", _randomGenerator.Generate(1000, 9000)));
            _podsistemi.Add(new PodsistemProizvodnje("PP222-NS2", TipProizvodnje.EcoGreen, "Lokacija 2", _randomGenerator.Generate(1000, 9000)));
            _podsistemi.Add(new PodsistemProizvodnje("PP223-NS3", TipProizvodnje.CvrstoGorivo, "Lokacija 3", _randomGenerator.Generate(1000, 9000)));
            _podsistemi.Add(new PodsistemProizvodnje("PP224-NS4", TipProizvodnje.Hidroelektrana, "Lokacija 4", _randomGenerator.Generate(1000, 9000)));
            _podsistemi.Add(new PodsistemProizvodnje("PP225-NS5", TipProizvodnje.EcoGreen, "Lokacija 5", _randomGenerator.Generate(1000, 9000)));

            Console.WriteLine("Podsistemi su uspješno inicijalizovani.");
        }

        public List<PodsistemProizvodnje> DohvatiSvePodsisteme()
         {
             return _podsistemi;
         }

        public PodsistemProizvodnje? NadjiPodsistemSaNajviseEnergije(double potrebnaEnergija)
        {
            var odgovarajuciPodsistemi = _podsistemi
                .Where(p => p.PreostalaKolicina >= potrebnaEnergija)
                .OrderByDescending(p => p.PreostalaKolicina)
                .ToList();

            if (odgovarajuciPodsistemi.Any())
            {
                Console.WriteLine($"Pronadjen je podsistem sa najviše energije: {odgovarajuciPodsistemi[0].Sifra} sa {odgovarajuciPodsistemi[0].PreostalaKolicina:F2} kW.");
                return odgovarajuciPodsistemi[0];
            }
            else
            {
                Console.WriteLine("Nije pronadjen podsistem sa dovoljnom količinom energije.");
                return null; // Može se vratiti null jer je tip sada nullable
            }
        }


        public double DohvatiNajvecuDostupnuEnergiju()
        {
            if (_podsistemi.Count == 0)
            {
                Console.WriteLine("Nema dostupnih podsistema.");
                return 0; 
            }
            return _podsistemi.Max(p => p.PreostalaKolicina);
        }

    }
}

