using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Helpers;
using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end.Model.BindingTargets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fix_it_tracker_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.AdminAccess)]
    public class AccountController : ControllerBase
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private readonly IFixItTrackerRepository _dataContext;

        public AccountController(IFixItTrackerRepository dataContext,
                                UserManager<IdentityUser> userMgr,
                                SignInManager<IdentityUser> signInMgr)
        {
            _dataContext = dataContext;
            userManager = userMgr;
            signInManager = signInMgr;
        }

        /// <summary>
        /// Logs a user in.
        /// </summary>
        /// <param name="login">The login object of the specific user.</param>
        [HttpPost("/api/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Name) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("enter a valid account name and password");
            }

            if (await _dataContext.DoLogin(login))
            {
                return Ok("Logged In");
            }

            return BadRequest("Login Failed");
        }

        /// <summary>
        /// Logs a user out.
        /// </summary>
        /// <param name="login">The login object of the specific user.</param>
        [HttpPost("/api/logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Name))
            {
                return BadRequest("enter a valid account name");
            }

            if (await _dataContext.DoLogout(login))
            {
                return Ok("Logged Out");
            }

            return BadRequest("Logout Failed");
        }

        /// <summary>
        /// Creates a user account.
        /// </summary>
        /// <param name="login">The login object of the specific user.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Name) ||
                string.IsNullOrEmpty(login.Password) ||
                string.IsNullOrEmpty(login.Email))
            {
                return BadRequest("enter a valid account name, password, and email");
            }

            if (await _dataContext.CreateAccount(login))
            {
                return Ok("Account Created");
            }

            return BadRequest("Account Creation Failed");
        }

        /// <summary>
        /// Updates the account name of a specific user.
        /// </summary>
        /// <param name="login">The login object of the specific user.</param>
        [HttpPost("name")]
        [Authorize]
        public async Task<IActionResult> UpdateAccountName([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Name) ||
                string.IsNullOrEmpty(login.NewName))
            {
                return BadRequest("enter a valid account name, and new account name");
            }

            if (await _dataContext.UpdateAccountName(login))
            {
                return Ok("Account Name Updated");
            }

            return BadRequest("Account Name Update Failed");
        }

        /// <summary>
        /// Updates the account password of a specific user.
        /// </summary>
        /// <param name="login">The login object of the specific user.</param>
        [HttpPost("password")]
        [Authorize]
        public async Task<IActionResult> UpdateAccountPassword([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Name) ||
                string.IsNullOrEmpty(login.Password) ||
                string.IsNullOrEmpty(login.NewPassword))
            {
                return BadRequest("enter a valid account name, existing password, and new password");
            }

            if (await _dataContext.UpdateAccountPassword(login))
            {
                return Ok("Account Password Updated");
            }

            return BadRequest("Account Password Update Failed");
        }

        /// <summary>
        /// Gives a specific account administrator access.
        /// </summary>
        /// <param name="login">The login object of the specific user.</param>
        [HttpPost("admin-role")]
        public async Task<IActionResult> AddToAdminRole([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Name))
            {
                return BadRequest("enter a valid account name");
            }

            if (await _dataContext.AddToAdminRole(login))
            {
                return Ok("Account Updated");
            }

            return BadRequest("Account Update Failed");
        }

        /// <summary>
        /// Deletes a specific user account.
        /// </summary>
        /// <param name="login">The login object of the specific user.</param>
        [HttpDelete]
        public async Task<IActionResult> RemoveAccount([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.Name))
            {
                return BadRequest("enter a valid account name");
            }

            if (await _dataContext.RemoveAccount(login))
            {
                return Ok("Account Removed");
            }

            return BadRequest("Account Remove Failed");
        }

        /// <summary>
        /// Gets a list of user accounts that are in the database.
        /// </summary>
        [HttpGet("accounts")]
        public ActionResult<IEnumerable<String>> GetAllAccounts()
        {
            var accountsToReturn = _dataContext.GetAllAccounts();

            if (accountsToReturn.Count() == 0)
            {
                return NotFound("No accounts found.");
            }
            else
            {
                return Ok(accountsToReturn);
            }
        }

    }
}