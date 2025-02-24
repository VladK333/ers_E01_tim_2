using Domain.Enums;
using Domain.Models;

namespace Tests.Domain.Models
{
    [TestFixture]
    public class PotrosacTests
    {
        [Test]
        [TestCase("Marko Markovic", "EPS3345K", TipSnabdijevanja.GARANTOVANO, 120.5, 50)]
        [TestCase("Jovana Jovanovic", "EPS3346K", TipSnabdijevanja.KOMERCIJALNO, 200.3, 75)]
        [TestCase("Ana Ivanovic", "EPS3347K", TipSnabdijevanja.GARANTOVANO, 350.8, 100)]
        public void PotrosacKonstruktor_Ok(string imePrezime, string brUgovora, TipSnabdijevanja tipSnabdevanja, double ukupnaPotrosnjaEe, double trenutnoZaduzenje)
        {
            var potrosac = new Potrosac(imePrezime, brUgovora, tipSnabdevanja, ukupnaPotrosnjaEe, trenutnoZaduzenje);

            Assert.That(potrosac, Is.Not.Null);
            Assert.That(potrosac.ImePrezime, Is.EqualTo(imePrezime));
            Assert.That(potrosac.BrUgovora, Is.EqualTo(brUgovora));
            Assert.That(potrosac.Tip_Snabdevanja, Is.EqualTo(tipSnabdevanja));
            Assert.That(potrosac.Ukupna_potrosnja_ee, Is.EqualTo(ukupnaPotrosnjaEe));
            Assert.That(potrosac.Trenutno_zaduzenje, Is.EqualTo(trenutnoZaduzenje));
        }

        [Test]
        public void PotrosacKonstruktor_PrazniPodaci()
        {
            var potrosac = new Potrosac();

            Assert.That(potrosac.Id, Is.Not.Null);  
            Assert.That(potrosac.ImePrezime, Is.EqualTo(string.Empty));
            Assert.That(potrosac.BrUgovora, Is.EqualTo(string.Empty));
            Assert.That(potrosac.Tip_Snabdevanja, Is.EqualTo(default(TipSnabdijevanja)));
            Assert.That(potrosac.Ukupna_potrosnja_ee, Is.EqualTo(0));
            Assert.That(potrosac.Trenutno_zaduzenje, Is.EqualTo(0));
        }
    }
}
