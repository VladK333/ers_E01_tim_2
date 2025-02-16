using Domain.Models;
using Domain.Repositories.PotrosacRepozitorijum;

namespace Domain.Repositories.PodsistemPotrosnjeRepozitorijum
{
    public class PotrosnjaRepozitorijum : IPotrosnjaRepozitorijum
    {
        private readonly List<PodsistemPotrosnje> _podsistemi;
        private readonly IPotrosacRepozitorijum potrosaci;

        public PotrosnjaRepozitorijum(IPotrosacRepozitorijum potrosacRepozitorijum)
        {
           potrosaci = potrosacRepozitorijum;

            _podsistemi =
            [
                new("Podsistem 1", "PSP3321-NS1", potrosaci.VratiSve())
            ];
        }

        public List<PodsistemPotrosnje> DohvatiSvePodsisteme()
        {
            return _podsistemi;
        }
    }
}