using System.ComponentModel.DataAnnotations;

namespace Company.Common.DTOs;

public record EmployeeDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? Salary { get; set; }
    public bool isUnionMember { get; set; }

    public int? DepartmentId { get; set; }
}
