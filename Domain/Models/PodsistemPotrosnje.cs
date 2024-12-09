namespace Domain.Models
{
    public class PodsistemPotrosnje
    {
        private static readonly List<Potrosac> _potrosaci = new List<Potrosac>();
        public string NazivPodsistema {  get; set; } = string.Empty;

        public string Sifra_Potrosnje {  get; set; } = string.Empty;

        public PodsistemPotrosnje(string nazivPodsistema, string sifra_Potrosnje)
        {
            NazivPodsistema = nazivPodsistema;
            Sifra_Potrosnje = sifra_Potrosnje;
        }

        public override string? ToString()
        {
            return $"\nPodsistem: {NazivPodsistema}\n" +
                $"Sifra podsistema potrosnje: {Sifra_Potrosnje}" + base.ToString();
        }
    }
}
