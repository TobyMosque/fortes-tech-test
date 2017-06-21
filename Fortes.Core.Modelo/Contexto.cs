using EntityFrameworkCore.Triggers;
using Fortes.Core.Modelo.Abstracoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fortes.Core.Modelo
{
    public class Contexto : DbContext
    {
        private string _grupoId;

        public Guid? SessaoID { get; set; }
        public int GrupoID {
            get { return Int32.Parse(_grupoId); }
            set { _grupoId = value.ToString(); }
        }

        public Contexto() : base()
        {

        }

        public Contexto(Guid? sessaoID, int grupoID) : base()
        {
            this.SessaoID = sessaoID;
            this.GrupoID = grupoID;
        }

        public Contexto(DbContextOptions options) : base(options)
        {
            
        }

        public Contexto(DbContextOptions options, Guid? sessaoID, int grupoID) : base(options)
        {
            this.SessaoID = sessaoID;
            this.GrupoID = grupoID;
        }

        public override Int32 SaveChanges()
        {
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess: true);
        }

        public override Int32 SaveChanges(Boolean acceptAllChangesOnSuccess)
        {
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess);
        }

        public override Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);
        }

        public override Task<Int32> SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Entidades.Grupo> Grupos { get; set; }
        public DbSet<Entidades.Sessao> Sessoes { get; set; }
        public DbSet<Entidades.Usuario> Usuarios { get; set; }
        public DbSet<Entidades.Recurso> Recursos { get; set; }
        public DbSet<Entidades.Movimentacao> Movimentacoes { get; set; }
        public DbSet<Entidades.Dominio.TipoMovimentacao> TiposMovimentacao { get; set; }

        public DbSet<Historicos.Usuario> HistoricoUsuarios { get; set; }
        public DbSet<Historicos.Recurso> HistoricoRecursos { get; set; }
        public DbSet<Historicos.Movimentacao> HistoricoMovimentacoes { get; set; }
        public DbSet<Historicos.Dominio.TipoHistorico> TiposHistorico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<Entidades.Dominio.TipoMovimentacao>(entity =>
            {
                entity.ForSqlServerToTable("TiposMovimentacao", "dominio").ForNpgsqlToTable("TiposMovimentacao", "dominio");
                entity.HasKey(x => x.TipoMovimentacaoID);
                entity.Property(x => x.Descricao).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Historicos.Dominio.TipoHistorico>(entity =>
            {
                entity.ForSqlServerToTable("TiposHistorico", "dominio").ForNpgsqlToTable("TiposHistorico", "dominio");
                entity.HasKey(x => x.TipoHistoricoID);
                entity.Property(x => x.Descricao).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Entidades.Grupo>(entity =>
            {
                entity.ForSqlServerToTable("Grupos", "entidade").ForNpgsqlToTable("Grupos", "entidade");
                entity.HasKey(x => x.GrupoID);
                entity.Property(x => x.GrupoID).ValueGeneratedNever();

                entity.HasIndex(x => x.Descricao).IsUnique();
                entity.Property(x => x.Descricao).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Entidades.Usuario>(entity =>
            {
                entity.ForSqlServerToTable("Usuarios", "entidade").ForNpgsqlToTable("Usuarios", "entidade");
                entity.HasKey(x => x.UsuarioID).ForSqlServerIsClustered(false); ;
                entity.HasIndex(x => new { x.GrupoID, x.Logon }).IsUnique();

                entity.Property(x => x.Logon).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Senha).HasMaxLength(64).IsRequired();
                entity.Property(x => x.Salt).HasMaxLength(16).IsRequired();

                this.ModelEntidades<Entidades.Usuario, Historicos.Usuario>(entity, x => x.Usuarios);
            });

            modelBuilder.Entity<Historicos.Usuario>(entity =>
            {
                entity.ForSqlServerToTable("Usuarios", "historico").ForNpgsqlToTable("Usuarios", "historico");
                entity.HasKey(x => x.HistoricoID).ForSqlServerIsClustered(false); ;

                entity.HasOne(x => x.Entidade).WithMany(x => x.Historicos).HasForeignKey(x => x.UsuarioID).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(x => x.UsuarioID);

                entity.Property(x => x.Logon).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Senha).HasMaxLength(64).IsRequired();
                entity.Property(x => x.Salt).HasMaxLength(16).IsRequired();

                this.ModelEntidades<Historicos.Usuario, Historicos.Usuario>(entity, null);
                this.ModelHistoricos<Historicos.Usuario>(entity);
            });

            modelBuilder.Entity<Entidades.Recurso>(entity =>
            {
                entity.ForSqlServerToTable("Recursos", "entidade").ForNpgsqlToTable("Recursos", "entidade");
                entity.HasKey(x => x.RecursoID).ForSqlServerIsClustered(false); ;
                entity.HasIndex(x => new { x.GrupoID, x.Descricao }).IsUnique();

                entity.Property(x => x.Descricao).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Observacao).HasMaxLength(250);

                this.ModelEntidades<Entidades.Recurso, Historicos.Recurso>(entity, x => x.Recursos);
            });

            modelBuilder.Entity<Historicos.Recurso>(entity =>
            {
                entity.ForSqlServerToTable("Recursos", "historico").ForNpgsqlToTable("Recursos", "historico");
                entity.HasKey(x => x.HistoricoID).ForSqlServerIsClustered(false); ;

                entity.HasOne(x => x.Entidade).WithMany(x => x.Historicos).HasForeignKey(x => x.RecursoID).OnDelete(DeleteBehavior.Restrict); ;
                entity.HasIndex(x => x.RecursoID);

                entity.Property(x => x.Descricao).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Observacao).HasMaxLength(250);

                this.ModelEntidades<Historicos.Recurso, Historicos.Recurso>(entity, null);
                this.ModelHistoricos<Historicos.Recurso>(entity);
            });

            modelBuilder.Entity<Entidades.Movimentacao>(entity =>
            {
                entity.ForSqlServerToTable("Movimentacoes", "entidade").ForNpgsqlToTable("Movimentacoes", "entidade");
                entity.HasKey(x => x.MovimentacaoID).ForSqlServerIsClustered(false); ;

                entity.HasOne(x => x.Recurso).WithMany(x => x.Movimentacoes).HasForeignKey(x => x.RecursoID);
                entity.HasIndex(x => x.RecursoID);

                entity.HasOne(x => x.Usuario).WithMany(x => x.Movimentacoes).HasForeignKey(x => x.UsuarioID);
                entity.HasIndex(x => x.UsuarioID);

                entity.HasOne(x => x.TipoMovimentacao).WithMany(navigationExpression: null).HasForeignKey(x => x.TipoMovimentacaoID);
                entity.HasIndex(x => x.TipoMovimentacaoID);

                this.ModelEntidades<Entidades.Movimentacao, Historicos.Movimentacao>(entity, x => x.Movimentacoes);
            });

            modelBuilder.Entity<Historicos.Movimentacao>(entity =>
            {
                entity.ForSqlServerToTable("Movimentacoes", "historico").ForNpgsqlToTable("Movimentacoes", "historico");
                entity.HasKey(x => x.HistoricoID).ForSqlServerIsClustered(false);

                entity.HasOne(x => x.Recurso).WithMany(navigationExpression: null).HasForeignKey(x => x.RecursoID);
                entity.HasIndex(x => x.RecursoID);

                entity.HasOne(x => x.Usuario).WithMany(navigationExpression: null).HasForeignKey(x => x.UsuarioID);
                entity.HasIndex(x => x.UsuarioID);

                entity.HasOne(x => x.TipoMovimentacao).WithMany(navigationExpression: null).HasForeignKey(x => x.TipoMovimentacaoID);
                entity.HasIndex(x => x.TipoMovimentacaoID);

                entity.HasOne(x => x.Entidade).WithMany(x => x.Historicos).HasForeignKey(x => x.MovimentacaoID).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(x => x.MovimentacaoID);

                this.ModelEntidades<Historicos.Movimentacao, Historicos.Movimentacao>(entity, null);
                this.ModelHistoricos<Historicos.Movimentacao>(entity);
            });

            modelBuilder.Entity<Entidades.Sessao>(entity =>
            {
                entity.ForSqlServerToTable("Sessoes", "entidade").ForNpgsqlToTable("Sessoes", "entidade");
                entity.HasKey(x => x.SessaoID).ForSqlServerIsClustered(false);

                entity.HasOne(x => x.Grupo).WithMany(navigationExpression: null).HasForeignKey(x => x.GrupoID).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(x => new { x.GrupoID, x.DataCriacao }).IsUnique().ForSqlServerIsClustered();

                entity.HasOne(x => x.Usuario).WithMany(x => x.Sessoes).HasForeignKey(x => x.UsuarioID);
                entity.HasIndex(x => x.UsuarioID);

                entity.Property(x => x.Token).HasMaxLength(64);
                entity.HasIndex(x => x.Token).IsUnique();
            });
        }

        private void ModelEntidades<T, T2>(EntityTypeBuilder<T> entity, Expression<Func<Entidades.Grupo, IEnumerable<T>>> navigationExpression)
            where T : EntidadeBase<T2>
            where T2 : class, IHistorico
        {
            entity.HasQueryFilter(x => !x.IsDeleted && x.GrupoID.ToString() == _grupoId);
            entity.HasOne(x => x.Grupo).WithMany(navigationExpression).HasForeignKey(x => x.GrupoID).OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.IsDeleted).IsRequired();

            var isHistorico = typeof(IHistorico).IsAssignableFrom(typeof(T));
            entity.HasIndex(new string[] { "GrupoID", isHistorico ? "DataHistorico" : "DataCriacao" }).IsUnique().ForSqlServerIsClustered();
            entity.HasIndex(x => x.IsDeleted);
        }

        private void ModelHistoricos<T>(EntityTypeBuilder<T> entity) where T : class, Abstracoes.IHistorico
        {
            entity.HasOne(x => x.Sessao).WithMany(navigationExpression: null).HasForeignKey(x => x.SessaoID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.TipoHistorico).WithMany(navigationExpression: null).HasForeignKey(x => x.TipoHistoricoID).OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(x => x.SessaoID);
            entity.HasIndex(x => x.TipoHistoricoID);
        }
    }
}
