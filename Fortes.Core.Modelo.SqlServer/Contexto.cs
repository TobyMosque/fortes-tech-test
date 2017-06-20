using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fortes.Core.Modelo.SqlServer
{
    public class Contexto : Modelo.Contexto
    {
        public Contexto() : base() { }
        public Contexto(Guid? sessaoID, int grupoID) : base(sessaoID, grupoID) { }
        public Contexto(DbContextOptions options) : base(options) { }
        public Contexto(DbContextOptions options, Guid? sessaoID, int grupoID) : base(options, sessaoID, grupoID) { }
    }
}
