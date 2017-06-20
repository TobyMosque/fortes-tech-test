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
    internal static class AuthConsultas
    {
        private static readonly Func<Contexto, AsyncEnumerable<Grupo>> _GetGrupos = EF.CompileAsyncQuery((Contexto db) => db.Grupos);

        private static readonly Func<Contexto, int, Task<Grupo>> _GetGrupoById = EF.CompileAsyncQuery((Contexto db, int grupoId) => db.Grupos.FirstOrDefault(u => u.GrupoID == grupoId));

        private static readonly Func<Contexto, string, Task<Usuario>> _GetUsuarioByLogon = EF.CompileAsyncQuery((Contexto db, string logon) => db.Usuarios.FirstOrDefault(u => u.Logon == logon));

        internal static async Task<List<Grupo>> GetGrupos(this Contexto db)
        {
            return await _GetGrupos(db).ToListAsync();
        }

        internal static async Task<Usuario> GetUsuarioByLogon(this Contexto db, string logon)
        {
            return await _GetUsuarioByLogon(db, logon);
        }

        internal static async Task<Grupo> GetGrupoById(this Contexto db, int grupoId)
        {
            return await _GetGrupoById(db, grupoId);
        }
    }
}
