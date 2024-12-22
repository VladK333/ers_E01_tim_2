using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Services.AutentifikacioniServisi;
using Services.EvidencioniServisi;
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
            // Inicijalizacija servisa
            IAutentifikacija autentifikacija = new AutentifikacioniServis();
            IDostupnaKolicinaEnergije dostupnaKolicinaEnergije = new DostupnaKolicinaEnergijeServis();

            ISnabdijevanje snabdijevanjeGarantovano = new GarantovanoServis();
            ISnabdijevanje snabdijevanjeKomercijalno = new KomercijalnoServis();

            IPotrosnja potrosnja = new PotrosnjaServis(snabdijevanjeGarantovano, snabdijevanjeKomercijalno);
            IPotrosac potrosacServis = new PotrosacServis(autentifikacija); 
            IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaProizvodnjeGarantovano = new UpravljanjePodsistemimaServis(dostupnaKolicinaEnergije, snabdijevanjeGarantovano);
            IUpravljanjePodsistemimaProizvodnje upravljanjePodsistemimaProizvodnjeKomercijalno = new UpravljanjePodsistemimaServis(dostupnaKolicinaEnergije, snabdijevanjeKomercijalno);

            IProizvodnjaEnergije proizvodnjaEnergijeGarantovano = new ProizvodnjaServis(snabdijevanjeGarantovano, upravljanjePodsistemimaProizvodnjeGarantovano);
            IProizvodnjaEnergije proizvodnjaEnergijeKomercijalno = new ProizvodnjaServis(snabdijevanjeKomercijalno, upravljanjePodsistemimaProizvodnjeKomercijalno);


            // Provera autentifikacije
            if (!autentifikacija.TryLogin(out Potrosac? potrosac))  
            {
                Console.WriteLine("Autentifikacija nije uspela.");
                return;
            }
            IZahtjevZaEnergiju zahtjevZaEnergijuGarantovano = new GarantovanoZahtjevServis(snabdijevanjeGarantovano, potrosacServis);
            IZahtjevZaEnergiju zahtjevZaEnergijuKomercijalno = new KomercijalnoZahtjevServis(snabdijevanjeKomercijalno, potrosacServis);

            IEvidencija evidencijaGarantovano = new EvidencijaServis(TipSnabdijevanja.GARANTOVANO);
            IEvidencija evidencijaKomercijalno = new EvidencijaServis(TipSnabdijevanja.KOMERCIJALNO);

            // Testiranje garantovanog snabdijevanja
            Console.WriteLine("Testiranje garantovanog snabdijevanja:");
            var zapis1 = new Zapis(DateTime.Now, 50);
            evidencijaGarantovano.DodajZapis(zapis1);

            if (File.Exists("evidencija.txt"))
            {
                Console.WriteLine("Datoteka je uspješno kreirana.");
            }
            else
            {
                Console.WriteLine("Datoteka nije kreirana.");
            }

            // Testiranje komercijalnog snabdijevanja
            Console.WriteLine("\nTestiranje komercijalnog snabdijevanja:");
            var zapis2 = new Zapis(DateTime.Now, 75);
            evidencijaKomercijalno.DodajZapis(zapis2);

            var zapisi = evidencijaKomercijalno.PregledZapisa();
            Console.WriteLine("Zapisi u memoriji:");
            foreach (var z in zapisi)
            {
                Console.WriteLine(z);
            }
            // Prikaz menija
            var meni = new IspisMeni(potrosacServis, upravljanjePodsistemimaProizvodnjeGarantovano);
            meni.PrikaziMeni();
        }
    }
}



