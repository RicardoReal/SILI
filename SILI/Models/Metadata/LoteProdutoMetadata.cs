using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(LoteProdutoMetadata))]
    public partial class LoteProduto { }

    public class LoteProdutoMetadata
    {
        [DataType(DataType.Date)]
        public DateTime Validade;

        [Display(Name = "Preço")]
        public long Preco;

        [Display(Name = "Data Alteração")]
        public long DataAlteracao;
    }

    
}