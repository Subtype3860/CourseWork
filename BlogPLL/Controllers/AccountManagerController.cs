using AutoMapper;
using BlogBLL.Ext;
using BlogBLL.ViewModels.Account;
using BlogBLL.ViewModels.User;
using BlogDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace BlogPLL.Controllers;

public class AccountManagerController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountManagerController> _logger;
    public AccountManagerController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMapper mapper,
        RoleManager<AppRole> roleManager, ILogger<AccountManagerController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _logger = logger;
        logger.LogInformation("AccountManagerController");
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = new User { Email = model.Email, UserName = model.Email };
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, model.Password!);
            if (result.Succeeded)
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);

                //Пользователь, роль "user"
                var userId = await _userManager.FindByEmailAsync(user.Email!);
                //создаём роль
                await _roleManager.CreateAsync(new AppRole { Name = "user" });
                //присваеваем роль "user" пользователю
                await AddRoleUser(userId!.Id, new List<string> { "user" });

                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        var listUser = _userManager.Users.Where(x => string.Equals(x.UserName!.ToLower(), "admin"));
        if (listUser.ToList().Count == 0)
        {
            var user = new User { UserName = "admin", Email = "admin@admin.ru" };
            var result = await _userManager.CreateAsync(user, "admin_pass");
            if (result.Succeeded)
            {
                var listRole = new[] { "admin", "user", "moderator" };
                foreach (var s in listRole)
                {
                    await _roleManager.CreateAsync(new AppRole { Name = s });
                }

                var admin = await _userManager.FindByEmailAsync(user.Email);
                await AddRoleUser(admin!.Id, new List<string> { "admin" });
            }
        }


        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result =
                await _signInManager.PasswordSignInAsync(model.UserName!, model.Password!, model.RememberMe, false);
            if (result.Succeeded)
            {
                // проверяем, принадлежит ли URL приложению
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
        }
        return View(model);
    }

    [Authorize]
    [Route("MyPage")]
    [HttpGet]
    public async Task<IActionResult> MyPage()
    {
        var user = User;
        var result = await _userManager.GetUserAsync(user);
        var model = _mapper.Map<UserEditViewModel>(result);
        return View(model);
    }

    [Authorize]
    [Route("Update")]
    [HttpPost]
    public async Task<IActionResult> Update(UserEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.UserId!);
            user!.Convert(model);
            if (!string.IsNullOrEmpty(model.Password))
            {
                var passwordValidator =
                    HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as
                        IPasswordValidator<User>;
                var passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                await passwordValidator!.ValidateAsync(_userManager, user!, model.Password);
                user!.PasswordHash = passwordHasher!.HashPassword(user, model.Password!);
            }
            var result = await _userManager.UpdateAsync(user!);
            if (result.Succeeded)
            {
                return RedirectToAction("MyPage", "AccountManager");
            }
            return RedirectToAction("Update", "AccountManager");

        }
        ModelState.AddModelError("", "Некорректные данные");
        return View("MyPage", model);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var user = _userManager.FindByIdAsync(id).Result;
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("MyPage");
    }
    public List<User> GetAllUser() => _userManager.Users.ToList();
    public User GetUserById(string id) => _userManager.FindByIdAsync(id).Result!;

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // удаляем аутентификационные куки
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    [Authorize(Roles = "admin")]
    public async Task AddRoleUser(string userId, List<string> roles)
    {
        // получаем пользователя
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // получем список ролей пользователя
            var userRoles = await _userManager.GetRolesAsync(user);
            // получаем список ролей, которые были добавлены
            var addedRoles = roles.Except(userRoles);
            // получаем роли, которые были удалены
            var removedRoles = userRoles.Except(roles);
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
        }

    }
}