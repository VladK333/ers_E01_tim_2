using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Potrosac
    {
        public string Id { get; set; }
        public string ImePrezime { get; set; } = string.Empty;

        public string Br_ugovora { get; set; } = string.Empty;

        public TipSnabdevanja Tip_Snabdevanja { get; set; }

        public double Ukupna_potrosnja_ee { get; set; } = 0;

        public double Trenutno_zaduzenje { get; set; } = 0;

        public Potrosac() { Id = Guid.NewGuid().ToString(); }

        public Potrosac(string imePrezime, string br_ugovora, TipSnabdevanja tip_Snabdevanja, double ukupna_potrosnja_ee, double trenutno_zaduzenje)
        {
            Id = Guid.NewGuid().ToString();
            ImePrezime = imePrezime;
            Br_ugovora = br_ugovora;
            Tip_Snabdevanja = tip_Snabdevanja;
            Ukupna_potrosnja_ee = ukupna_potrosnja_ee;
            Trenutno_zaduzenje = trenutno_zaduzenje;
        }

        public override string? ToString()
        {
            return $"\nIme i prezime: {ImePrezime}" +
                   $"\nBroj ugovora: {Br_ugovora}\n Tip snabdevanja: {Tip_Snabdevanja}" +
                   $"\nUkupna potrosnja ee: {Ukupna_potrosnja_ee}" +
                   $"\nTrenutno zaduzenje: {Trenutno_zaduzenje}\n" + base.ToString();
        }
    }
}
