using FluentValidation;
using Medicar.Data;
using Medicar.Domain.Commands;
using Medicar.Domain.Interfaces.Repository;
using Medicar.Infra.Data;
using Medicar.Infra.Data.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Medicar.Configuration
{
    public static class DependencyInjection
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            services.AddScoped<MedicarDbContext>();
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<MedicarDbContext>();


            services.AddScoped<IIdentityManager, IdentityManager>();

            services.AddTransient<IMedicoRepository, MedicoRepository>();
            services.AddTransient<IAgendaRepository, AgendaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigurationSwaggerOptions>();

            services.AddTransient<IValidator<CriarAgendaCommand>, CriarAgendaCommandValidator>();
            services.AddTransient<IValidator<CriarMedicoCommand>, CriarMedicoCommandValidator>();
        }
    }
}
