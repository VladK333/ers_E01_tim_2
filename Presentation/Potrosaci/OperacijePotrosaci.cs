using Domain.Services;
using Presentation.GenerisanjePotrosaca;

namespace Presentation.Potrosaci
{
    public class OperacijePotrosaci : IOperacijePotrosaci
    {
        private readonly IPotrosac _potrosacServis;
        private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemimaServis;
        private readonly IProizvodnjaEnergije _proizvodnjaEnergije;

        public OperacijePotrosaci(IPotrosac potrosacServis, IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis, IProizvodnjaEnergije proizvodnjaEnergije)
        {
            _potrosacServis = potrosacServis;
            _upravljanjePodsistemimaServis = upravljanjePodsistemimaServis;
            _proizvodnjaEnergije = proizvodnjaEnergije;
        }

        public void PregledSvihPotrosaca()
        {
            Console.WriteLine("\n======== PREGLED SVIH POTROSACA ========");

            var potrosaci = _potrosacServis.GetPotrosaci();

            if (potrosaci == null || !potrosaci.Any())
            {
                Console.WriteLine("Nema registrovanih potrosaca.");
                return;
            }

            foreach (var potrosac in potrosaci)
            {
                if (potrosac != null)
                {
                    Console.WriteLine($"{potrosac}\n===============================================");
                }
            }
        }

        public void UnosNovogPotrosaca()
        {
            try
            {
                var nasumicanPotrosac = NasumicanPotrosacGenerator.GenerisiNasumicanPotrosac(_upravljanjePodsistemimaServis);
                _potrosacServis.DodajPotrosaca(nasumicanPotrosac);

                _proizvodnjaEnergije.ProvjeriIPovecajKolicinu(nasumicanPotrosac.Tip_Snabdevanja);

                Console.WriteLine("=======NOVI POTROSAC=======");
                Console.WriteLine("Potrosac uspesno dodat nasumicnim generisanjem!\n");
                Console.WriteLine($"Detalji potrosaca:{nasumicanPotrosac}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Greska: {ex.Message}");
            }
        }

        public void TrenutnoZaduzenje()
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
