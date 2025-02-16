using Domain.Enums;

namespace Domain.Services
{
    public  interface IProizvodnjaEnergije
    {
        void ProvjeriIPovecajKolicinu(TipSnabdijevanja tipSnabdijevanja);  //tip snabdijevanja u podsistemu zavisi od trenutnog potrosaca
    }
}
