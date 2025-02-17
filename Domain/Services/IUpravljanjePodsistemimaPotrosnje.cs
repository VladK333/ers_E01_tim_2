using Domain.Models;

namespace Domain.Services
{
    public interface IUpravljanjePodsistemimaPotrosnje
    {
        IEnumerable<PodsistemPotrosnje> DohvatiSvePodsisteme();
        Potrosac? PronadjiPotrosaca(string idPotrosaca);
    }
}
