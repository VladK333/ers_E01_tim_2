using Domain.Enums;
using Domain.Services;
using Services.AutentifikacioniServisi;
using Services.EvidencioniServisi;
using Services.PotrosacServisi;
using Services.PotrosnjaServisi;
using Services.ProizvodnjaServisi;
using Services.SnabdijevanjeServisi;
using Presentation.Meni; 


namespace Application
{
    public class Program
    {
        public static void Main()
        {
            //Inicijalizacija servisa
            IAutentifikacija autentifikacija = new AutentifikacioniServis();
            IDostupnaKolicinaEnergije dostupnaKolicinaEnergije = new DostupnaKolicinaEnergijeServis();

            ISnabdijevanje snabdijevanjeGarantovano = new GarantovanoServis();
            ISnabdijevanje snabdijevanjeKomercijalno = new KomercijalnoServis();

            IPotrosnja potrosnja = new PotrosnjaServis(snabdijevanjeGarantovano, snabdijevanjeKomercijalno);

            IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaProizvodnjeGarantovano = new UpravljanjePodsistemimaServis(dostupnaKolicinaEnergije, snabdijevanjeGarantovano);
            IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaProizvodnjeKomercijalno = new UpravljanjePodsistemimaServis(dostupnaKolicinaEnergije, snabdijevanjeKomercijalno);

            IProizvodnjaEnergije proizvodnjaEnergijeGarantovano = new ProizvodnjaServis(snabdijevanjeGarantovano, upravljanjePodsistemimaProizvodnjeGarantovano);
            IProizvodnjaEnergije proizvodnjaEnergijeKomercijalno = new ProizvodnjaServis(snabdijevanjeKomercijalno, upravljanjePodsistemimaProizvodnjeKomercijalno);


            IPotrosac potrosac = new PotrosacServis(potrosnja);
            IZahtjevZaEnergiju zahtjevZaEnergijuGarantovano = new GarantovanoZahtjevServis(snabdijevanjeGarantovano, potrosac);
            IZahtjevZaEnergiju zahtjevZaEnergijuKomercijalno = new KomercijalnoZahtjevServis(snabdijevanjeKomercijalno, potrosac);

            IEvidencija evidencijaGarantovano = new EvidencijaServis(TipSnabdijevanja.GARANTOVANO);
            IEvidencija evidencijaKomercijalno = new EvidencijaServis(TipSnabdijevanja.KOMERCIJALNO);


            var meni = new IspisMeni(potrosac, upravljanjePodsistemimaProizvodnjeGarantovano);
            meni.PrikaziMeni();
        }
    }
}
