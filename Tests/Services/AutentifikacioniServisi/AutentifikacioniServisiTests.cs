using Domain.Models;
using Domain.Repositories.AutentifikacijaRepozitorijum;
using Moq;
using Services.AutentifikacioniServisi;

namespace Tests.Services.AutentifikacioniServisi
{
    [TestFixture]
    public class AutentifikacioniServisTests
    {
        // Mock objekat za repozitorijum
        readonly Mock<IAutentifikacijaRepozitorijum> _repozitorijumMock;
        readonly AutentifikacioniServis _authServis;

        // Konstruktor inicijalizator
        public AutentifikacioniServisTests()
        {
            _repozitorijumMock = new Mock<IAutentifikacijaRepozitorijum>();
            _authServis = new AutentifikacioniServis(_repozitorijumMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            // Resetovanje mock objekta prije svakog testa
            _repozitorijumMock.Reset();
        }

        [Test]
        [TestCase("Ivana", "123")]
        [TestCase("Vladana", "123")]
        public void PrijavaSaIspravnimPodacima_VracaTrue(string imePrezime, string brojUgovora)
        {
            //Kreiranje potrebnog objekta Potrosac
            var potrosac = new Potrosac { ImePrezime = imePrezime, BrUgovora = brojUgovora };

            //Ocekivana vrijednost koju treba da vrati repozitorijum
            _repozitorijumMock.Setup(x => x.DohvatiSveKorisnike()).Returns([potrosac]);

            //Pozivanje metode za prijavu
            var (uspjesnaAutentifikacija, prijavljenPotrosac) = _authServis.Prijava(imePrezime, brojUgovora);

            //Provjera da li je rezultat tacan
            Assert.That(uspjesnaAutentifikacija, Is.True);
            Assert.That(prijavljenPotrosac, Is.Not.Null);
            Assert.That(prijavljenPotrosac.ImePrezime, Is.EqualTo(imePrezime));
            Assert.That(prijavljenPotrosac.BrUgovora, Is.EqualTo(brojUgovora));
        }

        [Test]
        [TestCase("Ana", "123")]
        [TestCase("Olja", "123")]
        public void PrijavaSaNeispravnimPodacima_VracaFalse(string imePrezime, string brojUgovora)
        {
            //Vracanje praznog spiska korisnika kada su podaci netacni
            _repozitorijumMock.Setup(x => x.DohvatiSveKorisnike()).Returns([]);

            //Pozivanje metode za prijavu
            var (uspjesnaAutentifikacija, potrosac) = _authServis.Prijava(imePrezime, brojUgovora);

            // Provjera da li je rezultat tacan
            Assert.That(uspjesnaAutentifikacija, Is.False);
            Assert.That(potrosac.ImePrezime, Is.Empty);
            Assert.That(potrosac.BrUgovora, Is.Empty);
        }
    }
}
