using Domain.Services;
using Services.SnabdijevanjeServisi;

namespace Services.ProizvodnjaServisi
{
    public class ProizvodnjaServis : IProizvodnjaEnergije
    {
        private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemima;

        public ProizvodnjaServis(IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemima)
        {
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

                    if (GarantovanoServis.Instance != null)
                    {
                        podsistem.PreostalaKolicina *= 1.22;
                        Console.WriteLine($"[GARANTOVANO] Podsistem '{podsistem.Sifra}' imao je {prethodnaKolicina:F2} kWh. " +
                                          $"Povecano na {podsistem.PreostalaKolicina:F2} kWh " +
                                          $"(povecanje za {podsistem.PreostalaKolicina - prethodnaKolicina:F2} kWh).");
                    }
                    else if (KomercijalnoServis.Instance != null)
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
