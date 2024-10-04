using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InterviewAppTasklyWebApi.Entities;
using InterviewAppTasklyWebApi.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace InterviewAppTasklyWebApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(IConfiguration configuration, ILogger<UserController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
    {
        _configuration = configuration;
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel()
                {
                    Status = "Error",
                    Message = "Error Model Request."
                });
            }
            
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            
            if (user != null)
            {
                var resultSignIn = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);

                if (resultSignIn.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GetToken(authClaims);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                
                return Unauthorized();

            }
            
            return Unauthorized();
        }
        catch (Exception e)
        {
            _logger.LogError("Login failed due to exception: {Message}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, null);
        }
    }
    
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Configurations:JwtConfig:SecretKey")));
        int timeExpire = Convert.ToInt32(_configuration.GetValue<string>("Configurations:JwtConfig:Expires"));
        
        var token = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("Configurations:JwtConfig:ValidIssuer"),
            audience: _configuration["Configurations:JwtConfig:ValidAudience"],
            expires: DateTime.Now.AddMinutes(timeExpire),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
    
    [Authorize]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel()
                {
                    Status = "Error",
                    Message = "Error Model Request."
                });
            }
            
            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);

            if (userExists != null)
            {
                return Ok(new ResponseModel()
                {
                    Status = "Error", 
                    Message = "User already exists!"
                });
            }

            var user = new ApplicationUser
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                FirstName = registerModel.UserFirstName,
                LastName = registerModel.UserLastName
            };
            
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return Ok(new ResponseModel()
                {
                    Status = "Error", 
                    Message = "User creation failed! Please check user details and try again."
                });
            }
            
            var roleExists = await _roleManager.RoleExistsAsync(registerModel.UserRol);
            
            if (!roleExists)
            {
                return Ok(new ResponseModel
                {
                    Status = "Error", 
                    Message = "Role creation failed! Please check role details and try again."
                });
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, registerModel.UserRol);

            if (!addToRoleResult.Succeeded)
            {
                return Ok(new ResponseModel()
                {
                    Status = "Error",
                    Message = "Adding user to role failed! Please check user details and try again."
                });
            }
            
            return Ok(new ResponseModel()
            {
                Status = "Success", 
                Message = "User created successfully!"
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Register failed due to exception: {Message}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, null);
        }
    }
}