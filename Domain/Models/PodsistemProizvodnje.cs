using Domain.Enums;
using Domain.Services;

namespace Domain.Models
{
    public class PodsistemProizvodnje
    {
        public string Sifra {  get; set; }

        public TipProizvodnje Tip { get; set; }

        public string Lokacija {  get; set; }

        public double PreostalaKolicina { get; set; }

        private readonly IDostupnaKolicinaEnergije _randomGenerator;

        public PodsistemProizvodnje(string sifra, TipProizvodnje tip, string lokacija, IDostupnaKolicinaEnergije randomGenerator)
        {
            Sifra = sifra;
            Tip = tip;
            Lokacija = lokacija;
            _randomGenerator = randomGenerator;
            PreostalaKolicina = _randomGenerator.Generate(1000, 5000);
        }

        public override string ToString()
        {
            return $"Podsistem proizvodnje:\n" +
                   $"- Šifra: {Sifra}\n" +
                   $"- Tip proizvodnje: {Tip}\n" +
                   $"- Lokacija: {Lokacija}\n" +
                   $"- Preostala količina energije: {PreostalaKolicina:F2} kW";
        }
    }
}
