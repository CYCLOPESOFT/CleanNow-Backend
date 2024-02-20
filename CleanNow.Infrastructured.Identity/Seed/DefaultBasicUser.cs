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
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser user = new();
            user.Email = "addieljaquez@gmail.com";
            user.Name = "Juan";
            user.LastActivity = "Today";
            user.CreatedAt = DateTime.Now;
            user.EmailConfirmed = true;
            user.PhoneNumber = "8299678897";
            user.PhoneNumberConfirmed = true;
            if(userManager.Users.All(u=>u.Id != user.Id))
            {
                var users = await userManager.FindByEmailAsync(user.Email);
                if(user == null)
                {
                    await userManager.CreateAsync(user, "123Pa$$word!");
                    await userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                }
            }
        }
    }
}
