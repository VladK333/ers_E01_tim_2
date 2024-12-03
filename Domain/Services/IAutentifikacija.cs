using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services
{
    public interface IAutentifikacija
    {
        public (bool, Potrosac) Prijava(string ImePrezime, string Id);
    }
}
