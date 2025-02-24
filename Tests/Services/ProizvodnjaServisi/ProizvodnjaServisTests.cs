using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Moq;
using Services.ProizvodnjaServisi;

namespace Tests.Services.ProizvodnjaServisi
{
    [TestFixture]
    public class ProizvodnjaServisTests
    {
        private Mock<IUpravljanjePodsistemimaProizvodnje> _mockUpravljanjePodsistemima;
        private Mock<IIspis> _mockIspisServis;
        private ProizvodnjaServis _proizvodnjaServis;

        [SetUp]
        public void SetUp()
        {
            _mockUpravljanjePodsistemima = new Mock<IUpravljanjePodsistemimaProizvodnje>();
            _mockIspisServis = new Mock<IIspis>();

            _proizvodnjaServis = new ProizvodnjaServis(_mockUpravljanjePodsistemima.Object, _mockIspisServis.Object);
        }

        [Test]
        public void ProvjeriIPovecajKolicinu_GarantovanoSnabdijevanje_PovecavaPreostaluKolicinu()
        {
            var podsistemi = new List<PodsistemProizvodnje>
            {
                new("PP221-NS1", TipProizvodnje.Hidroelektrana, "Lokacija 1", 90),
                new("PP222-NS2", TipProizvodnje.EcoGreen, "Lokacija 2", 80)
            };

            _mockUpravljanjePodsistemima.Setup(x => x.DohvatiSvePodsisteme()).Returns(podsistemi);

            _proizvodnjaServis.ProvjeriIPovecajKolicinu(TipSnabdijevanja.GARANTOVANO);

            Assert.That(podsistemi[0].PreostalaKolicina, Is.EqualTo(109.8).Within(0.01));
            Assert.That(podsistemi[1].PreostalaKolicina, Is.EqualTo(97.6).Within(0.01));

            _mockIspisServis.Verify(x => x.Ispisi(It.IsAny<string>()), Times.AtLeastOnce);//Times.AtLeastOnce, metoda pozvana bar jednom
        }
        [Test]
        public void ProvjeriIPovecajKolicinu_KomercijalnoSnabdijevanje_PovecavaPreostaluKolicinu()
        {
            var podsistemi = new List<PodsistemProizvodnje>
            {
                new("PP223-NS3", TipProizvodnje.CvrstoGorivo, "Lokacija 3", 80),
                new("PP224-NS4", TipProizvodnje.Hidroelektrana, "Lokacija 4", 150) 
            };

            _mockUpravljanjePodsistemima.Setup(x => x.DohvatiSvePodsisteme()).Returns(podsistemi);

            _proizvodnjaServis.ProvjeriIPovecajKolicinu(TipSnabdijevanja.KOMERCIJALNO);

            Assert.That(podsistemi[0].PreostalaKolicina, Is.EqualTo(91.2).Within(0.01));
            Assert.That(podsistemi[1].PreostalaKolicina, Is.EqualTo(150.0).Within(0.01));

            _mockIspisServis.Verify(x => x.Ispisi(It.Is<string>(s => s.Contains("[KOMERCIJALNO]"))), Times.AtLeastOnce);
        }

        [Test]
        public void ProvjeriIPovecajKolicinu_NemaPromjenaZaVeceKolicine()
        {
            var podsistemi = new List<PodsistemProizvodnje>
            {
                new("PP225-NS5", TipProizvodnje.EcoGreen, "Lokacija 5", 150),
                new("PP226-NS6", TipProizvodnje.Hidroelektrana, "Lokacija 6", 200)
            };

            _mockUpravljanjePodsistemima.Setup(x => x.DohvatiSvePodsisteme()).Returns(podsistemi);

            _proizvodnjaServis.ProvjeriIPovecajKolicinu(TipSnabdijevanja.GARANTOVANO);

            Assert.That(podsistemi[0].PreostalaKolicina, Is.EqualTo(150));
            Assert.That(podsistemi[1].PreostalaKolicina, Is.EqualTo(200)); 

            _mockIspisServis.Verify(x => x.Ispisi(It.IsAny<string>()), Times.Never);
        }
    }
}
