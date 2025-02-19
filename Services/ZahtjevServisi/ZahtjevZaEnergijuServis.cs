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
            _ispisServis.Ispisi($"Potrosac sa ID {id} nije pronadjen.");
            return;
        }

        _proizvodnjaEnergije.ProvjeriIPovecajKolicinu(potrosac.Tip_Snabdevanja);

        ISnabdijevanje snabdijevanjeServis = potrosac.Tip_Snabdevanja == TipSnabdijevanja.GARANTOVANO
            ? GarantovanoServis.Instance
            : KomercijalnoServis.Instance;

        try
        {
            var odgovarajuciPodsistem = _upravljanjePodsistemimaServis.NadjiPodsistemSaNajviseEnergije(zeljenaEnergija);

            if (odgovarajuciPodsistem == null)
            {
                return;
            }

            snabdijevanjeServis.SmanjiKolicinuEnergije(odgovarajuciPodsistem, zeljenaEnergija);

            potrosac.Ukupna_potrosnja_ee += zeljenaEnergija;

            if (snabdijevanjeServis is GarantovanoServis)
            {
                potrosac.Trenutno_zaduzenje += zeljenaEnergija * CeneConsts.CenaGarantovano;
            }
            else if (snabdijevanjeServis is KomercijalnoServis)
            {
                potrosac.Trenutno_zaduzenje += zeljenaEnergija * CeneConsts.CenaKomercijalno;
            }

            _ispisServis.Ispisi($"======ZAHTEV======\n" +
                                $"Zahtev za energiju uspesno obradjen za potrosaca {id}.\n" +
                                $"Nova ukupna potrosnja: {potrosac.Ukupna_potrosnja_ee:F2} kWh.\n" +
                                $"Novo trenutno zaduzenje: {potrosac.Trenutno_zaduzenje:F2} RSD.");

            var zapis = new Zapis(DateTime.Now, zeljenaEnergija);
            _evidencija.DodajZapis(zapis, potrosac.Tip_Snabdevanja);
        }
        catch (Exception ex)
        {
            _ispisServis.Ispisi($"Doslo je do greske prilikom obrade zahteva: {ex.Message}");
        }
    }
}
