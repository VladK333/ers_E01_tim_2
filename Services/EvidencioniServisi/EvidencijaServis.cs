using Domain.Enums;
using Domain.Models;
using Domain.Services;

namespace Services.EvidencioniServisi
{
    public class EvidencijaServis : IEvidencija
    {
        private static readonly List<Zapis> _evidencija = [];
        private readonly string _putanjaDatoteke;
        private readonly TipSnabdijevanja _tipSnabdevanja;

        public EvidencijaServis(TipSnabdijevanja tipSnabdevanja, string? putanjaDatoteke = null)
        {
            _tipSnabdevanja = tipSnabdevanja;
            _putanjaDatoteke = putanjaDatoteke ?? "evidencija.txt";
        }

        public void DodajZapis(Zapis zapis)
        {
            try
            {
                if (_tipSnabdevanja == TipSnabdijevanja.GARANTOVANO)
                {
                    // Upisivanje u datoteku pomoću ToString metode
                    File.AppendAllText(_putanjaDatoteke, zapis.ToString() + Environment.NewLine);
                }
                else if (_tipSnabdevanja == TipSnabdijevanja.KOMERCIJALNO)
                {
                    // Dodavanje u kolekciju
                    _evidencija.Add(zapis);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Greška prilikom dodavanja zapisa: {ex.Message}");
            }
        }

        public IEnumerable<Zapis> PregledZapisa()
        {
            if (_tipSnabdevanja == TipSnabdijevanja.GARANTOVANO)
            {
                // Čitanje iz datoteke
                try
                {
                    var linije = File.ReadAllLines(_putanjaDatoteke);
                    var zapisi = new List<Zapis>();

                    foreach (var linija in linije)
                    {
                        // Pretpostavka: Parsiranje formata zapisa
                        var delovi = linija.Split(": Izdato je ");
                        if (delovi.Length == 2 &&
                            DateTime.TryParseExact(delovi[0], "dd.MM.yyyy HH:mm", null,
                                System.Globalization.DateTimeStyles.None, out var datumIVreme) &&
                            double.TryParse(delovi[1].Replace(" kW.", ""), out var kolicina))
                        {
                            zapisi.Add(new Zapis(datumIVreme, kolicina));
                        }
                    }

                    return zapisi;
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Greška prilikom čitanja zapisa: {ex.Message}");
                    return Enumerable.Empty<Zapis>();
                }
            }
            else
            {
                // Vraćanje zapisa iz memorije
                return _evidencija;
            }
        }
    }
}
