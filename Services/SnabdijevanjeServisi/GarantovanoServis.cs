using Domain.Models;
using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class GarantovanoServis : ISnabdijevanje
    {
        private static readonly GarantovanoServis _instance = new GarantovanoServis();

        private GarantovanoServis() { }

        public static GarantovanoServis Instance => _instance;

        public double CijenaPoKW
        {
            get { return 22.72; }
        }

        public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina)
        {
            if (podsistem == null || kolicina <= 0)
            {
                Console.WriteLine("Podsistem mora biti validan, a količina pozitivna.");
                return;
            }

            double kolicinaSaPovecanjem = kolicina * 1.02; 
            if (podsistem.PreostalaKolicina < kolicinaSaPovecanjem)
            {
                Console.WriteLine($"Nema dovoljno energije u podsistemu '{podsistem.Sifra}'. Preostala količina je {podsistem.PreostalaKolicina:F2} kWh.");
                // Postavljanje preostale količine na 0, ako nije dovoljno energije
                podsistem.PreostalaKolicina = 0;
                return;
            }

            podsistem.PreostalaKolicina -= kolicinaSaPovecanjem;

            Console.WriteLine($"Količina energije u podsistemu '{podsistem.Sifra}' smanjena za {kolicinaSaPovecanjem:F2} kWh.");
            Console.WriteLine($"Preostala količina: {podsistem.PreostalaKolicina:F2} kWh.");
        }



    }
}
