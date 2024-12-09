using Domain.Enums;
using Domain.Services;
using Services.PotrosacServisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SnabdijevanjeServisi
{
    public class KomercijalnoZahtjevServis : IZahtjevZaEnergiju
    {
        private readonly KomercijalnoServis _komercijalnoServis;
        private readonly PotrosacServis _potrosacServis;

        public KomercijalnoZahtjevServis(KomercijalnoServis komercijalnoServis, PotrosacServis potrosacServis)
        {
            _komercijalnoServis = komercijalnoServis;
            _potrosacServis = potrosacServis;
        }

        public void ObradiZahtjev(string potrosacId, double kolicinaEnergije)
        {
            if (kolicinaEnergije <= 0)
            {
                throw new ArgumentException($"Neispravan unos količine energije: {kolicinaEnergije}. Količina mora biti pozitivna.");
            }

            var potrosac = _potrosacServis.PronadjiPotrosaca(potrosacId);

            if (potrosac == null)
            {
                throw new KeyNotFoundException($"Potrošač sa ID {potrosacId} nije pronađen.");
            }

            if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.KOMERCIJALNO)
            {
                double dostupnaEnergija = _komercijalnoServis.SmanjenjeKolicine(kolicinaEnergije);

                Console.WriteLine($"Zahtjev za energiju od potrošača {potrosacId} obradjen sa količinom: {dostupnaEnergija} kWh");
            }
            else
            {
                throw new InvalidOperationException($"Potrošač {potrosacId} nije na komercijalnom snabdijevanju.");
            }
        }
    }
}
