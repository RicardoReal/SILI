using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SILI
{
    public class UserManager
    {
        public bool IsValid(string username, string password)
        {
            using (SILI_DBEntities ent = new SILI_DBEntities())
            {
                // if your users set name is Users
                return ent.User.Any(u => u.UserName == username && u.Password == password);
            }
        }
    }
}