using EntityFrameworkCore.Triggers;
using Fortes.Core.Modelo.Historicos.Enumeradores;
using System;
using System.Linq;
using System.Reflection;

namespace Fortes.Core.Modelo.Abstracoes
{
    public abstract class EntidadeBase<T> : IEntidade<T> where T : class, IHistorico
    {
        public int GrupoID { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool IsDeleted { get; set; }

        public Entidades.Grupo Grupo { get; set; }

        private static bool IsSimple(Type type)
        {
            var info = type.GetTypeInfo();
            return info.IsPrimitive || info.IsValueType || info.IsSealed;
        }

        private static T CriarHistorico(Contexto contexto, EntidadeBase<T> entity, TipoHistorico tipo)
        {
            var historico = (T)Activator.CreateInstance(typeof(T));
            var entityType = entity.GetType();
            var historyType = historico.GetType();

            var entityProps = entityType.GetProperties()
                .Where(x => IsSimple(x.PropertyType))
                .ToDictionary(x => x.Name, x => x);

            var historyProps = historyType.GetProperties()
                .Where(x => entityProps.ContainsKey(x.Name))
                .ToDictionary(x => x.Name, x => x);

            var props = new string[historyProps.Count];
            historyProps.Keys.CopyTo(props, 0);
            foreach (var prop in props)
            {
                var value = entityProps[prop].GetValue(entity);
                historyProps[prop].SetValue(historico, value);
            }

            historico.DataHistorico = DateTime.UtcNow;
            historico.HistoricoID = Guid.NewGuid();
            historico.TipoHistoricoID = tipo;
            historico.SessaoID = contexto.SessaoID;

            contexto.Add(historico);
            return historico;
        }

        static EntidadeBase()
        {
            Triggers<EntidadeBase<T>>.Inserting += entry =>
            {                
                if (entry.Entity is IHistorico)
                    return;

                var contexto = entry.Context as Contexto;
                entry.Entity.DataCriacao = DateTime.Now;
                entry.Entity.GrupoID = contexto.GrupoID;
                entry.Entity.IsDeleted = false;

                var historico = CriarHistorico(contexto, entry.Entity, TipoHistorico.Insert);
            };
            Triggers<EntidadeBase<T>>.Updating += entry =>
            {
                if (entry.Entity is IHistorico)
                    return;
                var contexto = entry.Context as Contexto;
                var historico = CriarHistorico(contexto, entry.Entity, TipoHistorico.Update);
            };
            Triggers<EntidadeBase<T>>.Deleting += entry =>
            {
                if (entry.Entity is IHistorico)
                    return;
                var contexto = entry.Context as Contexto;
                entry.Entity.IsDeleted = true;

                var historico = CriarHistorico(contexto, entry.Entity, TipoHistorico.Delete);
                entry.Cancel = true;
            };
        }
    }
}
