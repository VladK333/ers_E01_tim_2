using Domain.Models;

namespace Domain.Services
{
    public interface IPotrosnja
    {
        void ProvjeriPotrosnju(Potrosac potrosac);
        double UkupnaPotrošnja(Potrosac potrosac);
    }
}
