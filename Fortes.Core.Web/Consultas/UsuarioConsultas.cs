using Fortes.Core.Modelo.Entidades;
using Fortes.Core.Modelo.SqlServer;
using Fortes.Core.Web.Models.MovimentacaoModels;
using Fortes.Core.Web.Models.UsuarioModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.Core.Web.UsuarioConsultas
{
    internal static class Extensoes
    {
        private static readonly Func<Contexto, Guid, Task<Sessao>> _GetSessao = EF
            .CompileAsyncQuery((Contexto db, Guid sessaoId) => db.Sessoes
                .Include(s => s.Usuario)
                .FirstOrDefault(x => x.SessaoID == sessaoId));

        private static readonly Func<Contexto, Guid, Task<UsuarioViewModel>> _GetGetUsuarioBySessaoId = EF
            .CompileAsyncQuery((Contexto db, Guid sessaoId) => db.Sessoes
                .Include(s => s.Usuario)
                .Select(x => new UsuarioViewModel { UsuarioID = x.UsuarioID, Logon = x.Usuario.Logon })
                .FirstOrDefault();

        internal static async Task<Sessao> GetSessao(this Contexto db, Guid sessaoId)
        {
            return await _GetSessao(db, sessaoId);
        }

        internal static async Task<UsuarioViewModel> GetUsuarioBySessaoId(this Contexto db, Guid sessaoId)
        {
            return await _GetGetUsuarioBySessaoId(db, sessaoId);
        }
    }
}
