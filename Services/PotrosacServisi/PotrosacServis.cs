using Domain.Models;
using Domain.Repositories.PotrosacRepozitorijum;

namespace Domain.Services
{
    public class PotrosacServis : IPotrosac
    {
        private readonly IPotrosacRepozitorijum _repozitorijum;

        public PotrosacServis(IPotrosacRepozitorijum repozitorijum)
        {
            _repozitorijum = repozitorijum;
        }

        public void DodajPotrosaca(Potrosac potrosac)
        {
            if (potrosac != null)
            {
                _repozitorijum.Dodaj(potrosac);
            }
        }

        public Potrosac? PronadjiPotrosaca(string Id)
        {
            return _repozitorijum.PronadjiPoId(Id);
        }

        public List<Potrosac> GetPotrosaci()
        {
            return _repozitorijum.VratiSve();
        }
    }
}

