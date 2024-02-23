using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BlogBLL.ViewModels.Role
{
    public class ChangeRoleViewModel
    {
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRole { get; set; }

        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRole = new List<string>();
        }
    }
}