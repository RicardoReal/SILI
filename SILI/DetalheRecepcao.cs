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
    
    public partial class DetalheRecepcao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DetalheRecepcao()
        {
            this.Triagem = new HashSet<Triagem>();
        }
    
        public long ID { get; set; }
        public string NrDetalhe { get; set; }
        public long ClienteId { get; set; }
        public int NrVolumes { get; set; }
        public long TipoRecepcaoId { get; set; }
        public Nullable<long> NReferencia { get; set; }
        public long RecepcaoId { get; set; }
        public string NrGuiaTransporte { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Recepcao Recepcao { get; set; }
        public virtual TipoDevolucao TipoDevolucao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Triagem> Triagem { get; set; }
    }
}
