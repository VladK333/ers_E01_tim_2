using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class GarantovanoServis : ISnabdijevanje
    {
        public double CijenaPoKW
        {
            get { return 22.72; }
        }

        public double SmanjenjeKolicine(double kolicina)
        {
            return kolicina - (kolicina * 0.02);
        }
    }
}
