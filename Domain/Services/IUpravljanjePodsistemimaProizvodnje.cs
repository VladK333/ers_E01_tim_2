using Domain.Enums;
using Domain.Models;

namespace Domain.Services
{
    public interface IUpravljanjePodsistemimaProizvodnje
    {
        void DodajPodsistem(string sifra, TipProizvodnje tip, string lokacija);
        List<PodsistemProizvodnje> DohvatiSvePodsisteme();
    }
}
