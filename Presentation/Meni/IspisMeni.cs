using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.SnabdijevanjeServisi;
using Services.ProizvodnjaServisi;
using Presentation.GenerisanjePotrosaca;
using Services.EvidencioniServisi;

namespace Presentation.Meni
{
    public class IspisMeni
    {
        private readonly IPotrosac _potrosacServis;
        private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemimaServis;
        private readonly IZahtevZaEnergiju _zahtjevServis;
        private readonly IEvidencija _evidencijaServis; 

        public IspisMeni(IPotrosac potrosacServis, IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis, IZahtevZaEnergiju zahtjevServis, IEvidencija evidencijaServis)
        {
            _potrosacServis = potrosacServis;
            _upravljanjePodsistemimaServis = upravljanjePodsistemimaServis;
            _zahtjevServis = zahtjevServis;
            _evidencijaServis = evidencijaServis;
        }

        public void PrikaziMeni()
        {
            bool kraj = false;
            while (!kraj)
            {
                Console.WriteLine("\n1. Pregled svih potrošača\n" +
                                    "2. Unos novog potrošača\n" +
                                    "3. Zahtev za dobijanje električne energije\n" +
                                    "4. Trenutno zaduženje potrošača\n" +
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

            var potrosaci = _potrosacServis.GetPotrosaci();

            if (potrosaci == null || potrosaci.Count == 0)
            {
                Console.WriteLine("Nema registrovanih potrošača.");
                return;
            }

            foreach (var potrosac in potrosaci)
            {
                if (potrosac != null)
                {
                    Console.WriteLine($"{potrosac}\n===============================================\n");
                }
            }
        }
        
        private void UnosNovogPotrosaca()
        {
            try
            {
                var nasumicanPotrosac = NasumicanPotrosacGenerator.GenerisiNasumicanPotrosac(_upravljanjePodsistemimaServis);
                _potrosacServis.DodajPotrosaca(nasumicanPotrosac);
                _evidencijaServis.DodajZapis(new Zapis(DateTime.Now, nasumicanPotrosac.Ukupna_potrosnja_ee));  // Ako je dodijeljena početna energija

                Console.WriteLine("\nPotrošač uspešno dodat nasumičnim generisanjem!");
                Console.WriteLine($"Detalji potrošača:\n{nasumicanPotrosac}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greška: {ex.Message}");
            }
        }


        private void Zahtev()
        {
            Console.WriteLine("Unesite broj ugovora potrošača koji zahteva energiju: ");
            string? id = Console.ReadLine();// Dodajemo ? da oznacimo da id moze biti null

            if (string.IsNullOrEmpty(id)) 
            {
                Console.WriteLine("Broj ugovora ne može biti prazan.");
                return;
            }

            Console.WriteLine("Unesite željenu količinu energije (kWh): ");
            if (!double.TryParse(Console.ReadLine(), out double zeljenaEnergija) || zeljenaEnergija <= 0)
            {
                Console.WriteLine("Unesena količina mora biti validan broj veći od nule.");
                return;
            }

            _zahtjevServis.ObradiZahtev(id, zeljenaEnergija);
        }

        private void TrenutnoZaduzenje()
        {
            Console.WriteLine("Unesite broj ugovora potrošača: ");
            string? id = Console.ReadLine();

            if (string.IsNullOrEmpty(id)) 
            {
                Console.WriteLine("Broj ugovora ne može biti prazan.");
                return;
            }

            var potrosac = _potrosacServis.PronadjiPotrosaca(id);

            if (potrosac == null)
            {
                Console.WriteLine("Potrošač sa unetim brojem ugovora ne postoji.");
                return;
            }

            Console.WriteLine($"Trenutno zaduženje za potrošača {id} je: {potrosac.Trenutno_zaduzenje:F2} RSD.");
        }

    }
}
