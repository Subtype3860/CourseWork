using Microsoft.AspNetCore.Identity;

namespace BlogDAL.Models
{
    public class AppRole : IdentityRole
    {
        public string? About { get; set; }
    }
}
