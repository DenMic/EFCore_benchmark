using System.Collections.Generic;

namespace Infrastructure.DTO
{
    public class SupplierDto
    {
        public string Name { get; set; }
        public IEnumerable<ProductDto> Product { get; set; }
    }
}
