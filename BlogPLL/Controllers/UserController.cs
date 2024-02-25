using AutoMapper;
using BlogBLL.ViewModels.Account;
using BlogDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var  user = new User{Email = model.Email, UserName = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<UserEditViewModel>(user);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId!);
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
            if (user!=null)
            {
                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
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

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}