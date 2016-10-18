using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(TratamentoMetadata))]
    public partial class Tratamento
    {
        public static bool IsUnique(Tratamento obj)
        {
            using(SILI_DBEntities ent = new SILI_DBEntities())
            {
                return ent.Tratamento.Where(x => x.ID != obj.ID && x.Numero == obj.Numero).Count() == 0;
            }
        }
    }

    public class TratamentoMetadata
    {
        [Display(Name="Nr.")]
        public long Numero;

        [Display(Name = "Tratamento")]
        public string Descricao;
                
    }

    
}