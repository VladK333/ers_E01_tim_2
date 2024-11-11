using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PodsistemProizvodnje
    {
        public string Sifra {  get; set; }

        public TipProizvodnje Tip { get; set; }

        public string Lokacija {  get; set; }

        public double PreostalaKolicina {  get; set; }

        public PodsistemProizvodnje(string sifra, TipProizvodnje tip, string lokacija, double preostalaKolicina)
        {
            Sifra = sifra;
            Tip = tip;
            Lokacija = lokacija;
            PreostalaKolicina = preostalaKolicina;
        }

        public override string ToString()
        {
            return $"Šifra: {Sifra}, Tip: {Tip}, Lokacija: {Lokacija}, Preostala količina: {PreostalaKolicina}";
        }
    }
}
