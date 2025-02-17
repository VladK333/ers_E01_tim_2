using Domain.Enums;
using Domain.Models;

namespace Domain.Repositories.PotrosacRepozitorijum
{
    public class PotrosacRepozitorijum : IPotrosacRepozitorijum
    {
        private static readonly List<Potrosac> _potrosaci;
        static PotrosacRepozitorijum()
        {
            _potrosaci =
            [
                new("Ana Petrovic", "EPS5678K", TipSnabdijevanja.KOMERCIJALNO, 340.00, 14626.8),
                new("Nikola Ilic", "EPS9101K", TipSnabdijevanja.GARANTOVANO, 250.75, 5697.04),
                new("Jelena Savic", "EPS1123K", TipSnabdijevanja.KOMERCIJALNO, 180.20, 7752.2)
            ];
        }

        public void Dodaj(Potrosac potrosac)
        {
            _potrosaci.Add(potrosac);
        }

        public Potrosac? PronadjiPoId(string id)
        {
            return _potrosaci.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Potrosac> VratiSve()
        {
            return _potrosaci ?? [];
        }
    }
}
