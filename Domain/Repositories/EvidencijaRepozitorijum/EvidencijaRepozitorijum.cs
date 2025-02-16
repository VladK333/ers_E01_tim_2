using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories.EvidencijaRepozitorijum
{
    public class EvidencijaRepozitorijum : IEvidencijaRepozitorijum
    {
        private readonly List<Zapis> _evidencija = [];

        public void DodajZapis(Zapis zapis)
        {
            _evidencija.Add(zapis);
        }
    }
}
