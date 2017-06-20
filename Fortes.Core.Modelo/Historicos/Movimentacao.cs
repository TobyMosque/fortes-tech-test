using System;

namespace Fortes.Core.Modelo.Historicos
{
    public class Movimentacao : Abstracoes.Movimentacao, Abstracoes.IHistorico
    {
        public Guid HistoricoID { get; set; }
        public Enumeradores.TipoHistorico TipoHistoricoID { get; set; }
        public DateTime DataHistorico { get; set; }
        public Guid? SessaoID { get; set; }

        public Entidades.Sessao Sessao { get; set; }
        public Entidades.Movimentacao Entidade { get; set; }
        public Dominio.TipoHistorico TipoHistorico { get; set; }
    }
}
