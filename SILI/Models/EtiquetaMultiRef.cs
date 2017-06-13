using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SILI.Models
{
    public class EtiquetaMultiRef
    {
        public string NrDetalhe { get; set; }
        public string Localizacao { get; set; }
        public long TratamentoID { get; set; }
        public Tratamento Tratamento { get; set; }
        public long TipologiaID { get; set; }
        public Tipologia Tipologia { get; set; }
    }
}