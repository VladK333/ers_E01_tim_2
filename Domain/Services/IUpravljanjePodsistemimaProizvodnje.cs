using Domain.Enums;
using Domain.Models;

namespace Domain.Services
{
    public interface IUpravljanjePodsistemimaProizvodnje
    {
        List<PodsistemProizvodnje> DohvatiSvePodsisteme();

        public PodsistemProizvodnje? NadjiPodsistemSaNajviseEnergije(double potrebnaEnergija);

        public double DohvatiNajvecuDostupnuEnergiju();

    }
}
