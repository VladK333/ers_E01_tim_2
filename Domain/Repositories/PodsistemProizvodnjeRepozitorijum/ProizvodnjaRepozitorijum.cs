using Domain.Enums;
using Domain.Models;
using Domain.PomocneMetode.DostupnaKolicinaEnergije;
using Domain.Services;

namespace Domain.Repositories.PodsistemProizvodnjeRepozitorijum
{
    public class ProizvodnjaRepozitorijum : IProizvodnjaRepozitorijum
    {
        private readonly List<PodsistemProizvodnje> _podsistemi = new List<PodsistemProizvodnje>();
        private readonly IDostupnaKolicinaEnergije _randomGenerator;

        public ProizvodnjaRepozitorijum(IDostupnaKolicinaEnergije randomGenerator)
        {
            _randomGenerator = randomGenerator;

            _podsistemi = [
                new("PP221-NS1", TipProizvodnje.Hidroelektrana, "Lokacija 1", _randomGenerator.Generate(1000, 9000)),
                new("PP222-NS2", TipProizvodnje.EcoGreen, "Lokacija 2", _randomGenerator.Generate(1000, 9000)),
                new("PP223-NS3", TipProizvodnje.CvrstoGorivo, "Lokacija 3", _randomGenerator.Generate(1000, 9000)),
                new("PP224-NS4", TipProizvodnje.Hidroelektrana, "Lokacija 4", _randomGenerator.Generate(1000, 9000)),
                new("PP225-NS5", TipProizvodnje.EcoGreen, "Lokacija 5", _randomGenerator.Generate(1000, 9000))
            ];
        }

        public List<PodsistemProizvodnje> DohvatiSvePodsisteme()
        {
            return _podsistemi;
        }
    }
}
