using Domain.Constants;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.SnabdijevanjeServisi;
using Services.IspisServisi;

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
        var potrosac = _upravljanjePodsistemimaPotrosnje.PronadjiPotrosaca(id);

        if (potrosac == null)
        {
            _ispisServis.Ispisi($"Potrosac sa ID {id} nije pronađen.");
            return;
        }

        _proizvodnjaEnergije.ProvjeriIPovecajKolicinu(potrosac.Tip_Snabdevanja);

        ISnabdijevanje snabdijevanjeServis = potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO
            ? GarantovanoServis.Instance
            : KomercijalnoServis.Instance;

        try
        {
            var odgovarajuciPodsistem = _upravljanjePodsistemimaServis.NadjiPodsistemSaNajviseEnergije(zeljenaEnergija);

            bool pronadjenPodsistem = false;
            if (odgovarajuciPodsistem != null)
            {
                string poruka = "Pronadjen je podsistem sa dovoljno energije.";
                pronadjenPodsistem = _ispisServis.Ispisi(poruka);
            }
            else
            {
                return;
            }

            bool smanjenaKolicina = snabdijevanjeServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, zeljenaEnergija);
            if (smanjenaKolicina == true)
            {
                string poruka = "Kolicina u podsistemu smanjena.";
                _ispisServis.Ispisi(poruka);
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

            _ispisServis.Ispisi("======ZAHTEV======");
            _ispisServis.Ispisi($"Zahtev za energiju uspešno obradjen za potrosaca {id}.");
            _ispisServis.Ispisi($"Nova ukupna potrosnja: {potrosac.Ukupna_potrosnja_ee:F2} kWh.");
            _ispisServis.Ispisi($"Novo trenutno zaduzenje: {potrosac.Trenutno_zaduzenje:F2} RSD.");
        }
        catch (Exception ex)
        {
            _ispisServis.Ispisi($"Doslo je do greske prilikom obrade zahteva: {ex.Message}");
        }
    }
}
