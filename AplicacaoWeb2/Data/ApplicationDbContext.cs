using Microsoft.EntityFrameworkCore;
using AplicacaoWeb2.Models;

namespace AplicacaoWeb2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Doador> Doadores { get; set; }
        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; } 
        public DbSet<Visita> Visitas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendamento>()
                .HasKey(a => a.IdAgendamento); // Define IdAgendamento como chave primária

            modelBuilder.Entity<Visita>()
                .HasKey(v => v.IdVisita); // Define IdVisita como chave primária

            // Configuração do relacionamento entre Agendamento e Visitas
            modelBuilder.Entity<Agendamento>()
                .HasMany(a => a.Visitas)
                .WithOne(v => v.Agendamento)
                .HasForeignKey(v => v.IdAgendamento);

            base.OnModelCreating(modelBuilder);
        }
    }
}
