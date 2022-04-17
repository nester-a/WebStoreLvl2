using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity
{
    [ApiController]
    [Route(WebAPIAddresses.V1.Identity.Users)]
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> userStore;
        private readonly ILogger<UsersApiController> logger;

        public UsersApiController(WebStoreDB db, ILogger<UsersApiController> logger)
        {
            userStore = new (db);
            this.logger = logger;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await userStore.Users.ToArrayAsync();
        }
    }
}
