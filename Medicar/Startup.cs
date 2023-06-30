using FluentValidation.AspNetCore;
using Medicar.Configuration;
using Medicar.Domain;
using Medicar.Infra.Data;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Medicar
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
            // Add services to the container.
            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

            //Entity
            var settings = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            Variables.DefaultConnection = settings.DefaultConnection;

            services.AddDbContext<MedicarDbContext>(options =>
            {
                options.UseNpgsql(settings.DefaultConnection);
            });

            //Identity
            services.AddIdentityConfiguration(Configuration);

            services.AddCommands();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            //Json Options
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddSwaggerGen();

            services.AddControllersWithViews()
                .AddFluentValidation();

            services.AddCorsConfig();
            //Dependency Injection
            services.AddDIConfiguration();

            services.AddSwaggerConfig();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
