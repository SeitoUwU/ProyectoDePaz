using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDePaz.Models;

public partial class BdContext : DbContext
{
    public BdContext()
    {
    }

    public BdContext(DbContextOptions<BdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Doctieneetq> Doctieneetqs { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Etiqueta> Etiqueta { get; set; }

    public virtual DbSet<Generopersona> Generopersonas { get; set; }

    public virtual DbSet<Institucion> Institucions { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Publicacion> Publicacions { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolTienePermiso> RolTienePermisos { get; set; }

    public virtual DbSet<Tipodocumento> Tipodocumentos { get; set; }

    public virtual DbSet<Tipopersona> Tipopersonas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.DepId).HasName("PRIMARY");

            entity.ToTable("departamento");

            entity.Property(e => e.DepId)
                .HasMaxLength(45)
                .HasColumnName("DEP_ID");
            entity.Property(e => e.DepEstado)
                .HasMaxLength(45)
                .HasColumnName("DEP_Estado");
            entity.Property(e => e.DepNombre)
                .HasMaxLength(45)
                .HasColumnName("DEP_Nombre");
        });

        modelBuilder.Entity<Doctieneetq>(entity =>
        {
            entity.HasKey(e => new { e.FkdocId, e.FketqId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("doctieneetq");

            entity.HasIndex(e => e.FketqId, "fkEtqDocTienEtq_idx");

            entity.Property(e => e.FkdocId)
                .HasMaxLength(45)
                .HasColumnName("FKDOC_ID");
            entity.Property(e => e.FketqId)
                .HasMaxLength(45)
                .HasColumnName("FKETQ_ID");
            entity.Property(e => e.DoctienetqEstado).HasColumnName("DOCTIENETQ_Estado");

            entity.HasOne(d => d.Fkdoc).WithMany(p => p.Doctieneetqs)
                .HasForeignKey(d => d.FkdocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkDocDocTienEtq");

            entity.HasOne(d => d.Fketq).WithMany(p => p.Doctieneetqs)
                .HasForeignKey(d => d.FketqId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkEtqDocTienEtq");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.DocId).HasName("PRIMARY");

            entity.ToTable("documento");

            entity.HasIndex(e => e.FkmunId, "fkMunDocumento_idx");

            entity.HasIndex(e => e.FkperId, "fkPerDocumento_idx");

            entity.HasIndex(e => e.FktipdocId, "fkTipDocDocumento_idx");

            entity.Property(e => e.DocId)
                .HasMaxLength(45)
                .HasColumnName("DOC_ID");
            entity.Property(e => e.DocDescripcion)
                .HasMaxLength(45)
                .HasColumnName("DOC_Descripcion");
            entity.Property(e => e.DocDocumento)
                .HasColumnType("blob")
                .HasColumnName("DOC_Documento");
            entity.Property(e => e.DocLink)
                .HasMaxLength(45)
                .HasColumnName("DOC_Link");
            entity.Property(e => e.DocTitulo)
                .HasMaxLength(45)
                .HasColumnName("DOC_Titulo");
            entity.Property(e => e.FkmunId)
                .HasMaxLength(45)
                .HasColumnName("FKMUN_ID");
            entity.Property(e => e.FkperId)
                .HasMaxLength(45)
                .HasColumnName("FKPER_ID");
            entity.Property(e => e.FktipdocId)
                .HasMaxLength(45)
                .HasColumnName("FKTIPDOC_ID");

            entity.HasOne(d => d.Fkmun).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.FkmunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkMunDocumento");

            entity.HasOne(d => d.Fkper).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.FkperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPerDocumento");

            entity.HasOne(d => d.Fktipdoc).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.FktipdocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkTipDocDocumento");
        });

        modelBuilder.Entity<Etiqueta>(entity =>
        {
            entity.HasKey(e => e.EtqId).HasName("PRIMARY");

            entity.ToTable("etiqueta");

            entity.Property(e => e.EtqId)
                .HasMaxLength(45)
                .HasColumnName("ETQ_ID");
            entity.Property(e => e.EtqTipo)
                .HasMaxLength(45)
                .HasColumnName("ETQ_Tipo");
        });

        modelBuilder.Entity<Generopersona>(entity =>
        {
            entity.HasKey(e => e.GenId).HasName("PRIMARY");

            entity.ToTable("generopersona");

            entity.Property(e => e.GenId)
                .HasMaxLength(45)
                .HasColumnName("GEN_ID");
            entity.Property(e => e.GenEstado).HasColumnName("GEN_Estado");
            entity.Property(e => e.GenGeneroPersona)
                .HasMaxLength(45)
                .HasColumnName("GEN_GeneroPersona");
        });

        modelBuilder.Entity<Institucion>(entity =>
        {
            entity.HasKey(e => e.InsId).HasName("PRIMARY");

            entity.ToTable("institucion");

            entity.HasIndex(e => e.FkmunId, "fkMunInstitucion_idx");

            entity.Property(e => e.InsId)
                .HasMaxLength(45)
                .HasColumnName("INS_ID");
            entity.Property(e => e.FkmunId)
                .HasMaxLength(45)
                .HasColumnName("FKMUN_ID");
            entity.Property(e => e.InsInstitucion)
                .HasMaxLength(45)
                .HasColumnName("INS_Institucion");

            entity.HasOne(d => d.Fkmun).WithMany(p => p.Institucions)
                .HasForeignKey(d => d.FkmunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkMunInstitucion");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.MunId).HasName("PRIMARY");

            entity.ToTable("municipio");

            entity.HasIndex(e => e.FkdepId, "fkDepMun_idx");

            entity.Property(e => e.MunId)
                .HasMaxLength(45)
                .HasColumnName("MUN_ID");
            entity.Property(e => e.FkdepId)
                .HasMaxLength(45)
                .HasColumnName("FKDEP_ID");
            entity.Property(e => e.MunEstado).HasColumnName("MUN_Estado");
            entity.Property(e => e.MunNombre)
                .HasMaxLength(45)
                .HasColumnName("MUN_Nombre");

            entity.HasOne(d => d.Fkdep).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.FkdepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkDepMun");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.PermId).HasName("PRIMARY");

            entity.ToTable("permiso");

            entity.Property(e => e.PermId)
                .HasMaxLength(45)
                .HasColumnName("PERM_ID");
            entity.Property(e => e.PermPermiso)
                .HasMaxLength(45)
                .HasColumnName("PERM_Permiso");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.PerId).HasName("PRIMARY");

            entity.ToTable("persona");

            entity.HasIndex(e => e.FkgenId, "fkGenPersona_idx");

            entity.HasIndex(e => e.FkinsId, "fkInsPersona_idx");

            entity.HasIndex(e => e.FktiperId, "fkTiPerPersona_idx");

            entity.HasIndex(e => e.FkusuCorreo, "fkUsuPersona_idx");

            entity.Property(e => e.PerId)
                .HasMaxLength(45)
                .HasColumnName("PER_ID");
            entity.Property(e => e.FkgenId)
                .HasMaxLength(45)
                .HasColumnName("FKGEN_ID");
            entity.Property(e => e.FkinsId)
                .HasMaxLength(45)
                .HasColumnName("FKINS_ID");
            entity.Property(e => e.FktiperId)
                .HasMaxLength(45)
                .HasColumnName("FKTIPER_ID");
            entity.Property(e => e.FkusuCorreo)
                .HasMaxLength(45)
                .HasColumnName("FKUSU_Correo");
            entity.Property(e => e.PerApellidoDos)
                .HasMaxLength(45)
                .HasColumnName("PER_ApellidoDos");
            entity.Property(e => e.PerApellidoUno)
                .HasMaxLength(45)
                .HasColumnName("PER_ApellidoUno");
            entity.Property(e => e.PerEdad)
                .HasMaxLength(45)
                .HasColumnName("PER_Edad");
            entity.Property(e => e.PerNombreDos)
                .HasMaxLength(45)
                .HasColumnName("PER_NombreDos");
            entity.Property(e => e.PerNombreUno)
                .HasMaxLength(45)
                .HasColumnName("PER_NombreUno");
            entity.Property(e => e.PerTelefono).HasColumnName("PER_Telefono");

            entity.HasOne(d => d.Fkgen).WithMany(p => p.Personas)
                .HasForeignKey(d => d.FkgenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKGENPersona");

            entity.HasOne(d => d.Fkins).WithMany(p => p.Personas)
                .HasForeignKey(d => d.FkinsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKInsPersona");

            entity.HasOne(d => d.Fktiper).WithMany(p => p.Personas)
                .HasForeignKey(d => d.FktiperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTiPerPersona");

            entity.HasOne(d => d.FkusuCorreoNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.FkusuCorreo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUSUPersona");
        });

        modelBuilder.Entity<Publicacion>(entity =>
        {
            entity.HasKey(e => e.PubliId).HasName("PRIMARY");

            entity.ToTable("publicacion");

            entity.HasIndex(e => e.FkdocId, "fkDocPublicacion_idx");

            entity.Property(e => e.PubliId)
                .HasMaxLength(45)
                .HasColumnName("PUBLI_ID");
            entity.Property(e => e.FkdocId)
                .HasMaxLength(45)
                .HasColumnName("FKDOC_ID");
            entity.Property(e => e.PubliEstado).HasColumnName("PUBLI_Estado");
            entity.Property(e => e.PubliFechaPublicacion).HasColumnName("PUBLI_FechaPublicacion");

            entity.HasOne(d => d.Fkdoc).WithMany(p => p.Publicacions)
                .HasForeignKey(d => d.FkdocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkDocPublicacion");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.RolId)
                .HasMaxLength(45)
                .HasColumnName("ROL_ID");
            entity.Property(e => e.RolRol)
                .HasMaxLength(45)
                .HasColumnName("ROL_Rol");
        });

        modelBuilder.Entity<RolTienePermiso>(entity =>
        {
            entity.HasKey(e => new { e.PfkpermId, e.PfkrolId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasIndex(e => e.PfkrolId, "fkRolRolTienPerm_idx");

            entity.Property(e => e.PfkpermId)
                .HasMaxLength(45)
                .HasColumnName("PFKPERM_ID");
            entity.Property(e => e.PfkrolId)
                .HasMaxLength(45)
                .HasColumnName("PFKROL_ID");
            entity.Property(e => e.RoltienpermFechaAgregacion).HasColumnName("ROLTIENPERM_FechaAgregacion");

            entity.HasOne(d => d.Pfkperm).WithMany(p => p.RolTienePermisos)
                .HasForeignKey(d => d.PfkpermId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkPermRolTienPerm");

            entity.HasOne(d => d.Pfkrol).WithMany(p => p.RolTienePermisos)
                .HasForeignKey(d => d.PfkrolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkRolRolTienPerm");
        });

        modelBuilder.Entity<Tipodocumento>(entity =>
        {
            entity.HasKey(e => e.TipdocId).HasName("PRIMARY");

            entity.ToTable("tipodocumento");

            entity.Property(e => e.TipdocId)
                .HasMaxLength(45)
                .HasColumnName("TIPDOC_ID");
            entity.Property(e => e.TipdocTipo)
                .HasMaxLength(45)
                .HasColumnName("TIPDOC_Tipo");
        });

        modelBuilder.Entity<Tipopersona>(entity =>
        {
            entity.HasKey(e => e.TiperId).HasName("PRIMARY");

            entity.ToTable("tipopersona");

            entity.Property(e => e.TiperId)
                .HasMaxLength(45)
                .HasColumnName("TIPER_ID");
            entity.Property(e => e.TiperTipoPersona)
                .HasMaxLength(45)
                .HasColumnName("TIPER_TipoPersona");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuCorreo).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.FkrolId, "FKRolUsuario_idx");

            entity.Property(e => e.UsuCorreo)
                .HasMaxLength(45)
                .HasColumnName("USU_Correo");
            entity.Property(e => e.FkrolId)
                .HasMaxLength(45)
                .HasColumnName("FKROL_ID");
            entity.Property(e => e.UsuContrasenia)
                .HasMaxLength(45)
                .HasColumnName("USU_Contrasenia");

            entity.HasOne(d => d.Fkrol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkrolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRolUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
