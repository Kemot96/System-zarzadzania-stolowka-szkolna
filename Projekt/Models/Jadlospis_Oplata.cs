using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Projekt.Models;

namespace Projekt.Models
{
    public class Jadlospis_Oplata
    {
        public IEnumerable<Jadlospis> Jadlospis { get; set; }
        public IEnumerable<Oplata> Oplata { get; set; }
        public IEnumerable<Miesiac> Miesiac { get; set; }
        public IEnumerable<Rok> Rok { get; set; }
    }
}