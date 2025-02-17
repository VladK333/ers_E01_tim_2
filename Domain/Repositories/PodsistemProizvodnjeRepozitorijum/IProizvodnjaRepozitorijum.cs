using Domain.Models;

namespace Domain.Repositories.PodsistemProizvodnjeRepozitorijum
{
    public interface IProizvodnjaRepozitorijum
    {
        IEnumerable<PodsistemProizvodnje> DohvatiSvePodsisteme();
    }
}
