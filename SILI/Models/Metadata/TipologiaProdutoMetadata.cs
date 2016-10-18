using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(TipologiaProdutoMetadata))]
    public partial class Tipologia
    {
        public static bool IsUnique(Tipologia obj)
        {
            using(SILI_DBEntities ent = new SILI_DBEntities())
            {
                return ent.Tipologia.Where(x => x.ID != obj.ID && x.Numero == obj.Numero).Count() == 0;
            }
        }
    }

    public class TipologiaProdutoMetadata
    {
        [Display(Name="Nr.")]
        public long Numero;

        [Display(Name = "Tipologia de Produto")]
        public string Descricao;
                
    }

    
}