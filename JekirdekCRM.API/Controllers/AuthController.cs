using JekirdekCRM.API.Tools;
using JekirdekCRM.DTO.AuthDtos;
using JekirdekCRM.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JekirdekCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var user = new User
                {
                    CreatedAt = DateTime.UtcNow,
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    Role = "Member"
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Kullanıcı oluşturulamadı. : "+ result.Errors);
                    return Unauthorized("Kullanıcı oluşturulamadı!");
                }
                await _userManager.AddToRoleAsync(user, user.Role);
                return Ok("Kullanıcı başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kayıt işlemi sırasında bir hata oluştu.");
                return StatusCode(500, "Sunucu hatası oluştu!");
            }
           
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(loginDto.Username);
                if (user == null)
                {
                    _logger.LogInformation("Kullanıcı bulunamadı! Giriş başarısız.");
                    return Unauthorized("Kullanıcı bulunamadı!");
                }
                var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    _logger.LogInformation("Kullanıcı bulunamadı! Hatalı şifre.");
                    return Unauthorized("Şifre hatalıdır!");
                }

                LoginResponseDto loginResponseDto = new LoginResponseDto()
                {
                    Username = user.UserName,
                    Role = user.Role,
                };
                return Created("", JwtTokenGenerator.GenerateToken(loginResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login işlemi sırasında bir hata oluştu.");
                return StatusCode(500, "Sunucu hatası oluştu.");
            }
                     
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            try
            {
                var roleExists = await _roleManager.RoleExistsAsync(createRoleDto.Role);
                if (roleExists)
                {
                    _logger.LogInformation("Girilen rol zaten mevcut.");
                    return BadRequest("Girilen rol veritabanında bulunmaktadır!");
                }

                var result = await _roleManager.CreateAsync(new IdentityRole<int>(createRoleDto.Role));
                if (!result.Succeeded)
                {
                    _logger.LogInformation("Rol oluşturulamadı! : "+result.Errors);
                    return BadRequest(result.Errors);
                }

                return Ok("Rol oluşturulmuştur");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rol oluşturulurken bir hata meydana geldi!");
                return StatusCode(500, "Sunucu hatası oluştu.");
            }
           
        }



    }
}
