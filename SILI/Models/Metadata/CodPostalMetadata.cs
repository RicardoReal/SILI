using SILI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(CodPostalMetadata))]
    public partial class CodigoPostal
    {
        public override string ToString()
        {
            return this.CodPostal + " " + this.Localidade;
        }

        public string FormattedToString
        {
            get
            {
                return this.ToString();
            }
        }

        public static List<Autocomplete> GetCodPostais(string prefix)
        {
            List<Autocomplete> codPostais = new List<Autocomplete>();

            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                var results = (from c in ent.CodigoPostal
                               where c.CodPostal.ToString().Contains(prefix)
                               orderby c.CodPostal
                               select c).Take(10).ToList();

                foreach (var r in results)
                {
                    Autocomplete codPostal = new Autocomplete();

                    codPostal.Name = r.CodPostal;
                    codPostal.Id = (int)r.ID;
                    codPostais.Add(codPostal);
                }
            }

            return codPostais;
        }
    }

    public class CodPostalMetadata
    {
        [Display(Name="Cód. Postal")]
        [RegularExpression(@"\d{4}-\d{3}",ErrorMessage ="Cod. Postal com formato inválido.")]
        public string CodPostal;

        
                
    }

    
}