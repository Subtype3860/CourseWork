using AutoMapper;
using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.User;
using BlogDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BlogBLL.Ext;

namespace BlogApi.Controllers;

/// <summary>
/// Работа с пользователями
/// </summary>
[ApiController]
[Route("[controller]")]
public class UserApiController : ControllerBase
{
    #region Designer

    private UserManager<User> _userManager;
    private IMapper _mapper;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userManager">UserManager</param>
    /// <param name="mapper">mapper</param>
    public UserApiController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    #endregion

    #region GetAllUser

    /// <summary>
    /// Получаем всех пользователей
    /// </summary>
    /// <returns>List User</returns>
    [HttpGet]
    [Route("/GetAllUser")]
    public List<ApiUserViewModel> GetAllUser()
    {
        var users = _userManager.Users.ToList();
        return new UserExtentions().GetUserViewModels(users);
    }

    #endregion

    #region GetUserById

    /// <summary>
    /// Получить пользователя по ID
    /// </summary>
    /// <param name="id">ID пользователя</param>
    /// <returns>ApiUserViewModel</returns>
    [HttpGet]
    [Route("/GetUserById/{id}")]
    public ApiUserViewModel GetUserById(string id)
    {
        var user = _userManager.FindByIdAsync(id);
        var model = _mapper.Map<ApiUserViewModel>(user.Result);
        return model;
    }

    #endregion

    #region AddUser

    [HttpPost]
    [Route("/AddUser")]
    public IActionResult AddUser(User user)

    #endregion

}