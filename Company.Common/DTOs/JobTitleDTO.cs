using System.ComponentModel.DataAnnotations;

namespace Company.Common.DTOs;

public record JobTitleDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}
