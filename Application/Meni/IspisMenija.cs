using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Meni
{
    public class IspisMenija
    {
        private readonly Potrosac potrosac;

        public void PrikaziMeni()
        {
            bool kraj = false;
            while(!kraj)
            {
                Console.WriteLine("\n1. Pregled svih potrosaca\n2. Unos novog potrosaca\n" +
                    "3. Zahtev za dobijanje elektricne energije\n4. Trenutno zaduzenje potrosaca\n" +
                    "5. Odjava");
                Console.Write("Opcija: ");
                string? opcija = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(opcija))
                    continue;

                switch(opcija[0])
                {
                    case '1':
                        break;
                    case '2':  
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case '5':
                        kraj = true;
                        break;
                    default:
                        continue;
                }
            }
        }
    }
}
