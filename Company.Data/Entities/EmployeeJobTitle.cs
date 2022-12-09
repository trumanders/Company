namespace Company.Data.Entities;

public class EmployeeJobTitle : IReferenceEntity
{
    public int EmployeeId { get; set; }
    public int JobTitleId { get; set; }
    public JobTitle? JobTitle { get; set; }
    public Employee? Employee { get; set; } 
}
