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
    public class Firma_Pracownik
    {
        public IEnumerable<Firma> Firma { get; set; }
        public IEnumerable<Pracownik> Pracownik { get; set; }
    }
}