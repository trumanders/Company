namespace Company.Data.Contexts;


/// <summary>
/// This class is a bridge between the database and the application
/// "All database connections use this class to communicate with the database
/// because it contains properties for all entity classes, which represents
/// the tables in the database."
/// </summary>
public class CompanyContext : DbContext
{
	// CONSTRUCTOR
	// options is passed to the base class DbContext
	public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) { }
	

	// OnModelCreating is a virtual method in the DbContext-class
	// This method is used to "configure certain features of the database and call
	// a method to seed data in the database when it is created"
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Specify the composit keys (many-to-many-relations)
		modelBuilder.Entity<EmployeeJobTitle>().HasKey(ej => new { ej.EmployeeId, ej.JobTitleId });

		// Call the SeedData method at the end of the OnModeCreating method and pass in the builder object
		SeedData(modelBuilder);
    }

	/* You need to add the entity classes as DbSet properties in the CompanyContext class to generate
     migration data that Entity Framework can use to create tables in the database.The properties in this Context
	 class can then access the tables and their data. You then inject the CompanyContext class into the
	 constructor of the DBService class to perform CRUD (Create, Read, Update, Delete) operations on the tables. */

	// Set the DbSet properties with table names
	public DbSet<CompanyTable> Companies => Set<CompanyTable>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<JobTitle> JobTitles => Set<JobTitle>();
    public DbSet<EmployeeJobTitle> EmployeeJobTitles => Set<EmployeeJobTitle>();



	// Method to seed data into the database
	private void SeedData(ModelBuilder modelBuilder)
	{
		var companies = new List<CompanyTable>
		{
			new CompanyTable
			{
				Id = 1,
				Name = "Google",
				OrganizationNumber = "123ABC"
			},
			new CompanyTable
			{
				Id = 2,
				Name = "Apple",
				OrganizationNumber = "234ABC"
			}
		};

		// Add the list to the entity (added to migration)
		modelBuilder.Entity<CompanyTable>().HasData(companies);

		var departments = new List<Department>
		{
			new Department
			{
				Id = 1,
				Name = "Google Earth Department",
				CompanyId = 1
			},
			new Department
			{
				Id = 2,
				Name = "Iphone Department",
				CompanyId = 2
			}
		};

		// Add the list to the entity (added to migration)
		modelBuilder.Entity<Department>().HasData(departments);

		var employees = new List<Employee>
		{
			new Employee
			{
				Id = 1,
				FirstName = "Julia",
				LastName = "Johansson",
				DepartmentId = 2,
				Salary = 500000,
				isUnionMember = true
			},
			new Employee
			{
				Id = 2,
				FirstName = "Lukas",
				LastName = "Johansson",
				DepartmentId = 1,
				Salary = 500000,
				isUnionMember = false
			}
		};

		// Add the list to the entity (added to migration)
		modelBuilder.Entity<Employee>().HasData(employees);

		var jobTitles = new List<JobTitle>
		{
			new JobTitle
			{
				Id = 1,
				Name = "Software Engineer",
			},
			new JobTitle
			{
				Id = 2,
				Name = "Sales Manager",
			},

			new JobTitle
			{
				Id = 3,
				Name = "Customer Service Representative",
			},

			new JobTitle
			{
				Id = 4,
				Name = "Executive Assistant",
			}
		};

		// Add the list to the entity (added to migration)
		modelBuilder.Entity<JobTitle>().HasData(jobTitles);

		var employeeJobTitles = new List<EmployeeJobTitle>
		{
			new EmployeeJobTitle
			{
				EmployeeId = 2,
				JobTitleId = 3
			},
			new EmployeeJobTitle
			{
				EmployeeId = 2,
				JobTitleId = 2
			},

			new EmployeeJobTitle
			{
				EmployeeId = 2,
				JobTitleId = 1
			},
			new EmployeeJobTitle
			{
				EmployeeId = 1,
				JobTitleId = 3
			},
		};

		// Add the list to the entity (added to migration)
		modelBuilder.Entity<EmployeeJobTitle>().HasData(employeeJobTitles);

	}

}
