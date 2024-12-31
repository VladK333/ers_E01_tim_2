using Domain.Models;
using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class KomercijalnoServis : ISnabdijevanje
    {
        private static readonly KomercijalnoServis _instance = new KomercijalnoServis();

        private KomercijalnoServis() { }

        public static KomercijalnoServis Instance => _instance;

        public double CijenaPoKW
        {
            get { return 43.02; }
        }

        public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina)
        {
            if (podsistem == null)
                throw new ArgumentException("Podsistem mora biti validan.");

            if (kolicina <= 0)
                throw new ArgumentException("Količina mora biti pozitivna.");

            double smanjenaKolicina = kolicina * 0.01; 
            double ukupnaKolicinaZaSmanjenje = kolicina + smanjenaKolicina;

            // Provera da li postoji dovoljno energije u podsistemu (uzimajući u obzir 1% smanjenja)
            if (ukupnaKolicinaZaSmanjenje > podsistem.PreostalaKolicina)
            {
                throw new InvalidOperationException("Nema dovoljno energije u podsistemu.");
            }

            podsistem.PreostalaKolicina -= ukupnaKolicinaZaSmanjenje;

            if (podsistem.PreostalaKolicina < 0)
            {
                podsistem.PreostalaKolicina = 0;
            }

            Console.WriteLine($"Količina energije u podsistemu '{podsistem.Sifra}' smanjena za {ukupnaKolicinaZaSmanjenje:F2} kWh (plus 1% zbog nesavršenosti). Preostala količina: {podsistem.PreostalaKolicina:F2} kWh.");
        }

    }
}
