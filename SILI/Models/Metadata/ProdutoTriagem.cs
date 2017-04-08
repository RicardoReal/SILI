using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(ProdutoTriagemMetadata))]
    public partial class ProdutoTriagem
    {
        
    }

    public class ProdutoTriagemMetadata
    {
        [Required]
        [Display(Name="EAN / CNP")]
        public long EANCNP;

        [Required]
        [Display(Name = "Qtd. Devolvida")]
        public long QtdDevolvida;
    }

    
}