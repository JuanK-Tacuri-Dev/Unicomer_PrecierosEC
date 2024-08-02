using Microsoft.EntityFrameworkCore;
using PrecierosEC.Core.Utiliies;

namespace PrecierosEC.Infraestructure.Data
{
    public partial class Context : DbContext
    {

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {

            }
                //optionsBuilder.UseSqlServer(AppConfiguration.ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            OnModelCreatingPartialModelDb(modelBuilder);
        }
        partial void OnModelCreatingPartialModelDb(ModelBuilder modelBuilder);


    }
}
