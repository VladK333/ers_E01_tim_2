using Domain.Enums;
using Domain.Models;
using Domain.Repositories.EvidencijaRepozitorijum;
using Domain.Services;

namespace Services.EvidencioniServisi
{
    public class EvidencijaServis : IEvidencija
    {
        private readonly IEvidencijaRepozitorijum _evidencijaRepozitorijum;   
        private readonly string _putanjaDatoteke;

        public EvidencijaServis(IEvidencijaRepozitorijum evidencijaRepozitorijum, string? putanjaDatoteke = null)
        {
            _evidencijaRepozitorijum = evidencijaRepozitorijum;
            _putanjaDatoteke = putanjaDatoteke ?? "evidencija.txt";
        }

        public void DodajZapis(Zapis zapis, TipSnabdijevanja tipSnabdevanja)
        {
            try
            {
                if (tipSnabdevanja == TipSnabdijevanja.GARANTOVANO)
                {
                    File.AppendAllText(_putanjaDatoteke, zapis.ToString() + Environment.NewLine);
                }
                else if (tipSnabdevanja == TipSnabdijevanja.KOMERCIJALNO)
                {
                    _evidencijaRepozitorijum.DodajZapis(zapis);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Greska prilikom dodavanja zapisa: {ex.Message}");
            }
        }
    }
}
   
