using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SnabdijevanjeServisi
{
    public class KomercijalnoServis : ISnabdijevanje
    {
        public double CijenaPoKW {
            get { return 43.02; }
        }

        public double SmanjenjeKolicine(double kolicina)
        {
            return kolicina - (kolicina * 0.01);
        }
    }
}
