using AutoMapper;
using BlogBLL.Ext;
using BlogBLL.ViewModels.Account;
using BlogDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers;

public class AccountManagerController : Controller
{
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public AccountManagerController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IMapper mapper, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register() => View();
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new User
            {
                Email = model.EmailReg, 
                UserName = model.EmailReg
            };
            // добавляем пользователя											  
            var result = await _userManager.CreateAsync(user, model.EmailReg!);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //Пользователь, роль "user"
                var userId = await _userManager.FindByEmailAsync(user.Email!);
                //создаём роль
                await _roleManager.CreateAsync(new IdentityRole("user"));
                //присваеваем роль "user" пользователю
                await AddRoleUser(userId!.Id, new List<string> { "user" });
                return RedirectToAction("Login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string? userId, string? code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if(result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View("Error");
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl = null) => View(new LoginViewModel {ReturnUrl = returnUrl});
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
        var user = await _userManager.FindByNameAsync(model.Email!);
        if (user != null)
        {
            // проверяем, подтвержден ли email
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                return View(model);
            }
        }
        var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RemmberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            return View(model);
        }
        [Authorize]
        [Route("MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPage()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);
            var model = new UserViewModel(result!);
            return View("User", model);
        }
        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit()
        {
            var user = User;

            var result = _userManager.GetUserAsync(user).Result;

            var model = _mapper.Map<EditViewModel>(result);

            return View("Edit", model);
        }

        [Authorize]
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update(EditViewModel evm)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<User>(evm);
                var user = await _userManager.FindByIdAsync(model.Id);
                var result = await _userManager.UpdateAsync(user!);
                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
                }
                return RedirectToAction("Edit", "AccountManager");
            }
            ModelState.AddModelError("", "Некорректные данные");
            return View("Edit", evm);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id);
            var mode = _mapper.Map<User>(user);
            await _userManager.DeleteAsync(mode);
            return RedirectToAction("Index", "Home");
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