using Domain.Enums;
using Domain.Models;
using Domain.Services;

namespace Services.AutentifikacioniServisi
{
    public class AutentifikacioniServis : IAutentifikacija
    {
        private static readonly List<Potrosac> _autentifikacija_potrosaci;

        static AutentifikacioniServis()
        {

            _autentifikacija_potrosaci = new List<Potrosac>
            {
                new Potrosac("Vladana", "123", TipSnabdijevanja.KOMERCIJALNO, 20, 20),
                new Potrosac("Ivana", "123", TipSnabdijevanja.GARANTOVANO, 10, 10),
            };
        }

        public (bool, Potrosac) Prijava(string imePrezime, string brojUgovora)
        {
            imePrezime = imePrezime.Trim(); 
            brojUgovora = brojUgovora.Trim();

            foreach (var potrosac in _autentifikacija_potrosaci)
            {
                if (potrosac.ImePrezime.Equals(imePrezime) && potrosac.BrUgovora.Equals(brojUgovora))
                {
                    return (true, potrosac);
                }
            }

            return (false, new Potrosac());
        }
    }
}

