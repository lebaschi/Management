using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using InstallerManagement.Data;
using InstallerManagement.Repositories;
using InstallerManagement.Services;
using System.Text.Json.Serialization;
using Serilog;

namespace InstallerManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)

        {
            Log.Logger = new LoggerConfiguration()
             .WriteTo.Console()
             .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog();
            });

            services.AddDbContext<InstallerManagementDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AWSDatabaseConnection")));

            services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
            services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                });

            // Configure services related to Installers
            services.AddScoped<IInstallerRepository, InstallerRepository>();
            services.AddScoped<IInstallerService, InstallerService>();

            // Configure services related to Supervisors
            services.AddScoped<ISupervisorRepository, SupervisorRepository>();
            services.AddScoped<ISupervisorService, SupervisorService>();

            // Other services and configurations
            services.AddScoped<IInstallerSupervisorService, InstallerSupervisorService>();
            services.AddScoped<IInstallerSupervisorRepository, InstallerSupervisorRepository>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(builder => builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
