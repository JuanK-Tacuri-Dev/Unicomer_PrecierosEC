using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Service;
using PrecierosEC.Infraestructure.Repositories;

namespace PrecierosEC.APi.Extensions
{
    public static class ExtencionApi
    {
        public static void AddDependecyInjections(this IServiceCollection services)
        {
            
            services.AddControllers();
            services.AddScoped<IServiceErrorLog, ServiceErrorLog>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
        }
    }
}
