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
    }
}
