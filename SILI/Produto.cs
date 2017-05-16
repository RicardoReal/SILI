//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SILI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Produto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produto()
        {
            this.LoteProduto = new HashSet<LoteProduto>();
            this.DetalheTriagem = new HashSet<DetalheTriagem>();
            this.ProdutoTriagem = new HashSet<ProdutoTriagem>();
        }
    
        public long ID { get; set; }
        public string Referencia { get; set; }
        public string EAN { get; set; }
        public string CNP { get; set; }
        public string Descricao { get; set; }
        public long ClienteID { get; set; }
        public string Apresentacao { get; set; }
        public string Largura { get; set; }
        public string Altura { get; set; }
        public string Peso { get; set; }
        public Nullable<decimal> QtdCaixa { get; set; }
        public Nullable<decimal> QtdPalete { get; set; }
        public Nullable<decimal> UndVenda { get; set; }
        public long TipologiaID { get; set; }
        public Nullable<decimal> PrecoTabelado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoteProduto> LoteProduto { get; set; }
        public virtual Tipologia Tipologia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalheTriagem> DetalheTriagem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdutoTriagem> ProdutoTriagem { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
