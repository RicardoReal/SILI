using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(DetalheRecepcaoMetadata))]
    public partial class DetalheRecepcao { }

    public class DetalheRecepcaoMetadata
    {
        [Display(Name = "Nr. Detalhe")]
        public DateTime NrDetalhe;

        [Display(Name = "Nr. Volumes")]
        public long NrVolumes;

        [Display(Name = "Nr. Tipo Recepção")]
        public long NrTipoRecepcao;
    }

    
}