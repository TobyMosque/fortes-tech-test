using Fortes.Core.Modelo.Historicos.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fortes.Core.Modelo.Entidades
{
    public class Sessao
    {
        public Guid SessaoID { get; set; }
        public Guid UsuarioID { get; set; }
        public byte[] Token { get; set; }
		public bool IsActive { get; set; }
		public int GrupoID { get; set; }
        public DateTime DataCriacao { get; set; }

        public Usuario Usuario { get; set; }
        public Grupo Grupo { get; set; }
    }
}
