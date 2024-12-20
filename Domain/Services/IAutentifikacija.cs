using Domain.Models;

namespace Domain.Services
{
    public interface IAutentifikacija
    {
        public (bool, Potrosac) Prijava(string ImePrezime, string BrUgovora);
    }
}
