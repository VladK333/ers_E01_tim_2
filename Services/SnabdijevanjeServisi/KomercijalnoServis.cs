using Domain.Models;
using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class KomercijalnoServis : ISnabdijevanje
    {
        private static readonly KomercijalnoServis _instance = new KomercijalnoServis();

        private KomercijalnoServis() { }

        public static KomercijalnoServis Instance => _instance;

        public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina)
        {
            if (podsistem == null || kolicina <= 0)
            {
                Console.WriteLine("Podsistem mora biti validan, a kolicina pozitivna.");
                return;
            }

            double kolicinaSaPovecanjem = kolicina * 1.01;

            if (podsistem.PreostalaKolicina < kolicinaSaPovecanjem)
            {
                podsistem.PreostalaKolicina = 0;
            }
            podsistem.PreostalaKolicina -= kolicinaSaPovecanjem;

            Console.WriteLine($"Kolicina energije u podsistemu '{podsistem.Sifra}' smanjena za {kolicina:F2} kWh + 1% usled nesavrsenosti sistema.");
            Console.WriteLine($"Preostala kolicina: {podsistem.PreostalaKolicina:F2} kWh.\n");
        }

    }
}
