using Domain.Models;
using Domain.Repositories.PodsistemProizvodnjeRepozitorijum;
using Domain.Services;

namespace Services.ProizvodnjaServisi
{
    public class UpravljanjePodsistemimaServis : IUpravljanjePodsistemimaProizvodnje
    {
        private readonly IProizvodnjaRepozitorijum _proizvodnjaRepozitorijum;
     
        public UpravljanjePodsistemimaServis(IProizvodnjaRepozitorijum proizvodnjaRepozitorijum)
        {
            _proizvodnjaRepozitorijum = proizvodnjaRepozitorijum;
        }
      
        public List<PodsistemProizvodnje> DohvatiSvePodsisteme()
        {
            return _proizvodnjaRepozitorijum.DohvatiSvePodsisteme();
        }

        public PodsistemProizvodnje? NadjiPodsistemSaNajviseEnergije(double potrebnaEnergija)
        {
            var odgovarajuciPodsistemi = _proizvodnjaRepozitorijum.DohvatiSvePodsisteme()
                .Where(p => p.PreostalaKolicina >= potrebnaEnergija)
                .OrderByDescending(p => p.PreostalaKolicina)
                .ToList();

            if (odgovarajuciPodsistemi.Any())
            {
               // Console.WriteLine($"Pronadjen je podsistem sa najviše energije: {odgovarajuciPodsistemi[0].Sifra} sa {odgovarajuciPodsistemi[0].PreostalaKolicina:F2} kW.");
                return odgovarajuciPodsistemi[0];
            }
            else
            {
                return null; // Može se vratiti null jer je tip sada nullable
            }
        }
    }
}

