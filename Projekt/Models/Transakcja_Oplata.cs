using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Transakcja_Oplata
    {
        public IEnumerable<Transakcja> Transakcja { get; set; }
        public IEnumerable<Oplata> Oplata { get; set; }
        public IEnumerable<Miesiac> Miesiac { get; set; }
        public IEnumerable<Rok> Rok { get; set; }
        public IEnumerable<Dziecko> Dziecko { get; set; }
    }
}