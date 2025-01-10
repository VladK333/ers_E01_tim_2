namespace Domain.Models
{
    public class Zapis
    {
        public DateTime DatumIVreme { get; set; }
        public double Kolicina { get; set; }

        public Zapis(DateTime datumIVreme, double kolicina)
        {
            DatumIVreme = datumIVreme;
            Kolicina = kolicina;
        }

        public override string ToString()
        {
            return $"{DatumIVreme:dd.MM.yyyy HH:mm:ss} Izdato je {Kolicina} kW.";
        }
    }
}

