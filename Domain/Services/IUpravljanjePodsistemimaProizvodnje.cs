using Domain.Models;

namespace Domain.Services
{
    public interface IUpravljanjePodsistemimaProizvodnje
    {
        void DodajPodsistem(PodsistemProizvodnje podsistem);
        List<PodsistemProizvodnje> DohvatiSvePodsisteme();
    }
}
