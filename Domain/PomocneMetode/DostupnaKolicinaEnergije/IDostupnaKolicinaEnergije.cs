using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PomocneMetode.DostupnaKolicinaEnergije
{
    public interface IDostupnaKolicinaEnergije
    {
        double Generate(double minVr, double maxVr);
    }
}
