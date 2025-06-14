using Domain.Constants;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.ZahtjevServisi;
using Moq;

namespace Tests.Services.ZahtjevServisi
{
    public class ZahtevZaEnergijuServisTests
    {
        private Mock<IUpravljanjePodsistemimaPotrosnje> _mockPotrosnja = null!;
        private Mock<IUpravljanjePodsistemimaProizvodnje> _mockProizvodnja = null!;
        private Mock<IProizvodnjaEnergije> _mockProizvodnjaEnergije = null!;
        private Mock<IEvidencija> _mockEvidencija = null!;
        private Mock<IIspis> _mockIspis = null!;
        private ZahtevZaEnergijuServis _servis = null!;

        [SetUp]
        public void Setup()
        {
            _mockPotrosnja = new Mock<IUpravljanjePodsistemimaPotrosnje>();
            _mockProizvodnja = new Mock<IUpravljanjePodsistemimaProizvodnje>();
            _mockProizvodnjaEnergije = new Mock<IProizvodnjaEnergije>();
            _mockEvidencija = new Mock<IEvidencija>();
            _mockIspis = new Mock<IIspis>();

            _servis = new ZahtevZaEnergijuServis(
                _mockPotrosnja.Object,
                _mockProizvodnja.Object,
                _mockProizvodnjaEnergije.Object,
                _mockEvidencija.Object,
                _mockIspis.Object
            );
        }

        [Test]
        public void ObradiZahtev_TrebaDaIspiseGreskuKadaPotrosacNePostoji()
        {
            string id = "P1";
            _mockPotrosnja.Setup(x => x.PronadjiPotrosaca(id)).Returns((Potrosac?)null);

            _servis.ObradiZahtev(id, 500);

            _mockIspis.Verify(x => x.Ispisi(It.Is<string>(s => s.Contains("Potrosac sa ID P1 nije pronadjen."))), Times.Once);
        }

        [Test]
        public void ObradiZahtev_TrebaDaObradiZahtevKadaImaDovoljnoEnergije()
        {
            string id = "P2";
            var potrosac = new Potrosac(id, "Naziv", TipSnabdijevanja.GARANTOVANO, 0, 0);
            var podsistem = new PodsistemProizvodnje("PP1", TipProizvodnje.Hidroelektrana, "Lokacija", 1000);
            double zeljenaEnergija = 200;
            double ocekivanaKolicina = 1000 - (200 * 1.02);
            double ocekivanoZaduzenje = 200 * CeneConsts.CenaGarantovano;

            _mockPotrosnja.Setup(x => x.PronadjiPotrosaca(id)).Returns(potrosac);
            _mockProizvodnja.Setup(x => x.NadjiPodsistemSaNajviseEnergije(zeljenaEnergija)).Returns(podsistem);

            _servis.ObradiZahtev(id, zeljenaEnergija);

            Assert.That(podsistem.PreostalaKolicina, Is.EqualTo(ocekivanaKolicina).Within(0.01));
            Assert.That(potrosac.Ukupna_potrosnja_ee, Is.EqualTo(zeljenaEnergija));
            Assert.That(potrosac.Trenutno_zaduzenje, Is.EqualTo(ocekivanoZaduzenje));

            _mockEvidencija.Verify(x => x.DodajZapis(It.IsAny<Zapis>(), TipSnabdijevanja.GARANTOVANO), Times.Once);//pozvano tacno jednom 
        }

        [Test]
        public void ObradiZahtev_TrebaDaNeRadiNistaAkoNemaDovoljnoEnergije()
        {
            string id = "P3";
            var potrosac = new Potrosac(id, "Naziv", TipSnabdijevanja.KOMERCIJALNO, 0, 0);
            double zeljenaEnergija = 500;

            _mockPotrosnja.Setup(x => x.PronadjiPotrosaca(id)).Returns(potrosac);
            _mockProizvodnja.Setup(x => x.NadjiPodsistemSaNajviseEnergije(zeljenaEnergija)).Returns((PodsistemProizvodnje?)null);

            _servis.ObradiZahtev(id, zeljenaEnergija);

            Assert.That(potrosac.Ukupna_potrosnja_ee, Is.EqualTo(0));
            Assert.That(potrosac.Trenutno_zaduzenje, Is.EqualTo(0));

            _mockEvidencija.Verify(x => x.DodajZapis(It.IsAny<Zapis>(), It.IsAny<TipSnabdijevanja>()), Times.Never);
        }

    }
}
