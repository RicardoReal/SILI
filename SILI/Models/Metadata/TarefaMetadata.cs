using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(TarefaMetadata))]
    public partial class Tarefa
    {
        public static bool IsUnique(Tarefa obj)
        {
            using(SILI_DBEntities ent = new SILI_DBEntities())
            {
                return ent.Tarefa.Where(x => x.ID != obj.ID && x.Numero == obj.Numero).Count() == 0;
            }
        }
    }

    public class TarefaMetadata
    {
        [Display(Name="Nr.")]
        public long Numero;

        [Display(Name = "Tarefa")]
        public string Descricao;
                
    }

    
}