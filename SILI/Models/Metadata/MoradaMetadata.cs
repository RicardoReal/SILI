using SILI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(MoradaMetadata))]
    public partial class Morada
    {
        public override string ToString()
        {
            return this.NIF + " - " + this.Nome;
        }

        public string FormattedToString
        {
            get
            {
                return this.ToString();
            }
        }

        public static List<Autocomplete> GetMoradas(string prefix)
        {
            List<Autocomplete> moradas = new List<Autocomplete>();

            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                var results = (from m in ent.Morada
                               where m.NIF.ToString().Contains(prefix) || m.Nome.Contains(prefix)
                               orderby m.NIF
                               select m).Take(10).ToList();

                foreach (var r in results)
                {
                    Autocomplete morada = new Autocomplete();

                    morada.Name = r.FormattedToString;
                    morada.Id = (int)r.ID;
                    moradas.Add(morada);
                }
            }

            return moradas;
        }
    }

    public class MoradaMetadata
    {
        [Required()]
        public long NIF;

        [MaxLength(100)]
        public string Nome;

        [Display(Name = "Cód. Postal")]
        public long CodPostalID;

        [Display(Name = "Cód. Postal")]
        public CodigoPostal CodigoPostal;

        [MaxLength(200)]
        [Display(Name = "Morada")]
        public string Morada1;

        public int? Telefone;

        [Display(Name = "Nome de Contacto")]
        public string NomeContacto;

        [Display(Name = "Tipo de Devolvedor")]
        public long TipoDevolvedorID;
    }

    
}