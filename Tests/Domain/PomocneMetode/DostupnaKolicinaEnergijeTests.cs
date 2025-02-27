using Domain.PomocneMetode.DostupnaKolicinaEnergije;
using Moq;

namespace Tests.Domain.PomocneMetode;

[TestFixture]
public class DostupnaKolicinaEnergijeTestovi
{
    private Mock<IDostupnaKolicinaEnergije> _mockDostupnaKolicinaEnergije;
    private DostupnaKolicinaEnergije _dostupnaKolicinaEnergije;

    [SetUp]
    public void Postavi()
    {
        _mockDostupnaKolicinaEnergije = new Mock<IDostupnaKolicinaEnergije>();
        _dostupnaKolicinaEnergije = new DostupnaKolicinaEnergije();
    }

    [Test]
    public void Generisi_VrijednostUnutarOpsega()
    {
        double minVr = 100.0;
        double maxVr = 200.0;
        _mockDostupnaKolicinaEnergije.Setup(m => m.Generate(minVr, maxVr)).Returns(150.0);

        double rezultat = _dostupnaKolicinaEnergije.Generate(minVr, maxVr);

        Assert.That(rezultat, Is.InRange(minVr, maxVr), "Generisana vrijednost je unutar ocekivanog opsega.");
    }
}
