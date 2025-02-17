using Domain.Models;

namespace Domain.Repositories.AutentifikacijaRepozitorijum
{
    public interface IAutentifikacijaRepozitorijum
    {
        IEnumerable<Potrosac> DohvatiSveKorisnike();
    }
}
