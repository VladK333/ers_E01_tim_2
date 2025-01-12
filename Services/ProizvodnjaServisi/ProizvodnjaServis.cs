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
                    double prethodnaKolicina = podsistem.PreostalaKolicina;

                    if (_snabdijevanjeServis is GarantovanoServis)
                    {
                        podsistem.PreostalaKolicina *= 1.22;
                        Console.WriteLine($"[GARANTOVANO] Podsistem '{podsistem.Sifra}' imao je {prethodnaKolicina:F2} kWh. " +
                                          $"Povecano na {podsistem.PreostalaKolicina:F2} kWh " +
                                          $"(povecanje za {podsistem.PreostalaKolicina - prethodnaKolicina:F2} kWh).");
                    }
                    else if (_snabdijevanjeServis is KomercijalnoServis)
                    {
                        podsistem.PreostalaKolicina *= 1.14;
                        Console.WriteLine($"[KOMERCIJALNO] Podsistem '{podsistem.Sifra}' imao je {prethodnaKolicina:F2} kWh. " +
                                          $"Povecano na {podsistem.PreostalaKolicina:F2} kWh " +
                                          $"(povecanje za {podsistem.PreostalaKolicina - prethodnaKolicina:F2} kWh).");
                    }
                }
            }
        }
    }
}
