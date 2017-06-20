using System;
using System.ComponentModel.DataAnnotations;

namespace Fortes.Core.Modelo.Abstracoes
{
    public abstract class Movimentacao : EntidadeBase<Historicos.Movimentacao>
    {
        public Guid MovimentacaoID { get; set; }
        public Guid RecursoID { get; set; }
        public Guid UsuarioID { get; set; }
        public Entidades.Enumeradores.TipoMovimentacao TipoMovimentacaoID { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "O Campo Quantidade é obrigatorio e deve ser maior que 0")]
        public int Quantidade { get; set; }

        public Entidades.Usuario Usuario { get; set; }
        public Entidades.Recurso Recurso { get; set; }
        public Entidades.Dominio.TipoMovimentacao TipoMovimentacao { get; set; }
    }
}
