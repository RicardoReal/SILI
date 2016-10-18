using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(CodPostalMetadata))]
    public partial class CodigoPostal
    {
        public override string ToString()
        {
            return this.CodPostal + " " + this.Localidade;
        }

        public string FormattedToString
        {
            get
            {
                return this.ToString();
            }
        }
    }

    public class CodPostalMetadata
    {
        [Display(Name="Cód. Postal")]
        [RegularExpression(@"\d{4}-\d{3}",ErrorMessage ="Cod. Postal com formato inválido.")]
        public string CodPostal;

        
                
    }

    
}