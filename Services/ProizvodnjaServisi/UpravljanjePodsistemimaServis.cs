using Domain.Models;
using Domain.Services;

namespace Services.ProizvodnjaServisi
{
    public class UpravljanjePodsistemimaServis : IUpravljanjePodsistemimaProizvodnje
    {
        
         private readonly List<PodsistemProizvodnje> _podsistemi;

         public UpravljanjePodsistemimaServis()
         {
            _podsistemi = new List<PodsistemProizvodnje>();
         }

        public void DodajPodsistem(PodsistemProizvodnje podsistem)
        {
             _podsistemi.Add(podsistem);
        }

        public List<PodsistemProizvodnje> DohvatiSvePodsisteme()
         {
             return _podsistemi;
         }
        
    }
}
