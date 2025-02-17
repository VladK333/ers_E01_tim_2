using Domain.Models;

namespace Domain.Services
{
    public interface ISnabdijevanje
    {
        bool SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina);
    }
}
