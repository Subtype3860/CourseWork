using BlogBLL.ViewModels.User;
using BlogDAL.Models;

namespace BlogBLL.Ext
{
    public static class UserFromModel
    {
        public static User Convert(this User user, UserEditViewModel usereditvm)
        {
            user.LastName = usereditvm.LastName;
            user.MiddleName = usereditvm.MiddleName;
            user.FirstName = usereditvm.FirstName;
            user.Email = usereditvm.Email;
            user.BirthDate = usereditvm.BirthDate;
            user.UserName = usereditvm.UserName;
            user.Status = usereditvm.Status;
            user.About = usereditvm.About;
            user.PhoneNumber = usereditvm.PhoneNumber;
            return user;
        }
    }
}
