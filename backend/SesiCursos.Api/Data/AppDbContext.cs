using Microsoft.EntityFrameworkCore;
using SesiCursos.Api.Models;

namespace SesiCursos.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Status> Status => Set<Status>();
    public DbSet<Modalidade> Modalidades => Set<Modalidade>();
    public DbSet<Categoria> Categorias => Set<Categoria>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Categoria>(e =>
        {
            e.ToTable("categorias");
            e.HasKey(x => x.IdCategoria);
            e.Property(x => x.IdCategoria).HasColumnName("id_categoria");
            e.Property(x => x.Nome).HasColumnName("categoria").HasMaxLength(80).IsRequired();
        });

        mb.Entity<Modalidade>(e =>
        {
            e.ToTable("modalidades");
            e.HasKey(x => x.IdModalidade);
            e.Property(x => x.IdModalidade).HasColumnName("id_modalidade");
            e.Property(x => x.Nome).HasColumnName("modalidade").HasMaxLength(30).IsRequired();
        });

        mb.Entity<Status>(e =>
        {
            e.ToTable("status");
            e.HasKey(x => x.IdStatus);
            e.Property(x => x.IdStatus).HasColumnName("id_status");
            e.Property(x => x.Nome).HasColumnName("status").HasMaxLength(20).IsRequired();
        });

        mb.Entity<Curso>(e =>
        {
            e.ToTable("cursos");
            e.HasKey(x => x.IdCursos);
            e.Property(x => x.IdCursos).HasColumnName("id_cursos");
            e.Property(x => x.NomeCompleto).HasColumnName("nome_completo").HasMaxLength(200).IsRequired();
            e.Property(x => x.NomeBreve).HasColumnName("nome_breve").HasMaxLength(50).IsRequired();
            e.Property(x => x.Descricao).HasColumnName("descricao").HasColumnType("text").IsRequired();
            e.Property(x => x.CargaHoraria).HasColumnName("carga_horaria");
            e.Property(x => x.PublicoAlvo).HasColumnName("publico_alvo").HasMaxLength(255).IsRequired();
            e.Property(x => x.ImagemApresentacao).HasColumnName("imagem_apresentacao").HasMaxLength(255).IsRequired();
            e.Property(x => x.DataDisponibilizacao).HasColumnName("data_disponibilizacao").HasColumnType("date");
            e.Property(x => x.FkIdStatus).HasColumnName("fk_id_status");
            e.Property(x => x.FkIdModalidade).HasColumnName("fk_id_modalidade");
            e.Property(x => x.FkIdCategoria).HasColumnName("fk_id_categoria");

            e.HasOne(x => x.Status).WithMany(s => s.Cursos).HasForeignKey(x => x.FkIdStatus);
            e.HasOne(x => x.Modalidade).WithMany(m => m.Cursos).HasForeignKey(x => x.FkIdModalidade);
            e.HasOne(x => x.Categoria).WithMany(c => c.Cursos).HasForeignKey(x => x.FkIdCategoria);
        });
    }
}
