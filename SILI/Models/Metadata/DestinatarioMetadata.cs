using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(DestinatarioMetadata))]
    public partial class Destinatario { }

    public class DestinatarioMetadata
    {
        [Display(Name = "Cliente")]
        public long ClienteID;

        [Display(Name = "Cliente")]
        public Cliente Cliente;

        [Display(Name = "NIF (Destinatário)")]
        public long NIFDestinatario;

        [Display(Name = "Cód. Postal")]
        public long CodPostalID;

        [Display(Name = "Cód. Postal")]
        public CodigoPostal CodigoPostal;

        [Display(Name = "Cód. Destinatário")]
        public long CodigoDestinatario;


    }

    
}