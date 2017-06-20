using Fortes.Core.Modelo.Historicos.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fortes.Core.Modelo.Entidades.Dominio
{
    public class TipoMovimentacao
    {
        public Enumeradores.TipoMovimentacao TipoMovimentacaoID { get; set; }
        public string Descricao { get; set; }
    }
}
