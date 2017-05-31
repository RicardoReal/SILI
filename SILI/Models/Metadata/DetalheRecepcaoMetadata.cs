using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(DetalheRecepcaoMetadata))]
    public partial class DetalheRecepcao
    {
        public static string GenerateNrDetalheRecepcao(long RecepcaoID)
        {
            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                string nrRecepcao = ent.Recepcao.Where(r => r.ID == RecepcaoID).FirstOrDefault().NrRecepcao;

                DetalheRecepcao dr = ent.DetalheRecepcao.Where(r => r.NrDetalhe.StartsWith(nrRecepcao)).OrderByDescending(r => r.NrDetalhe).FirstOrDefault();
                int seq = 1;
                if(dr != null)
                {
                    string aux = dr.NrDetalhe;
                    seq = int.Parse(dr.NrDetalhe.Substring(11)) + 1;

                }
                
                if (seq < 10) return nrRecepcao + "00" + seq;
                else if (seq < 100) return nrRecepcao + "0" + seq;

                return nrRecepcao + seq;
            }
        }
    }

    public class DetalheRecepcaoMetadata
    {
        [Display(Name = "Nr. Detalhe")]
        public DateTime NrDetalhe;

        [Display(Name = "Nr. Volumes")]
        public long NrVolumes;

        [Display(Name = "N/ Referência")]
        public long NReferencia;

        [Display(Name = "Nr Guia Transporte")]
        public long NrGuiaTransporte;
        
    }

    
}