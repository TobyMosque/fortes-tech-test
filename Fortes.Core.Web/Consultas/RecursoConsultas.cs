using Fortes.Core.Modelo.Entidades;
using Fortes.Core.Modelo.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.Core.Web.Consultas
{
    internal static class RecursoConsultas
    {
        private static readonly Func<Contexto, AsyncEnumerable<Recurso>> _GetRecursos = EF.CompileAsyncQuery((Contexto db) => db.Recursos);

        private static readonly Func<Contexto, Guid, Task<Recurso>> _GetRecursoById = EF.CompileAsyncQuery((Contexto db, Guid recursoId) => db.Recursos.Include(x => x.Movimentacoes).FirstOrDefault(r => r.RecursoID == recursoId));

        internal static async Task<List<Recurso>> GetRecursos(this Contexto db)
        {
            return await _GetRecursos(db).ToListAsync();
        }

        internal static async Task<Recurso> GetRecursoById(this Contexto db, Guid recursoId)
        {
            return await _GetRecursoById(db, recursoId);
        }
    }
}
