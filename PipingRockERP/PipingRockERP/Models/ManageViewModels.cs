using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace PipingRockERP.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class UsersAndRolesModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class UsersRolesModels
    {
        public IEnumerable<UsersAndRolesModel> UsersAndRolesModels { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}