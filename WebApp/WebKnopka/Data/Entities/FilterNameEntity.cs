using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebKnopka.Data.Entities
{
    /// <summary>
    /// Назви фільтрів
    /// </summary>
    [Table("tblFilterNames")]
    public class FilterNameEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        public virtual ICollection<FilterNameGroupEntity> FilterNameGroups { get; set; }
    }
}
