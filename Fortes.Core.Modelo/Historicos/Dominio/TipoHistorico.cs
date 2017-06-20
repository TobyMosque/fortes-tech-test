using Fortes.Core.Modelo.Historicos.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fortes.Core.Modelo.Historicos.Dominio
{
    public class TipoHistorico
    {
        public Enumeradores.TipoHistorico TipoHistoricoID { get; set; }
        public string Descricao { get; set; }
    }
}
