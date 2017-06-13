using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(RecepcaoMetadata))]
    public partial class Recepcao
    {
        public string GenerateNrRecepcao()
        {
            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                string auxDate = DateTime.Now.ToString("yyyyMMdd");

                int seq = ent.Recepcao.Where(r => r.NrRecepcao.StartsWith(auxDate)).Count() + 1;

                if (seq < 10) return auxDate + "00" + seq;
                else if (seq < 100) return auxDate + "0" + seq;
            
                return auxDate + seq;
            }
        }
    }

    public class RecepcaoMetadata
    {
        [Display(Name="Nr. Recepção")]
        public long NrRecepcao;

        [Display(Name = "Data/Hora")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime DataHora;

        [Display(Name = "Data Chegada Armz.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public long DataChegadaArmazem;

        [Display(Name = "Hora Chegada Armz.")]
        [DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm}")]
        public long HoraChegadaArmazem;

        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observacoes;

        [Required]
        [Display(Name = "Nr. Volumes Recepcionados")]
        public string NrVolumesRecepcionados;

        public User Colaborador;

    }

    
}