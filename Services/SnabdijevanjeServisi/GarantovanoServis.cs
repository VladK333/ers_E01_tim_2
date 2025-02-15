using Domain.Models;
using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class GarantovanoServis : ISnabdijevanje
    {
        private static readonly GarantovanoServis _instance = new GarantovanoServis();

        private GarantovanoServis() { }

        public static GarantovanoServis Instance => _instance;

        public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina)
        {
            if (podsistem == null || kolicina <= 0)
            {
                Console.WriteLine("Podsistem mora biti validan, a kolicina pozitivna.");
                return;
            }

            double kolicinaSaPovecanjem = kolicina * 1.02;
            if (podsistem.PreostalaKolicina < kolicinaSaPovecanjem)
            {
                podsistem.PreostalaKolicina = 0;
            }
            else
            {
                podsistem.PreostalaKolicina -= kolicinaSaPovecanjem;
            }

            Console.WriteLine($"Kolicina energije u podsistemu '{podsistem.Sifra}' smanjena za {kolicina:F2} kWh + 2% usled nesavrsenosti sistema.");
            Console.WriteLine($"Preostala kolicina: {podsistem.PreostalaKolicina:F2} kWh.\n");
        }
    }
}
