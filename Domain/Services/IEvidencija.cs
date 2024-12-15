using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services
{
    public interface IEvidencija
    {
        public void DodajZapis(Zapis zapis);

        public IEnumerable<Zapis> PregledZapisa();
    }
}
