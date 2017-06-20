using System;
using System.ComponentModel.DataAnnotations;

namespace Fortes.Core.Web.Models.RecursoModels
{
    public class RecursoViewModel
    {
        public Guid? RecursoID { get; set; }
        [Required(ErrorMessage = "O Campo Descrição é obrigatorio")]
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "O Campo Quantidade é obrigatorio e deve ser maior que 0")]
        public int Quantidade { get; set; }
    }
}
