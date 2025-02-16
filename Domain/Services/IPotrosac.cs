using Domain.Models;

namespace Domain.Services
{
    public interface IPotrosac
    {
        void DodajPotrosaca(Potrosac potrosac);
        Potrosac? PronadjiPotrosaca(string id); 
        List<Potrosac> GetPotrosaci(); 
    }
}
