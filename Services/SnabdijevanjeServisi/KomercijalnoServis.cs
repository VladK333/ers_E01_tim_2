using Domain.Models;
using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class KomercijalnoServis : ISnabdijevanje
    {
        private static readonly KomercijalnoServis _instance = new();

        private KomercijalnoServis() { }

        public static KomercijalnoServis Instance => _instance;

        public bool SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina)
        {
            double kolicinaSaPovecanjem = kolicina * 1.01;

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
