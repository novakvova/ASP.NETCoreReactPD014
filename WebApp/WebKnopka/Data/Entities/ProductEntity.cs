using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebKnopka.Data.Entities
{
    [Table("tblProducts")]
    public class ProductEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        public virtual ICollection<FilterEntity> Filters { get; set; }
    }
}
