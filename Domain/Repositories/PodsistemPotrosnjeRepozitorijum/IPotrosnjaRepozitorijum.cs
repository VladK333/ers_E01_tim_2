using Domain.Models;

namespace Domain.Repositories.PodsistemPotrosnjeRepozitorijum
{
    public interface IPotrosnjaRepozitorijum
    {
        List<PodsistemPotrosnje> DohvatiSvePodsisteme();
    }
}
