using Dev.Temp.Model.Empresas;
using Dev.Temp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dev.Temp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var services = new ServiceCollection();

            services.AddDbContext<DbContext, SampleDbContext>(
                options => options
                    .UseLazyLoadingProxies()
                    .UseMongoDb(mb => mb.UseMongoUrlBuilder(ub => 
                    {
                        ub.Parse("mongodb://localhost:27018,localhost:27019,localhost:27020/?replicaSet=rs1");
                        ub.DatabaseName= "dev";
                    })));

            var provider = services.BuildServiceProvider();

            using (IServiceScope serviceScope = provider.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<DbContext>();

                var empresas = db.Set<Empresa>();

                foreach (var empresa in empresas)
                {
                    Console.WriteLine(empresa.ToString());
                }
            }
        }
    }
}
