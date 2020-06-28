using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdvocaciaPPFinal.Models;

namespace AdvocaciaPPFinal.Data
{
    public class AdvocaciaPPFinalContext : DbContext
    {
        public AdvocaciaPPFinalContext (DbContextOptions<AdvocaciaPPFinalContext> options)
            : base(options)
        {
        }

        public DbSet<AdvocaciaPPFinal.Models.Advogado> Advogado { get; set; }

        public DbSet<AdvocaciaPPFinal.Models.Cliente> Cliente { get; set; }

        public DbSet<AdvocaciaPPFinal.Models.Emprestimo> Emprestimo { get; set; }

        public DbSet<AdvocaciaPPFinal.Models.Estagiario> Estagiario { get; set; }

        public DbSet<AdvocaciaPPFinal.Models.Funcionario> Funcionario { get; set; }

        public DbSet<AdvocaciaPPFinal.Models.Livro> Livro { get; set; }

        public DbSet<AdvocaciaPPFinal.Models.Processo> Processo { get; set; }
    }
}
