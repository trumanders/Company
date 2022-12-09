﻿
namespace Company.Data.Entities;

public class Department : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CompanyId { get; set; }

    // Enable eager loading. All data from the company table is now available in this object, via the CompanyId property.
    public Company? Company { get; set; } 
}
