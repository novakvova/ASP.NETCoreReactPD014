using Microsoft.AspNetCore.Identity;
using WebKnopka.Constants;
using WebKnopka.Data;
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
                var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();
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
                if (!context.FilterNames.Any())
                {
                    Console.WriteLine("У табличні назв фільтрів пусто");
                    string[] filterNames = {
                        "Виробник", "Процесор"
                    };

                    foreach (string filterName in filterNames)
                    {
                        var fn = new FilterNameEntity
                        {
                            DateCreated = DateTime.UtcNow,
                            Name = filterName,
                        };
                        context.FilterNames.Add(fn);
                        context.SaveChanges();
                    }
                }

                if (!context.FilterValues.Any())
                {
                    Console.WriteLine("У табличні значення фільтрів пусто");
                    string[] filterValues = {
                        "HP", "Dell", "Lenovo",
                        "Intel Core i5", "Intel Core i7"
                    };

                    foreach (string filterValue in filterValues)
                    {
                        var fv = new FilterValueEntity
                        {
                            DateCreated = DateTime.UtcNow,
                            Name = filterValue,
                        };
                        context.FilterValues.Add(fv);
                        context.SaveChanges();
                    }
                }

                if (!context.FilterNameGroups.Any())
                {
                    Console.WriteLine("У табличні групування фільтрів пусто");
                    Dictionary<int, int> fng = new Dictionary<int, int>
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 2},
                    { 5, 2 }
                };

                    foreach (var data in fng)
                    {
                        var entity = new FilterNameGroupEntity
                        {
                            FilterNameId = data.Value,
                            FilterValueId = data.Key
                        };
                        context.FilterNameGroups.Add(entity);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
