using BlogBLL.ViewModels.User;
using BlogDAL.Models;

namespace BlogBLL.Ext;

public class UserExtentions
{
    public List<ApiUserViewModel> GetUserViewModels(IEnumerable<User> users)
    {
        var listUser = new List<ApiUserViewModel>();
        foreach (var user in users)
        {
            listUser.Add(new ApiUserViewModel
            {
                Id = user.Id,
                About = user.About!,
                Email = user.Email!,
                Status = user.Status!,
                BirthDate = user.BirthDate,
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                MiddleName = user.LastName!,
                PhoneNumber = user.PhoneNumber!,
                UserName = user.UserName!
            });
        }

        return listUser;
    }
}