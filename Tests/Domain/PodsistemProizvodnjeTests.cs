using Domain.Enums;
using Domain.Models;

namespace Tests.Domain
{
    [TestFixture]
    public class PodsistemProizvodnjeTests
    {
        [Test]
        [TestCase("PP221-NS1", TipProizvodnje.Hidroelektrana, "Lokacija 1", 5000)]
        [TestCase("PP222-NS2", TipProizvodnje.EcoGreen, "Lokacija 2", 6000)]
        [TestCase("PP223-NS3", TipProizvodnje.CvrstoGorivo, "Lokacija 3", 7000)]
        public void PodsistemProizvodnjeKonstruktor_Ok(string sifra, TipProizvodnje tip, string lokacija, double preostalaKolicina)
        {
            var podsistem = new PodsistemProizvodnje(sifra, tip, lokacija, preostalaKolicina);

            Assert.That(podsistem.Sifra, Is.EqualTo(sifra));
            Assert.That(podsistem.Tip, Is.EqualTo(tip));
            Assert.That(podsistem.Lokacija, Is.EqualTo(lokacija));
            Assert.That(podsistem.PreostalaKolicina, Is.EqualTo(preostalaKolicina));
        }
    }
}
