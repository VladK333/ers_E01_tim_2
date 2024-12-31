using Domain.Models;

namespace Domain.Services
{
    public interface ISnabdijevanje
    {
        double CijenaPoKW { get; }

        public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina);

    }
}
