using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Tebnabawe.Application.AboutT;
using Tebnabawe.Application.AboutT.Dto;
using Tebnabawe.Data;
using Tebnabawe.Data.Models;
namespace Tebnabawe.Web
{
    public static class DataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedAdminAsync(userManager);
        }

        public static void SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("AboYahya").Result == null)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = "AboYahya";
                user.Email = "atebnabawe@gmail.com";
                user.FirstName = "AboYahya";
                user.LastName = "Almagraby";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, "Ab!12345").Result;

                if (result.Succeeded)
                {
                     userManager.AddToRoleAsync(user, "أدمن");
                }

            }

        }
      
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("مستخدم").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "مستخدم";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("أدمن").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "أدمن";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("مشرف").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "مشرف";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

     
    }
}
