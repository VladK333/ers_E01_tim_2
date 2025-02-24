using Domain.Models;

namespace Tests.Domain.Models
{
    [TestFixture]
    public class ZapisTests
    {
        [Test]
        public void ZapisKonstruktor_Ok()
        {
            DateTime datumIVreme = new DateTime(2025, 2, 22, 15, 30, 0);
            double kolicina = 100.5;

            var zapis = new Zapis(datumIVreme, kolicina);

            Assert.That(zapis, Is.Not.Null);
            Assert.That(zapis.DatumIVreme, Is.EqualTo(datumIVreme));
            Assert.That(zapis.Kolicina, Is.EqualTo(kolicina));
        }

        [Test]
        public void ZapisToString_Ok()
        {
            DateTime datumIVreme = new DateTime(2025, 2, 22, 15, 30, 0); 
            double kolicina = 100.5;

            var zapis = new Zapis(datumIVreme, kolicina);

            string ocekivaniString = $"{datumIVreme:dd.MM.yyyy HH:mm:ss} Izdato je {kolicina:F2} kW.";

            string stvarniString = zapis.ToString();

            Assert.That(stvarniString, Is.EqualTo(ocekivaniString));
        }

        [Test]
        public void ZapisKonstruktor_PraznaKolicina()
        {
            DateTime datumIVreme = new DateTime(2025, 2, 22, 15, 30, 0); 
            double kolicina = 0;

            var zapis = new Zapis(datumIVreme, kolicina);

            Assert.That(zapis.Kolicina, Is.EqualTo(0));
            Assert.That(zapis.DatumIVreme.Date, Is.EqualTo(datumIVreme.Date));
        }

    }
}
