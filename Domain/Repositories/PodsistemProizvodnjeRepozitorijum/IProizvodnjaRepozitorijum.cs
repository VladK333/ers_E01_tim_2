using Domain.Models;

namespace Domain.Repositories.PodsistemProizvodnjeRepozitorijum
{
    public interface IProizvodnjaRepozitorijum
    {
        List<PodsistemProizvodnje> DohvatiSvePodsisteme();

    }
}
