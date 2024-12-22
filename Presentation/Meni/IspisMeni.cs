using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Presentation.GenerisanjePotrosaca;
using Services.AutentifikacioniServisi;
using Services.SnabdijevanjeServisi;
using Presentation.GenerisanjePotrosaca;

namespace Presentation.Meni
{
    public class IspisMeni
    {
        private readonly IPotrosac _potrosacServis;
        private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemimaServis;

        public IspisMeni(IPotrosac potrosacServis, IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis)
        {
            _potrosacServis = potrosacServis;
            _upravljanjePodsistemimaServis = upravljanjePodsistemimaServis;

            // Inicijalizacija sa 10 nasumičnih potrošača prilikom pokretanja
            InicijalizujNasumicnePotrosace(10);
        }

        private void InicijalizujNasumicnePotrosace(int brojPotrosaca)
        {
            for (int i = 0; i < brojPotrosaca; i++)
            {
                Potrosac nasumicanPotrosac = NasumicanPotrosacGenerator.GenerisiNasumicanPotrosac();
                _potrosacServis.DodajPotrosaca(nasumicanPotrosac); // Dodavanje nasumično generisanog potrošača
            }

            Console.WriteLine($"{brojPotrosaca} nasumičnih potrošača je dodano.");
        }

        public void PrikaziMeni()
        {
            bool kraj = false;
            while (!kraj)
            {
                Console.WriteLine("\n1. Pregled svih potrošača\n2. Unos novog potrošača\n" +
                    "3. Zahtev za dobijanje električne energije\n4. Trenutno zaduženje potrošača\n" +
                    "5. Odjava");
                Console.Write("Opcija: ");
                string? opcija = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(opcija))
                    continue;

                switch (opcija[0])
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
            Console.WriteLine("\n======== PREGLED SVIH POTROŠAČA ========\n");

            var potrošači = _potrosacServis.GetPotrosaci();  
            if (potrošači.Count == 0)
            {
                Console.WriteLine("Nema registrovanih potrošača.");
                return;
            }

            foreach (var potrošač in potrošači)
            {
                Console.WriteLine(potrošač.ToString() + "\n===============================================\n");
            }
        }

        private void UnosNovogPotrosaca()
        {
            Console.Write("Unesite ime i prezime: ");
            string imePrezime = Console.ReadLine();

            Console.Write("Unesite broj ugovora: ");
            string brUgovora = Console.ReadLine();


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
            //trenutno zaduzenje na osnovu tipa snabdijevanja i potrosnje

           double trenutnoZaduzenje = ukupnaPotrosnja * snabdijevanjeServis.CijenaPoKW;

            _upravljanjePodsistemimaServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, ukupnaPotrosnja);

            var noviPotrosac = new Potrosac(imePrezime, brUgovora, tipSnab, ukupnaPotrosnja, trenutnoZaduzenje);
            _potrosacServis.DodajPotrosaca(noviPotrosac);
            Console.WriteLine("\nPotrošač uspešno dodat!");
        }

        private void Zahtev()
        {
            Console.WriteLine("Unesite broj ugovora potrošača koji zahteva energiju: ");
            string id = Console.ReadLine();

            var potrosac = _potrosacServis.PronadjiPotrosaca(id);

            if (potrosac == null)
            {
                Console.WriteLine("Potrošač sa unetim brojem ugovora ne postoji.");
                return;
            }

            Console.WriteLine("Unesite željenu količinu energije (kWh): ");
            if (!double.TryParse(Console.ReadLine(), out double zeljenaEnergija) || zeljenaEnergija <= 0)
            {
                Console.WriteLine("Unesena količina mora biti validан broj veći od nule.");
                return;
            }

            ISnabdijevanje snabdijevanjeServis = potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO
                ? new GarantovanoServis()
                : new KomercijalnoServis();

            try
            {
                var odgovarajuciPodsistem = _upravljanjePodsistemimaServis.NadjiPodsistemSaNajviseEnergije(zeljenaEnergija);

                if (odgovarajuciPodsistem == null)
                {
                    Console.WriteLine("Nema dovoljno energije u podsistemima da bi se ispunio zahtev.");
                    return;
                }

                snabdijevanjeServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, zeljenaEnergija);

                // Ažuriranje podataka potrošača
                potrosac.Ukupna_potrosnja_ee += zeljenaEnergija;
                potrosac.Trenutno_zaduzenje += zeljenaEnergija * snabdijevanjeServis.CijenaPoKW;

                // Informacije o uspjesnoj obradi zahtjeva 
                Console.WriteLine($"Zahtev za energiju uspešno obrađen za potrošača {id}.");
                Console.WriteLine($"Nova ukupna potrošnja: {potrosac.Ukupna_potrosnja_ee:F2} kWh.");
                Console.WriteLine($"Novo trenutno zaduženje: {potrosac.Trenutno_zaduzenje:F2} RSD.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Došlo je do greške prilikom obrade zahteva: {ex.Message}");
            }
        }



        private void TrenutnoZaduzenje()
        {
            Console.WriteLine("Unesite broj ugovora potrosaca: ");
            string id = Console.ReadLine();

            var potrosac = _potrosacServis.PronadjiPotrosaca(id);

            if (potrosac != null)
            {
                Console.WriteLine($"Trenutno zaduženje potrošača sa brojem ugovora {id} je: {potrosac.Trenutno_zaduzenje.ToString("N2")} RSD");//N2 za razdvajanje na hiljade  i dvije decimale 
            }
        }
    }
}
