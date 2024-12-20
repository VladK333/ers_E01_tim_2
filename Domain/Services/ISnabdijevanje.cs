using Domain.Models;

namespace Domain.Services
{
    public interface ISnabdijevanje
    {
        double CijenaPoKW { get; }

        double SmanjenjeKolicine(double kolicina);

        public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina);

    }
}
