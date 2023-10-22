
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using Newtonsoft.Json;
using ProjectBackend.Areas.Identity.Data;
using ProjectBackend.Models;
namespace ProjectBackend.Controllers;

public class ClientAuthenticationController : ControllerBase
{
    private UserManager<ARENKidUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    private IConfiguration _configuration;

    public ClientAuthenticationController(UserManager<ARENKidUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("api/load-session")]
    public async Task<IActionResult> LoadSession()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        var response = new AuthenticationResponse { Status = "Success", Message = "Authentication success " + identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value };

        await Task.Yield();

        return Ok(response);
    }


    [AllowAnonymous]
    [HttpPost("api/login")]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        var user = await _userManager.FindByNameAsync(userLogin.Username!);
        if (user != null && await _userManager.CheckPasswordAsync(user, userLogin.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            var message = JsonConvert.SerializeObject(new
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                LevelAge = user.LevelAge,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });

            var response = new AuthenticationResponse { Status = "Success", Message = message };

            return Ok(response);
        }
        return StatusCode(StatusCodes.Status403Forbidden, new AuthenticationResponse { Status = "Error", Message = "The email and/or password you entered are incorrect. Please try again." });
    }

    [HttpPost]
    [Route("api/register")]
    public async Task<IActionResult> Register([FromBody] UserRegister model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username!);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new AuthenticationResponse { Status = "Error", Message = "User already exists!" });

        ARENKidUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await _userManager.CreateAsync(user, model.Password!);
        if (!result.Succeeded)
        {
            var errorMsg = "";
            foreach (var err in result.Errors)
            {
                errorMsg += err.Description + "\n";
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new AuthenticationResponse { Status = "Error", Message = errorMsg });
        }

        // var userCreated = await _userManager.FindByNameAsync(user.UserName!);

        // var message = JsonConvert.SerializeObject(new
        // {
        //     Id = user.Id,
        //     UserName = user.UserName,
        //     Email = user.Email,
        //     LevelAge = user.LevelAge,
        //     Token = new JwtSecurityTokenHandler().WriteToken(token),
        //     Expiration = token.ValidTo
        // });

        return Ok(new AuthenticationResponse { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("api/register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserRegister model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username!);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new AuthenticationResponse { Status = "Error", Message = "User already exists!" });

        ARENKidUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await _userManager.CreateAsync(user, model.Password!);
        if (!result.Succeeded)
        {
            var errorMsg = "";
            foreach (var err in result.Errors)
            {
                errorMsg += err.Description + "\n";
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new AuthenticationResponse { Status = "Error", Message = errorMsg });
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }
        return Ok(new AuthenticationResponse { Status = "Success", Message = "User created successfully!" });
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    [Authorize]
    [HttpPost]
    [Route("api/update-level-age")]
    public async Task<IActionResult> UpdateLevelAge(int age)
    {
        if (age >= 3 && age <= 5)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);
            user!.LevelAge = age;
            await _userManager.UpdateAsync(user);
            return Ok();
        }

        return StatusCode(StatusCodes.Status304NotModified);
    }
}
