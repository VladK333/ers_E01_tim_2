using Domain.Models;
using Domain.Repositories.AutentifikacijaRepozitorijum;
using Domain.Services;

namespace Services.AutentifikacioniServisi
{
    public class AutentifikacioniServis : IAutentifikacija
    {
        private readonly IAutentifikacijaRepozitorijum _repozitorijum;

        public AutentifikacioniServis(IAutentifikacijaRepozitorijum repozitorijum) => _repozitorijum = repozitorijum;

        public (bool, Potrosac) Prijava(string imePrezime, string brojUgovora)
        {
            imePrezime = imePrezime.Trim(); 
            brojUgovora = brojUgovora.Trim();

            IEnumerable<Potrosac> _korisnici = _repozitorijum.DohvatiSveKorisnike();

            foreach (var potrosac in _korisnici)
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

