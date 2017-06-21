using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Fortes.Core.Web.Models.RecursoModels;
using Fortes.Core.Modelo.Entidades.Enumeradores;
using Fortes.Core.Web.UsuarioConsultas;
using Fortes.Core.Web.Models.UsuarioModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fortes.Core.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuario = await db.GetUsuarioBySessaoId(db.SessaoID.Value);
            return Ok(usuario);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var sessao = await db.GetSessao(db.SessaoID.Value);
            sessao.IsActive = false;
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
