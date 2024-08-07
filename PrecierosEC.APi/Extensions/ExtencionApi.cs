using PrecierosEC.Core.Extensions;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Repositories;
using PrecierosEC.Core.Service;

namespace PrecierosEC.APi.Extensions
{
    public static class ExtencionApi
    {
        public static void AddDependecyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddControllers();
            services.AddScoped<IServiceErrorLog, ServiceErrorLog>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
