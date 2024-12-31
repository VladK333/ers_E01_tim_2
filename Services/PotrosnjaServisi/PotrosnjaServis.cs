/*using Domain.Enums;
using Domain.Models;
using Domain.Services;

namespace Services.PotrosnjaServisi
{
    public class PotrosnjaServis : IPotrosnja
    {
        private readonly ISnabdijevanje _garantovanoServis;
        private readonly ISnabdijevanje _komercijalnoServis;
        private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemimaServis;

        public PotrosnjaServis(ISnabdijevanje garantovanoServis, ISnabdijevanje komercijalnoServis, IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis)
        {
            _garantovanoServis = garantovanoServis;
            _komercijalnoServis = komercijalnoServis;
            _upravljanjePodsistemimaServis = upravljanjePodsistemimaServis;
        }

        public void ProvjeriPotrosnju(Potrosac potrosac)
        {
            double potrebnaEnergija = potrosac.Ukupna_potrosnja_ee;
            double cenaPoKW = 0.0;
            PodsistemProizvodnje? odgovarajuciPodsistem = null;

            if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO)
            {
                odgovarajuciPodsistem = _upravljanjePodsistemimaServis.NadjiPodsistemSaNajviseEnergije(potrebnaEnergija);
            }
            else if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.KOMERCIJALNO)
            {
                odgovarajuciPodsistem = _upravljanjePodsistemimaServis.NadjiPodsistemSaNajviseEnergije(potrebnaEnergija);
            }

            if (odgovarajuciPodsistem == null)
            {
                Console.WriteLine("Nema dostupnog podsistema sa dovoljno energije.");
                return;
            }

            if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO)
            {
                _garantovanoServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, potrebnaEnergija);

                double dostupnaEnergija = odgovarajuciPodsistem.PreostalaKolicina;

                cenaPoKW = _garantovanoServis.CijenaPoKW;
                double zaduzenje = dostupnaEnergija * cenaPoKW;
                potrosac.Trenutno_zaduzenje = zaduzenje;

                Console.WriteLine($"Zahtjev za energiju sa potrošača {potrosac.ImePrezime} obradjen sa količinom: {dostupnaEnergija} kWh.");
            }
            else if (potrosac.Tip_Snabdevanja == TipSnabdijevanja.KOMERCIJALNO)
            {
                _komercijalnoServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, potrebnaEnergija);

                double dostupnaEnergija = odgovarajuciPodsistem.PreostalaKolicina;

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
    }
}
*/
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.SnabdijevanjeServisi;

namespace Services.PotrosnjaServisi
{
    public class PotrosnjaServis : IPotrosnja
    {
        private readonly ISnabdijevanje _garantovanoServis;
        private readonly ISnabdijevanje _komercijalnoServis;
        private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemimaServis;

        // Konstruktor bez servisa kao zavisnosti (umesto toga koristi Singleton instancu)
        public PotrosnjaServis(IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis)
        {
            _garantovanoServis = GarantovanoServis.Instance;
            _komercijalnoServis = KomercijalnoServis.Instance;
            _upravljanjePodsistemimaServis = upravljanjePodsistemimaServis;
        }

        public void ProvjeriPotrosnju(Potrosac potrosac)
        {
            double potrebnaEnergija = potrosac.Ukupna_potrosnja_ee;
            double cenaPoKW = 0.0;
            PodsistemProizvodnje? odgovarajuciPodsistem = _upravljanjePodsistemimaServis.NadjiPodsistemSaNajviseEnergije(potrebnaEnergija);

            if (odgovarajuciPodsistem == null)
            {
                Console.WriteLine("Nema dostupnog podsistema sa dovoljno energije.");
                return;
            }

            switch (potrosac.Tip_Snabdevanja)
            {
                case TipSnabdijevanja.GARANTOVANO:
                    _garantovanoServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, potrebnaEnergija);
                    cenaPoKW = _garantovanoServis.CijenaPoKW;
                    break;

                case TipSnabdijevanja.KOMERCIJALNO:
                    _komercijalnoServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, potrebnaEnergija);
                    cenaPoKW = _komercijalnoServis.CijenaPoKW;
                    break;

                default:
                    Console.WriteLine($"Potrošač {potrosac.ImePrezime} nije registrovan za odgovarajući tip snabdevanja.");
                    return;
            }

            double dostupnaEnergija = odgovarajuciPodsistem.PreostalaKolicina;
            double zaduzenje = dostupnaEnergija * cenaPoKW;
            potrosac.Trenutno_zaduzenje = zaduzenje;

            Console.WriteLine($"Zahtjev za energiju sa potrošača {potrosac.ImePrezime} obradjen sa količinom: {dostupnaEnergija} kWh.");
        }
    }
}
