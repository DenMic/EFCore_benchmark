using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }

        [Column(TypeName = "decimal(12, 2)")]
        public decimal UnitPrice { get; set; }

        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }

        public Supplier Supplier { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
