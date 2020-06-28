using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvocaciaPPFinal.Migrations.AdvocaciaPPFinal
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advogado",
                columns: table => new
                {
                    Id_Advogado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Advogado = table.Column<string>(maxLength: 60, nullable: false),
                    CPF_Advogado = table.Column<string>(maxLength: 11, nullable: false),
                    Data_de_nascimento = table.Column<DateTime>(nullable: false),
                    Inscricao_Advogado = table.Column<string>(nullable: true),
                    Instituicao_Advogado = table.Column<string>(nullable: true),
                    Especializacao_Advogado = table.Column<string>(nullable: true),
                    Telefone_Advogado = table.Column<long>(nullable: false),
                    Email_Advogado = table.Column<string>(nullable: true),
                    Numero_casos = table.Column<int>(nullable: false),
                    DataAdmissao_Advogado = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advogado", x => x.Id_Advogado);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id_Cliente = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Cliente = table.Column<string>(nullable: true),
                    CPF_Cliente = table.Column<string>(nullable: true),
                    DataNascimento_Cliente = table.Column<DateTime>(nullable: false),
                    Telefone_Cliente = table.Column<long>(nullable: false),
                    Email_Cliente = table.Column<string>(nullable: true),
                    CEP = table.Column<long>(nullable: false),
                    Rua = table.Column<string>(nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Bairro = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    DataCadastro_Cliente = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id_Cliente);
                });

            migrationBuilder.CreateTable(
                name: "Emprestimo",
                columns: table => new
                {
                    Id_Emprestimo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor_Emprestimo = table.Column<decimal>(nullable: false),
                    Data_Emprestimo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimo", x => x.Id_Emprestimo);
                });

            migrationBuilder.CreateTable(
                name: "Estagiario",
                columns: table => new
                {
                    Id_Estagiario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Estagiario = table.Column<string>(nullable: true),
                    CPF_Estagiario = table.Column<string>(nullable: true),
                    DataNascimento_Estagiario = table.Column<DateTime>(nullable: false),
                    Telefone_Estagiario = table.Column<long>(nullable: false),
                    Email_Estagiario = table.Column<string>(nullable: true),
                    Funcao = table.Column<string>(nullable: true),
                    DataCadastro_Estagiario = table.Column<DateTime>(nullable: false),
                    Data_fim_contrato = table.Column<DateTime>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    Nome_Resonsavel = table.Column<string>(nullable: true),
                    Telefone_Responsalvel = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estagiario", x => x.Id_Estagiario);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id_Funcionario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Funcionario = table.Column<string>(nullable: true),
                    CPF_Funcionario = table.Column<string>(nullable: true),
                    DataNascimento_Funcionario = table.Column<DateTime>(nullable: false),
                    Cargo_Funcionario = table.Column<string>(nullable: true),
                    CEP_Funcionario = table.Column<long>(nullable: false),
                    Rua_Funcionario = table.Column<string>(nullable: true),
                    Numero_Funcionario = table.Column<int>(nullable: false),
                    Cidade_Funcionario = table.Column<string>(nullable: true),
                    Bairro_Funcionario = table.Column<string>(nullable: true),
                    DataCadastro_Funcionario = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id_Funcionario);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Id_Livro = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Livro = table.Column<string>(nullable: true),
                    Genero_Livro = table.Column<string>(nullable: true),
                    Autor_Livro = table.Column<string>(nullable: true),
                    Editora_Livro = table.Column<string>(nullable: true),
                    Ano_Publicacao = table.Column<DateTime>(nullable: false),
                    Status_Livro = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Id_Livro);
                });

            migrationBuilder.CreateTable(
                name: "Processo",
                columns: table => new
                {
                    Id_Processo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAdvogado_Processo = table.Column<string>(nullable: true),
                    NomeCliente_Processo = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Data_de_Inicio = table.Column<DateTime>(nullable: false),
                    Data_de_termino = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processo", x => x.Id_Processo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advogado");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Emprestimo");

            migrationBuilder.DropTable(
                name: "Estagiario");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Livro");

            migrationBuilder.DropTable(
                name: "Processo");
        }
    }
}
