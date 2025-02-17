using Domain.Enums;
using Domain.Models;

namespace Domain.Services
{
    public interface IEvidencija
    {
        void DodajZapis(Zapis zapis, TipSnabdijevanja tipSnabdijevanja);
    }
}
