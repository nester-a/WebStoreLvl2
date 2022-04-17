﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.DTO.Identity;
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

        #region Users

        [HttpPost("UserId")] // POST: api/v1/identity/users/UserId
        public async Task<string> GetUserIdAsync([FromBody] User user) => await userStore.GetUserIdAsync(user);

        [HttpPost("UserName")]
        public async Task<string> GetUserNameAsync([FromBody] User user) => await userStore.GetUserNameAsync(user);

        [HttpPost("UserName/{name}")] // api/v1/identity/users/UserName/TestUser
        public async Task<string> SetUserNameAsync([FromBody] User user, string name)
        {
            await userStore.SetUserNameAsync(user, name);
            //user.UserName = name;
            await userStore.UpdateAsync(user);
            return user.UserName;
        }

        [HttpPost("NormalUserName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody] User user) => await userStore.GetNormalizedUserNameAsync(user);

        [HttpPost("NormalUserName/{name}")]
        public async Task<string> SetNormalizedUserNameAsync([FromBody] User user, string name)
        {
            await userStore.SetNormalizedUserNameAsync(user, name);
            await userStore.UpdateAsync(user);
            return user.NormalizedUserName;
        }

        [HttpPost("User")] // POST -> api/v1/identity/users/user
        public async Task<bool> CreateAsync([FromBody] User user)
        {
            var creation_result = await userStore.CreateAsync(user);

            // добавление ошибок создания нового пользователя в журнал
            if (!creation_result.Succeeded)
                logger.LogWarning("Ошибка создания пользователя {0}:{1}",
                    user,
                    string.Join(", ", creation_result.Errors.Select(e => e.Description)));

            return creation_result.Succeeded;
        }

        [HttpPut("User")] // PUT -> api/v1/identity/users/user
        public async Task<bool> UpdateAsync([FromBody] User user)
        {
            var update_result = await userStore.UpdateAsync(user);

            if (!update_result.Succeeded)
                logger.LogWarning("Ошибка редактирования пользователя {0}:{1}",
                    user,
                    string.Join(", ", update_result.Errors.Select(e => e.Description)));

            return update_result.Succeeded;
        }

        [HttpPost("User/Delete")] // POST api/v1/identity/users/user/delete
        [HttpDelete("User/Delete")] // DELETE api/v1/identity/users/user/delete
        [HttpDelete] // DELETE api/v1/identity/users
        public async Task<bool> DeleteAsync([FromBody] User user)
        {
            var delete_result = await userStore.DeleteAsync(user);

            if (!delete_result.Succeeded)
                logger.LogWarning("Ошибка удаления пользователя {0}:{1}",
                    user,
                    string.Join(", ", delete_result.Errors.Select(e => e.Description)));

            return delete_result.Succeeded;
        }

        [HttpGet("User/Find/{id}")] // api/v1/identity/users/user/Find/9E5CB5E7-41DE-4449-829E-45F4C97AA54B
        public async Task<User> FindByIdAsync(string id) => await userStore.FindByIdAsync(id);

        [HttpGet("User/Normal/{name}")] // api/v1/identity/users/user/Normal/TestUser
        public async Task<User> FindByNameAsync(string name) => await userStore.FindByNameAsync(name);

        [HttpPost("Role/{role}")] // POST -> api/v1/identity/users/role/admin - добавляет роль "admin" пользователю, который передаётся в теле запроса
        public async Task AddToRoleAsync([FromBody] User user, string role/*, [FromServices] WebStoreDB db*/)
        {
            await userStore.AddToRoleAsync(user, role);
            await userStore.Context.SaveChangesAsync();
            //await db.SaveChangesAsync();
        }

        [HttpDelete("Role/{role}")]
        [HttpPost("Role/Delete/{role}")]
        public async Task RemoveFromRoleAsync([FromBody] User user, string role/*, [FromServices] WebStoreDB db*/)
        {
            await userStore.RemoveFromRoleAsync(user, role);
            await userStore.Context.SaveChangesAsync();
            //await db.SaveChangesAsync();
        }

        [HttpPost("Roles")]
        public async Task<IList<string>> GetRolesAsync([FromBody] User user) => await userStore.GetRolesAsync(user);

        [HttpPost("InRole/{role}")]
        public async Task<bool> IsInRoleAsync([FromBody] User user, string role) => await userStore.IsInRoleAsync(user, role);

        [HttpGet("UsersInRole/{role}")]
        public async Task<IList<User>> GetUsersInRoleAsync(string role) => await userStore.GetUsersInRoleAsync(role);

        [HttpPost("GetPasswordHash")]
        public async Task<string> GetPasswordHashAsync([FromBody] User user) => await userStore.GetPasswordHashAsync(user);

        [HttpPost("SetPasswordHash")]
        public async Task<string> SetPasswordHashAsync([FromBody] PasswordHashDTO hash)
        {
            await userStore.SetPasswordHashAsync(hash.User, hash.Hash);
            await userStore.UpdateAsync(hash.User);
            return hash.User.PasswordHash;
        }

        [HttpPost("HasPassword")]
        public async Task<bool> HasPasswordAsync([FromBody] User user) => await userStore.HasPasswordAsync(user);

        #endregion

        #region Claims

        [HttpPost("GetClaims")]
        public async Task<IList<Claim>> GetClaimsAsync([FromBody] User user) => await userStore.GetClaimsAsync(user);

        [HttpPost("AddClaims")]
        public async Task AddClaimsAsync([FromBody] ClaimDTO ClaimInfo/*, [FromServices] WebStoreDB db*/)
        {
            await userStore.AddClaimsAsync(ClaimInfo.User, ClaimInfo.Claims);
            await userStore.Context.SaveChangesAsync();
            //await db.SaveChangesAsync();
        }

        [HttpPost("ReplaceClaim")]
        public async Task ReplaceClaimAsync([FromBody] ReplaceClaimDTO ClaimInfo/*, [FromServices] WebStoreDB db*/)
        {
            await userStore.ReplaceClaimAsync(ClaimInfo.User, ClaimInfo.Claim, ClaimInfo.NewClaim);
            await userStore.Context.SaveChangesAsync();
            //await db.SaveChangesAsync();
        }

        [HttpPost("RemoveClaim")]
        public async Task RemoveClaimsAsync([FromBody] ClaimDTO ClaimInfo/*, [FromServices] WebStoreDB db*/)
        {
            await userStore.RemoveClaimsAsync(ClaimInfo.User, ClaimInfo.Claims);
            await userStore.Context.SaveChangesAsync();
            //await db.SaveChangesAsync();
        }

        [HttpPost("GetUsersForClaim")]
        public async Task<IList<User>> GetUsersForClaimAsync([FromBody] Claim claim) =>
            await userStore.GetUsersForClaimAsync(claim);

        #endregion

        #region TwoFactor

        [HttpPost("GetTwoFactorEnabled")]
        public async Task<bool> GetTwoFactorEnabledAsync([FromBody] User user) => await userStore.GetTwoFactorEnabledAsync(user);

        [HttpPost("SetTwoFactor/{enable}")]
        public async Task<bool> SetTwoFactorEnabledAsync([FromBody] User user, bool enable)
        {
            await userStore.SetTwoFactorEnabledAsync(user, enable);
            await userStore.UpdateAsync(user);
            return user.TwoFactorEnabled;
        }

        #endregion

        #region Email/Phone

        [HttpPost("GetEmail")]
        public async Task<string> GetEmailAsync([FromBody] User user) => await userStore.GetEmailAsync(user);

        [HttpPost("SetEmail/{email}")]
        public async Task<string> SetEmailAsync([FromBody] User user, string email)
        {
            await userStore.SetEmailAsync(user, email);
            await userStore.UpdateAsync(user);
            return user.Email;
        }

        [HttpPost("GetNormalizedEmail")]
        public async Task<string> GetNormalizedEmailAsync([FromBody] User user) => await userStore.GetNormalizedEmailAsync(user);

        [HttpPost("SetNormalizedEmail/{email?}")]
        public async Task<string> SetNormalizedEmailAsync([FromBody] User user, string? email)
        {
            await userStore.SetNormalizedEmailAsync(user, email);
            await userStore.UpdateAsync(user);
            return user.NormalizedEmail;
        }

        [HttpPost("GetEmailConfirmed")]
        public async Task<bool> GetEmailConfirmedAsync([FromBody] User user) => await userStore.GetEmailConfirmedAsync(user);

        [HttpPost("SetEmailConfirmed/{enable}")]
        public async Task<bool> SetEmailConfirmedAsync([FromBody] User user, bool enable)
        {
            await userStore.SetEmailConfirmedAsync(user, enable);
            await userStore.UpdateAsync(user);
            return user.EmailConfirmed;
        }

        [HttpGet("UserFindByEmail/{email}")]
        public async Task<User> FindByEmailAsync(string email) => await userStore.FindByEmailAsync(email);

        [HttpPost("GetPhoneNumber")]
        public async Task<string> GetPhoneNumberAsync([FromBody] User user) => await userStore.GetPhoneNumberAsync(user);

        [HttpPost("SetPhoneNumber/{phone}")]
        public async Task<string> SetPhoneNumberAsync([FromBody] User user, string phone)
        {
            await userStore.SetPhoneNumberAsync(user, phone);
            await userStore.UpdateAsync(user);
            return user.PhoneNumber;
        }

        [HttpPost("GetPhoneNumberConfirmed")]
        public async Task<bool> GetPhoneNumberConfirmedAsync([FromBody] User user) =>
            await userStore.GetPhoneNumberConfirmedAsync(user);

        [HttpPost("SetPhoneNumberConfirmed/{confirmed}")]
        public async Task<bool> SetPhoneNumberConfirmedAsync([FromBody] User user, bool confirmed)
        {
            await userStore.SetPhoneNumberConfirmedAsync(user, confirmed);
            await userStore.UpdateAsync(user);
            return user.PhoneNumberConfirmed;
        }

        #endregion

        #region Login/Lockout

        [HttpPost("AddLogin")]
        public async Task AddLoginAsync([FromBody] AddLoginDTO login/*, [FromServices] WebStoreDB db*/)
        {
            await userStore.AddLoginAsync(login.User, login.UserLoginInfo);
            await userStore.Context.SaveChangesAsync();
            //await db.SaveChangesAsync();
        }

        [HttpPost("RemoveLogin/{LoginProvider}/{ProviderKey}")]
        public async Task RemoveLoginAsync([FromBody] User user, string LoginProvider, string ProviderKey/*, [FromServices] WebStoreDB db*/)
        {
            await userStore.RemoveLoginAsync(user, LoginProvider, ProviderKey);
            await userStore.Context.SaveChangesAsync();
            //await db.SaveChangesAsync();
        }

        [HttpPost("GetLogins")]
        public async Task<IList<UserLoginInfo>> GetLoginsAsync([FromBody] User user) => await userStore.GetLoginsAsync(user);

        [HttpGet("User/FindByLogin/{LoginProvider}/{ProviderKey}")]
        public async Task<User> FindByLoginAsync(string LoginProvider, string ProviderKey) => await userStore.FindByLoginAsync(LoginProvider, ProviderKey);

        [HttpPost("GetLockoutEndDate")]
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync([FromBody] User user) => await userStore.GetLockoutEndDateAsync(user);

        [HttpPost("SetLockoutEndDate")]
        public async Task<DateTimeOffset?> SetLockoutEndDateAsync([FromBody] SetLockoutDTO LockoutInfo)
        {
            await userStore.SetLockoutEndDateAsync(LockoutInfo.User, LockoutInfo.LockoutEnd);
            await userStore.UpdateAsync(LockoutInfo.User);
            return LockoutInfo.User.LockoutEnd;
        }

        [HttpPost("IncrementAccessFailedCount")]
        public async Task<int> IncrementAccessFailedCountAsync([FromBody] User user)
        {
            var count = await userStore.IncrementAccessFailedCountAsync(user);
            await userStore.UpdateAsync(user);
            return count;
        }

        [HttpPost("ResetAccessFailedCount")]
        public async Task<int> ResetAccessFailedCountAsync([FromBody] User user)
        {
            await userStore.ResetAccessFailedCountAsync(user);
            await userStore.UpdateAsync(user);
            return user.AccessFailedCount;
        }

        [HttpPost("GetAccessFailedCount")]
        public async Task<int> GetAccessFailedCountAsync([FromBody] User user) => await userStore.GetAccessFailedCountAsync(user);

        [HttpPost("GetLockoutEnabled")]
        public async Task<bool> GetLockoutEnabledAsync([FromBody] User user) => await userStore.GetLockoutEnabledAsync(user);

        [HttpPost("SetLockoutEnabled/{enable}")]
        public async Task<bool> SetLockoutEnabledAsync([FromBody] User user, bool enable)
        {
            await userStore.SetLockoutEnabledAsync(user, enable);
            await userStore.UpdateAsync(user);
            return user.LockoutEnabled;
        }

        #endregion
    }
}
