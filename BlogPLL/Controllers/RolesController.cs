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
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ILogger<RolesController> _logger;
        public RolesController(RoleManager<AppRole> roleManager, ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
            logger.LogInformation("Roles");
        }
        // GET
        public IActionResult Index() => View(_roleManager.Roles.ToList());
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AppRole role)
        {
            if (string.IsNullOrEmpty(role.Name)) return View(role.Name);
            var result = await _roleManager.CreateAsync(new AppRole{Name = role.Name, About = role.About});
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditRoles(EditAppRoleViewModel model) => View(model);

        [HttpPost]
        public async Task<IActionResult> RolesEdit(EditAppRoleViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            await _roleManager.CreateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null) await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

    }
}