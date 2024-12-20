using Domain.Enums;
using Domain.Models;
using Domain.Services;

namespace Services.PotrosnjaServisi
{
    public class PotrosnjaServis : IPotrosnja
    {
        private readonly ISnabdijevanje _garantovanoServis;
        private readonly ISnabdijevanje _komercijalnoServis;

        public PotrosnjaServis(ISnabdijevanje garantovanoServis, ISnabdijevanje komercijalnoServis)
        {
            _garantovanoServis = garantovanoServis;
            _komercijalnoServis = komercijalnoServis;
        }


        public void ProvjeriPotrosnju(Potrosac potrosac)
        {
            double potrebnaEnergija = potrosac.Ukupna_potrosnja_ee;
            double cenaPoKW = 0.0;
 
            if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO)
            {
                double dostupnaEnergija = _garantovanoServis.SmanjenjeKolicine(potrebnaEnergija);
                cenaPoKW = _garantovanoServis.CijenaPoKW;
                double zaduzenje = dostupnaEnergija * cenaPoKW;
                potrosac.Trenutno_zaduzenje = zaduzenje;
                Console.WriteLine($"Zahtjev za energiju sa potrošača {potrosac.ImePrezime} obradjen sa količinom: {dostupnaEnergija} kWh.");
            }
            else if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.KOMERCIJALNO)
            {
                double dostupnaEnergija = _komercijalnoServis.SmanjenjeKolicine(potrebnaEnergija);
                cenaPoKW = _komercijalnoServis.CijenaPoKW;
                double zaduzenje = dostupnaEnergija * cenaPoKW;
                potrosac.Trenutno_zaduzenje = zaduzenje;
                Console.WriteLine($"Zahtjev za energiju sa potrošača {potrosac.ImePrezime} obradjen sa količinom: {dostupnaEnergija} kWh.");
            }
            else
            {
                Console.WriteLine($"Potrošač {potrosac.ImePrezime} nije registrovan za odgovarajući tip snabdevanja.");
            }
        }

        public double UkupnaPotrošnja(Potrosac potrosac)
        {
            return potrosac.Ukupna_potrosnja_ee;
        }

        /*double IzracunajZaduzenje(TipSnabdijevanja tipSnab, double ee)
        {
            double zaduzenje = 0.0;

            return zaduzenje;

        }*/
    }

}