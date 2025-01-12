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
        private readonly IProizvodnjaEnergije _proizvodnjaEnergije;
        public IspisMeni(IPotrosac potrosacServis, IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis, IZahtevZaEnergiju zahtjevServis, IEvidencija evidencijaServis, IProizvodnjaEnergije proizvodnjaEnergije)
        {
            _potrosacServis = potrosacServis;
            _upravljanjePodsistemimaServis = upravljanjePodsistemimaServis;
            _zahtjevServis = zahtjevServis;
            _evidencijaServis = evidencijaServis;
            _proizvodnjaEnergije = proizvodnjaEnergije;
        }

        public void PrikaziMeni()
        {
            bool kraj = false;
            while (!kraj)
            {
                Console.WriteLine("\n1. Pregled svih potrosaca\n" +
                                    "2. Unos novog potrosaca\n" +
                                    "3. Zahtev za dobijanje elektricne energije\n" +
                                    "4. Trenutno zaduzenje potrosaca\n" +
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
            Console.WriteLine("\n======== PREGLED SVIH POTROSACA ========\n");

            var potrosaci = _potrosacServis.GetPotrosaci();

            if (potrosaci == null || potrosaci.Count == 0)
            {
                Console.WriteLine("Nema registrovanih potrosaca.");
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
            _proizvodnjaEnergije.ProvjeriIPovecajKolicinu();

            try
            {
                var nasumicanPotrosac = NasumicanPotrosacGenerator.GenerisiNasumicanPotrosac(_upravljanjePodsistemimaServis);
                _potrosacServis.DodajPotrosaca(nasumicanPotrosac);
                //_evidencijaServis.DodajZapis(new Zapis(DateTime.Now, nasumicanPotrosac.Ukupna_potrosnja_ee)); 

                Console.WriteLine("=======NOVI POTROSAC=======");
                Console.WriteLine("Potrosac uspesno dodat nasumicnim generisanjem!\n");
                Console.WriteLine($"Detalji potrosaca:{nasumicanPotrosac}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greska: {ex.Message}");
            }
        }


        private void Zahtev()
        {
            Console.WriteLine("Unesite id potrosaca koji zahteva energiju: ");
            string? id = Console.ReadLine();

            if (string.IsNullOrEmpty(id)) 
            {
                Console.WriteLine("Id ne moze biti prazan.");
                return;
            }

            Console.WriteLine("Unesite zeljenu kolicinu energije (kWh): ");
            if (!double.TryParse(Console.ReadLine(), out double zeljenaEnergija) || zeljenaEnergija <= 0)
            {
                Console.WriteLine("Unesena kolicina mora biti validan broj veci od nule.");
                return;
            }

            _zahtjevServis.ObradiZahtev(id, zeljenaEnergija);
        }

        private void TrenutnoZaduzenje()
        {
            Console.WriteLine("Unesite id potrosaca: ");
            string? id = Console.ReadLine();

            if (string.IsNullOrEmpty(id)) 
            {
                Console.WriteLine("Id ne moze biti prazan.");
                return;
            }

            var potrosac = _potrosacServis.PronadjiPotrosaca(id);

            if (potrosac == null)
            {
                Console.WriteLine("Potrosac sa unetim id ne postoji.");
                return;
            }

            Console.WriteLine($"Trenutno zaduzenje za potrosaca {id} je: {potrosac.Trenutno_zaduzenje:F2} RSD.");
        }

    }
}
