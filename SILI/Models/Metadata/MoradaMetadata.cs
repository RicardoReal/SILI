using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(MoradaMetadata))]
    public partial class Morada { }

    public class MoradaMetadata
    {
        [Required()]
        public long NIF;

        [MaxLength(100)]
        public string Nome;

        [Display(Name = "Cód. Postal")]
        public long CodPostalID;

        [Display(Name = "Cód. Postal")]
        public CodigoPostal CodigoPostal;

        [MaxLength(200)]
        [Display(Name = "Morada")]
        public string Morada1;

        public int? Telefone;

        [Display(Name = "Nome de Contacto")]
        public string NomeContacto;

        [Display(Name = "Tipo de Devolvedor")]
        public long TipoDevolvedorID;
    }

    
}