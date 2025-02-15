using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PomocneMetode.DostupnaKolicinaEnergije
{
    public class DostupnaKolicinaEnergije : IDostupnaKolicinaEnergije
    {
        private static Random _random = new Random();

        public double Generate(double minVr, double maxVr)
        {
            return _random.NextDouble() * (maxVr - minVr) + minVr;
        }
    }
}
