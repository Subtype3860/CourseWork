using AutoMapper;
using BlogBLL.Ext;
using BlogBLL.ViewModels.Role;
using BlogDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

/// <summary>
/// Контроллер Roles
/// </summary>
[ApiController]
[Route("[controller]")]
public class RolesApiController : ControllerBase
{
    #region Designer

    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор RolesApiController
    /// </summary>
    /// <param name="roleManager">RoleManager AppRole</param>
    /// <param name="mapper"></param>
    public RolesApiController(RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    #endregion
    
    #region GetAllRoles

    /// <summary>
    /// Получение всех ролей 
    /// </summary>
    /// <returns>List ApiGetAllRolesViewModel</returns>
    [HttpGet]
    [Route("/GetAllRoles")]
    public List<ApiGetAllRolesViewModel> GetAllRoles()
    {
        var roles = _roleManager.Roles;
        return new RolesExtentions().GetAll(roles);
    }

    #endregion

    #region GetRolesById

    /// <summary>
    /// Получение роли по ID
    /// </summary>
    /// <param name="id">ID Roles</param>
    /// <returns></returns>
    [HttpGet]
    [Route("/GetRolesById/{id}")]
    public async Task<ApiGetRolesByIdViewModel> GetRolesById(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        var model = _mapper.Map<ApiGetRolesByIdViewModel>(role);
        return model;
    }

    #endregion

    #region AddRoles

    /// <summary>
    /// Добавление новой роли
    /// </summary>
    /// <param name="apiAddRoleViewModel">ViewModel ApiAddRoleViewModel</param>
    /// <returns>StatusCodeResult</returns>
    [HttpPost]
    [Route("/AddRoles")]
    public async Task<StatusCodeResult> AddRoles(ApiAddRoleViewModel apiAddRoleViewModel)
    {
        var model = _mapper.Map<AppRole>(apiAddRoleViewModel);
        var result = await _roleManager.CreateAsync(model);
        if (result.Succeeded)
        {
            return Ok();
        }

        return StatusCode(500);
    }

    #endregion
}