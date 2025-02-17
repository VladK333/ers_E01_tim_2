using Domain.Services;

namespace Services.IspisServisi
{
    public class IspisServis : IIspis
    {
        public bool Ispisi(string poruka)
        {
            try
            {
                Console.WriteLine(poruka);
                return true; 
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
