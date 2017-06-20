using Fortes.Core.Modelo.Entidades;
using Fortes.Core.Modelo.SqlServer;
using Fortes.Core.Web.Models.RecursoModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.Core.Web.RecursoConsultas
{
    internal static class Extensoes
    {
        private static readonly Func<Contexto, AsyncEnumerable<RecursoViewModel>> _GetRecursos = EF
            .CompileAsyncQuery((Contexto db) => db.Recursos
                .Select(r => new RecursoViewModel
                {
                    Descricao = r.Descricao,
                    Observacao = r.Observacao,
                    Quantidade = r.Quantidade,
                    RecursoID = r.RecursoID
                }));

        private static readonly Func<Contexto, Guid, Task<Recurso>> _GetRecursoById = EF
            .CompileAsyncQuery((Contexto db, Guid recursoId) => db.Recursos.Find(recursoId));

        internal static async Task<List<RecursoViewModel>> GetRecursos(this Contexto db)
        {
            return await _GetRecursos(db).ToListAsync();
        }

        internal static async Task<Recurso> GetRecursoById(this Contexto db, Guid recursoId)
        {
            return await _GetRecursoById(db, recursoId);
        }
    }
}
