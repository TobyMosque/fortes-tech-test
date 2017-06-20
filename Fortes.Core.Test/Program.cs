using Fortes.Core.Modelo.Utils;
using Microsoft.EntityFrameworkCore;

namespace Fortes.Core.Console.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new ContextoFactory().Create())
            {
                contexto.Database.EnsureDeleted();
                contexto.Database.Migrate();
                contexto.Seed();
            }
        }
    }
}