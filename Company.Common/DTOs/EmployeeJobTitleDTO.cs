namespace Company.Common.DTOs;

public record EmployeeJobTitleDTO
{
    public int EmployeeId { get; set; }
    public int JobTitleId { get; set; }
    //public JobTitle? JobTitle { get; set; }
    //public Employee? Employee { get; set; }

    public EmployeeJobTitleDTO(int employeeId, int jobTitleId)
    {
        // Assign the parameter values to the properties
        EmployeeId = employeeId;
        JobTitleId = jobTitleId;
    }
}
