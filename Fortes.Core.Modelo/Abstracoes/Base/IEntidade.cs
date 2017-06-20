using System;

namespace Fortes.Core.Modelo.Abstracoes
{
    public interface IEntidade<T> where T : IHistorico
    {
        int GrupoID { get; set; }
        DateTime DataCriacao { get; set; }
        bool IsDeleted { get; set; }

        Entidades.Grupo Grupo { get; set; }
    }
}
