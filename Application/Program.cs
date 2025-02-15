using Domain.Models;
using Domain.PomocneMetode.DostupnaKolicinaEnergije;
using Domain.Repositories.PodsistemPotrosnjeRepozitorijum;
using Domain.Repositories.PodsistemProizvodnjeRepozitorijum;
using Domain.Repositories.PotrosacRepozitorijum;
using Domain.Services;
using Presentation.Authentifikacija;
using Presentation.Meni;
using Services.AutentifikacioniServisi;
using Services.PodsistemPotrosnjeServisi;
using Services.ProizvodnjaServisi;
public class Program
{
    public static void Main()
    {
        IAutentifikacija autentifikacijaServis = new AutentifikacioniServis();
        IDostupnaKolicinaEnergije dostupnaKolicinaEnergije = new DostupnaKolicinaEnergije();

        IProizvodnjaRepozitorijum proizvodnjaRepozitorijum = new ProizvodnjaRepozitorijum(dostupnaKolicinaEnergije);
        IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaProizvodnje = new UpravljanjePodsistemimaServis(proizvodnjaRepozitorijum);

        IPotrosacRepozitorijum repozitorijum = new PotrosacRepozitorijum();
        IPotrosac potrosacServis = new PotrosacServis(repozitorijum);

        IPotrosnjaRepozitorijum potrosnjaRepozitorijum = new PotrosnjaRepozitorijum(repozitorijum);
        IUpravljanjePodsistemimaPotrosnje upravljanjePodsistemimaPotrosnje = new UpravljanjePodsistemimaPotrosnjeServis(potrosnjaRepozitorijum);

        IProizvodnjaEnergije proizvodnjaServis = new ProizvodnjaServis(upravljanjePodsistemimaProizvodnje);

        IZahtevZaEnergiju zahtevServis = new ZahtevZaEnergijuServis(upravljanjePodsistemimaPotrosnje, upravljanjePodsistemimaProizvodnje, proizvodnjaServis);

        var auth = new AutentifikacijaKorisnika(autentifikacijaServis);

        if (!auth.TryLogin(out Potrosac potrosac))
        {
            Console.WriteLine("Autentifikacija nije uspela.");
            return;
        }

        var meni = new IspisMeni(potrosacServis, upravljanjePodsistemimaProizvodnje, zahtevServis, proizvodnjaServis);

        meni.PrikaziMeni();
    }
}

