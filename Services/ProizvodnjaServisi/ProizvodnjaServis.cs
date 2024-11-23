using Domain.Services;
using Services.SnabdijevanjeServisi;

namespace Services.ProizvodnjaServisi
{
    public class ProizvodnjaServis : IProizvodnjaEnergije
    {
        private readonly ISnabdijevanje _snabdijevanjeServis;
        private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemima;

        public ProizvodnjaServis(ISnabdijevanje snabdijevanjeServis, IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemima)
        {
            _snabdijevanjeServis = snabdijevanjeServis;
            _upravljanjePodsistemima = upravljanjePodsistemima;
        }

        public void ProvjeriIPovecajKolicinu()
        {
            var podsistemi = _upravljanjePodsistemima.DohvatiSvePodsisteme();

            foreach (var podsistem in podsistemi)
            {
                if (podsistem.PreostalaKolicina < 100)
                {
                    if (_snabdijevanjeServis is GarantovanoServis)
                    {
                        podsistem.PreostalaKolicina *= 1.22;
                    }
                    else if (_snabdijevanjeServis is KomercijalnoServis)
                    {
                        podsistem.PreostalaKolicina *= 1.14;
                    }
                }
            }
        }
    }
}
