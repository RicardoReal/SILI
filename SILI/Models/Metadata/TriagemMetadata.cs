using SILI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(TriagemMetadata))]
    public partial class Triagem
    {
        public string GenerateNrTriagem(Cliente cliente)
        {
            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                string nrCliente = ent.Cliente.Where(cl => cl.ID == cliente.ID).FirstOrDefault().NrInterno.ToString();

                string year = DateTime.Now.Year.ToString();

                string month = "";
                if (DateTime.Now.Month < 10) month = "0" + DateTime.Now.Month.ToString();
                else month = DateTime.Now.Month.ToString();

                string day = "";
                if (DateTime.Now.Day < 10) day = "0" + DateTime.Now.Day.ToString();
                else day = DateTime.Now.Day.ToString();

                string date = year + month + day;

                int ct = ent.Triagem.Where(tr => tr.NrProcesso.Contains(nrCliente + "-" + date)).Count() + 1;

                string nrSeq = "";

                if (ct < 10) { nrSeq = "00" + ct; }
                else if (ct < 100) { nrSeq = "0" + ct; }
                else { nrSeq = ct.ToString(); }

                return nrCliente + "-" + date + "-" + nrSeq;
            }
        }

        
    }

    public class TriagemMetadata
    {
        [Display(Name = "Nr. Processo")]
        public long NrProcesso;

        [Display(Name = "Data/Hora")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime DataHoraRecepcao;

        [Editable(false)]
        [Display(Name = "Colaborador")]
        public User ColaboradorID;

        [Display(Name = "Nome")]
        public string NomeMorada;

        [Display(Name = "Nr. Guia / Nota Devolução")]
        public string NrGuiaNotaDevol;

        [Display(Name = "Data Guia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataGuia;

        [Display(Name = "Sub Unidades")]
        public string SubUnidades;

    }


}