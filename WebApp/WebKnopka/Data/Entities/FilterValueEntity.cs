using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebKnopka.Data.Entities
{
    [Table("tblFilterValues")]
    public class FilterValueEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        public virtual ICollection<FilterNameGroupEntity> FilterNameGroups { get; set; }
    }
}
