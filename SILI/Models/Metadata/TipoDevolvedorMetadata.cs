using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(TipoDevolvedorMetadata))]
    public partial class TipoDevolvedor
    {
        public static bool IsUnique(TipoDevolvedor obj)
        {
            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                return ent.TipoDevolvedor.Where(x => x.ID != obj.ID && x.Numero == obj.Numero).Count() == 0;
            }
        }
    }

    public class TipoDevolvedorMetadata
    {
        [Display(Name="Nr.")]
        public long Numero;

        [Display(Name = "Tipo de Devolvedor")]
        public string Descricao;
                
    }

    
}