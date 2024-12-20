using Domain.Models;
using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class GarantovanoServis : ISnabdijevanje
    {
        public double CijenaPoKW
        {
            get { return 22.72; }
        }

        public double SmanjenjeKolicine(double kolicina)
        {
            // Validacija unosa količine energije
            if (kolicina <= 0)
            {
                throw new ArgumentException($"Količina energije mora biti pozitivna. Količina: {kolicina}.");
            }

            // Implementacija smanjenja količine energije
            double smanjenaKolicina = kolicina - (kolicina * 0.02);

            // Povratna informacija korisnicima (ako je potrebno)
            Console.WriteLine($"Količina energije smanjena za 2%: {smanjenaKolicina} kWh");

            return smanjenaKolicina;
        }

       /* public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina)
        {
            if (podsistem == null || kolicina <= 0)
                throw new ArgumentException("Podsistem mora biti validan, a količina pozitivna.");

            podsistem.PreostalaKolicina -= kolicina-(kolicina * 0.02);

            Console.WriteLine($"Količina energije u podsistemu '{podsistem.Sifra}' smanjena za {kolicina:F2} kWh.");
        }*/
    }
}
