using Domain.Enums;
using Domain.Models;

namespace Domain.Services
{
    public interface IUpravljanjePodsistemimaProizvodnje
    {
        void DodajPodsistem(string sifra, TipProizvodnje tip, string lokacija);
        List<PodsistemProizvodnje> DohvatiSvePodsisteme();

        public PodsistemProizvodnje NadjiPodsistemSaNajviseEnergije(double potrebnaEnergija);

        public double DohvatiNajvecuDostupnuEnergiju();

        // public PodsistemProizvodnje OdrediPodsistemZaPotrosaca(double potrebnaEnergija);

        public void SmanjiKolicinuEnergije(PodsistemProizvodnje podsistem, double kolicina);

    }
}
