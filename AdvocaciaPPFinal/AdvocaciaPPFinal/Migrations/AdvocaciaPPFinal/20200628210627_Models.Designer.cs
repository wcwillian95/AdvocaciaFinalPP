﻿// <auto-generated />
using System;
using AdvocaciaPPFinal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdvocaciaPPFinal.Migrations.AdvocaciaPPFinal
{
    [DbContext(typeof(AdvocaciaPPFinalContext))]
    [Migration("20200628210627_Models")]
    partial class Models
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdvocaciaPPFinal.Models.Advogado", b =>
                {
                    b.Property<int>("Id_Advogado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF_Advogado")
                        .IsRequired()
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.Property<DateTime>("DataAdmissao_Advogado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Data_de_nascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email_Advogado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Especializacao_Advogado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Inscricao_Advogado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instituicao_Advogado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome_Advogado")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<int>("Numero_casos")
                        .HasColumnType("int");

                    b.Property<long>("Telefone_Advogado")
                        .HasColumnType("bigint");

                    b.HasKey("Id_Advogado");

                    b.ToTable("Advogado");
                });

            modelBuilder.Entity("AdvocaciaPPFinal.Models.Cliente", b =>
                {
                    b.Property<int>("Id_Cliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CEP")
                        .HasColumnType("bigint");

                    b.Property<string>("CPF_Cliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCadastro_Cliente")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento_Cliente")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email_Cliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome_Cliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Telefone_Cliente")
                        .HasColumnType("bigint");

                    b.HasKey("Id_Cliente");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("AdvocaciaPPFinal.Models.Emprestimo", b =>
                {
                    b.Property<int>("Id_Emprestimo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data_Emprestimo")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Valor_Emprestimo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id_Emprestimo");

                    b.ToTable("Emprestimo");
                });

            modelBuilder.Entity("AdvocaciaPPFinal.Models.Estagiario", b =>
                {
                    b.Property<int>("Id_Estagiario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF_Estagiario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comentario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCadastro_Estagiario")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento_Estagiario")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Data_fim_contrato")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email_Estagiario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Funcao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome_Estagiario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome_Resonsavel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Telefone_Estagiario")
                        .HasColumnType("bigint");

                    b.Property<long>("Telefone_Responsalvel")
                        .HasColumnType("bigint");

                    b.HasKey("Id_Estagiario");

                    b.ToTable("Estagiario");
                });

            modelBuilder.Entity("AdvocaciaPPFinal.Models.Funcionario", b =>
                {
                    b.Property<int>("Id_Funcionario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro_Funcionario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CEP_Funcionario")
                        .HasColumnType("bigint");

                    b.Property<string>("CPF_Funcionario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cargo_Funcionario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade_Funcionario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCadastro_Funcionario")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento_Funcionario")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome_Funcionario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero_Funcionario")
                        .HasColumnType("int");

                    b.Property<string>("Rua_Funcionario")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Funcionario");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("AdvocaciaPPFinal.Models.Livro", b =>
                {
                    b.Property<int>("Id_Livro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Ano_Publicacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Autor_Livro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Editora_Livro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genero_Livro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome_Livro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status_Livro")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Livro");

                    b.ToTable("Livro");
                });

            modelBuilder.Entity("AdvocaciaPPFinal.Models.Processo", b =>
                {
                    b.Property<int>("Id_Processo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data_de_Inicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Data_de_termino")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeAdvogado_Processo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeCliente_Processo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Processo");

                    b.ToTable("Processo");
                });
#pragma warning restore 612, 618
        }
    }
}
