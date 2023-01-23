using Microsoft.AspNetCore.Identity;
using WebKnopka.Constants;
using WebKnopka.Data.Entities;

namespace WebKnopka.Services
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUserEntity>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRoleEntity>>();
                if(!roleManager.Roles.Any())
                {
                    var result = roleManager.CreateAsync(new AppRoleEntity
                    {
                        Name = Roles.Admin
                    }).Result;

                    result = roleManager.CreateAsync(new AppRoleEntity
                    {
                        Name = Roles.User
                    }).Result;
                }

                if(!userManager.Users.Any())
                {
                    string email = "admin@gmail.com";
                    var user = new AppUserEntity
                    {
                        Email = email,
                        UserName = email,
                        Photo = "fgbugdqn.bdv.jpeg",
                        PhoneNumber = "+11(111)111-11-11"
                    };
                    var result = userManager.CreateAsync(user, "Qwerty1+").Result;
                    result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
                }

            }
        }
    }
}
