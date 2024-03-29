using BlogBLL.ViewModels.Role;
using BlogDAL.Models;

namespace BlogBLL.Ext;

public class RolesExtentions
{
    public List<ApiGetAllRolesViewModel> GetAll(IQueryable<AppRole> roles)
    {
        var listRolesApi = new List<ApiGetAllRolesViewModel>();
        foreach (var role in roles)
        {
            listRolesApi.Add(new ApiGetAllRolesViewModel
            {
                Id = role.Id,
                Name = role.Name,
                About = role.About
            });
        }

        return listRolesApi;
    }
}