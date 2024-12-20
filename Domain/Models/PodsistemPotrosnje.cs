namespace Domain.Models
{
    public class PodsistemPotrosnje
    {
       // private static readonly List<Potrosac> _potrosaci = new List<Potrosac>();
        private readonly List<Potrosac> _potrosaci = new List<Potrosac>();//Nestatička lista omogućava da svaki objekat klase PodsistemPotrosnje ima svoju posebnu listu potrošača.
        public string NazivPodsistema {  get; set; } = string.Empty;

        public string SifraPotrosnje {  get; set; } = string.Empty;

        public PodsistemPotrosnje(string nazivPodsistema, string sifra_Potrosnje)
        {
            NazivPodsistema = nazivPodsistema;
            SifraPotrosnje = sifra_Potrosnje;
        }

        public override string? ToString()
        {
            return $"\nPodsistem: {NazivPodsistema}\n" +
                $"Sifra podsistema potrosnje: {SifraPotrosnje}" + base.ToString();
        }
    }
}
