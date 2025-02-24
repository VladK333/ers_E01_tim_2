using Domain.Enums;
using Domain.Models;
using Domain.Repositories.PodsistemProizvodnjeRepozitorijum;
using Domain.Services;
using Moq;
using Services.ProizvodnjaServisi;

namespace Services.Tests
{
    [TestFixture]
    public class UpravljanjePodsistemimaServisTests
    {
        private Mock<IProizvodnjaRepozitorijum> _mockRepozitorijum;
        private Mock<IIspis> _mockIspisServis;
        private UpravljanjePodsistemimaServis _servis;

        [SetUp]
        public void Setup()
        {
            _mockRepozitorijum = new Mock<IProizvodnjaRepozitorijum>();
            _mockIspisServis = new Mock<IIspis>();
            _servis = new UpravljanjePodsistemimaServis(_mockRepozitorijum.Object, _mockIspisServis.Object);
        }

        [Test]
        public void NadjiPodNadjiPodsistemSaNajviseEnergije_TrebaDaVratiPodsistemSaNajviseEnergije_Dostupan()
        {
            var podsistemi = new List<PodsistemProizvodnje>
            {
                new("PP221-NS1", TipProizvodnje.Hidroelektrana, "Lokacija 1", 500),
                new("PP222-NS2", TipProizvodnje.EcoGreen, "Lokacija 2", 1500),
                new("PP223-NS3", TipProizvodnje.CvrstoGorivo, "Lokacija 3", 1000)
            };

            _mockRepozitorijum.Setup(r => r.DohvatiSvePodsisteme()).Returns(podsistemi);

            var rezultat = _servis.NadjiPodsistemSaNajviseEnergije(500);

            Assert.That(rezultat, Is.Not.Null);
            Assert.That(rezultat.Sifra, Is.EqualTo("PP222-NS2")); //Ocekivani podsistem sa najvise energije
        }

        [Test]
        public void NadjiPodsistemSaNajviseEnergije_TrebaDaVratiNull_KadaNemaDostupnogPodsistema()
        {
            var podsistemi = new List<PodsistemProizvodnje>();

            _mockRepozitorijum.Setup(r => r.DohvatiSvePodsisteme()).Returns(podsistemi);

            var rezultat = _servis.NadjiPodsistemSaNajviseEnergije(500);

            Assert.That(rezultat, Is.Null);
        }

        [Test]
        public void DohvatiSvePodsisteme_TrebaDaVratiSvePodsisteme()
        {
            var podsistemi = new List<PodsistemProizvodnje>
            {
                new("PP221-NS1", TipProizvodnje.Hidroelektrana, "Lokacija 1", 1000),
                new("PP222-NS2", TipProizvodnje.EcoGreen, "Lokacija 2", 2000)
            };

            _mockRepozitorijum.Setup(r => r.DohvatiSvePodsisteme()).Returns(podsistemi);

            var rezultat = _servis.DohvatiSvePodsisteme();

            Assert.That(rezultat.Count(), Is.EqualTo(2));
            Assert.That(rezultat.First().Sifra, Is.EqualTo("PP221-NS1")); // Provjeravamo da li je prvi podsistem ocekivani
        }
    }
}
