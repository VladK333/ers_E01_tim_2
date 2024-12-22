using Domain.Models;

namespace Domain.Services
{
    public interface IAutentifikacija
    {
        (bool, Potrosac) Prijava(string imePrezime, string brUgovora);

        bool TryLogin(out Potrosac potrosac); 
    }
}
