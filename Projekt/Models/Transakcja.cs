//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projekt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transakcja
    {
        public int id { get; set; }
        public int dziecko_id { get; set; }
        public int oplata_id { get; set; }
        public float kwota { get; set; }
        public System.DateTime data_zaplaty { get; set; }
    
        public virtual Dziecko Dziecko { get; set; }
        public virtual Oplata Oplata { get; set; }
    }
}
