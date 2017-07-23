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

    public class ListagemTriagem
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
        public string NrProcesso { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public DateTime HoraFimTriagem { get; set; }
        public long? NIF { get; set; }
        public string CodPostal { get; set; }
        public string Nome { get; set; }
        public DateTime? DataGuia { get; set; }
        public int? NrGuia_NotaDevolucao { get; set; }
        public bool SubUnidades { get; set; }
        public string EAN { get; set; }
        public string Ref { get; set; }
        public string CodSecundario { get; set; }
        public string Descricao { get; set; }
        public int QtdDevolvida { get; set; }
        public string Lote { get; set; }
        public DateTime? Validade { get; set; }
        public decimal? Preco { get; set; }
        public string Tratamento { get; set; }
        public string Localizacao { get; set; }
        public string MotivoDevolucao { get; set; }
        public string Tipoogia { get; set; }
        public string Obs { get; set; }
    }
}
