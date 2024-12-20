using Domain.Models;

namespace Domain.Services
{
    public interface IPotrosac
    {
        void DodajPotrosaca(Potrosac potrosac);
        Potrosac PronadjiPotrosaca(string id);
        void AzurirajPotrosaca(Potrosac potrosac);
        void ObrisiPotrosaca(string id);

        public List<Potrosac> GetPotrosaci();

    }
}
