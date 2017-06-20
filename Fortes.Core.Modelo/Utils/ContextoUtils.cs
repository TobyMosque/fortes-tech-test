using Fortes.Core.Modelo.Entidades.Enumeradores;
using Fortes.Core.Modelo.Historicos.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fortes.Core.Modelo.Utils
{
    public static class ContextoUtils
    {
        public static void Seed(this Contexto db)
        {
            db.SeedGrupos();
            db.SeedTiposHistorico();
            db.SeedTiposMovimentacao();
            db.SeedUsuarioAdmin();
            db.SaveChanges();
        }

        private static void SeedUsuarioAdmin(this Contexto db)
        {
            var usuarioId = Guid.Parse("{30EA0242-4937-4C2D-8BE4-EA9FA4B2A97E}");
            var usuario = db.Usuarios.Where(x => x.UsuarioID == usuarioId).FirstOrDefault();
            if (usuario == null)
            {
                usuario = new Entidades.Usuario();
                usuario.UsuarioID = usuarioId;
                usuario.Logon = "admin";
                usuario.CadastrarSenha("H3ll0@W0rld");

                db.Usuarios.Add(usuario);
            }
        }

        private static void SeedGrupos(this Contexto db)
        {
            var grupos = new Entidades.Grupo[]
            {
                new Entidades.Grupo { GrupoID = db.GrupoID, Descricao = "Grupo 01" },
                new Entidades.Grupo { GrupoID = 2, Descricao = "Grupo 02" },
                new Entidades.Grupo { GrupoID = 3, Descricao = "Grupo 03" },
                new Entidades.Grupo { GrupoID = 4, Descricao = "Grupo 04" },
            };

            var gruposId = grupos.Select(x => x.GrupoID).ToList();
            var dbGrupos = db.Grupos.Where(x => gruposId.Contains(x.GrupoID)).ToList();
            grupos = (
                from dsGrupo in grupos
                join dbGrupo in dbGrupos on dsGrupo.GrupoID equals dbGrupo.GrupoID into left
                from dbGrupo in left.DefaultIfEmpty()
                where dbGrupo == null
                select dsGrupo).ToArray();

            db.Grupos.AddRange(grupos);
        }

        private static void SeedTiposMovimentacao(this Contexto db)
        {
            var tiposMovimentacaoId = Enum.GetValues(typeof(TipoMovimentacao)).Cast<TipoMovimentacao>().ToArray();
            var tiposMovimentacao = tiposMovimentacaoId.Select(tipo => {
                var tipoMovimentacao = new Entidades.Dominio.TipoMovimentacao();
                tipoMovimentacao.TipoMovimentacaoID = tipo;
                tipoMovimentacao.Descricao = Enum.GetName(typeof(TipoMovimentacao), tipo);
                return tipoMovimentacao;
            }).ToArray();

            var dbTiposMovimentacao = db.TiposMovimentacao.Where(x => tiposMovimentacaoId.Contains(x.TipoMovimentacaoID)).ToList();
            tiposMovimentacao = (
                from dsTipoMovimentacao in tiposMovimentacao
                join dbTipoMovimentacao in dbTiposMovimentacao on dsTipoMovimentacao.TipoMovimentacaoID equals dbTipoMovimentacao.TipoMovimentacaoID into left
                from dbTipoMovimentacao in left.DefaultIfEmpty()
                where dbTipoMovimentacao == null
                select dsTipoMovimentacao).ToArray();

            db.TiposMovimentacao.AddRange(tiposMovimentacao);
        }

        private static void SeedTiposHistorico(this Contexto db)
        {
            var tiposHistoricoId = Enum.GetValues(typeof(TipoHistorico)).Cast<TipoHistorico>().ToArray();
            var tiposHistorico = tiposHistoricoId.Select(tipo => {
                var tipoHistorico = new Historicos.Dominio.TipoHistorico();
                tipoHistorico.TipoHistoricoID = tipo;
                tipoHistorico.Descricao = Enum.GetName(typeof(TipoHistorico), tipo);
                return tipoHistorico;
            }).ToArray();

            var dbTiposHistorico = db.TiposHistorico.Where(x => tiposHistoricoId.Contains(x.TipoHistoricoID)).ToList();
            tiposHistorico = (
                from dsTipoHistorico in tiposHistorico
                join dbTipoHistorico in dbTiposHistorico on dsTipoHistorico.TipoHistoricoID equals dbTipoHistorico.TipoHistoricoID into left
                from dbTipoHistorico in left.DefaultIfEmpty()
                where dbTipoHistorico == null
                select dsTipoHistorico).ToArray();

            db.TiposHistorico.AddRange(tiposHistorico);
        }
    }
}
