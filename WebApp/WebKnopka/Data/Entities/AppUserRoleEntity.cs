using Microsoft.AspNetCore.Identity;

namespace WebKnopka.Data.Entities
{
    public class AppUserRoleEntity : IdentityUserRole<long>
    {
        public virtual AppUserEntity User { get; set; }
        public virtual AppRoleEntity Role { get; set; }
    }
}