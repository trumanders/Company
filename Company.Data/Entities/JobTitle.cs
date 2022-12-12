namespace Company.Data.Entities;

public class JobTitle : IEntity
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
