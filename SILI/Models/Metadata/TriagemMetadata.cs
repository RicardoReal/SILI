using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(TriagemMetadata))]
    public partial class Triagem
    {
        public string GenerateNrTriagem()
        {
            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                //string auxDate = DateTime.Now.ToString("yyyyMMdd");

                //int seq = ent.Triagem.Where(r => r.NrTriagem.StartsWith(auxDate)).Count() + 1;

                //if (seq < 10) return auxDate + "00" + seq;
                //else if (seq < 100) return auxDate + "0" + seq;

                //return auxDate + seq;
                return "";
            }
        }
    }

    public class TriagemMetadata
    {
        [Display(Name= "Nr. Processo")]
        public long NrProcesso;

        [Display(Name = "Data/Hora")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime DataHoraRecepcao;

        [Editable(false)]
        public User ColaboradorID;



    }

    
}