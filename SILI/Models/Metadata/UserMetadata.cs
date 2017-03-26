using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SILI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        public string FormattedToString
        {
            get { return this.ToString(); }
        }

        public static long GetUserIdByUsername(string Username)
        {
            using(SILI_DBEntities ent = new SILI_DBEntities())
            {
                return ent.User.Where(u => u.UserName == Username).FirstOrDefault().ID;
            }
        }
    }

    public class UserMetadata
    {

        [Required]
        public string UserName;

        [Display(Name="First Name")]
        public string FirstName;

        [Display(Name = "Last Name")]
        public string LastName;

        [DataType(DataType.Password)]
        public string Password;

        [Display(Name = "Last Login")]
        public DateTime LastLogin;

        [Display(Name = "Is Active")]
        public bool IsActive;
    }

    
}