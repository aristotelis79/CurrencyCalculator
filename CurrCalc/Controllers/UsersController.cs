using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurrCalc.Data.Entities;
using CurrCalc.Models;
using CurrCalc.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CurrCalc.Controllers
{
    /// <inheritdoc />
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <inheritdoc />
        public UsersController(ILogger<UsersController> logger,
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager) : base(logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentException(nameof(roleManager));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/token")]
        public async Task<IActionResult> CreateToken([FromBody] AuthenticateModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null) return NotFound();

                var signIn = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (!signIn.Succeeded) return Content("Not valid credentials");


                var securityToken = new JwtSecurityToken(issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Audience"],
                    claims : (await GetUserClaims(user).ConfigureAwait(false)).ToArray(),
                    expires: DateTime.Now.AddDays(Convert.ToInt32(_configuration["Tokens:Expires"])),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"])),
                        SecurityAlgorithms.HmacSha256));

                return OkObjectResult(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    expiration = securityToken.ValidTo
                });
            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(UsersController), e);
            }

        }

        private async Task<List<Claim>> GetUserClaims(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
            };

            var userClaims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);
            var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            claims.AddRange(userClaims);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                
                var role = await _roleManager.FindByNameAsync(userRole).ConfigureAwait(false);
                
                if (role == null) continue;
                
                var roleClaims = await _roleManager.GetClaimsAsync(role).ConfigureAwait(false);

                claims.AddRange(roleClaims);
            }
            return claims;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel model)
        {
            //await _roleManager.CreateAsync(new IdentityRole("Admin"));
            //await _roleManager.CreateAsync(new IdentityRole("Trader"));
            //await _roleManager.CreateAsync(new IdentityRole("User"));

            //var currentUser1 = await _userManager.FindByEmailAsync("trader@trader.com").ConfigureAwait(false);
            //await _userManager.AddToRoleAsync(currentUser1, Enum.GetName(typeof(Role), Role.Trader)).ConfigureAwait(false); 

            //var currentUser2 = await _userManager.FindByEmailAsync("admin@admin.com").ConfigureAwait(false);
            //await _userManager.AddToRoleAsync(currentUser2, Enum.GetName(typeof(Role), Role.Admin)).ConfigureAwait(false); 

            //var currentUser3 = await _userManager.FindByEmailAsync("test@test.com").ConfigureAwait(false);
            //await _userManager.AddToRoleAsync(currentUser3, Enum.GetName(typeof(Role), Role.User)).ConfigureAwait(false); 

            if (!ModelState.IsValid)                     
                return BadRequestObjectResult(nameof(UserManager<AppUser>), ModelState.Values);

            try
            {
                var createdUser = await _userManager.CreateAsync(new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email
                }, model.Password);

                if (!createdUser.Succeeded)
                    return BadRequestObjectResult(nameof(UserManager<AppUser>),
                        createdUser.Errors.Select(s => s.Description));

                var currentUser = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false); 

                await _userManager.AddToRoleAsync(currentUser, Enum.GetName(typeof(Role), model.Role)).ConfigureAwait(false); 

                return CreateObjectResult(currentUser.ToString());

            }
            catch (Exception e)
            {
                return LogAndError500Response(nameof(UsersController), e);
            }
        }
    }
}
