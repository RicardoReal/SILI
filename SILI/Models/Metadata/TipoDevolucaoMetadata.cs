using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(TipoDevolucaoMetadata))]
    public partial class TipoDevolucao
    {
        public static bool IsUnique(TipoDevolucao obj)
        {
            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                return ent.TipoDevolucao.Where(x => x.ID != obj.ID && x.Numero == obj.Numero).Count() == 0;
            }
        }
    }

    public class TipoDevolucaoMetadata
    {
        [Display(Name="Nr.")]
        public long Numero;

        [Display(Name = "Tipo de Devolução")]
        public string Descricao;

        [Display(Name = "Disponível")]
        public string Disponivel;

    }

    
}