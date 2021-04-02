using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using mssqltest1.Controllers;

namespace mssqltest1
{
    // Get connection string from secrets
    // https://phoenixnap.com/kb/helm-environment-variables

    // To establish proxy for MSSQL Server, run
    // kubectl port-forward service/mssql-headless 1433:1433 --namespace incomm-poc

    // Scaffold a controller
    // dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    // dotnet add package Microsoft.EntityFrameworkCore.Design
    // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    // dotnet tool install -g dotnet-aspnet-codegenerator
    // dotnet aspnet-codegenerator controller -name TodoItemsController -async -api -m TodoItem -dc TodoContext -outDir Controllers    

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<HealthCheck>("health_check");
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "mssqltest1", Version = "v1" });
            });

            services.AddDbContext<CustomerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CustomerContext")));
            services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeContext")));
                        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mssqltest1 v1"));
            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
