using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.EvidencioniServisi;
using Services.SnabdijevanjeServisi;

public class ZahtevZaEnergijuServis : IZahtevZaEnergiju
{
    private readonly IPotrosac _potrosacServis;
    private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemimaServis;
    private readonly IProizvodnjaEnergije _proizvodnjaEnergije;
    //private readonly IEvidencija _evidencija;

    public ZahtevZaEnergijuServis(IPotrosac potrosacServis, IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis, IEvidencija evidencija, IProizvodnjaEnergije proizvodnjaEnergije)
    {
        _potrosacServis = potrosacServis;
        _upravljanjePodsistemimaServis = upravljanjePodsistemimaServis;
        _proizvodnjaEnergije = proizvodnjaEnergije;
        // _evidencija = evidencija;
    }

    public void ObradiZahtev(string id, double zeljenaEnergija)
    {
        _proizvodnjaEnergije.ProvjeriIPovecajKolicinu();

        var potrosac = _potrosacServis.PronadjiPotrosaca(id);

        if (potrosac == null)
        {
            Console.WriteLine("Potrosac sa unetim id ne postoji.");
            return;
        }

        if (zeljenaEnergija <= 0)
        {
            Console.WriteLine("Unesena kolicina mora biti validan broj veci od nule.");
            return;
        }

        //Singleton instance za snabdijevanje
        ISnabdijevanje snabdijevanjeServis = potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO
            ? GarantovanoServis.Instance
            : KomercijalnoServis.Instance;

        var evidencija = new EvidencijaServis(potrosac.Tip_Snabdevanja);

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
            evidencija.DodajZapis(zapis);

            Console.WriteLine("======ZAHTEV======");
            Console.WriteLine($"Zahtev za energiju uspešno obradjen za potrosaca {id}.");
            Console.WriteLine($"Nova ukupna potrosnja: {potrosac.Ukupna_potrosnja_ee:F2} kWh.");
            Console.WriteLine($"Novo trenutno zaduzenje: {potrosac.Trenutno_zaduzenje:F2} RSD.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Doslo je do greske prilikom obrade zahteva: {ex.Message}");
        }
    }
}
