using SILI.Models;
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
            return this.EAN + " - " + this.Descricao;
        }

        public string FormattedToString
        {
            get { return this.ToString(); }
        }

        public static List<Autocomplete> GetProdutos(string prefix)
        {
            List<Autocomplete> produtos = new List<Autocomplete>();

            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                var results = (from p in ent.Produto
                               where p.EAN.Contains(prefix) || p.Descricao.Contains(prefix)
                               orderby p.Descricao
                               select p).Take(10).ToList();

                foreach (var r in results)
                {
                    Autocomplete produto = new Autocomplete();

                    produto.Name = r.FormattedToString;
                    produto.Id = (int)r.ID;
                    produtos.Add(produto);
                }
            }

            return produtos;
        }

        public static bool IsValid(string code)
        {
            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                return ent.Produto.Where(p => p.EAN == code || p.CNP == code).Count() > 1;
            }
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

        [Display(Name = "Largura (mm)")]
        public decimal Largura;

        [Display(Name = "Peso (gr)")]
        public decimal Peso;

        [Display(Name = "Altura (mm)")]
        public decimal Altura;

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