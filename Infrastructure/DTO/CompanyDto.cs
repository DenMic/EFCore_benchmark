using System.Collections.Generic;

namespace Infrastructure.DTO
{
    public class CompanyDto
    {
        public string CompanyName { get; set; }
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}
