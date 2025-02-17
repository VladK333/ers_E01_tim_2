using Domain.Constants;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.EvidencioniServisi;
using Services.IspisServisi;
using Services.SnabdijevanjeServisi;

public class ZahtevZaEnergijuServis : IZahtevZaEnergiju
{
    private readonly IUpravljanjePodsistemimaPotrosnje _upravljanjePodsistemimaPotrosnje;
    private readonly IUpravljanjePodsistemimaProizvodnje _upravljanjePodsistemimaServis;
    private readonly IProizvodnjaEnergije _proizvodnjaEnergije;
    private readonly IEvidencija _evidencija;
    private readonly IIspis _ispisServis;

    public ZahtevZaEnergijuServis(IUpravljanjePodsistemimaPotrosnje upravljanjePodsistemimaPotrosnje, IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaServis, IProizvodnjaEnergije proizvodnjaEnergije, IEvidencija evidencija, IIspis ispisServis)
    {
        _upravljanjePodsistemimaPotrosnje = upravljanjePodsistemimaPotrosnje;
        _upravljanjePodsistemimaServis = upravljanjePodsistemimaServis;
        _proizvodnjaEnergije = proizvodnjaEnergije;
        _evidencija = evidencija;
        _ispisServis = ispisServis;
    }

    public void ObradiZahtev(string id, double zeljenaEnergija)
    {
        var potrosac = _upravljanjePodsistemimaPotrosnje.PronadjiPotrosaca(id);     //potrosac se trazi u listi aktivnih potrosaca 

        _proizvodnjaEnergije.ProvjeriIPovecajKolicinu(potrosac.Tip_Snabdevanja);

        //Singleton instance za snabdijevanje
        ISnabdijevanje snabdijevanjeServis = potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO
            ? GarantovanoServis.Instance
            : KomercijalnoServis.Instance;

        try
        {
            var odgovarajuciPodsistem = _upravljanjePodsistemimaServis.NadjiPodsistemSaNajviseEnergije(zeljenaEnergija);

            bool pronadjenPodsistem = false;        //trebace za mock
            if (odgovarajuciPodsistem != null)
            {
                string poruka = "Pronadjen je podsistem sa dovoljno energije.";
                pronadjenPodsistem = _ispisServis.Ispisi(poruka);
            }
            else
            {
                return;
            }

            bool smanjenaKolicina = snabdijevanjeServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, zeljenaEnergija);     //za mock
            if (smanjenaKolicina == true)
            {
                string poruka = "Kolicina u podsistemu smanjena.";
                smanjenaKolicina = _ispisServis.Ispisi(poruka);
            }

            potrosac.Ukupna_potrosnja_ee += zeljenaEnergija;

            if (snabdijevanjeServis is GarantovanoServis)
            {
                potrosac.Trenutno_zaduzenje += zeljenaEnergija * CeneConsts.CenaGarantovano;
            }
            else if (snabdijevanjeServis is KomercijalnoServis)
            {
                potrosac.Trenutno_zaduzenje += zeljenaEnergija * CeneConsts.CenaKomercijalno;
            }

            var zapis = new Zapis(DateTime.Now, zeljenaEnergija);
            _evidencija.DodajZapis(zapis, potrosac.Tip_Snabdevanja);

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
