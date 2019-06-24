using PayPlusModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PayPlusModels.CustomAuthentication
{
    public class CustomMembershipUser : MembershipUser
    {
        #region User Properties  

        public int UserId { get; set; }
        public string User_Name { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public byte[] Image { get; set; }
        public ICollection<Role> Roles { get; set; }

        #endregion

        public CustomMembershipUser(UserSession user) : base("CustomMembership", user.USERNAME, user.USER_ID, user.EMAIL, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.USER_ID;
            User_Name = user.USERNAME;
            Roles = user.Roles;
            Name = user.NAME;
            CustomerId = user.CUSTOMER_ID;
            Email = user.EMAIL;
            Image = user.IMAGE;
        }
    }
}