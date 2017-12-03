using System;
using AutoMapper;
using Employees.Data;
using Employees.Services;
using Microsoft.EntityFrameworkCore;

namespace Employees.App
{
    using Microsoft.Extensions.DependencyInjection;
    public class StartUp
    {
        static void Main()
        {
            var serviceProvider = ConfigureServices();
            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        static IServiceProvider ConfigureServices()
        {
            var serviceCollection=new ServiceCollection();
            serviceCollection.AddDbContext<EmployeesContext>(options =>
                options.UseSqlServer(ServerConfiguration.ConnectionString));

            serviceCollection.AddTransient<EmployeeService>();

            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<AutomapperProfile>());
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
