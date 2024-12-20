using Domain.Enums;
using Domain.Services;

namespace Services.SnabdijevanjeServisi
{
    public class GarantovanoZahtjevServis : IZahtjevZaEnergiju
    {
        private readonly ISnabdijevanje _garantovanoServis;
        private readonly IPotrosac _potrosacServis;

        public GarantovanoZahtjevServis(ISnabdijevanje garantovanoServis, IPotrosac potrosacServis)
        {
            _garantovanoServis = garantovanoServis;
            _potrosacServis = potrosacServis;
        }

        public void ObradiZahtjev(string potrosacId, double kolicinaEnergije)
        {
            if (kolicinaEnergije <= 0 )
            {
                throw new ArgumentException($"Neispravan unos količine energije: {kolicinaEnergije}. Količina mora biti pozitivna.");
            }

            var potrosac = _potrosacServis.PronadjiPotrosaca(potrosacId);

            if (potrosac == null)
            {
                throw new KeyNotFoundException($"Potrošač sa ID {potrosacId} nije pronađen.");
            }

            if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO)
            {
                double dostupnaEnergija = _garantovanoServis.SmanjenjeKolicine(kolicinaEnergije);

                Console.WriteLine($"Zahtjev za energiju od potrošača {potrosacId} obradjen sa količinom: {dostupnaEnergija} kWh");
            }
            else
            {
                throw new InvalidOperationException($"Potrošač {potrosacId} nije na garantovanom snabdijevanju.");
            }
        }
    }
}
