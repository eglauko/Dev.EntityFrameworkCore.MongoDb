using Dev.Temp.Model.Empresas;
using Dev.Temp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Dev.Temp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            PureMongo();

            var services = new ServiceCollection();

            services.AddDbContext<DbContext, SampleDbContext>(
                options => options
                    .UseLazyLoadingProxies()
                    .UseMongoDb(mb => mb.UseMongoUrlBuilder(ub => 
                    {
                        ub.Parse("mongodb://localhost:27018,localhost:27019,localhost:27020/?replicaSet=rs1");
                        ub.DatabaseName= "admin";
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

            Console.ReadKey();
        }

        private static void PureMongo()
        {
            Console.WriteLine("PureMongo");

            var url = new MongoUrl("mongodb://localhost:27018,localhost:27019,localhost:27020/?replicaSet=rs1");

            var empresaMap = new BsonClassMap<Empresa>();
            BsonClassMap.RegisterClassMap(empresaMap);
            empresaMap.MapIdProperty(nameof(Empresa.Id)).SetElementName("_id");
            empresaMap.MapProperty(nameof(Empresa.Nome));
            empresaMap.MapProperty(nameof(Empresa.Apelido)).SetElementName("NomeFantasia");
            

            var client = new MongoClient(url);
            var db = client.GetDatabase("admin");

            var coll = db.GetCollection<BsonDocument>("Empresa");
            
            Console.WriteLine(coll.AsQueryable().ToString());
            coll.AsQueryable().ToList().ForEach(Console.WriteLine);

            Console.WriteLine(coll.AsQueryable().OfType<Empresa>().ToString());
            coll.AsQueryable().OfType<Empresa>().ToList().ForEach(Console.WriteLine);

            var queryBase = db.GetCollection<Empresa>("Empresa").AsQueryable();

            Console.WriteLine(queryBase.ToString());

            foreach (var empresa in queryBase)
            {
                Console.WriteLine(empresa.ToString());
            }

            var queryFiltred = queryBase.Where(e => e.Id == 1);

            Console.WriteLine(queryFiltred.ToString());

            foreach (var empresa in queryFiltred)
            {
                Console.WriteLine(empresa.ToString());
            }
        }
    }
}
