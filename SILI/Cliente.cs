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
    
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            this.Produto = new HashSet<Produto>();
            this.DetalheRecepcao = new HashSet<DetalheRecepcao>();
        }
    
        public long ID { get; set; }
        public int NrInterno { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }
        public Nullable<long> NIF { get; set; }
        public string Email { get; set; }
        public Nullable<int> NrContacto { get; set; }
        public string NomeContacto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produto> Produto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalheRecepcao> DetalheRecepcao { get; set; }
    }
}
