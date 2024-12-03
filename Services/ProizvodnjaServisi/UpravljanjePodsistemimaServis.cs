using Domain.Enums;
using Domain.Models;
using Domain.Services;

namespace Services.ProizvodnjaServisi
{
    public class UpravljanjePodsistemimaServis : IUpravljanjePodsistemimaProizvodnje
    {
        
        private readonly List<PodsistemProizvodnje> _podsistemi;
        private readonly IDostupnaKolicinaEnergije _randomGenerator;

        public UpravljanjePodsistemimaServis(IDostupnaKolicinaEnergije randomGenerator)
        {
            _podsistemi = new List<PodsistemProizvodnje>();
            _randomGenerator = randomGenerator;
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

        public double DostupnaEnergija()
        {
            // Saberi preostale količine energije iz svih podsistema proizvodnje
            return _podsistemi.Sum(p => p.PreostalaKolicina);
        }
    }
}
