//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SILI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Destinatario
    {
        public long ID { get; set; }
        public long ClienteID { get; set; }
        public long NIFDestinatario { get; set; }
        public long CodPostalID { get; set; }
        public int CodigoDestinatario { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual CodigoPostal CodigoPostal { get; set; }
    }
}
