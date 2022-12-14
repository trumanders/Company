using System.ComponentModel.DataAnnotations;

namespace Company.Common.DTOs;

public record DepartmentDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CompanyId { get; set; }
}
