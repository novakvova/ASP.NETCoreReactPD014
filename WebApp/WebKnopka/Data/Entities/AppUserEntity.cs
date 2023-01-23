using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebKnopka.Data.Entities
{
    public class AppUserEntity : IdentityUser<long>
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string SecondName { get; set; }
        [StringLength(100)]
        public string Photo { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        public virtual ICollection<AppUserRoleEntity>? UserRoles { get; set; }
    }
}
