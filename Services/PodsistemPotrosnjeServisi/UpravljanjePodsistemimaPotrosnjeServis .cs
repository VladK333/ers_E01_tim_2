using Domain.Models;
using Domain.Repositories.PodsistemPotrosnjeRepozitorijum;
using Domain.Services;

namespace Services.PodsistemPotrosnjeServisi
{
    public class UpravljanjePodsistemimaPotrosnjeServis : IUpravljanjePodsistemimaPotrosnje
    {
        private readonly IPotrosnjaRepozitorijum _potrosnjaRepozitorijum;

        public UpravljanjePodsistemimaPotrosnjeServis(IPotrosnjaRepozitorijum potrosnjaRepozitorijum)
        {
            _potrosnjaRepozitorijum = potrosnjaRepozitorijum;
        }

        public List<PodsistemPotrosnje> DohvatiSvePodsisteme()
        {
            return _potrosnjaRepozitorijum.DohvatiSvePodsisteme();
        }

        public Potrosac? PronadjiPotrosaca(string idPotrosaca)
        {
            foreach (var podsistem in _potrosnjaRepozitorijum.DohvatiSvePodsisteme())
            {
                var potrosac = podsistem.Potrosaci.FirstOrDefault(p => p.Id == idPotrosaca);

                if (potrosac != null)
                {
                    return potrosac;
                }
            }

            return null;
        }

    }
}
