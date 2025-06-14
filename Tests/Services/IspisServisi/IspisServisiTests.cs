using Services.IspisServisi;

namespace Tests.Services.IspisServisi
{
    [TestFixture]
    public class IspisServisTestovi
    {
        private IspisServis _ispisServis = null!;

        [SetUp]
        public void Postavi()
        {
            // Priprema objekta za testove
            _ispisServis = new IspisServis();
        }

        [Test]
        public void Ispisi_IspravnaPoruka_VracaTrue()
        {
            string poruka = "Test poruka";

            bool rezultat = _ispisServis.Ispisi(poruka);

            Assert.That(rezultat, Is.True);
        }

        [Test]
        public void Ispisi_NeispravnaPoruka_VracaFalse()
        {
            string? poruka = null;

            bool rezultat = _ispisServis.Ispisi(poruka);

            Assert.That(rezultat, Is.False);
        }
    }
}
