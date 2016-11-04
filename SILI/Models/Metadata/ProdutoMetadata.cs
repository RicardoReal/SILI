using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(ProdutoMetadata))]
    public partial class Produto
    {
        public override string ToString()
        {
            return this.Referencia + " - " + this.Descricao;
        }

        public string FormattedToString
        {
            get { return this.ToString(); }
        }
    }

    public class ProdutoMetadata
    {
        [Required]
        [Display(Name="Referência")]
        public long Referencia;

        [Required]
        public long EAN;

        [Required]
        public long CNP;

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao;

        [Required]
        [Display(Name = "Apresentação")]
        public string Apresentacao;

        [Display(Name = "Qtd. Caixa")]
        public decimal QtdCaixa;

        [Display(Name = "Qtd. Palete")]
        public decimal QtdPalete;

        [Display(Name = "Und. Venda")]
        public decimal UndVenda;

        [Display(Name = "Preço Tabelado")]
        public decimal PrecoTabelado;
    }

    
}