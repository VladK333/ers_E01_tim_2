using Domain.Enums;

namespace Domain.Models
{
    public class Potrosac
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ImePrezime { get; set; } = string.Empty;

        public string BrUgovora { get; set; } = string.Empty;

        public TipSnabdijevanja Tip_Snabdevanja { get; set; }

        public double Ukupna_potrosnja_ee { get; set; } = 0;

        public double Trenutno_zaduzenje { get; set; } = 0;

        public Potrosac() {}

        public Potrosac(string imePrezime, string br_ugovora, TipSnabdijevanja tip_Snabdevanja, double ukupna_potrosnja_ee, double trenutno_zaduzenje)
        {
            Id = Guid.NewGuid().ToString();
            ImePrezime = imePrezime;
            BrUgovora = br_ugovora;
            Tip_Snabdevanja = tip_Snabdevanja;
            Ukupna_potrosnja_ee = ukupna_potrosnja_ee;
            Trenutno_zaduzenje = trenutno_zaduzenje;
        }

        public override string? ToString()
        {
            return $"\nIme i prezime: {ImePrezime}" +
                   $"\nBroj ugovora: {BrUgovora}" +
                   $"\nTip snabdevanja: {Tip_Snabdevanja}" +
                   $"\nUkupna potrosnja ee: {Ukupna_potrosnja_ee.ToString("F2")}" +
                   $"\nTrenutno zaduzenje: {Trenutno_zaduzenje.ToString("F2")}\n";
        }
    }
}
