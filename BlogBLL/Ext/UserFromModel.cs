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

        public static User ConvertApi(this User user, ApiUpdateUserViewModel model)
        {
            user.Email = string.IsNullOrEmpty(model.Email) ? user.Email : model.Email;
            user.UserName = string.IsNullOrEmpty(model.UserName) ? user.UserName : model.UserName;
            user.FirstName = string.IsNullOrEmpty(model.FirstName) ? user.FirstName : model.FirstName;
            user.LastName = string.IsNullOrEmpty(model.LastName) ? user.LastName : model.LastName;
            user.MiddleName = string.IsNullOrEmpty(model.MiddleName) ? user.MiddleName : model.MiddleName;
            user.BirthDate = model.BirthDate ?? user.BirthDate;
            user.About = string.IsNullOrEmpty(model.About) ? user.About : model.About;
            user.PhoneNumber = string.IsNullOrEmpty(model.PhoneNumber) ? user.PhoneNumber : model.PhoneNumber;
            user.Status = string.IsNullOrEmpty(model.Status) ? user.Status : model.Status;
            return user;
        }
    }
}
