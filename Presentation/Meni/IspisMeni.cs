using Domain.Services;

namespace Presentation.Meni
{
    public class IspisMeni
    {
        private readonly IOperacijePotrosaci _operacijePotrosaci;
        private readonly IZahtevZaEnergiju _zahtjevServis;
        private readonly IPotrosac _potrosacServis;
        public IspisMeni(IOperacijePotrosaci operacijePotrosaci, IZahtevZaEnergiju zahtjevServis, IPotrosac potrosacServis)
        {
            _operacijePotrosaci = operacijePotrosaci;
            _zahtjevServis = zahtjevServis;
            _potrosacServis = potrosacServis;
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
                        _operacijePotrosaci.PregledSvihPotrosaca();
                        break;
                    case '2':
                        _operacijePotrosaci.UnosNovogPotrosaca();
                        break;
                    case '3':
                        Zahtev();
                        break;
                    case '4':
                        _operacijePotrosaci.TrenutnoZaduzenje();
                        break;
                    case '5':
                        kraj = true;
                        break;
                    default:
                        continue;
                }
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

            var potrosac = _potrosacServis.PronadjiPotrosaca(id);

            if (potrosac == null)
            {
                Console.WriteLine("Potrosac sa unetim id ne postoji.");
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
    }
}
