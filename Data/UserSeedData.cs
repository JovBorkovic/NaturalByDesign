using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NbdAplication.Data
{
    public static class UserSeedData
    {
        public static async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            //Create Roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Sales", "Designer", "Production", "Manager", "Botanist" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Create Users
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            if (userManager.FindByEmailAsync("KeriYamaguchi@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "KeriYamaguchi@hotmail.com",
                    Email = "KeriYamaguchi@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "keri123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    userManager.AddToRoleAsync(user, "Sales").Wait();
                    userManager.AddToRoleAsync(user, "Designer").Wait();
                    userManager.AddToRoleAsync(user, "Production").Wait();
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                    userManager.AddToRoleAsync(user, "Botanist").Wait();
                }
                context.SaveChanges();
            }
            if (userManager.FindByEmailAsync("StanFenton@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "StanFenton@hotmail.com",
                    Email = "StanFenton@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "stan123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    userManager.AddToRoleAsync(user, "Sales").Wait();
                    userManager.AddToRoleAsync(user, "Designer").Wait();
                    userManager.AddToRoleAsync(user, "Production").Wait();
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                    userManager.AddToRoleAsync(user, "Botanist").Wait();
                }
                context.SaveChanges();
            }
            if (userManager.FindByEmailAsync("ConnieNguyen@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "ConnieNguyen@hotmail.com",
                    Email = "ConnieNguyen@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "connie123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
                context.SaveChanges();
            }
            if (userManager.FindByEmailAsync("CherylPoy@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "CherylPoy@hotmail.com",
                    Email = "CherylPoy@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "cheryl123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Designer").Wait();
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
                context.SaveChanges();
            }
            if (userManager.FindByEmailAsync("BobReinhardt@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "BobReinhardt@hotmail.com",
                    Email = "BobReinhardt@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "bob123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Sales").Wait();
                }
                context.SaveChanges();
            }
            if (userManager.FindByEmailAsync("TamaraBakken@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "TamaraBakken@hotmail.com",
                    Email = "TamaraBakken@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "tamara123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Designer").Wait();
                }
                context.SaveChanges();
            }
            if (userManager.FindByEmailAsync("SueKaufman@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "SueKaufman@hotmail.com",
                    Email = "SueKaufman@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "sue123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Production").Wait();
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
                context.SaveChanges();
            }
            if (userManager.FindByEmailAsync("MonicaGoce@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "MonicaGoce@hotmail.com",
                    Email = "MonicaGoce@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "monica123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Production").Wait();
                }
                context.SaveChanges();
            }
            if (userManager.FindByEmailAsync("BertSwenson@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "BertSwenson@hotmail.com",
                    Email = "BertSwenson@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "bert123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Production").Wait();
                }
                context.SaveChanges();
            }




        }

    }
}
