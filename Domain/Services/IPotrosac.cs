using Domain.Models;

namespace Domain.Services
{
    public interface IPotrosac
    {
        void DodajPotrosaca(Potrosac potrosac);  
        Potrosac PronadjiPotrosaca(string brUgovora); 
        void AzurirajPotrosaca(Potrosac potrosac);  
        void ObrisiPotrosaca(string id);  
        List<Potrosac> GetPotrosaci();  
    }
}
