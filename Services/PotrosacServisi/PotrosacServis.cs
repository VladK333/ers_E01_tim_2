using Domain.Models;
using Domain.Repositories.PodsistemPotrosnjeRepozitorijum;
using Domain.Repositories.PotrosacRepozitorijum;

namespace Domain.Services
{
    public class PotrosacServis : IPotrosac
    {
        private readonly IPotrosacRepozitorijum _repozitorijum;
        private readonly IPotrosnjaRepozitorijum _potrosnjaRepozitorijum;
        public PotrosacServis(IPotrosacRepozitorijum repozitorijum, IPotrosnjaRepozitorijum potrosnjaRepozitorijum)
        {
            _repozitorijum = repozitorijum;
            _potrosnjaRepozitorijum = potrosnjaRepozitorijum;
        }

        public void DodajPotrosaca(Potrosac potrosac)
        {
            if (potrosac != null)
            {
                _repozitorijum.Dodaj(potrosac);
                _potrosnjaRepozitorijum.DodajPotrosacaUPodsistem(potrosac);
            }
        }

        public Potrosac? PronadjiPotrosaca(string Id)
        {
            return _repozitorijum.PronadjiPoId(Id);
        }

        public IEnumerable<Potrosac> GetPotrosaci()
        {
            return _repozitorijum.VratiSve();
        }
    }
}

