namespace Company.Data.Entities;

public class CompanyTable : IEntity
{
    public int Id { get; set; }

    [MaxLength(50), Required]
    public string Name { get; set; }

    [MaxLength(50), Required]
    public string OrganizationNumber { get; set; }
}
