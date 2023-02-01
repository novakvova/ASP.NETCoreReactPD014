using System.ComponentModel.DataAnnotations.Schema;

namespace WebKnopka.Data.Entities
{
    [Table("tblFilters")]
    public class FilterEntity
    {
        [ForeignKey("FilterValue")]
        public int FilterValueId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual FilterValueEntity FilterValue { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}
