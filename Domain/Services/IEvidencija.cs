using Domain.Enums;
using Domain.Models;

namespace Domain.Services
{
    public interface IEvidencija
    {
        public void DodajZapis(Zapis zapis, TipSnabdijevanja tipSnabdijevanja);
    }
}
