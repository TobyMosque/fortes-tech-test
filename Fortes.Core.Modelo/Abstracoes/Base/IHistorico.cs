using System;

namespace Fortes.Core.Modelo.Abstracoes
{
    public interface IHistorico
    {
        Guid HistoricoID { get; set; }
        Historicos.Enumeradores.TipoHistorico TipoHistoricoID { get; set; }
        DateTime DataHistorico { get; set; }
        Guid? SessaoID { get; set; }

        Entidades.Sessao Sessao { get; set; }
        Historicos.Dominio.TipoHistorico TipoHistorico { get; set; }
    }
}
