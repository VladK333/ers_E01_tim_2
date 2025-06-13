using Domain.Enums;
using Domain.Models;

namespace Tests.Domain.Models
{
    [TestFixture]
    public class PodsistemPotrosnjeTests
    {
        [Test]
        [TestCase("Podsistem 1", "PSP3321-NS1")]
        [TestCase("Podsistem 2", "PSP3322-NS2")]
        [TestCase("Podsistem 3", "PSP3323-NS3")]
        public void PodsistemPotrosnjeKonstruktor_Ok(string nazivPodsistema, string sifraPotrosnje)
        {
            var potrosaci = new List<Potrosac>
            {
                new("Marko Markovic", "EPS3345K", TipSnabdijevanja.GARANTOVANO, 120.5, 50),
                new("Jovana Jovanovic", "EPS3346K", TipSnabdijevanja.KOMERCIJALNO, 200.3, 75)
            };

            var podsistem = new PodsistemPotrosnje(nazivPodsistema, sifraPotrosnje, potrosaci);

            Assert.That(podsistem, Is.Not.Null);
            Assert.That(podsistem.NazivPodsistema, Is.EqualTo(nazivPodsistema));
            Assert.That(podsistem.SifraPotrosnje, Is.EqualTo(sifraPotrosnje));
            Assert.That(podsistem.Potrosaci, Is.Not.Null);
            Assert.That(podsistem.Potrosaci.Count, Is.EqualTo(potrosaci.Count));

            Assert.That(podsistem.Potrosaci[0].ImePrezime, Is.EqualTo("Marko Markovic"));
            Assert.That(podsistem.Potrosaci[1].ImePrezime, Is.EqualTo("Jovana Jovanovic"));
        }

        [Test]
        public void PodsistemPotrosnjeKonstruktor_BezPotrosaca()
        {
            var potrosaci = new List<Potrosac>(); 
            var podsistem = new PodsistemPotrosnje("Podsistem 4", "PSP3324-NS4", potrosaci);

            Assert.That(podsistem.Potrosaci.Count, Is.EqualTo(0));
        }

        [Test]
        public void PodsistemPotrosnjeKonstruktor_SamoJedanPotrosac()
        {
            var potrosaci = new List<Potrosac>
            {
                new("Marko Markovic", "EPS3345K", TipSnabdijevanja.GARANTOVANO, 120.5, 50)
            };
            var podsistem = new PodsistemPotrosnje("Podsistem 5", "PSP3325-NS5", potrosaci);

            Assert.That(podsistem.Potrosaci.Count, Is.EqualTo(1));
            Assert.That(podsistem.Potrosaci[0].ImePrezime, Is.EqualTo("Marko Markovic"));
        }
    }
}
