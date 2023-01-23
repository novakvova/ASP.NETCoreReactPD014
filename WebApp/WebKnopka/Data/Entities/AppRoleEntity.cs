using Microsoft.AspNetCore.Identity;

namespace WebKnopka.Data.Entities
{
    public class AppRoleEntity : IdentityRole<long>
    {
        public virtual ICollection<AppUserRoleEntity> UserRoles { get; set; }
    }
}