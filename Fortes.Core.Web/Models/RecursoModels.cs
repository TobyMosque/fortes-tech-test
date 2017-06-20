using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Fortes.Core.Modelo.Utils;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Fortes.Core.Modelo;
using Fortes.Core.Web.Consultas;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
