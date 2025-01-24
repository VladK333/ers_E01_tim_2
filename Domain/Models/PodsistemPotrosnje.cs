namespace Domain.Models
{
    public class PodsistemPotrosnje
    {
        public List<Potrosac> Potrosaci { get; set; } = [];
        public string NazivPodsistema { get; set; } = string.Empty;

        public string SifraPotrosnje { get; set; } = string.Empty;

        public PodsistemPotrosnje(string nazivPodsistema, string sifra_Potrosnje, List<Potrosac> potrosaci)
        {
            NazivPodsistema = nazivPodsistema;
            SifraPotrosnje = sifra_Potrosnje;
            Potrosaci = potrosaci;
        }

        public override string? ToString()
        {
            return $"\nPodsistem: {NazivPodsistema}\n" +
                $"Sifra podsistema potrosnje: {SifraPotrosnje}" + base.ToString();
        }
    }
}
