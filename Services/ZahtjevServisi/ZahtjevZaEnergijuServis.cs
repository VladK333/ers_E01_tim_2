using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.EvidencioniServisi;
using Services.SnabdijevanjeServisi;

public class ZahtevZaEnergijuServis : IZahtevZaEnergiju
{
    private readonly IPotrosac _potrosacServis;
    private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemimaServis;
    private readonly IEvidencija _evidencija;

    public ZahtevZaEnergijuServis(IPotrosac potrosacServis, IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis, IEvidencija evidencija)
    {
        _potrosacServis = potrosacServis;
        _upravljanjePodsistemimaServis = upravljanjePodsistemimaServis;
        _evidencija = evidencija;
    }

    public void ObradiZahtev(string brojUgovora, double zeljenaEnergija)
    {
        var potrosac = _potrosacServis.PronadjiPotrosaca(brojUgovora);

        if (potrosac == null)
        {
            Console.WriteLine("Potrošač sa unetim brojem ugovora ne postoji.");
            return;
        }

        if (zeljenaEnergija <= 0)
        {
            Console.WriteLine("Unesena količina mora biti validan broj veći od nule.");
            return;
        }

        //Singleton instance za snabdijevanje
        ISnabdijevanje snabdijevanjeServis = potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO
            ? GarantovanoServis.Instance
            : KomercijalnoServis.Instance;

        try
        {
            var odgovarajuciPodsistem = _upravljanjePodsistemimaServis.NadjiPodsistemSaNajviseEnergije(zeljenaEnergija);

            if (odgovarajuciPodsistem == null)
            {
                Console.WriteLine("Nema dovoljno energije u podsistemima da bi se ispunio zahtev.");
                return;
            }

            snabdijevanjeServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, zeljenaEnergija);

            potrosac.Ukupna_potrosnja_ee += zeljenaEnergija;
            potrosac.Trenutno_zaduzenje += zeljenaEnergija * snabdijevanjeServis.CijenaPoKW;

            // Dodavanje zapisa u evidenciju
            var zapis = new Zapis(DateTime.Now, zeljenaEnergija);
            _evidencija.DodajZapis(zapis);

            Console.WriteLine($"Zahtev za energiju uspešno obrađen za potrošača {brojUgovora}.");
            Console.WriteLine($"Nova ukupna potrošnja: {potrosac.Ukupna_potrosnja_ee:F2} kWh.");
            Console.WriteLine($"Novo trenutno zaduženje: {potrosac.Trenutno_zaduzenje:F2} RSD.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Došlo je do greške prilikom obrade zahteva: {ex.Message}");
        }
    }
}
