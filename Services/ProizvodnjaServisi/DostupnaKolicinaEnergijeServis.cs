using Domain.Services;

namespace Services.ProizvodnjaServisi
{
    public class DostupnaKolicinaEnergijeServis : IDostupnaKolicinaEnergije
    {
        private static Random _random = new Random();

        public double Generate(double minVr, double maxVr)
        {
            return _random.NextDouble() * (maxVr - minVr) + minVr;
        }
    }
}
