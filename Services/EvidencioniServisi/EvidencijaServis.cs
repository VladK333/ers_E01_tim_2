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
        private readonly IIspis _ispisServis;


        public EvidencijaServis(IEvidencijaRepozitorijum evidencijaRepozitorijum, IIspis ispisServis, string? putanjaDatoteke = null)
        {
            _evidencijaRepozitorijum = evidencijaRepozitorijum;
            _putanjaDatoteke = putanjaDatoteke ?? "evidencija.txt";
            _ispisServis = ispisServis;
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
                _ispisServis.Ispisi("Evidencija uspesna.");
            }
            catch (IOException ex)
            {
                _ispisServis.Ispisi($"Greska prilikom dodavanja zapisa: {ex.Message}");
            }
        }
    }
}
   
