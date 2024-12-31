/*using Domain.Models;
using Domain.Services;
using Services.PotrosnjaServisi;
using System.Collections.Generic;
using System.Linq;

namespace Services.PodsistemPotrosnjeServisi
{
    public class UpravljanjePodsistemimaServis : IUpravljanjePodsistemimaPotrosnje
    {
        private readonly List<PodsistemPotrosnje> _podsistemi;
        private readonly PotrosnjaServis _potrosnjaServis;
        private readonly IUpravljanjePodsistemimaProizvodnje _proizvodnjaServis;

        // Konstruktor prima servis za potrošnju i servis za proizvodnju, implementirajući Dependency Inversion princip
        public UpravljanjePodsistemimaServis(PotrosnjaServis potrosnjaServis, IUpravljanjePodsistemimaProizvodnje proizvodnjaServis)
        {
            _podsistemi = new List<PodsistemPotrosnje>
            {
                new PodsistemPotrosnje("Podsistem 1", "PSP3321-NS1"),
                new PodsistemPotrosnje("Podsistem 2", "PSP3322-NS2"),
                new PodsistemPotrosnje("Podsistem 3", "PSP3323-NS3"),
                new PodsistemPotrosnje("Podsistem 4", "PSP3324-NS4")
            };

            _potrosnjaServis = potrosnjaServis;
            _proizvodnjaServis = proizvodnjaServis;
        }

        // Dohvatanje svih dostupnih podsistema potrošnje
        public List<PodsistemPotrosnje> DohvatiSvePodsisteme()
        {
            return _podsistemi;
        }

        // Pretraga podsistema po šifri
        public PodsistemPotrosnje? NadjiPodsistemPotrosnje(string sifraPotrosnje)
        {
            return _podsistemi.FirstOrDefault(p => p.SifraPotrosnje == sifraPotrosnje);
        }

        // Dohvatanje svih potrošača za određeni podsistem
        public List<Potrosac> DohvatiPotrosacePovezaneSaPodsistemom(string sifraPotrosnje)
        {
            var podsistem = NadjiPodsistemPotrosnje(sifraPotrosnje);
            return podsistem?.Potrosaci ?? new List<Potrosac>();
        }

        // Povezivanje potrošača sa određenim podsistemom, uz proveru da li proizvodnja može da zadovolji potrebe
        public bool PoveziPotrosaceSaPodsistemom(string sifraPodsistema, List<Potrosac> potrosaci)
        {
            // Provera da li postoji dovoljno energije u proizvodnim podsistemima
            foreach (var potrosac in potrosaci)
            {
                double energijaPotrosena = potrosac.Ukupna_potrosnja_ee;
                var odgovarajuciPodsistem = _proizvodnjaServis.NadjiPodsistemSaNajviseEnergije(energijaPotrosena);
                if (odgovarajuciPodsistem == null || odgovarajuciPodsistem.PreostalaKolicina < energijaPotrosena)
                {
                    return false; // Ako nema dovoljno energije, povezivanje nije moguće
                }
            }

            // Povezivanje potrošača sa podsistemom
            var podsistem = NadjiPodsistemPotrosnje(sifraPodsistema);
            if (podsistem != null)
            {
                podsistem.Potrosaci.AddRange(potrosaci); // Direktno dodavanje potrošača u listu
                return true;
            }

            return false;
        }
    }
}
*/
