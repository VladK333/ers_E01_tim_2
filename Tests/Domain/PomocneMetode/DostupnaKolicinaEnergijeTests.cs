using Domain.PomocneMetode.DostupnaKolicinaEnergije;
using Moq;
using System.Reflection.Emit;

namespace Tests.Domain.PomocneMetode;

[TestFixture]
public class DostupnaKolicinaEnergijeTestovi
{
    private DostupnaKolicinaEnergije _dostupnaKolicinaEnergije = null!;

    [SetUp]
    public void Postavi()
    {
        _dostupnaKolicinaEnergije = new DostupnaKolicinaEnergije();
    }

    [Test]
    public void Generisi_VrijednostUnutarOpsega()
    {
        double minVr = 100.0;
        double maxVr = 200.0;

        double rezultat = _dostupnaKolicinaEnergije.Generate(minVr, maxVr);

        Assert.That(rezultat, Is.InRange(minVr, maxVr), "Generisana vrijednost nije unutar ocekivanog opsega.");
    }
    [Test]
    public void Generate_VracaTacanRezultatKadaJeMinJednakMax()
    {
        double vrednost = 75.0;

        double rezultat = _dostupnaKolicinaEnergije.Generate(vrednost, vrednost);

        Assert.That(rezultat, Is.EqualTo(vrednost), "Ako su min i max jednaki, rezultat mora biti ta vrednost.");
    }
}
