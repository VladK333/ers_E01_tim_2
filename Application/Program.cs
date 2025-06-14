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
using Presentation.Potrosaci;
using Services.AutentifikacioniServisi;
using Services.EvidencioniServisi;
using Services.IspisServisi;
using Services.PodsistemPotrosnjeServisi;
using Services.PotrosacServisi;
using Services.ProizvodnjaServisi;
using Services.ZahtjevServisi;
public class Program
{
    public static void Main()
    {
        IAutentifikacijaRepozitorijum autentifikacijaRepozitorijum = new AutentifikacijaRepozitorijum();
        IAutentifikacija autentifikacijaServis = new AutentifikacioniServis(autentifikacijaRepozitorijum);
        IIspis ispisServis = new IspisServis();

        IDostupnaKolicinaEnergije dostupnaKolicinaEnergije = new DostupnaKolicinaEnergije();
        IProizvodnjaRepozitorijum proizvodnjaRepozitorijum = new ProizvodnjaRepozitorijum(dostupnaKolicinaEnergije);
        IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaProizvodnje = new UpravljanjePodsistemimaServis(proizvodnjaRepozitorijum, ispisServis);

        IPotrosacRepozitorijum repozitorijum = new PotrosacRepozitorijum();

        IPotrosnjaRepozitorijum potrosnjaRepozitorijum = new PotrosnjaRepozitorijum(repozitorijum);
        IUpravljanjePodsistemimaPotrosnje upravljanjePodsistemimaPotrosnje = new UpravljanjePodsistemimaPotrosnjeServis(potrosnjaRepozitorijum);
        
        IPotrosac potrosacServis = new PotrosacServis(repozitorijum,potrosnjaRepozitorijum);

        IEvidencijaRepozitorijum evidencijaRepozitorijum = new EvidencijaRepozitorijum();
        IEvidencija evidencijaServis = new EvidencijaServis(evidencijaRepozitorijum, ispisServis);
        
        IProizvodnjaEnergije proizvodnjaServis = new ProizvodnjaServis(upravljanjePodsistemimaProizvodnje, ispisServis);

        IZahtevZaEnergiju zahtevServis = new ZahtevZaEnergijuServis(upravljanjePodsistemimaPotrosnje, upravljanjePodsistemimaProizvodnje, proizvodnjaServis, evidencijaServis, ispisServis);
        var auth = new AutentifikacijaKorisnika(autentifikacijaServis);

        auth.TryLogin(out Potrosac potrosac);

        IOperacijePotrosaci operacijePotrosaci = new OperacijePotrosaci(potrosacServis, upravljanjePodsistemimaProizvodnje, proizvodnjaServis);
        var meni = new IspisMeni(operacijePotrosaci, zahtevServis, potrosacServis);
        meni.PrikaziMeni();
    }
}

