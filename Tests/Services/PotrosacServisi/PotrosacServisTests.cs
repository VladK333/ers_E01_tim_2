using Domain.Models;
using Domain.Repositories.PodsistemPotrosnjeRepozitorijum;
using Domain.Repositories.PotrosacRepozitorijum;
using Domain.Services;
using Moq;

namespace Tests
{
    [TestFixture]
    public class PotrosacServisTestovi
    {
        private Mock<IPotrosacRepozitorijum> _repozitorijumMock;
        private Mock<IPotrosnjaRepozitorijum> _potrosnjaRepozitorijumMock;
        private PotrosacServis _potrosacServis;

        [SetUp]
        public void Postavi()
        {
            _repozitorijumMock = new Mock<IPotrosacRepozitorijum>();
            _potrosnjaRepozitorijumMock = new Mock<IPotrosnjaRepozitorijum>();
            _potrosacServis = new PotrosacServis(_repozitorijumMock.Object, _potrosnjaRepozitorijumMock.Object);
        }

        [Test]
        public void DodajPotrosaca_ValidanPotrosac_DodajePotrosacaUPodsistem()
        {
            var potrosac = new Potrosac { Id = "1", ImePrezime = "Marko" };

            _potrosacServis.DodajPotrosaca(potrosac);

            _repozitorijumMock.Verify(r => r.Dodaj(potrosac), Times.Once, "Potrosac nije dodat u repozitorijum.");
            _potrosnjaRepozitorijumMock.Verify(p => p.DodajPotrosacaUPodsistem(potrosac), Times.Once, "Potrosac nije povezan sa podsistemom.");
        }

 
        [Test]
        public void PronadjiPotrosaca_ValidanId_VracaPotrosaca()
        {
            var potrosacId = "1";
            var potrosac = new Potrosac { Id = potrosacId, ImePrezime = "Marko" };
            _repozitorijumMock.Setup(r => r.PronadjiPoId(potrosacId)).Returns(potrosac);

            var result = _potrosacServis.PronadjiPotrosaca(potrosacId);

            Assert.That(result, Is.EqualTo(potrosac), "Potrosac sa datim ID-om nije pronadjen.");
        }

        [Test]
        public void GetPotrosaci_VracanjeListePotrosaca()
        {
            var potrosci = new List<Potrosac>
            {
                new() { Id = "1", ImePrezime = "Marko" },
                new() { Id = "2", ImePrezime = "Jovana" }
            };
            _repozitorijumMock.Setup(r => r.VratiSve()).Returns(potrosci);

            var result = _potrosacServis.GetPotrosaci();

            Assert.That(result, Is.EqualTo(potrosci), "Lista potrosaca nije ispravno vracena.");
        }
    }
}
