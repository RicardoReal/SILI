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
    
    public partial class TipoDevolucao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoDevolucao()
        {
            this.DetalheRecepcao = new HashSet<DetalheRecepcao>();
        }
    
        public long ID { get; set; }
        public long Numero { get; set; }
        public string Descricao { get; set; }
        public bool Disponivel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalheRecepcao> DetalheRecepcao { get; set; }
    }
}
