using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace PayPadAdministrator.CustomAuthentication
{
    public class CustomPrincipal : IPrincipal
    {
        #region Identity Properties  

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string[] Roles { get; set; }

        #endregion

        public IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            var rol = Roles.Where(r => r.ToUpper().Equals(role.ToUpper())).FirstOrDefault();
            if (rol != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);                    
        }
    }
}