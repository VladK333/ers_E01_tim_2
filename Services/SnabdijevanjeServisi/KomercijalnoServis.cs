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
            if (podsistem == null || kolicina <= 0)
            {
                Console.WriteLine("Podsistem mora biti validan, a kolicina pozitivna.");
                return;
            }

            double kolicinaSaPovecanjem = kolicina * 1.01;


            // Provera da li postoji dovoljno energije u podsistemu (uzimajući u obzir 1% smanjenja)
            if (podsistem.PreostalaKolicina < kolicinaSaPovecanjem)
            {
                //Console.WriteLine($"Nema dovoljno energije u podsistemu '{podsistem.Sifra}'. Preostala kolicina je {podsistem.PreostalaKolicina:F2} kWh.");
                // Postavljanje preostale količine na 0, ako nije dovoljno energije
                podsistem.PreostalaKolicina = 0;
                //return;
            }
            podsistem.PreostalaKolicina -= kolicinaSaPovecanjem;

            Console.WriteLine($"Kolicina energije u podsistemu '{podsistem.Sifra}' smanjena za {kolicina:F2} kWh + 1% usled nesavrsenosti sistema.");
            Console.WriteLine($"Preostala kolicina: {podsistem.PreostalaKolicina:F2} kWh.\n");
        }

    }
}
