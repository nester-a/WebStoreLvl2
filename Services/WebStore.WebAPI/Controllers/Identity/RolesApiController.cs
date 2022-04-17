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
    }
}
