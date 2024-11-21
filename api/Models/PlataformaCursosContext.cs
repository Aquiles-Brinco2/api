using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public partial class PlataformaCursosContext : DbContext
{
    public PlataformaCursosContext()
    {
    }

    public PlataformaCursosContext(DbContextOptions<PlataformaCursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calificacione> Calificaciones { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<CursoCategorium> CursoCategoria { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Leccione> Lecciones { get; set; }

    public virtual DbSet<Materiale> Materiales { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Soporte> Soportes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calificacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Califica__3214EC071663C989");

            entity.Property(e => e.FechaCalificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calificac__IdCur__5070F446");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calificac__IdUsu__5165187F");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC078970FFD0");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCF6E363BFC").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comentar__3214EC073C3FC4C6");

            entity.Property(e => e.FechaComentario)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comentari__IdCur__4AB81AF0");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comentari__IdUsu__4BAC3F29");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cursos__3214EC07BE6F41B1");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(255);

            entity.HasOne(d => d.IdInstructorNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdInstructor)
                .HasConstraintName("FK__Cursos__IdInstru__3D5E1FD2");
        });

        modelBuilder.Entity<CursoCategorium>(entity =>
        {
            entity.HasKey(e => new { e.IdCurso, e.IdCategoria }).HasName("PK__Curso_Ca__12632577C40A5928");

            entity.ToTable("Curso_Categoria");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.CursoCategoria)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Curso_Cat__IdCat__5CD6CB2B");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.CursoCategoria)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Curso_Cat__IdCur__5BE2A6F2");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facturas__3214EC07C881C533");

            entity.HasIndex(e => e.NumeroFactura, "UQ__Facturas__CF12F9A6B18E98F9").IsUnique();

            entity.Property(e => e.FechaFactura)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NumeroFactura).HasMaxLength(50);

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas__IdPago__6E01572D");
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inscripc__3214EC07B2ACD109");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("activo");
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__IdCur__46E78A0C");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__IdUsu__45F365D3");
        });

        modelBuilder.Entity<Leccione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leccione__3214EC079B44135A");

            entity.Property(e => e.Titulo).HasMaxLength(255);

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Lecciones)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lecciones__IdCur__403A8C7D");
        });

        modelBuilder.Entity<Materiale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC07965D9117");

            entity.Property(e => e.Tipo).HasMaxLength(20);
            entity.Property(e => e.Url).HasMaxLength(255);

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Materiales)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Materiale__IdCur__5535A963");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3214EC07D9DB6A62");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("completado");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MetodoPago).HasMaxLength(50);
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdInscripcionNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdInscripcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__IdInscrip__693CA210");
        });

        modelBuilder.Entity<Soporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Soporte__3214EC074FE09E93");

            entity.ToTable("Soporte");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("pendiente");
            entity.Property(e => e.FechaMensaje)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Soportes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Soporte__IdUsuar__628FA481");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC0702D117F7");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D1053434255EAE").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Tipo).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
