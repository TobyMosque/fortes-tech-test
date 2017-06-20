using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fortes.Core.Web.MovimentacaoConsultas;
using Microsoft.AspNetCore.Http;
using Fortes.Core.Web.Models.RecursoModels;
using Fortes.Core.Modelo.Entidades.Enumeradores;
using Fortes.Core.Web.Models.MovimentacaoModels;
using Fortes.Core.Web.UsuarioConsultas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fortes.Core.Web.Controllers
{
    [Route("api/[controller]")]
    public class MovimentacaoController : Controller
    {
        [HttpGet]
        [Route("{recursoId}")]
        public async Task<IActionResult> Get(Guid recursoId)
        {
            var recursoExists = await db.GetRecursoExists(recursoId);
            if (!recursoExists)
                return NotFound();

            var usuario = await db.GetUsuarioBySessaoId(db.SessaoID.Value);
            var movimentacoes = await db.GetMovimentacoesByRecursoById(recursoId, usuario.UsuarioID);
            
            return Ok(new {
                Usuario = usuario,
                Movimentacoes = movimentacoes
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovimentacaoViewModel model)
        {
            var recurso = await db.GetRecursoById(model.RecursoID);
            if (recurso == null)
                ModelState.AddModelError("RecursoID", "Recurso não encontrado");
            else if (model.TipoMovimentacaoID == TipoMovimentacao.Saida && model.Quantidade > recurso.Quantidade)
                ModelState.AddModelError("Quantidade", "Não é possivel realizar uma saida superior ao estoque deste recurso");
            
            var erros = this.GetError();
            if (erros != string.Empty)
                return StatusCode(StatusCodes.Status400BadRequest, erros);

            var usuario = await db.GetUsuarioBySessaoId(model.RecursoID);
            switch (model.TipoMovimentacaoID)
            {
                case TipoMovimentacao.Entrada:
                    recurso.Quantidade += model.Quantidade;
                    break;
                case TipoMovimentacao.Saida:
                    recurso.Quantidade -= model.Quantidade;
                    break;
            }
            
            var movimentacao = new Modelo.Entidades.Movimentacao();
            movimentacao.MovimentacaoID = Guid.NewGuid();
            movimentacao.RecursoID = recurso.RecursoID;
            movimentacao.UsuarioID = usuario.UsuarioID;
            movimentacao.TipoMovimentacaoID = model.TipoMovimentacaoID;
            movimentacao.Quantidade = model.Quantidade;
            
            await db.Movimentacoes.AddAsync(movimentacao);
            await db.SaveChangesAsync();
            return Ok(model.MovimentacaoID);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MovimentacaoViewModel model)
        {
            var usuario = await db.GetUsuarioBySessaoId(model.RecursoID);
            var movimentacao = await db.GetGetMovimentacaoById(model.MovimentacaoID);
            if (movimentacao == null)
                ModelState.AddModelError("MovimentacaoID", "Movimentacao não encontrada");

            var diff = 0;
            if (model.TipoMovimentacaoID == movimentacao.TipoMovimentacaoID)
            {
                switch (model.TipoMovimentacaoID)
                {
                    case TipoMovimentacao.Entrada:
                        diff = model.Quantidade - movimentacao.Quantidade;
                        break;
                    case TipoMovimentacao.Saida:
                        diff = movimentacao.Quantidade - model.Quantidade;
                        break;
                }
            }
            else
            {
                diff = model.Quantidade + movimentacao.Quantidade;
                if (model.TipoMovimentacaoID == TipoMovimentacao.Saida)
                    diff *= -1;
            }
            var quantidade = Math.Abs(diff);

            if (diff < 0 && quantidade > movimentacao.Recurso.Quantidade)
                ModelState.AddModelError("Quantidade", "Não é possivel realizar uma saida superior ao estoque deste recurso");

            if (usuario.UsuarioID != movimentacao.UsuarioID)
                ModelState.AddModelError("UsuarioID", "Não é possivel alterar uma movimentação realizada por outro usuario");

            var erros = this.GetError();
            if (erros != string.Empty)
                return StatusCode(StatusCodes.Status400BadRequest, erros);

            movimentacao.Recurso.Quantidade += diff;
            movimentacao.Quantidade = model.Quantidade;
            movimentacao.TipoMovimentacaoID = model.TipoMovimentacaoID;

            await db.SaveChangesAsync();
            return Ok(model.MovimentacaoID);
        }

        [HttpDelete]
        [Route("{recursoId}")]
        public async Task<IActionResult> Delete(Guid movimentacaoId)
        {
            var movimentacao = await db.GetGetMovimentacaoById(movimentacaoId);
            if (movimentacao == null)
            {
                ModelState.AddModelError("RecursoID", "Movimentação não encontrado");
                var erros = this.GetError();
                if (erros != string.Empty)
                    return StatusCode(StatusCodes.Status400BadRequest, erros);
            }

            db.Movimentacoes.Remove(movimentacao);
            await db.SaveChangesAsync();
            return Ok(movimentacaoId);
        }
    }
}
