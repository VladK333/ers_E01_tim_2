using System;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.SnabdijevanjeServisi;

namespace Services.PotrosnjaServisi
{
    public class PotrosnjaServis : IPotrosnjaServis
    {
        private readonly GarantovanoServis _garantovanoServis;
        private readonly KomercijalnoServis _komercijalnoServis;

        public PotrosnjaServis(GarantovanoServis garantovanoServis, KomercijalnoServis komercijalnoServis)
        {
            _garantovanoServis = garantovanoServis;
            _komercijalnoServis = komercijalnoServis;
        }

        public void ProvjeriPotrosnju(Potrosac potrosac)
        {
            double potrebnaEnergija = potrosac.Ukupna_potrosnja_ee;
            double cenaPoKW = 0.0;

            // Na osnovu načina snabdevanja, pozivaju se odgovarajući servisi
            if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO)
            {
                // Obrađuje zahtev za energiju u garantovanom snabdevanju
                double dostupnaEnergija = _garantovanoServis.SmanjenjeKolicine(potrebnaEnergija);
                cenaPoKW = _garantovanoServis.CijenaPoKW;
                double zaduzenje = dostupnaEnergija * cenaPoKW;
                potrosac.Trenutno_zaduzenje = zaduzenje;
                Console.WriteLine($"Zahtjev za energiju sa potrošača {potrosac.ImePrezime} obradjen sa količinom: {dostupnaEnergija} kWh.");
            }
            else if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.KOMERCIJALNO)
            {
                // Obrađuje zahtev za energiju u komercijalnom snabdevanju
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
            // Vraća ukupnu potrošnju za potrošača
            return potrosac.Ukupna_potrosnja_ee;
        }
    }

}
