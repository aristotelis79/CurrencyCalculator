using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurrCalc.Data.Entities;
using CurrCalc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CurrCalc.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        /// <inheritdoc />
        public UsersController(ILogger<UsersController> logger,
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager) : base(logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] AuthenticateModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null) return NotFound();

            var signIn = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!signIn.Succeeded) return Content("Not valid credentials");

            var securityToken = new JwtSecurityToken(issuer: _configuration["Tokens:Issuer"],
                                            audience: _configuration["Tokens:Audience"],
                                            claims : new[] {new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())},
                                            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Tokens:Expires"])),
                                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"])),
                                                                                    SecurityAlgorithms.HmacSha256));

            return Ok(new ApiResponse
            {
                Data = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    expiration = securityToken.ValidTo
                }
            });

        }
    }
}
