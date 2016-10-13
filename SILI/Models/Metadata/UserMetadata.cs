using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User { }

    public class UserMetadata
    {
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