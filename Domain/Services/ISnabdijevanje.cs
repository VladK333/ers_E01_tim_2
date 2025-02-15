using Domain.Models;

namespace Domain.Services
{
    public interface ISnabdijevanje
    {
        public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina);
    }
}
