using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(MotivoDevolucaoMetadata))]
    public partial class MotivoDevolucao
    {
        public static bool IsUnique(MotivoDevolucao obj)
        {
            using(SILI_DBEntities ent = new SILI_DBEntities())
            {
                return ent.MotivoDevolucao.Where(x => x.ID != obj.ID && x.Numero == obj.Numero).Count() == 0;
            }
        }
    }

    public class MotivoDevolucaoMetadata
    {
        [Display(Name="Nr.")]
        public long Numero;

        [Display(Name = "Motivo")]
        public string Motivos;
                
    }

    
}