namespace Domain.Services
{
    public interface ISnabdijevanje
    {
        double CijenaPoKW { get; }

        double SmanjenjeKolicine(double kolicina);
    }
}
