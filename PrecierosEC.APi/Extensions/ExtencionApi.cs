using PrecierosEC.Core.Interface;
using PrecierosEC.Core.Utiliies;
using PrecierosEC.Infraestructure;

namespace PrecierosEC.APi.Extensions
{
    public static class ExtencionApi
    {
        public static void AddDependecyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddControllers();
            //services.AddScoped<ISqlServiceErrorLog, SqlServiceErrorLog>();
            services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
