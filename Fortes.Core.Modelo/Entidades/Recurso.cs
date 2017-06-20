using System.Collections.Generic;

namespace Fortes.Core.Modelo.Entidades
{
    public class Recurso : Abstracoes.Recurso
    {
        public ICollection<Historicos.Recurso> Historicos { get; set; }
        public ICollection<Entidades.Movimentacao> Movimentacoes { get; set; }
    }
}
