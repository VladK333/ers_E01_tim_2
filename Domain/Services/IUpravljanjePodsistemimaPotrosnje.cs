using Domain.Models;

namespace Domain.Services
{
    public interface IUpravljanjePodsistemimaPotrosnje
    {
        List<PodsistemPotrosnje> DohvatiSvePodsisteme();
        public Potrosac? PronadjiPotrosaca(string idPotrosaca);
    }
}
