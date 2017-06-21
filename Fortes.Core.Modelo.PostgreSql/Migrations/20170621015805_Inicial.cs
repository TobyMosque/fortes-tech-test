using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Fortes.Core.Modelo.PostgreSql.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dominio");

            migrationBuilder.EnsureSchema(
                name: "entidade");

            migrationBuilder.EnsureSchema(
                name: "historico");

            migrationBuilder.CreateTable(
                name: "TiposMovimentacao",
                schema: "dominio",
                columns: table => new
                {
                    TipoMovimentacaoID = table.Column<byte>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMovimentacao", x => x.TipoMovimentacaoID);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                schema: "entidade",
                columns: table => new
                {
                    GrupoID = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.GrupoID);
                });

            migrationBuilder.CreateTable(
                name: "TiposHistorico",
                schema: "dominio",
                columns: table => new
                {
                    TipoHistoricoID = table.Column<byte>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposHistorico", x => x.TipoHistoricoID);
                });

            migrationBuilder.CreateTable(
                name: "Recursos",
                schema: "entidade",
                columns: table => new
                {
                    RecursoID = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    GrupoID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Observacao = table.Column<string>(maxLength: 250, nullable: true),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.RecursoID);
                    table.ForeignKey(
                        name: "FK_Recursos_Grupos_GrupoID",
                        column: x => x.GrupoID,
                        principalSchema: "entidade",
                        principalTable: "Grupos",
                        principalColumn: "GrupoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "entidade",
                columns: table => new
                {
                    UsuarioID = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    GrupoID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Logon = table.Column<string>(maxLength: 50, nullable: false),
                    Salt = table.Column<byte[]>(maxLength: 16, nullable: false),
                    Senha = table.Column<byte[]>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Grupos_GrupoID",
                        column: x => x.GrupoID,
                        principalSchema: "entidade",
                        principalTable: "Grupos",
                        principalColumn: "GrupoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacoes",
                schema: "entidade",
                columns: table => new
                {
                    MovimentacaoID = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    GrupoID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    RecursoID = table.Column<Guid>(nullable: false),
                    TipoMovimentacaoID = table.Column<byte>(nullable: false),
                    UsuarioID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacoes", x => x.MovimentacaoID);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Grupos_GrupoID",
                        column: x => x.GrupoID,
                        principalSchema: "entidade",
                        principalTable: "Grupos",
                        principalColumn: "GrupoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Recursos_RecursoID",
                        column: x => x.RecursoID,
                        principalSchema: "entidade",
                        principalTable: "Recursos",
                        principalColumn: "RecursoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_TiposMovimentacao_TipoMovimentacaoID",
                        column: x => x.TipoMovimentacaoID,
                        principalSchema: "dominio",
                        principalTable: "TiposMovimentacao",
                        principalColumn: "TipoMovimentacaoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalSchema: "entidade",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessoes",
                schema: "entidade",
                columns: table => new
                {
                    SessaoID = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    GrupoID = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Token = table.Column<byte[]>(maxLength: 64, nullable: true),
                    UsuarioID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessoes", x => x.SessaoID);
                    table.ForeignKey(
                        name: "FK_Sessoes_Grupos_GrupoID",
                        column: x => x.GrupoID,
                        principalSchema: "entidade",
                        principalTable: "Grupos",
                        principalColumn: "GrupoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessoes_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalSchema: "entidade",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacoes",
                schema: "historico",
                columns: table => new
                {
                    HistoricoID = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataHistorico = table.Column<DateTime>(nullable: false),
                    GrupoID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MovimentacaoID = table.Column<Guid>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    RecursoID = table.Column<Guid>(nullable: false),
                    SessaoID = table.Column<Guid>(nullable: true),
                    TipoHistoricoID = table.Column<byte>(nullable: false),
                    TipoMovimentacaoID = table.Column<byte>(nullable: false),
                    UsuarioID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacoes", x => x.HistoricoID);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Grupos_GrupoID",
                        column: x => x.GrupoID,
                        principalSchema: "entidade",
                        principalTable: "Grupos",
                        principalColumn: "GrupoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Movimentacoes_MovimentacaoID",
                        column: x => x.MovimentacaoID,
                        principalSchema: "entidade",
                        principalTable: "Movimentacoes",
                        principalColumn: "MovimentacaoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Recursos_RecursoID",
                        column: x => x.RecursoID,
                        principalSchema: "entidade",
                        principalTable: "Recursos",
                        principalColumn: "RecursoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Sessoes_SessaoID",
                        column: x => x.SessaoID,
                        principalSchema: "entidade",
                        principalTable: "Sessoes",
                        principalColumn: "SessaoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_TiposHistorico_TipoHistoricoID",
                        column: x => x.TipoHistoricoID,
                        principalSchema: "dominio",
                        principalTable: "TiposHistorico",
                        principalColumn: "TipoHistoricoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_TiposMovimentacao_TipoMovimentacaoID",
                        column: x => x.TipoMovimentacaoID,
                        principalSchema: "dominio",
                        principalTable: "TiposMovimentacao",
                        principalColumn: "TipoMovimentacaoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalSchema: "entidade",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recursos",
                schema: "historico",
                columns: table => new
                {
                    HistoricoID = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataHistorico = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    GrupoID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Observacao = table.Column<string>(maxLength: 250, nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    RecursoID = table.Column<Guid>(nullable: false),
                    SessaoID = table.Column<Guid>(nullable: true),
                    TipoHistoricoID = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.HistoricoID);
                    table.ForeignKey(
                        name: "FK_Recursos_Grupos_GrupoID",
                        column: x => x.GrupoID,
                        principalSchema: "entidade",
                        principalTable: "Grupos",
                        principalColumn: "GrupoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recursos_Recursos_RecursoID",
                        column: x => x.RecursoID,
                        principalSchema: "entidade",
                        principalTable: "Recursos",
                        principalColumn: "RecursoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recursos_Sessoes_SessaoID",
                        column: x => x.SessaoID,
                        principalSchema: "entidade",
                        principalTable: "Sessoes",
                        principalColumn: "SessaoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recursos_TiposHistorico_TipoHistoricoID",
                        column: x => x.TipoHistoricoID,
                        principalSchema: "dominio",
                        principalTable: "TiposHistorico",
                        principalColumn: "TipoHistoricoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "historico",
                columns: table => new
                {
                    HistoricoID = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataHistorico = table.Column<DateTime>(nullable: false),
                    GrupoID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Logon = table.Column<string>(maxLength: 50, nullable: false),
                    Salt = table.Column<byte[]>(maxLength: 16, nullable: false),
                    Senha = table.Column<byte[]>(maxLength: 64, nullable: false),
                    SessaoID = table.Column<Guid>(nullable: true),
                    TipoHistoricoID = table.Column<byte>(nullable: false),
                    UsuarioID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.HistoricoID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Grupos_GrupoID",
                        column: x => x.GrupoID,
                        principalSchema: "entidade",
                        principalTable: "Grupos",
                        principalColumn: "GrupoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_Sessoes_SessaoID",
                        column: x => x.SessaoID,
                        principalSchema: "entidade",
                        principalTable: "Sessoes",
                        principalColumn: "SessaoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_TiposHistorico_TipoHistoricoID",
                        column: x => x.TipoHistoricoID,
                        principalSchema: "dominio",
                        principalTable: "TiposHistorico",
                        principalColumn: "TipoHistoricoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalSchema: "entidade",
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_Descricao",
                schema: "entidade",
                table: "Grupos",
                column: "Descricao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_IsDeleted",
                schema: "entidade",
                table: "Movimentacoes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_RecursoID",
                schema: "entidade",
                table: "Movimentacoes",
                column: "RecursoID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_TipoMovimentacaoID",
                schema: "entidade",
                table: "Movimentacoes",
                column: "TipoMovimentacaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_UsuarioID",
                schema: "entidade",
                table: "Movimentacoes",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_GrupoID_DataCriacao",
                schema: "entidade",
                table: "Movimentacoes",
                columns: new[] { "GrupoID", "DataCriacao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_IsDeleted",
                schema: "entidade",
                table: "Recursos",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_GrupoID_DataCriacao",
                schema: "entidade",
                table: "Recursos",
                columns: new[] { "GrupoID", "DataCriacao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_GrupoID_Descricao",
                schema: "entidade",
                table: "Recursos",
                columns: new[] { "GrupoID", "Descricao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_Token",
                schema: "entidade",
                table: "Sessoes",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_UsuarioID",
                schema: "entidade",
                table: "Sessoes",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_GrupoID_DataCriacao",
                schema: "entidade",
                table: "Sessoes",
                columns: new[] { "GrupoID", "DataCriacao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IsDeleted",
                schema: "entidade",
                table: "Usuarios",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_GrupoID_DataCriacao",
                schema: "entidade",
                table: "Usuarios",
                columns: new[] { "GrupoID", "DataCriacao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_GrupoID_Logon",
                schema: "entidade",
                table: "Usuarios",
                columns: new[] { "GrupoID", "Logon" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_IsDeleted",
                schema: "historico",
                table: "Movimentacoes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_MovimentacaoID",
                schema: "historico",
                table: "Movimentacoes",
                column: "MovimentacaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_RecursoID",
                schema: "historico",
                table: "Movimentacoes",
                column: "RecursoID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_SessaoID",
                schema: "historico",
                table: "Movimentacoes",
                column: "SessaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_TipoHistoricoID",
                schema: "historico",
                table: "Movimentacoes",
                column: "TipoHistoricoID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_TipoMovimentacaoID",
                schema: "historico",
                table: "Movimentacoes",
                column: "TipoMovimentacaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_UsuarioID",
                schema: "historico",
                table: "Movimentacoes",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_GrupoID_DataHistorico",
                schema: "historico",
                table: "Movimentacoes",
                columns: new[] { "GrupoID", "DataHistorico" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_IsDeleted",
                schema: "historico",
                table: "Recursos",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_RecursoID",
                schema: "historico",
                table: "Recursos",
                column: "RecursoID");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_SessaoID",
                schema: "historico",
                table: "Recursos",
                column: "SessaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_TipoHistoricoID",
                schema: "historico",
                table: "Recursos",
                column: "TipoHistoricoID");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_GrupoID_DataHistorico",
                schema: "historico",
                table: "Recursos",
                columns: new[] { "GrupoID", "DataHistorico" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IsDeleted",
                schema: "historico",
                table: "Usuarios",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SessaoID",
                schema: "historico",
                table: "Usuarios",
                column: "SessaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoHistoricoID",
                schema: "historico",
                table: "Usuarios",
                column: "TipoHistoricoID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UsuarioID",
                schema: "historico",
                table: "Usuarios",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_GrupoID_DataHistorico",
                schema: "historico",
                table: "Usuarios",
                columns: new[] { "GrupoID", "DataHistorico" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimentacoes",
                schema: "historico");

            migrationBuilder.DropTable(
                name: "Recursos",
                schema: "historico");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "historico");

            migrationBuilder.DropTable(
                name: "Movimentacoes",
                schema: "entidade");

            migrationBuilder.DropTable(
                name: "Sessoes",
                schema: "entidade");

            migrationBuilder.DropTable(
                name: "TiposHistorico",
                schema: "dominio");

            migrationBuilder.DropTable(
                name: "Recursos",
                schema: "entidade");

            migrationBuilder.DropTable(
                name: "TiposMovimentacao",
                schema: "dominio");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "entidade");

            migrationBuilder.DropTable(
                name: "Grupos",
                schema: "entidade");
        }
    }
}
