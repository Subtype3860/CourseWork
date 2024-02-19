using BlogDAL.Models;

namespace BlogPLL.ViewModels.Account
{
    public class UserWithFriendExt: User
    {
        public bool IsFriendWithCurrent { get; set; }
    }
}
