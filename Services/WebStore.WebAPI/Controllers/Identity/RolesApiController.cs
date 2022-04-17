using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity
{
    [ApiController]
    [Route(WebAPIAddresses.V1.Identity.Roles)]
    public class RolesApiController : ControllerBase
    {
        private readonly ILogger<RolesApiController> logger;
        private readonly RoleStore<Role> roleStore;

        public RolesApiController(WebStoreDB db, ILogger<RolesApiController> logger)
        {
            roleStore = new RoleStore<Role>(db);
            this.logger = logger;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAll()
        {
            return await roleStore.Roles.ToArrayAsync();
        }
        /* ------------------------------------------------------------ */

        [HttpPost]
        public async Task<bool> CreateAsync(Role role)
        {
            var creation_result = await roleStore.CreateAsync(role);

            if (!creation_result.Succeeded)
                logger.LogWarning("Ошибка создания роли {0}:{1}",
                    role,
                    string.Join(", ", creation_result.Errors.Select(e => e.Description)));

            return creation_result.Succeeded;
        }

        [HttpPut]
        public async Task<bool> UpdateAsync(Role role)
        {
            var uprate_result = await roleStore.UpdateAsync(role);

            if (!uprate_result.Succeeded)
                logger.LogWarning("Ошибка изменения роли {0}:{1}",
                    role,
                    string.Join(", ", uprate_result.Errors.Select(e => e.Description)));

            return uprate_result.Succeeded;
        }

        [HttpDelete]
        [HttpPost("Delete")]
        public async Task<bool> DeleteAsync(Role role)
        {
            var delete_result = await roleStore.DeleteAsync(role);

            if (!delete_result.Succeeded)
                logger.LogWarning("Ошибка удаления роли {0}:{1}",
                    role,
                    string.Join(", ", delete_result.Errors.Select(e => e.Description)));

            return delete_result.Succeeded;
        }

        [HttpPost("GetRoleId")]
        public async Task<string> GetRoleIdAsync([FromBody] Role role) => await roleStore.GetRoleIdAsync(role);

        [HttpPost("GetRoleName")]
        public async Task<string> GetRoleNameAsync([FromBody] Role role) => await roleStore.GetRoleNameAsync(role);

        [HttpPost("SetRoleName/{name}")]
        public async Task<string> SetRoleNameAsync(Role role, string name)
        {
            await roleStore.SetRoleNameAsync(role, name);
            await roleStore.UpdateAsync(role);
            return role.Name;
        }

        [HttpPost("GetNormalizedRoleName")]
        public async Task<string> GetNormalizedRoleNameAsync(Role role) => await roleStore.GetNormalizedRoleNameAsync(role);

        [HttpPost("SetNormalizedRoleName/{name}")]
        public async Task<string> SetNormalizedRoleNameAsync(Role role, string name)
        {
            await roleStore.SetNormalizedRoleNameAsync(role, name);
            await roleStore.UpdateAsync(role);
            return role.NormalizedName;
        }

        [HttpGet("FindById/{id}")]
        public async Task<Role> FindByIdAsync(string id) => await roleStore.FindByIdAsync(id);

        [HttpGet("FindByName/{name}")]
        public async Task<Role> FindByNameAsync(string name) => await roleStore.FindByNameAsync(name);
    }
}
