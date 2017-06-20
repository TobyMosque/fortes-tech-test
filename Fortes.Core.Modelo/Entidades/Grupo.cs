using Fortes.Core.Modelo.Historicos.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fortes.Core.Modelo.Entidades
{
    public class Grupo
    {
        public int GrupoID { get; set; }
        public string Descricao { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<Recurso> Recursos { get; set; }
        public ICollection<Movimentacao> Movimentacoes { get; set; }
    }
}
