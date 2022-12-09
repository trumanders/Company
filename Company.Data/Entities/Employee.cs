namespace Company.Data.Entities;

public class Employee : IEntity
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string FirstName { get; set; }
    [MaxLength(50), Required]
    public string LastName { get; set; }
    [Required]
    public int? Salary { get; set; }
    [Required]
    public bool isUnionMember { get; set; }

    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
}
