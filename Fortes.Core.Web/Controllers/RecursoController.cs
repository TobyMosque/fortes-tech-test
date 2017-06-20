using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fortes.Core.Web.RecursoConsultas;
using Microsoft.AspNetCore.Http;
using Fortes.Core.Web.Models.RecursoModels;
using Fortes.Core.Modelo.Entidades.Enumeradores;
using Fortes.Core.Web.UsuarioConsultas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fortes.Core.Web.Controllers
{
    [Route("api/[controller]")]
    public class RecursoController : Controller
    {
        [HttpGet]
        public async Task<List<RecursoViewModel>> Get()
        {
            return await db.GetRecursos();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RecursoViewModel model)
        {
            var erros = this.GetError();
            if (erros != string.Empty)
                return StatusCode(StatusCodes.Status400BadRequest, erros);

            model.RecursoID = Guid.NewGuid();
            var sessao = await db.GetSessao(db.SessaoID.Value);            

            var recurso = new Modelo.Entidades.Recurso();
            recurso.RecursoID = model.RecursoID.Value;
            recurso.Descricao = model.Descricao;
            recurso.Observacao = model.Observacao;
            recurso.Quantidade = model.Quantidade;

            var movimentacao = new Modelo.Entidades.Movimentacao();
            movimentacao.MovimentacaoID = Guid.NewGuid();
            movimentacao.RecursoID = model.RecursoID.Value;
            movimentacao.UsuarioID = sessao.UsuarioID;
            movimentacao.TipoMovimentacaoID = Modelo.Entidades.Enumeradores.TipoMovimentacao.Entrada;
            movimentacao.Quantidade = model.Quantidade;

            await db.Recursos.AddAsync(recurso);
            await db.Movimentacoes.AddAsync(movimentacao);

            await db.SaveChangesAsync();
            return Ok(model.RecursoID);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RecursoViewModel model)
        {
            var recurso = db.Recursos.Find(model.RecursoID);
            if (recurso == null)
                ModelState.AddModelError("RecursoID", "Recurso não encontrado");

            var erros = this.GetError();
            if (erros != string.Empty)
                return StatusCode(StatusCodes.Status400BadRequest, erros);

            var sessao = await db.Sessoes.Include(x => x.Usuario)
                .FirstOrDefaultAsync(u => u.SessaoID == db.SessaoID);

            var diff = model.Quantidade - recurso.Quantidade;
            recurso.Descricao = model.Descricao;
            recurso.Observacao = model.Observacao;
            recurso.Quantidade = model.Quantidade;

            if (diff != 0)
            {
                var movimentacao = new Modelo.Entidades.Movimentacao();
                movimentacao.MovimentacaoID = Guid.NewGuid();
                movimentacao.RecursoID = model.RecursoID.Value;
                movimentacao.UsuarioID = sessao.UsuarioID;
                movimentacao.TipoMovimentacaoID = diff > 0 ? TipoMovimentacao.Entrada : TipoMovimentacao.Saida;
                movimentacao.Quantidade = Math.Abs(diff);
                await db.Movimentacoes.AddAsync(movimentacao);
            }

            await db.SaveChangesAsync();
            return Ok(model.RecursoID);
        }

        [HttpDelete]
        [Route("{recursoId}")]
        public async Task<IActionResult> Delete(Guid recursoId)
        {
            var recurso = db.GetRecursoById(recursoId);
            if (recurso == null)
            {
                ModelState.AddModelError("RecursoID", "Recurso não encontrado");
                var erros = this.GetError();
                if (erros != string.Empty)
                    return StatusCode(StatusCodes.Status400BadRequest, erros);
            }

            db.Recursos.Remove(recurso);
            await db.SaveChangesAsync();
            return Ok(recursoId);
        }
    }
}
