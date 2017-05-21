using SILI.Models;
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

        public static List<Autocomplete> GetClientes(string prefix)
        {
            List<Autocomplete> clientes = new List<Autocomplete>();

            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                var results = (from c in ent.Cliente
                               where c.Nome.ToString().Contains(prefix)
                               orderby c.Nome
                               select c).Take(10).ToList();

                foreach (var r in results)
                {
                    Autocomplete cliente = new Autocomplete();

                    cliente.Name = r.Nome;
                    cliente.Id = (int)r.ID;
                    clientes.Add(cliente);
                }
            }

            return clientes;
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