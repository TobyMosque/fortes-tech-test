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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fortes.Core.Web.Models.AuthModels
{
    public class LoginSignupViewModel
    {
        public int GrupoId { get; set; }
        public string Logon { get; set; }
        public string Senha { get; set; }
    }
}
