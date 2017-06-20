using System.Collections.Generic;

namespace Fortes.Core.Modelo.Entidades
{
    public class Movimentacao : Abstracoes.Movimentacao
    {
        public ICollection<Historicos.Movimentacao> Historicos { get; set; }
    }
}
