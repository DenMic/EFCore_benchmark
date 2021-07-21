using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_performance.EF.Model
{
    [Table("Company")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } 
        public string BusinessName { get; set; } 
        public string Piva { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}
