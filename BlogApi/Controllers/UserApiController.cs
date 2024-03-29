using AutoMapper;
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

    /// <summary>
    /// Добавление пользователя
    /// </summary>
    /// <param name="userApi">Модель User</param>
    /// <returns></returns>
    [HttpPost]
    [Route("/AddUser")]
    public async Task<IActionResult> AddUser(ApiAddUserViewModel userApi)
    {
        if (!ModelState.IsValid) return StatusCode(400);
        var userFind = await _userManager.FindByEmailAsync(userApi.Email);
        if (userFind != default) return StatusCode(500);
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = userApi.Email,
            About = userApi.About,
            FirstName = userApi.FirstName,
            LastName = userApi.LastName,
            MiddleName = userApi.MiddleName,
            BirthDate = userApi.BirthDate,
            UserName = userApi.UserName,
            PhoneNumber = userApi.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, userApi.Password);
        if (result.Succeeded)
        {
            return Ok();
        }

        return StatusCode(500);
    }

    #endregion

    #region UpdateUser

    /// <summary>
    /// Обновление данных пользователя
    /// </summary>
    /// <param name="model">ApiUpdateUserViewModel</param>
    /// <returns></returns>
    [HttpPut]
    [Route("/UpdateUser")]
    public async Task<IActionResult> UpdateUser(ApiUpdateUserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (!string.IsNullOrEmpty(model.Password) && user != null)
        {
            var passwordValidator =
                HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as
                    IPasswordValidator<User>;
            var passwordHasher =
                HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

            await passwordValidator!.ValidateAsync(_userManager, user, model.Password);
            user.PasswordHash = passwordHasher!.HashPassword(user, model.Password);
            user.ConvertApi(model);
        }
        var result = await _userManager.UpdateAsync(user!);
        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode(500);
    }

    #endregion

    #region DeleteUser

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="id">ID пользователя</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("/DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == default) return StatusCode(500);
        await _userManager.DeleteAsync(user);
        return Ok();
    }

    #endregion

}