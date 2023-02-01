using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebKnopka.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebKnopka.Data
{
    public class AppEFContext : IdentityDbContext<AppUserEntity, AppRoleEntity, long, 
        IdentityUserClaim<long>,
        AppUserRoleEntity, IdentityUserLogin<long>,
        IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options) 
            : base(options)
        {

        }
        public DbSet<FilterNameEntity> FilterNames { get; set; }
        public DbSet<FilterValueEntity> FilterValues { get; set; }
        public DbSet<FilterNameGroupEntity> FilterNameGroups { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<FilterEntity> Filters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserRoleEntity>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                builder.Entity<FilterNameGroupEntity>(fng =>
                {
                    fng.HasKey(b => new { b.FilterNameId, b.FilterValueId });
                });

                builder.Entity<FilterEntity>(fng =>
                {
                    fng.HasKey(b => new { b.ProductId, b.FilterValueId });
                });

            });
        }
    }
}
