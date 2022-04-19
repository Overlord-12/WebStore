using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.APIAdresses;
using WebStore.Domain.Entities.Identity;

namespace WebStore.WebApi.Controllers.Identity
{
    [ApiController]
    [Route(WebAPIAdresses.V1.Identity.Roles)]
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _Logger;
        private readonly RoleStore<Role> _RoleStore;

        public RoleController(WebStoreDB db, ILogger<RoleController> Logger)
        {
            _Logger = Logger;
            _RoleStore = new(db);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAll() => await _RoleStore.Roles.ToArrayAsync();

        [HttpPost]
        public async Task<bool> CreateAsync(Role role)
        {
            var creation_result = await _RoleStore.CreateAsync(role);

            if (!creation_result.Succeeded)
                _Logger.LogWarning("Ошибка создания роли {0}:{1}",
                    role,
                    string.Join(", ", creation_result.Errors.Select(e => e.Description)));

            return creation_result.Succeeded;
        }

        [HttpPut]
        public async Task<bool> UpdateAsync(Role role)
        {
            var uprate_result = await _RoleStore.UpdateAsync(role);

            if (!uprate_result.Succeeded)
                _Logger.LogWarning("Ошибка изменения роли {0}:{1}",
                    role,
                    string.Join(", ", uprate_result.Errors.Select(e => e.Description)));

            return uprate_result.Succeeded;
        }

        [HttpDelete]
        [HttpPost("Delete")]
        public async Task<bool> DeleteAsync(Role role)
        {
            var delete_result = await _RoleStore.DeleteAsync(role);

            if (!delete_result.Succeeded)
                _Logger.LogWarning("Ошибка удаления роли {0}:{1}",
                    role,
                    string.Join(", ", delete_result.Errors.Select(e => e.Description)));

            return delete_result.Succeeded;
        }

        [HttpPost("GetRoleId")]
        public async Task<string> GetRoleIdAsync([FromBody] Role role) => await _RoleStore.GetRoleIdAsync(role);

        [HttpPost("GetRoleName")]
        public async Task<string> GetRoleNameAsync([FromBody] Role role) => await _RoleStore.GetRoleNameAsync(role);

        [HttpPost("SetRoleName/{name}")]
        public async Task<string> SetRoleNameAsync(Role role, string name)
        {
            await _RoleStore.SetRoleNameAsync(role, name);
            await _RoleStore.UpdateAsync(role);
            return role.Name;
        }

        [HttpPost("GetNormalizedRoleName")]
        public async Task<string> GetNormalizedRoleNameAsync(Role role) => await _RoleStore.GetNormalizedRoleNameAsync(role);

        [HttpPost("SetNormalizedRoleName/{name}")]
        public async Task<string> SetNormalizedRoleNameAsync(Role role, string name)
        {
            await _RoleStore.SetNormalizedRoleNameAsync(role, name);
            await _RoleStore.UpdateAsync(role);
            return role.NormalizedName;
        }

        [HttpGet("FindById/{id}")]
        public async Task<Role> FindByIdAsync(string id) => await _RoleStore.FindByIdAsync(id);

        [HttpGet("FindByName/{name}")]
        public async Task<Role> FindByNameAsync(string name) => await _RoleStore.FindByNameAsync(name);
    }
}
