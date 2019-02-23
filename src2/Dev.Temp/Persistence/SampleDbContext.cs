using Dev.Temp.Model.Empresas;
using Microsoft.EntityFrameworkCore;

namespace Dev.Temp.Persistence
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>(b =>
            {
                b.HasAnnotation("DbName", "Empresa");
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).HasAnnotation("DbName", "_id");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
