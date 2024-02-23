using BlogBLL.ViewModels.Role;
using BlogDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPLL.Controllers
{
    [Authorize(Roles= "admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        // GET
        public IActionResult Index() => View(_roleManager.Roles.ToList());
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name)) return View(name);
            var result = await _roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null) await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
        public IActionResult UserList() => View(_userManager.Users.ToList());

		public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                var allRole =  _roleManager.Roles.ToList();
                var model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRole = userRole,
                    AllRoles = allRole
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
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

                return RedirectToAction("UserList");
            }

            return NotFound();

        }

    }
}