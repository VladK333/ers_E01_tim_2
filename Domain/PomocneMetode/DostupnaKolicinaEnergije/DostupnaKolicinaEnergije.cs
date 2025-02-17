namespace Domain.PomocneMetode.DostupnaKolicinaEnergije
{
    public class DostupnaKolicinaEnergije : IDostupnaKolicinaEnergije
    {
        private static readonly Random _random = new Random();

        public double Generate(double minVr, double maxVr)
        {
            return _random.NextDouble() * (maxVr - minVr) + minVr;
        }
    }
}
