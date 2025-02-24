using Domain.Models;
using Services.SnabdijevanjeServisi;
using NUnit.Framework;
using Domain.Enums;

namespace Tests.Services.SnabdijevanjeServisi
{
    public class KomercijalnoServisTests
    {
        [Test]
        public void SmanjiKolicinuEnergije_TrebaDaSmanjiEnergijuSaPovecanjemKadaJeDovoljnoDostupno()
        {
            var podsistem = new PodsistemProizvodnje("PP222-NS2", TipProizvodnje.EcoGreen, "Lokacija 2", 1000);
            double trazenaKolicina = 500;
            double ocekivanaKolicina = 1000 - (500 * 1.01);

            bool rezultat = KomercijalnoServis.Instance.SmanjiKolicinuEnergije(podsistem, trazenaKolicina);

            Assert.That(rezultat, Is.True);
            Assert.That(podsistem.PreostalaKolicina, Is.EqualTo(ocekivanaKolicina).Within(0.01)); 
        }

        [Test]
        public void SmanjiKolicinuEnergije_TrebaDaPostaviEnergijuNaNuluKadaNemaDovoljno()
        {
            var podsistem = new PodsistemProizvodnje("PP223-NS3", TipProizvodnje.CvrstoGorivo, "Lokacija 3", 100);
            double trazenaKolicina = 200; 

            bool rezultat = KomercijalnoServis.Instance.SmanjiKolicinuEnergije(podsistem, trazenaKolicina);

            Assert.That(rezultat, Is.True);
            Assert.That(podsistem.PreostalaKolicina, Is.EqualTo(0));
        }
    }
}
