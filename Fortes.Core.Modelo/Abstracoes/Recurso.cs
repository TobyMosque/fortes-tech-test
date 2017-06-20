using System;
using System.ComponentModel.DataAnnotations;

namespace Fortes.Core.Modelo.Abstracoes
{
    public abstract class Recurso : EntidadeBase<Historicos.Recurso>
    {
        public Guid RecursoID { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public int Quantidade { get; set; }
    }
}
