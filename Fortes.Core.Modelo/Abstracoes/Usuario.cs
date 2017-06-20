using System;

namespace Fortes.Core.Modelo.Abstracoes
{
    public abstract class Usuario : EntidadeBase<Historicos.Usuario>
    {
        public Guid UsuarioID { get; set; }
        public string Logon { get; set; }
        public byte[] Senha { get; set; }
        public byte[] Salt { get; set; }
    }
}
