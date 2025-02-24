using Domain.Enums;
using Domain.Models;
using Services.SnabdijevanjeServisi;

namespace Tests.Services.SnabdijevanjeServisi
{
    public class GarantovanoServisTests
    {
        [Test]
        public void SmanjiKolicinuEnergije_TrebaDaSmanjiEnergijuSaPovecanjemKadaJeDovoljnoDostupno()
        {
            var podsistem = new PodsistemProizvodnje("PP221-NS1", TipProizvodnje.Hidroelektrana, "Lokacija 1", 1000);
            double trazenaKolicina = 500;
            double ocekivanaKolicina = 1000 - (500 * 1.02);

            bool rezultat = GarantovanoServis.Instance.SmanjiKolicinuEnergije(podsistem, trazenaKolicina);

            Assert.That(rezultat, Is.True);
            Assert.That(podsistem.PreostalaKolicina, Is.EqualTo(ocekivanaKolicina).Within(0.01)); // Tolerancija zbog decimalnih operacija
        }

        [Test]
        public void SmanjiKolicinuEnergije_TrebaDaPostaviEnergijuNaNuluKadaNemaDovoljno()
        {
            var podsistem = new PodsistemProizvodnje("PP221-NS1", TipProizvodnje.Hidroelektrana, "Lokacija 1", 100);
            double trazenaKolicina = 200; 

            bool rezultat = GarantovanoServis.Instance.SmanjiKolicinuEnergije(podsistem, trazenaKolicina);

            Assert.That(rezultat, Is.True);
            Assert.That(podsistem.PreostalaKolicina, Is.EqualTo(0));
        }
    }
}
