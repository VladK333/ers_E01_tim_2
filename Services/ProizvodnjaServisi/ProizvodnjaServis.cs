using Domain.Enums;
using Domain.Services;

namespace Services.ProizvodnjaServisi
{
    public class ProizvodnjaServis : IProizvodnjaEnergije
    {
        private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemima;
        private readonly IIspis _ispisServis;

        public ProizvodnjaServis(IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemima, IIspis ispisServis)
        {
            _upravljanjePodsistemima = upravljanjePodsistemima;
            _ispisServis = ispisServis;
        }

        public void ProvjeriIPovecajKolicinu(TipSnabdijevanja tipSnabdijevanja)
        {
            var podsistemi = _upravljanjePodsistemima.DohvatiSvePodsisteme();

            foreach (var podsistem in podsistemi)
            {
                if (podsistem.PreostalaKolicina < 100)
                {
                    double prethodnaKolicina = podsistem.PreostalaKolicina;

                    if (tipSnabdijevanja == TipSnabdijevanja.GARANTOVANO)
                    {
                        podsistem.PreostalaKolicina *= 1.22;
                        if (podsistem.PreostalaKolicina == 0.00)
                            _ispisServis.Ispisi($"[GARANTOVANO] Podsistem '{podsistem.Sifra}' imao je {prethodnaKolicina:F2} kWh. Nije preostalo energije u ovom podsistemu.");
                        else
                            _ispisServis.Ispisi($"[GARANTOVANO] Podsistem '{podsistem.Sifra}' imao je {prethodnaKolicina:F2} kWh. " +
                                             $"Povecano na {podsistem.PreostalaKolicina:F2} kWh " +
                                             $"(povecanje za {podsistem.PreostalaKolicina - prethodnaKolicina:F2} kWh).");
                    }
                    else if (tipSnabdijevanja == TipSnabdijevanja.KOMERCIJALNO)
                    {
                        podsistem.PreostalaKolicina *= 1.14;
                        if (podsistem.PreostalaKolicina == 0.00)
                            _ispisServis.Ispisi($"[KOMERCIJALNO] Podsistem '{podsistem.Sifra}' imao je {prethodnaKolicina:F2} kWh. Nije preostalo energije u ovom podsistemu.");
                        else
                            _ispisServis.Ispisi($"[KOMERCIJALNO] Podsistem '{podsistem.Sifra}' imao je {prethodnaKolicina:F2} kWh. " +
                                             $"Povecano na {podsistem.PreostalaKolicina:F2} kWh " +
                                             $"(povecanje za {podsistem.PreostalaKolicina - prethodnaKolicina:F2} kWh).");
                    }
                }
            }
        }
    }
}
