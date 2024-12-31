using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Presentation.Authentifikacija;
using Presentation.Meni;
using Services.AutentifikacioniServisi;
using Services.EvidencioniServisi;
using Services.PotrosnjaServisi;
using Services.ProizvodnjaServisi;
using Services.SnabdijevanjeServisi;
public class Program
{
    public static void Main()
    {
        IAutentifikacija autentifikacijaServis = new AutentifikacioniServis();
        IDostupnaKolicinaEnergije dostupnaKolicinaEnergije = new DostupnaKolicinaEnergijeServis();

        IPotrosac potrosacServis = new PotrosacServis();

        // Koristimo Singleton instance servisa za snabdevanje
        ISnabdijevanje snabdijevanjeGarantovano = GarantovanoServis.Instance;
        ISnabdijevanje snabdijevanjeKomercijalno = KomercijalnoServis.Instance;

        IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaProizvodnje =
            new UpravljanjePodsistemimaServis(dostupnaKolicinaEnergije, snabdijevanjeGarantovano);

        IPotrosnja potrosnja = new PotrosnjaServis(upravljanjePodsistemimaProizvodnje);

        var proizvodnjaServis = new ProizvodnjaServis(snabdijevanjeGarantovano, upravljanjePodsistemimaProizvodnje);
        proizvodnjaServis.ProvjeriIPovecajKolicinu();

        var auth = new AutentifikacijaKorisnika(autentifikacijaServis);
        if (!auth.TryLogin(out Potrosac potrosac))
        {
            Console.WriteLine("Autentifikacija nije uspela.");
            return;
        }

        IEvidencija evidencijaGarantovano = new EvidencijaServis(TipSnabdijevanja.GARANTOVANO);
        IEvidencija evidencijaKomercijalno = new EvidencijaServis(TipSnabdijevanja.KOMERCIJALNO);

        IZahtevZaEnergiju zahtevServis = new ZahtevZaEnergijuServis(potrosacServis, upravljanjePodsistemimaProizvodnje, evidencijaGarantovano);

        var meni = new IspisMeni(potrosacServis, upravljanjePodsistemimaProizvodnje, zahtevServis, evidencijaGarantovano);
        meni.PrikaziMeni();
    }
}

