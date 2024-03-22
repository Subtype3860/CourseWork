using BlogBLL.UnitOfWork;
using BlogDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Controllers;

/// <summary>
/// Работа с пользователями
/// </summary>
public class UserApiController : ControllerBase
{
    #region Designer

    private UserManager<User> _userManager;
    private IUnitOfWork _unitOfWork;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userManager">UserManager</param>
    /// <param name="unitOfWork">IUnitOfWork</param>
    public UserApiController(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    #endregion
    
    
    public IActionResult All()
    {
        var users = _userManager.Users.ToList();
        return Ok();
    }
}