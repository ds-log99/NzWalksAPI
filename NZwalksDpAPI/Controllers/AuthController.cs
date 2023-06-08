using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalksDpAPI.Models.DTO;
using NZwalksDpAPI.Repositories;

namespace NZwalksDpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register )
        {
            var identityUser = new IdentityUser
            { 
                UserName = register.UserName,
                Email = register.UserName
            };
          var identityResult =  await userManager.CreateAsync(identityUser, register.Password);

            if (identityResult.Succeeded)
            {
                if (register.Roles != null && register.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, register.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("user registered");
                    }
                }
            }
           
            return BadRequest("400 Something went wrong");

        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LogInDto logIn)
        {
           var user = await userManager.FindByEmailAsync(logIn.UserName);

            if (user != null)
            {
                var checkUser = await userManager.CheckPasswordAsync(user, logIn.Password);

                if (checkUser)
                {
                    // create auth token 
                    var userRole = await userManager.GetRolesAsync(user);

                    if (userRole != null)
                    {
                      var jwtToken =   tokenRepository.CreateJWTToken(user, userRole.ToList());
                        var response = new LoginResponse
                        {
                            JwtToken = jwtToken,
                        };

                        return Ok(response);
                    }

                }
            }

            return BadRequest();
        }
    }
}
