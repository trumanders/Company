using System.ComponentModel.DataAnnotations;

namespace Company.Common.DTOs;

public record CompanyTableDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string OrganizationNumber { get; set; }
}
