using Domain.Models;
using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class GarantovanoServis : ISnabdijevanje
    {
        private static readonly GarantovanoServis _instance = new GarantovanoServis();
        //private readonly IProizvodnjaEnergije _proizvodnjaServis;

       /* public GarantovanoServis(IProizvodnjaEnergije proizvodnjaServis)
        {
            _proizvodnjaServis = proizvodnjaServis;
        }
       */
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
                Console.WriteLine("Podsistem mora biti validan, a kolicina pozitivna.");
                return;
            }

            double kolicinaSaPovecanjem = kolicina * 1.02;
            if (podsistem.PreostalaKolicina < kolicinaSaPovecanjem)
            {
                // Console.WriteLine($"Nema dovoljno energije u podsistemu '{podsistem.Sifra}'. Preostala kolicina je {podsistem.PreostalaKolicina:F2} kWh.");
                // Postavljanje preostale količine na 0, ako nije dovoljno energije
                podsistem.PreostalaKolicina = 0;
                //return;
            }
            else
            {
                podsistem.PreostalaKolicina -= kolicinaSaPovecanjem;
                //_proizvodnjaServis.ProvjeriIPovecajKolicinu();
            }

            Console.WriteLine($"Kolicina energije u podsistemu '{podsistem.Sifra}' smanjena za {kolicina:F2} kWh + 2% usled nesavrsenosti sistema.");
            Console.WriteLine($"Preostala kolicina: {podsistem.PreostalaKolicina:F2} kWh.\n");
        }



    }
}
