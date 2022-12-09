using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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
	}
}
