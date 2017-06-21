using Fortes.Core.Modelo.Entidades.Enumeradores;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fortes.Core.Web.Models.MovimentacaoModels
{
    public class MovimentacaoViewModel
    {
        public Guid? MovimentacaoID { get; set; }
        public Guid RecursoID { get; set; }
        public Guid UsuarioID { get; set; }
        public TipoMovimentacao TipoMovimentacaoID { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "O Campo Quantidade é obrigatorio e deve ser maior que 0")]
        public int Quantidade { get; set; }

        public string UsuarioNome { get; set; }
        public Boolean IsOwner { get; set; }
    }
}
