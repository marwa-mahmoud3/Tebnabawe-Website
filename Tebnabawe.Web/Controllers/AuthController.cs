using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Tebnabawe.Application.Authentication;
using Tebnabawe.Application.Authentication.Dto;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IHttpContextAccessor httpContextAccessor,IAuthService authService, IConfiguration configuration, IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _emailService = emailService;
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

        }
        [HttpPost("register/{Role}")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model,string Role = "مستخدم")  
        {

            var result = await _authService.RegisterAsync(model,Role);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {

            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpGet("ConfirmEmail/{userId}/{token}")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _authService.ConfirmEmailAsync(userId, token);

            if (result.IsAuthenticated)
            {
                return Redirect($"{_configuration["AppUrl"]}/ConfirmEmail.html");
            }

            return BadRequest(result.Message);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (string.IsNullOrEmpty(forgotPasswordModel.Email))
                return NotFound();

            var result = await _authService.ForgetPasswordAsync(forgotPasswordModel);

            if (result.IsAuthenticated)
                return Ok(new { Message = result.Message }); 
            return BadRequest(result.Message); 
        }
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.Password != model.ConfrimPassword)
                return BadRequest("كلمة المرور وتكرار كلمة المرور غير متطابقين");
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("البريد الالكترونى غير صحيح");
            var hashedNewPassword = _userManager.PasswordHasher.HashPassword(user, model.Password);
            user.PasswordHash = hashedNewPassword;
            await _userManager.UpdateAsync(user);
            return Ok(user);
        }
    }
}