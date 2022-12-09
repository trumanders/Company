namespace Company.Data.Entities;

public class Employee : IEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Salary { get; set; }
    public bool isUnionMember { get; set; }
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
}
