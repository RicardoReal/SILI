using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(TipoDevolvedorMetadata))]
    public partial class TipoDevolvedor { }

    public class TipoDevolvedorMetadata
    {
        [Display(Name="Nr.")]
        public long Numero;

        [Display(Name = "Descrição")]
        public string Descricao;
                
    }

    
}