using Domain.Models;
using Domain.Repositories.PodsistemProizvodnjeRepozitorijum;
using Domain.Services;

namespace Services.ProizvodnjaServisi
{
    public class UpravljanjePodsistemimaServis : IUpravljanjePodsistemimaProizvodnje
    {
        private readonly IProizvodnjaRepozitorijum _proizvodnjaRepozitorijum;
        private readonly IIspis _ispisServis;

        public UpravljanjePodsistemimaServis(IProizvodnjaRepozitorijum proizvodnjaRepozitorijum, IIspis ispisServis)
        {
            _proizvodnjaRepozitorijum = proizvodnjaRepozitorijum;
            _ispisServis = ispisServis;
        }

        public IEnumerable<PodsistemProizvodnje> DohvatiSvePodsisteme()
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
                string poruka = $"Pronadjen je podsistem sa najviše energije: {odgovarajuciPodsistemi[0].Sifra} sa {odgovarajuciPodsistemi[0].PreostalaKolicina:F2} kW.";
                _ispisServis.Ispisi(poruka);
                return odgovarajuciPodsistemi[0];
            }
            else
            {
                return null; 
            }
        }
    }
}

