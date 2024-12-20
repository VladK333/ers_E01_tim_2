using Domain.Models;

namespace Domain.Services
{
    public interface IEvidencija
    {
        public void DodajZapis(Zapis zapis);

        public IEnumerable<Zapis> PregledZapisa();
    }
}
