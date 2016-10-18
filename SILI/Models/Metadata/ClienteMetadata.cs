using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(ClienteMetadata))]
    public partial class Cliente
    {
        public override string ToString()
        {
            return this.NrInterno + " - " + this.Nome;
        }

        public string FormattedToString
        {
            get
            {
                return this.ToString();
            }
        }
    }

    public class ClienteMetadata
    {
        [Display(Name = "Nr. Interno")]
        public int NrInterno;

        [Display(Name = "Nr. Contacto")]
        public int NrContacto;

        [EmailAddress]
        public bool Email;

        [Display(Name = "Nome de Contacto")]
        public string NomeContacto;
    }
}