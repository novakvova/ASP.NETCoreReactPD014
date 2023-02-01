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

                if (!context.Products.Any())
                {
                    var hp = new ProductEntity
                    {
                        Name = "HP ProBook 640 G8",
                        DateCreated = DateTime.UtcNow,
                        Price = 40899,
                        Description = "Екран 14\" IPS (1920x1080) Full HD, матовий / Intel Core i5-1135G7 (2.4 - 4.2 ГГц) / RAM 8 ГБ / SSD 256 ГБ / Intel Iris Xe Graphics / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / DOS / 1.38 кг / срібний"
                    };
                    context.Products.Add(hp);
                    context.SaveChanges();

                    var dell = new ProductEntity
                    {
                        Name = "Dell Latitude 5420",
                        DateCreated = DateTime.UtcNow,
                        Price = 104272,
                        Description = "Екран 14\" IPS (1920x1080) Full HD, глянсовий з антивідблисковим покриттям / Intel Core i7-1185G7 (3.0 - 4.8 ГГц) / RAM 64 ГБ / SSD 1 ТБ / Intel Iris Xe Graphics / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / Linux / 1.37 кг / срібний"
                    };
                    context.Products.Add(dell);
                    context.SaveChanges();

                    var dell2 = new ProductEntity
                    {
                        Name = "DELL Latitude 5530",
                        DateCreated = DateTime.UtcNow,
                        Price = 68432,
                        Description = "Екран 15.6” TN+film (1920x1080) Full HD, матовий / Intel Core i5-1135G7 (1.3 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / Intel Iris Xe Graphics / без ОД / Wi-Fi / LAN / Bluetooth / веб-камера / Windows 10 Pro / 1.6 кг / сірий"
                    };
                    context.Products.Add(dell2);
                    context.SaveChanges();
                }

                if (!context.Filters.Any())
                {
                    FilterEntity[] newFilters =
                    {
                    new FilterEntity { FilterValueId=1, ProductId=1 },
                    new FilterEntity { FilterValueId=4, ProductId=1 },

                    new FilterEntity { FilterValueId=2, ProductId=2 },
                    new FilterEntity { FilterValueId=5, ProductId=2 },

                    new FilterEntity { FilterValueId=2, ProductId=3 },
                    new FilterEntity { FilterValueId=4, ProductId=3 }

                };
                    context.Filters.AddRange(newFilters);
                    context.SaveChanges();
                }
            }
        }
    }
}
