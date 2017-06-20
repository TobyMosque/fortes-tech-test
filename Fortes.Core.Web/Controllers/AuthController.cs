using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Fortes.Core.Modelo.Utils;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Fortes.Core.Modelo;
using Fortes.Core.Web.AuthConsultas;
using Fortes.Core.Web.Models.AuthModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fortes.Core.Web.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet]
        [Route("grupos")]
        public async Task<List<Modelo.Entidades.Grupo>> GetGrupos()
        {
            return await db.GetGrupos();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginSignupViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Logon) || string.IsNullOrWhiteSpace(model.Senha))
                return StatusCode(StatusCodes.Status400BadRequest, "Logon e Senha não podem ser vazios");

            this.db.GrupoID = model.GrupoId;
            var user = await db.GetUsuarioByLogon(model.Logon);
            if (user == null || !user.VerificarSenha(model.Senha))
                return StatusCode(StatusCodes.Status400BadRequest, "Usuario não encontrado ou senha não confere");

            var sessao = user.GerarSessao();
            await this.db.Sessoes.AddAsync(sessao);
            await this.db.SaveChangesAsync();

            return Ok(Convert.ToBase64String(sessao.Token));
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] LoginSignupViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Logon) || string.IsNullOrWhiteSpace(model.Senha))
                return StatusCode(StatusCodes.Status400BadRequest, "Logon e Senha não podem ser vazios");

            var grupo = await db.GetGrupoById(model.GrupoId);
            if (grupo == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Grupo não encontrado");

            this.db.GrupoID = model.GrupoId;

            var t1 = await db.Usuarios.FirstOrDefaultAsync(u => u.Logon == model.Logon);
            var user = await db.GetUsuarioByLogon(model.Logon);
            if (user != null)
                return StatusCode(StatusCodes.Status400BadRequest, "Logon está em uso por outro usuario");

            user = new Modelo.Entidades.Usuario();
            user.UsuarioID = Guid.NewGuid();
            user.Logon = model.Logon;
            user.GrupoID = model.GrupoId;
            user.CadastrarSenha(model.Senha);
            var sessao = user.GerarSessao();

            await this.db.Usuarios.AddAsync(user);
            await this.db.Sessoes.AddAsync(sessao);
            await this.db.SaveChangesAsync();

            return Ok(Convert.ToBase64String(sessao.Token));
        }


        private Modelo.SqlServer.Contexto db = CreateContexto();
        protected override void Dispose(bool disposing)
        {
            if (this.db != null)
                this.db.Dispose();
        }


        private static IConfigurationRoot Root { get; set; }
        static AuthController()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");
            AuthController.Root = builder.Build();
        }

        public static string SqlServerConnection { get { return AuthController.Root["connectionStrings:sqlServer"]; } }
        public static string PostgreSqlConnection { get { return AuthController.Root["connectionStrings:postgreSql"]; } }

        private static Modelo.SqlServer.Contexto CreateContexto()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Modelo.SqlServer.Contexto>();
            optionsBuilder.UseSqlServer(Controller.SqlServerConnection);
            return new Modelo.SqlServer.Contexto(optionsBuilder.Options);
        }
    }
}
