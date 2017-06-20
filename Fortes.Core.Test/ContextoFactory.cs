using Fortes.Core.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;

namespace Fortes.Core.Console.Migrations
{
    internal static class Configuration
    {
        private static IConfigurationRoot Root { get; set; }
        static Configuration()
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json");
            Configuration.Root = builder.Build();
        }

        public static Guid? SessaoID
        {
            get
            {
                var config = Configuration.Root["sessaoId"];
                if (string.IsNullOrWhiteSpace(config))
                    return default(Guid?);
                return Guid.Parse(config);
            }
        }

        public static int GrupoID { get { return Int32.Parse(Configuration.Root["grupoId"]); } }

        public static string SqlServerConnection { get { return Configuration.Root["connectionStrings:sqlServer"]; } }
        public static string PostgreSqlConnection { get { return Configuration.Root["connectionStrings:postgreSql"]; } }
    }

    public class ContextoFactory : IDbContextFactory<Modelo.SqlServer.Contexto>
    {
        public Modelo.SqlServer.Contexto Create(params string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Contexto>();
            optionsBuilder.UseSqlServer(Configuration.SqlServerConnection);
            return new Modelo.SqlServer.Contexto(optionsBuilder.Options, Configuration.SessaoID, Configuration.GrupoID);
        }
    }

    //public class ContextoFactory : IDbContextFactory<Modelo.PostgreSql.Contexto>
    //{
    //    public Modelo.PostgreSql.Contexto Create(params string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<Contexto>();
    //        optionsBuilder.UseNpgsql(Configuration.PostgreSqlConnection);
    //        return new Modelo.PostgreSql.Contexto(optionsBuilder.Options, Configuration.SessaoID, Configuration.GrupoID);
    //    }
    //}
}
