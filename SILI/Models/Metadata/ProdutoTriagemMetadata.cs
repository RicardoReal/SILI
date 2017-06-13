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
        public bool HasLote(string lote, long produtoId, out DateTime? validade)
        {
            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                var aux = ent.LoteProduto.Where(lp => lp.ProdutoID == produtoId && lp.Lote == lote);

                if (aux.Count() > 0)
                {
                    validade = aux.FirstOrDefault().Validade;
                    return true;
                }

                validade = null;
                return false;
            }
        }
    }

    public class ProdutoTriagemMetadata
    {
        [Required]
        [Display(Name="EAN")]
        public long EANCNP;

        [Required]
        [Display(Name = "Qtd. Devolvida")]
        public long QtdDevolvida;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Validade;

        [Display(Name = "Localização")]
        public string Localizacao;

    }

    
}