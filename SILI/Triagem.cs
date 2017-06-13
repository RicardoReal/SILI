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
    
    public partial class Triagem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Triagem()
        {
            this.ProdutoTriagem = new HashSet<ProdutoTriagem>();
        }
    
        public long ID { get; set; }
        public string NrProcesso { get; set; }
        public System.DateTime DataHoraRecepcao { get; set; }
        public long ColaboradorID { get; set; }
        public Nullable<long> NIF { get; set; }
        public Nullable<long> CodPostalID { get; set; }
        public string NomeMorada { get; set; }
        public string Localidade { get; set; }
        public string NrGuiaNotaDevol { get; set; }
        public Nullable<System.DateTime> DataGuia { get; set; }
        public bool SubUnidades { get; set; }
        public Nullable<long> DetalheRecepcaoId { get; set; }
    
        public virtual CodigoPostal CodigoPostal { get; set; }
        public virtual Morada Morada { get; set; }
        public virtual User User { get; set; }
        public virtual DetalheRecepcao DetalheRecepcao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdutoTriagem> ProdutoTriagem { get; set; }
    }
}
