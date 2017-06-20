using Fortes.Core.Modelo.Entidades;
using Fortes.Core.Modelo.SqlServer;
using Fortes.Core.Web.Models.MovimentacaoModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.Core.Web.MovimentacaoConsultas
{
    internal static class Extensoes
    {
        private static readonly Func<Contexto, Guid, Guid, AsyncEnumerable<MovimentacaoViewModel>> _GetMovimentacoesByRecursoById = EF
            .CompileAsyncQuery((Contexto db, Guid recursoId, Guid usuarioId) => db.Movimentacoes
                .Include(r => r.Usuario)
                .Include(r => r.TipoMovimentacao)
                .Include(r => r.Recurso)
                .Where(r => r.RecursoID == recursoId)
                .Select(m => new MovimentacaoViewModel
                {
                    MovimentacaoID = m.MovimentacaoID,
                    RecursoID = m.RecursoID,
                    UsuarioID = m.UsuarioID,
                    TipoMovimentacaoID = m.TipoMovimentacaoID,
                    Quantidade = m.Quantidade,
                    Total = m.Recurso.Quantidade,
                    TipoDescricao = m.TipoMovimentacao.Descricao,
                    UsuarioNome = m.Usuario.Logon,
                    IsOwner = m.UsuarioID == usuarioId
                }));

        private static readonly Func<Contexto, Guid, Task<bool>> _GetRecursoExists = EF
            .CompileAsyncQuery((Contexto db, Guid recursoId) => db.Recursos
                .Any(x => x.RecursoID == recursoId));

        private static readonly Func<Contexto, Guid, Task<Recurso>> _GetGetRecursoById = EF
            .CompileAsyncQuery((Contexto db, Guid recursoId) => db.Recursos.Find(recursoId));

        private static readonly Func<Contexto, Guid, Task<Movimentacao>> _GetMovimentacaoById = EF
            .CompileAsyncQuery((Contexto db, Guid movimentacaoId) => db.Movimentacoes.Find(movimentacaoId));

        internal static async Task<List<MovimentacaoViewModel>> GetMovimentacoesByRecursoById(this Contexto db, Guid recursoId, Guid usuarioId)
        {
            return await _GetMovimentacoesByRecursoById(db, recursoId, usuarioId).ToListAsync();
        }

        internal static async Task<bool> GetRecursoExists(this Contexto db, Guid recursoId)
        {
            return await _GetRecursoExists(db, recursoId);
        }

        internal static async Task<Recurso> GetRecursoById(this Contexto db, Guid recursoId)
        {
            return await _GetGetRecursoById(db, recursoId);
        }

        internal static async Task<Movimentacao> GetGetMovimentacaoById(this Contexto db, Guid movimentacaoId)
        {
            return await _GetMovimentacaoById(db, movimentacaoId);
        }

        
    }
}
