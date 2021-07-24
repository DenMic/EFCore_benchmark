using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }

        [Column(TypeName = "decimal(12, 2)")]
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
