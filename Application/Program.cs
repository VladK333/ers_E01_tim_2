using Domain.Models;
using Domain.PomocneMetode.DostupnaKolicinaEnergije;
using Domain.Repositories.AutentifikacijaRepozitorijum;
using Domain.Repositories.EvidencijaRepozitorijum;
using Domain.Repositories.PodsistemPotrosnjeRepozitorijum;
using Domain.Repositories.PodsistemProizvodnjeRepozitorijum;
using Domain.Repositories.PotrosacRepozitorijum;
using Domain.Services;
using Presentation.Autentifikacija;
using Presentation.Meni;
using Services.AutentifikacioniServisi;
using Services.EvidencioniServisi;
using Services.IspisServisi;
using Services.PodsistemPotrosnjeServisi;
using Services.ProizvodnjaServisi;

public class Program
{
    public static void Main()
    {
        IAutentifikacijaRepozitorijum autentifikacijaRepozitorijum = new AutentifikacijaRepozitorijum();
        IAutentifikacija autentifikacijaServis = new AutentifikacioniServis(autentifikacijaRepozitorijum);
       
        IDostupnaKolicinaEnergije dostupnaKolicinaEnergije = new DostupnaKolicinaEnergije();
        IProizvodnjaRepozitorijum proizvodnjaRepozitorijum = new ProizvodnjaRepozitorijum(dostupnaKolicinaEnergije);
        IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaProizvodnje = new UpravljanjePodsistemimaServis(proizvodnjaRepozitorijum);

        IPotrosacRepozitorijum repozitorijum = new PotrosacRepozitorijum();

        IPotrosnjaRepozitorijum potrosnjaRepozitorijum = new PotrosnjaRepozitorijum(repozitorijum);
        IUpravljanjePodsistemimaPotrosnje upravljanjePodsistemimaPotrosnje = new UpravljanjePodsistemimaPotrosnjeServis(potrosnjaRepozitorijum);
        
        IPotrosac potrosacServis = new PotrosacServis(repozitorijum,potrosnjaRepozitorijum);

        IEvidencijaRepozitorijum evidencijaRepozitorijum = new EvidencijaRepozitorijum();
        IEvidencija evidencijaServis = new EvidencijaServis(evidencijaRepozitorijum);


        IIspis ispisServis = new IspisServis();
        IProizvodnjaEnergije proizvodnjaServis = new ProizvodnjaServis(upravljanjePodsistemimaProizvodnje, ispisServis);

        IZahtevZaEnergiju zahtevServis = new ZahtevZaEnergijuServis(upravljanjePodsistemimaPotrosnje, upravljanjePodsistemimaProizvodnje, proizvodnjaServis, evidencijaServis, ispisServis);
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

