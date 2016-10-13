using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(CodPostalMetadata))]
    public partial class CodigoPostal { }

    public class CodPostalMetadata
    {
        [Display(Name="Cod. Postal")]
        [RegularExpression(@"\d{4}-\d{3}",ErrorMessage ="Cod. Postal com formato inválido.")]
        public string CodPostal;
                
    }

    
}