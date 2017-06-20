using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fortes.Core.Web.Consultas;
using Microsoft.AspNetCore.Http;
using Fortes.Core.Web.Models.RecursoModels;
using Fortes.Core.Modelo.Entidades.Enumeradores;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fortes.Core.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var sessao = await db.Sessoes.FindAsync(db.SessaoID);
            sessao.IsActive = false;
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
