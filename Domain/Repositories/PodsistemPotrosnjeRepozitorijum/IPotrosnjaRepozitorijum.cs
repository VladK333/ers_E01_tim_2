using Domain.Models;

namespace Domain.Repositories.PodsistemPotrosnjeRepozitorijum
{
    public interface IPotrosnjaRepozitorijum
    {
        IEnumerable<PodsistemPotrosnje> DohvatiSvePodsisteme();
        void DodajPotrosacaUPodsistem(Potrosac potrosac);
    }
}
