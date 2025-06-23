using Microsoft.EntityFrameworkCore;
using LJJ_VITINHO.Models;

namespace LJJ_VITINHO.Data
{
    public class BancoDados : DbContext
    {
        string conexao;

        //Mapeamento das tabelas do banco de dados
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<Agendamentos> Agendamento { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendamentos>()
                .HasOne(b => b.Clientes)
                .WithMany(a => a.Agendamentos)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Agendamentos>()
                .HasOne(b => b.Funcionarios)
                .WithMany(a => a.Agendamentos)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            conexao = @"Server=localhost\SQLExpress;
Database=LJJVitinho;
Integrated Security=True;
TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(conexao);
            base.OnConfiguring(optionsBuilder);
        }

    }
}
