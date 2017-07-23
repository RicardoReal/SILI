using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SILI.Models
{
    public class ListagemRecepcao
    {
        public string NrRecepcao { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime HoraFim { get; set; }
        public string Colaborador { get; set; }
        public DateTime DataChegadaArmz { get; set; }
        public TimeSpan HoraChegadaArmz { get; set; }
        public string EntreguePor { get; set; }
        public string Observacoes { get; set; }
        public int NrVolumesRecepcionados { get; set; }
        public bool DCR { get; set; }
        public string NrDetalhe { get; set; }
        public long NrCliente { get; set; }
        public string Cliente { get; set; }
        public int NrVolumes { get; set; }
        public string TipoDevolucao { get; set; }
        public string NReferencia { get; set; }
        public int? NrGuiaTransporte { get; set; }
        public string Devolvedor { get; set; }
    }
}
