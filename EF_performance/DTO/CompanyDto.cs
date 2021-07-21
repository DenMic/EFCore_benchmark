using System.Collections.Generic;

namespace EF_performance.DTO
{
    public class CompanyDto
    {
        public string CompanyName { get; set; }
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}
