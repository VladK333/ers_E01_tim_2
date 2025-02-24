using Domain.Models;
using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class GarantovanoServis : ISnabdijevanje
    {
        private static readonly GarantovanoServis _instance = new GarantovanoServis();

        private GarantovanoServis() { }

        public static GarantovanoServis Instance => _instance;

        public bool SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina)
        {
            double kolicinaSaPovecanjem = kolicina * 1.02;

            if (podsistem.PreostalaKolicina < kolicinaSaPovecanjem)
            {
                podsistem.PreostalaKolicina = 0;
            }
            else
            {
                podsistem.PreostalaKolicina -= kolicinaSaPovecanjem;
            }
            return true;
        }
    }
}
