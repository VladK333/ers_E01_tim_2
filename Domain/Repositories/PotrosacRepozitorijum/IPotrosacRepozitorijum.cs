using Domain.Models;

namespace Domain.Repositories.PotrosacRepozitorijum
{
    public interface IPotrosacRepozitorijum
    {
        void Dodaj(Potrosac potrosac);
        Potrosac? PronadjiPoId(string id);
        IEnumerable<Potrosac> VratiSve();

    }
}
