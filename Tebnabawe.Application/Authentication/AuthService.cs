using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Authentication.Dto;
using Tebnabawe.Application.Configuration;
using Tebnabawe.Data;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.Authentication
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly TebnabaweContext _context;
        private readonly JWT _jwt;

        public AuthService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWT> jwt,
            IEmailService mailService,
            IConfiguration configuration,
            TebnabaweContext context,
            IMapper mapper) : base(mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _configuration = configuration;
            _context = context;
            _jwt = jwt.Value;
        }


        [Obsolete]
        public async Task<AuthModel> RegisterAsync(RegisterModel model, string Role)
        {
            var userbyEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userbyEmail != null)
                return new AuthModel { Message = " البريد الالكترونى  هذا مستخدم " };
            var userbyName = await _userManager.FindByNameAsync(model.UserName);

            if (userbyName != null)
                return new AuthModel { Message = "ادخل اسم مستخدم اخر لانه مستخدم" };

            if (model.Password != model.ConfirmPassword)
                return new AuthModel { Message = "كلمه المرور وتاكيد كلمه المرور غير متطابقين" };
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, Role);
            
            var jwtSecurityToken = await CreateJwtToken(user);
           
            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = $"{_configuration["AppUrl"]}/api/Auth/ConfirmEmail/{user.Id}/{validEmailToken}";

            _mailService.SendEmail(user.Email, "Confirm your email", $"<h1>Welcome in Tebnabawe Website</h1>" +
                $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { Role },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.Id,
            };
        }
       
        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "  البريد الالكترونى أو كلمه المرور غير صحيحة";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();
            authModel.FirstName = user.FirstName;
            authModel.LastName = user.LastName;
            authModel.UserId = user.Id;
            return authModel;
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something wrong";
        }

        public async Task<AuthModel> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new AuthModel
                {
                    IsAuthenticated = false,
                    Message = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new AuthModel
                {
                    Message = "Email confirmed successfully!",
                    IsAuthenticated = true,
                };

            var errors = string.Empty;
            foreach (var error in result.Errors)
                errors += $"{error.Description}";
            return new AuthModel
            {
                IsAuthenticated = false,
                Message = "Email did not confirm, " + errors
            };
        }

        public async Task<AuthModel> ForgetPasswordAsync(ForgotPasswordModel forgotPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return new AuthModel
                {
                    IsAuthenticated = false,
                    Message = "No user associated with email",
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{forgotPasswordModel.ClientURI}?email={forgotPasswordModel.Email}&token={validToken}";

            _mailService.SendEmail(forgotPasswordModel.Email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{url}'>Click here</a></p>");

            return new AuthModel
            {
                IsAuthenticated = true,
                Message = "Reset password URL has been sent to the email successfully!"
            };
        }

        public async Task<AuthModel> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new AuthModel { Message = "Invail Email or Not Register" };
            var result = await _userManager.IsEmailConfirmedAsync(user);
            if (!result)
                return new AuthModel { Message = "Email isn't Confirm " };
            if (model.Password != model.ConfrimPassword)
                return new AuthModel { Message = "Password and Confirm Password Not Match" };

            var authModel = new AuthModel();

            var jwtSecurityToken = await CreateJwtToken(user);
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            await _userManager.ResetPasswordAsync(user, authModel.Token, model.Password);
            return new AuthModel { Message = "" };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}