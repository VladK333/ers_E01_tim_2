using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class KomercijalnoServis : ISnabdijevanje
    {
        public double CijenaPoKW {
            get { return 43.02; }
        }

        public double SmanjenjeKolicine(double kolicina)
        {
            return kolicina - (kolicina * 0.01);
        }
    }
}
