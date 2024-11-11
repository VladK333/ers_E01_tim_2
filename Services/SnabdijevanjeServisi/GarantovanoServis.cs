using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SnabdijevanjeServisi
{
    public class GarantovanoServis : ISnabdijevanje
    {
        public double CijenaPoKW
        {
            get { return 22.72; }
        }

        public double SmanjenjeKolicine(double kolicina)
        {
            return kolicina - (kolicina * 0.02);
        }
    }
}
