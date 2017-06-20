using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fortes.Core.Web.Controllers
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        private static IConfigurationRoot Root { get; set; }
        static Controller()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");
            Controller.Root = builder.Build();
        }

        public static string SqlServerConnection { get { return Controller.Root["connectionStrings:sqlServer"]; } }
        public static string PostgreSqlConnection { get { return Controller.Root["connectionStrings:postgreSql"]; } }

        protected Modelo.SqlServer.Contexto db { get; private set; }
        private Guid? SessaoID { get; set; }
        private int GrupoID { get; set; }

        private Modelo.SqlServer.Contexto CreateContexto(params string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Modelo.SqlServer.Contexto>();
            optionsBuilder.UseSqlServer(Controller.SqlServerConnection);
            return new Modelo.SqlServer.Contexto(optionsBuilder.Options, this.SessaoID, this.GrupoID);
        }

        public string GetError()
        {
            var mensagem = string.Empty;
            if (!ModelState.IsValid)
            {
                mensagem = String.Join("<br />" + Environment.NewLine, ModelState.Select(m => {
                    return String.Join("<br />" + Environment.NewLine, m.Value.Errors.Select(e => $"{m.Key} => {e.ErrorMessage}"));
                }));
            }
            return mensagem;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var values = default(StringValues);
            var headers = context.HttpContext.Request.Headers;
            if (headers.TryGetValue("token", out values))
            {   
                var token = Convert.FromBase64String(values.First());
                if (token.Length == 64)
                {
                    this.db = this.CreateContexto();
                    var sessao = this.db.Sessoes.FirstOrDefault(s => s.Token == token && s.IsActive);
                    if (sessao != null)
                    {
                        this.db.SessaoID = sessao.SessaoID;
                        this.db.GrupoID = sessao.GrupoID;
                        return;
                    }
                }
            }
            context.Result = StatusCode(StatusCodes.Status401Unauthorized);
        }
     

        protected override void Dispose(bool disposing)
        {
            if (this.db != null)
                this.db.Dispose();
        }
    }
}
