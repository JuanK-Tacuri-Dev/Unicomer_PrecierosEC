using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PrecierosEC.Core.Interface;
using PrecierosEC.Core.Utiliies;


namespace PrecierosEC.Core.Extensions
{
    public static class ProgramExtensions
    {

        public static void AddCorsProgram(this IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

        }
        public static void SetAppsetings(this IServiceCollection services, IConfiguration configuration)
        {
            ApplicationService.Configure(services, configuration);
        }

 

        public static void SwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc(AppConfiguration.NameApi_Name, new OpenApiInfo
                {
                    Title = AppConfiguration.NameApi_Title,
                    Version = AppConfiguration.NameApi_Version,
                    Description = AppConfiguration.NameApi_Description,
                    TermsOfService = new Uri(AppConfiguration.NameApi_TermsOfService),
                    Contact = new OpenApiContact
                    {
                        Name = AppConfiguration.NameApi_ContactName,
                        Email = AppConfiguration.NameApi_ContactEmail,
                        Url = new Uri(AppConfiguration.NameApi_ContactUrl),
                    }
                });

            });


        }

    }

}
