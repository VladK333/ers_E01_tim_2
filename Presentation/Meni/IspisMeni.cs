using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.AutentifikacioniServisi;
using Services.SnabdijevanjeServisi;

namespace Presentation.Meni
{
    public class IspisMeni
    {
        private Potrosac potrosac;
        private readonly IPotrosac _potrosacServis;//interfejs izmijenjeno
        private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemimaServis;

        public IspisMeni(IPotrosac potrosacServis, IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis)
        {
            _potrosacServis = potrosacServis;
            _upravljanjePodsistemimaServis = upravljanjePodsistemimaServis;
        }


        public void PrikaziMeni()
        {
            bool kraj = false;
            while(!kraj)
            {
                Console.WriteLine("\n1. Pregled svih potrosaca\n2. Unos novog potrosaca\n" +
                    "3. Zahtev za dobijanje elektricne energije\n4. Trenutno zaduzenje potrosaca\n" +
                    "5. Odjava");
                Console.Write("Opcija: ");
                string? opcija = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(opcija))
                    continue;

                switch(opcija[0])
                {
                    case '1':
                        PregledSvihPotrosaca();
                        break;
                    case '2':
                        UnosNovogPotrosaca();
                        break;
                    case '3':
                        Zahtev();
                        break;
                    case '4':
                        TrenutnoZaduzenje();
                        break;
                    case '5':
                        kraj = true;
                        break;
                    default:
                        continue;
                }
            }
        }

        private void PregledSvihPotrosaca()
        {
            Console.WriteLine("\n======== PREGLED SVIH POTROSACA ========\n");

            var potrosaci = AutentifikacioniServis.GetPotrosaci();
            if (potrosaci.Count == 0)
            {
                Console.WriteLine("Nema registrovanih potrošača.");
                return;
            }

            foreach (var potrosac in potrosaci)
            {
                Console.WriteLine(potrosac.ToString() + "\n===============================================\n");
            }
        }

        private void UnosNovogPotrosaca()
        {
            Console.Write("Unesite ime i prezime: ");
            string imePrezime = Console.ReadLine();

            Console.Write("Unesite broj ugovora: ");
            string brUgovora = Console.ReadLine();

            //tip snabdevanja na osnovu dostupne energije

            Console.Write("Unesite ukupnu potrošnju električne energije: ");
            if (!double.TryParse(Console.ReadLine(), out double ukupnaPotrosnja) || ukupnaPotrosnja <= 0)
            {
                Console.WriteLine("Unesena količina mora biti validan broj veći od nule.");
                return;
            }

            var odgovarajuciPodsistem = _upravljanjePodsistemimaServis.NadjiPodsistemSaNajviseEnergije(ukupnaPotrosnja);

            if(odgovarajuciPodsistem==null)
            {
                var najvecaDostupnaEnergija = _upravljanjePodsistemimaServis.DohvatiNajvecuDostupnuEnergiju();

                Console.WriteLine($"Nema podsistema sa dovoljnim kapacitetom. Najveća dostupna količina energije je: {najvecaDostupnaEnergija:F2} kWh.");
            }

            Console.WriteLine("Odaberite tip snabdevanja:");
            Console.WriteLine("1. Garantovano");
            Console.WriteLine("2. Komercijalno");
            Console.Write("Vaš izbor: ");
            string izbor = Console.ReadLine();
            ISnabdijevanje snabdijevanjeServis;
            TipSnabdijevanja tipSnab;

            if (izbor == "1")
            {
                snabdijevanjeServis = new GarantovanoServis();
                tipSnab = TipSnabdijevanja.GARANTOVANO;
            }
            else if (izbor == "2")
            {
                snabdijevanjeServis = new KomercijalnoServis();
                tipSnab = TipSnabdijevanja.KOMERCIJALNO;
            }
            else
            {
                Console.WriteLine("Nevažeći izbor. Potrošač nije dodat.");
                return;
            }
            //trenutno zaduzenje na osnovu tipa snabdevanja i potrosnje

           double trenutnoZaduzenje = ukupnaPotrosnja * snabdijevanjeServis.CijenaPoKW;

            _upravljanjePodsistemimaServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, ukupnaPotrosnja);

            var noviPotrosac = new Potrosac(imePrezime, brUgovora, tipSnab, ukupnaPotrosnja, trenutnoZaduzenje);
            _potrosacServis.DodajPotrosaca(noviPotrosac);
            Console.WriteLine("\nPotrošač uspešno dodat!");
        }

        private void Zahtev()
        {
            Console.WriteLine("Unesite broj ugovora potrosaca koji zahteva energiju: ");
            string id = Console.ReadLine();

            Console.WriteLine("Unesite zeljenu kolicinu energije: ");
            double zeljenaEnergija = double.Parse(Console.ReadLine());

            
        }

        private void TrenutnoZaduzenje()
        {
            Console.WriteLine("Unesite broj ugovora potrosaca: ");
            string id = Console.ReadLine();

            var potrosac = _potrosacServis.PronadjiPotrosaca(id);

            if (potrosac != null)
            {
                Console.WriteLine($"Trenutno zaduženje potrošača sa brojem ugovora {id} je: {potrosac.Trenutno_zaduzenje} RSD");
            }
        }
    }
}
