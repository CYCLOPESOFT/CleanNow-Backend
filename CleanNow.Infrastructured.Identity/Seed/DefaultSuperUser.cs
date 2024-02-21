using CleanNow.Core.Application.Enum;
using CleanNow.Infrastructured.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Infrastructured.Identity.Seed
{
    public static class DefaultSuperUser
    {
        public static async Task SeedAsync (UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.Email = "mjaquez191@gmail.com";
            defaultUser.Name = "Addiel";
            defaultUser.LastActivity = "Today";
            defaultUser.CreatedAt = DateTime.Now;
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumber = "8299678897";
            defaultUser.PhoneNumberConfirmed = true;
            if(userManager.Users.All(u=>u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user != null)
                {
                    await userManager.CreateAsync(user, "123Pa$$word!");
                    var roles = new List<string>{ Roles.Basic.ToString(),Roles.SuperAdmin.ToString(),Roles.Admin.ToString() };
                    await userManager.AddToRolesAsync(user, roles);
                }
            }
        }
    }
}
