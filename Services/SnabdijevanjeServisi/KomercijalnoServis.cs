using Domain.Models;
using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class KomercijalnoServis : ISnabdijevanje
    {
        public double CijenaPoKW
        {
            get { return 43.02; }
        }

        public double SmanjenjeKolicine(double kolicina)
        {
            if (kolicina <= 0)
            {
                throw new ArgumentException($"Količina energije mora biti pozitivna. Količina: {kolicina}.");
            }

            double smanjenaKolicina = kolicina - (kolicina * 0.01);

            Console.WriteLine($"Količina energije smanjena za 1%: {smanjenaKolicina} kWh");

            return smanjenaKolicina;
        }

        public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina)
        {
            
            if (podsistem == null)
                throw new ArgumentException("Podsistem mora biti validan.");

            if (kolicina <= 0)
                throw new ArgumentException("Količina mora biti pozitivna.");

            if (kolicina > podsistem.PreostalaKolicina)
            {
                throw new InvalidOperationException("Nema dovoljno energije u podsistemu.");
            }

            double smanjenaKolicina = kolicina - (kolicina * 0.01);

            podsistem.PreostalaKolicina -= smanjenaKolicina;

            Console.WriteLine($"Količina energije u podsistemu '{podsistem.Sifra}' smanjena za {smanjenaKolicina:F2} kWh. Preostala količina: {podsistem.PreostalaKolicina:F2} kWh.");
        }
    }
}
