using Domain.Models;

namespace Domain.Services
{
    public interface IUpravljanjePodsistemimaProizvodnje
    {
        IEnumerable<PodsistemProizvodnje> DohvatiSvePodsisteme();
        PodsistemProizvodnje? NadjiPodsistemSaNajviseEnergije(double potrebnaEnergija);
    }
}
