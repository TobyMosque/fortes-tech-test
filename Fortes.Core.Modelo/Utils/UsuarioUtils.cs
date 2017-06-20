using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Fortes.Core.Modelo.Utils
{
    public static class UsuarioUtils
    {
        public static void CadastrarSenha(this Entidades.Usuario usuario, string senha)
        {
            var pepper = usuario.UsuarioID.ToByteArray().Sum(x => x);
            var salt = new byte[16];
            var random = RandomNumberGenerator.Create();
            random.GetBytes(salt);

            usuario.Salt = salt;
            usuario.Senha = KeyDerivation.Pbkdf2(senha, salt, KeyDerivationPrf.HMACSHA512, 8000 + pepper, 64);
        }

        public static bool VerificarSenha(this Entidades.Usuario usuario, string senha)
        {
            var pepper = usuario.UsuarioID.ToByteArray().Sum(x => x);
            var binary = KeyDerivation.Pbkdf2(senha, usuario.Salt, KeyDerivationPrf.HMACSHA512, 8000 + pepper, 64);
            return usuario.Senha.SequenceEqual(binary);
        }

		public static Modelo.Entidades.Sessao GerarSessao(this Entidades.Usuario usuario)
		{
			var token = new byte[64];
			RandomNumberGenerator.Create().GetBytes(token);

			var sessao = new Modelo.Entidades.Sessao();
			sessao.SessaoID = Guid.NewGuid();
			sessao.UsuarioID = usuario.UsuarioID;
			sessao.GrupoID = usuario.GrupoID;
			sessao.Token = token;
			sessao.IsActive = true;
			sessao.DataCriacao = DateTime.UtcNow;
			return sessao;
		}
	}
}
