using Domain.Models;

namespace Domain.Services
{
    public interface ISnabdijevanje
    {
        public bool SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina);
    }
}
