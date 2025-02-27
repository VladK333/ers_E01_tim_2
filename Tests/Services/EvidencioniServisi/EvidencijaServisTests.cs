using Domain.Enums;
using Domain.Models;
using Domain.Repositories.EvidencijaRepozitorijum;
using Domain.Services;
using Moq;
using Services.EvidencioniServisi;

namespace Tests
{
    [TestFixture]
    public class EvidencijaServisTestovi
    {
        private Mock<IEvidencijaRepozitorijum> _repozitorijumMock;
        private Mock<IIspis> _ispisMock;
        private EvidencijaServis _servis;
        private string _putanjaDatoteke;

        [SetUp]
        public void Postavi()
        {
            _repozitorijumMock = new Mock<IEvidencijaRepozitorijum>();
            _ispisMock = new Mock<IIspis>();
            _putanjaDatoteke = "evidencija_test.txt";
            _servis = new EvidencijaServis(_repozitorijumMock.Object, _ispisMock.Object, _putanjaDatoteke);
        }

        [Test]
        public void DodajZapis_GarantovaniTipSnabdijevanja_DodajeZapisUDatoteku()
        {
            var zapis = new Zapis(DateTime.Now, 10.5);
            var tipSnabdijevanja = TipSnabdijevanja.GARANTOVANO;

            _servis.DodajZapis(zapis, tipSnabdijevanja);

            _ispisMock.Verify(x => x.Ispisi("Evidencija uspesna."), Times.Once);
            Assert.That(File.Exists(_putanjaDatoteke), Is.True, "Zapis nije dodat u datoteku.");
        }

        [Test]
        public void DodajZapis_KomercijalniTipSnabdijevanja_DodajeZapisURepozitorijum()
        {
            var zapis = new Zapis(DateTime.Now, 10.5);
            var tipSnabdijevanja = TipSnabdijevanja.KOMERCIJALNO;

            _repozitorijumMock.Setup(x => x.DodajZapis(It.IsAny<Zapis>())).Verifiable();

            _servis.DodajZapis(zapis, tipSnabdijevanja);

            _repozitorijumMock.Verify(x => x.DodajZapis(zapis), Times.Once);
            _ispisMock.Verify(x => x.Ispisi("Evidencija uspesna."), Times.Once);
        }

        [Test]
        public void DodajZapis_GreskaPriDodavanjuZapisa_IspisujePorukuGreske()
        {
            var zapis = new Zapis(DateTime.Now, 10.5);
            var tipSnabdijevanja = TipSnabdijevanja.KOMERCIJALNO;
            var exceptionMessage = "Greska pri dodavanju zapisa!";

            _repozitorijumMock.Setup(x => x.DodajZapis(It.IsAny<Zapis>())).Throws(new IOException(exceptionMessage));

            _servis.DodajZapis(zapis, tipSnabdijevanja);

            _ispisMock.Verify(x => x.Ispisi($"Greska prilikom dodavanja zapisa: {exceptionMessage}"), Times.Once);
        }
    }
}
