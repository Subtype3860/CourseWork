using Microsoft.AspNetCore.Identity;

namespace BlogBLL.ViewModels.Role
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRole { get; set; }

        public ChangeRoleViewModel(string userId, string userEmail, List<IdentityRole> allRoles, IList<string> userRole)
        {
            UserId = userId;
            UserEmail = userEmail;
            AllRoles = allRoles;
            UserRole = userRole;
        }
    }
}