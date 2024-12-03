using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.SnabdijevanjeServisi;

namespace Services.PotrosnjaServisi
{
    public class PotrosnjaServis : IPotrosnja
    {
        private readonly IUpravljanjePodsistemimaProizvodnje _podsistemProizvodnje;
        private readonly GarantovanoServis _garantovanoServis;
        private readonly KomercijalnoServis _komercijalnoServis;

        public PotrosnjaServis(IUpravljanjePodsistemimaProizvodnje podsistemProizvodnje, GarantovanoServis garantovanoServis,
            KomercijalnoServis komercijalnoServis)
        {
            _podsistemProizvodnje = podsistemProizvodnje;
            _garantovanoServis = garantovanoServis;
            _komercijalnoServis = komercijalnoServis;
        }

        public void ProvjeriPotrosnju(Potrosac potrosac)
        {
            double potrebnaEnergija = potrosac.Ukupna_potrosnja_ee;
            double cenaPoKW = 0.0;

            // Primer logike: Na osnovu načina snabdevanja, poziva se odgovarajući servis.
            if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO)
            {
                _garantovanoServis.SmanjenjeKolicine(potrebnaEnergija);
                cenaPoKW = _garantovanoServis.CijenaPoKW;
                double zaduzenje = potrebnaEnergija * cenaPoKW;
                potrosac.Trenutno_zaduzenje = zaduzenje;
            }
            else if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.KOMERCIJALNO)
            {
                _komercijalnoServis.SmanjenjeKolicine(potrebnaEnergija);
                cenaPoKW = _komercijalnoServis.CijenaPoKW;
                double zaduzenje = potrebnaEnergija * cenaPoKW;
                potrosac.Trenutno_zaduzenje = zaduzenje;
            }
        }

        public double UkupnaPotrošnja(Potrosac potrosac)
        {
            // Izračunajte ukupnu potrošnju za potrošača
            return potrosac.Ukupna_potrosnja_ee;
        }
    }

    public interface IPotrosnja
    {
    }
}
