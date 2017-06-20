using Fortes.Core.Modelo.Entidades.Enumeradores;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fortes.Core.Web.Models.UsuarioModels
{
    public class UsuarioViewModel
    {
        public Guid UsuarioID { get; set; }
        public string Logon { get; set; }
    }
}
