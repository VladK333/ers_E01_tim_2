using Domain.Enums;
using Domain.Models;
using Domain.Repositories.PodsistemPotrosnjeRepozitorijum;
using Moq;
using Services.PodsistemPotrosnjeServisi;

namespace Tests.Services.PodsistemPotrosnjeServisi
{
    [TestFixture]
    public class UpravljanjePodsistemimaPotrosnjeServisTestovi
    {
        private Mock<IPotrosnjaRepozitorijum> _mockPotrosnjaRepozitorijum;
        private UpravljanjePodsistemimaPotrosnjeServis _servis;

        [SetUp]
        public void SetUp()
        {
            // Mockovanje repozitorijuma
            _mockPotrosnjaRepozitorijum = new Mock<IPotrosnjaRepozitorijum>();

            // Kreiranje instance servisa
            _servis = new UpravljanjePodsistemimaPotrosnjeServis(_mockPotrosnjaRepozitorijum.Object);
        }

        [Test]
        public void DohvatiSvePodsisteme_VratiSvePodsisteme()
        {
            var podsistemi = new List<PodsistemPotrosnje>
            {
                new PodsistemPotrosnje("Podsistem 1", "PSP3321-NS1", new List<Potrosac>()),
                new PodsistemPotrosnje("Podsistem 2", "PSP3321-NS2", new List<Potrosac>())
            };

            _mockPotrosnjaRepozitorijum.Setup(r => r.DohvatiSvePodsisteme()).Returns(podsistemi);

            var rezultat = _servis.DohvatiSvePodsisteme();

            Assert.That(rezultat.Count(), Is.EqualTo(2));
        }

        [Test]
        public void PronadjiPotrosaca_PotrosacPostoji_VracaPotrosaca()
        {
            var potrosacId = Guid.NewGuid().ToString();  

            var potrosac = new Potrosac("Ana Petrovic", "EPS5678K", TipSnabdijevanja.KOMERCIJALNO, 340.00, 14626.8)
            {
                Id = potrosacId // Postavi ID koji se koristi u testu
            };

            var podsistemi = new List<PodsistemPotrosnje>
            {
                new PodsistemPotrosnje("Podsistem 1", "PSP3321-NS1", new List<Potrosac>
                {
                    potrosac, 
                    new("Nikola Ilic", "EPS9101K", TipSnabdijevanja.GARANTOVANO, 250.75, 5697.04)
                }),

                new("Podsistem 2", "PSP3321-NS2", [])
            };

            _mockPotrosnjaRepozitorijum.Setup(r => r.DohvatiSvePodsisteme()).Returns(podsistemi);

            var rezultat = _servis.PronadjiPotrosaca(potrosacId);

            Assert.That(rezultat, Is.Not.Null);
            Assert.That(rezultat?.Id, Is.EqualTo(potrosacId));
        }



        [Test]
        public void PronadjiPotrosaca_PotrosacNePostoji_VracaNull()
        {
            var potrosacId = "NepostojeciID";
            var podsistemi = new List<PodsistemPotrosnje>
            {
                new("Podsistem 1", "PSP3321-NS1", new List<Potrosac>
                {
                    new("Ana Petrovic", "EPS5678K", TipSnabdijevanja.KOMERCIJALNO, 340.00, 14626.8)
                })
            };

            _mockPotrosnjaRepozitorijum.Setup(r => r.DohvatiSvePodsisteme()).Returns(podsistemi);

            var rezultat = _servis.PronadjiPotrosaca(potrosacId);

            Assert.That(rezultat, Is.Null);
        }
    }
}
