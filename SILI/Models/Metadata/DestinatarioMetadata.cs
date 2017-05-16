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
        [Display(Name = "Morada")]
        public long MoradaID;

        [Display(Name = "Morada")]
        public Cliente Morada;

        [Display(Name = "NIF (Destinatário)")]
        public long NIFDestinatario;

        [Display(Name = "Cód. Postal")]
        public long CodPostalID;

        [Display(Name = "Cód. Postal")]
        public CodigoPostal CodigoPostal;

        [Display(Name = "Cód. Destinatário")]
        public long CodigoDestinatario;

        [Display(Name = "Cód. SAP")]
        public long CodigoSAP;


    }

    
}