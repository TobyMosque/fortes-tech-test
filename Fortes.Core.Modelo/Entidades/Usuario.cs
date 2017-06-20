using System.Collections.Generic;

namespace Fortes.Core.Modelo.Entidades
{
    public class Usuario : Abstracoes.Usuario
    {
        public ICollection<Historicos.Usuario> Historicos { get; set; }
        public ICollection<Entidades.Movimentacao> Movimentacoes { get; set; }
        public ICollection<Entidades.Sessao> Sessoes { get; set; }
    }
}
