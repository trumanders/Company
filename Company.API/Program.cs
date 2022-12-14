using Company.Common.DTOs;
using Company.Data.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        /* Add the necessary service for handling database calls to the SQL
        Server using the CourseContext and the connection string. */
        builder.Services.AddDbContext<CompanyContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("CompanyConnection")));  // "CompanyConnection" was set in appsettings.json


        // Call method to configure automapper as a service
        ConfigureAutomapper(builder.Services);

        // Register service
        RegisterServices(builder.Services);


        var app = builder.Build();


        /* MIDDLE-WARE */

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Security BEFORE mapping controllers!
        app.UseAuthorization();

        // Map URL to controllers
        app.MapControllers();

        app.Run();
        

        // Configure AutoMapper and "REGISTER" it as a service.
        // Register mappings between entity > DTO (CreateMap) and DTO -> entity (ReverseMap)
        void ConfigureAutomapper(IServiceCollection services)
        {
            // Create configuration object and specify what configurations
            // automapper can do.
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyTable, CompanyTableDTO>().ReverseMap();
                cfg.CreateMap<Department, DepartmentDTO>().ReverseMap();
                cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap();
                cfg.CreateMap<JobTitle, JobTitleDTO>().ReverseMap();
                cfg.CreateMap<EmployeeJobTitle, EmployeeJobTitleDTO>().ReverseMap();
            });


            // config is of type MapperConfiguration
            // mapper is of type IMapper
            var mapper = config.CreateMapper();

            // Register the mapper as service
            // services is of type IServiceCollection
            services.AddSingleton(mapper);            
        }


        // Method to register a service to live for the entire
        // HTTP request using the method AddScoped
        void RegisterServices(IServiceCollection services)
        {
            // Create new instance on every request
            services.AddScoped<IDbService, DbService>();
        }

        // 
    }
}