using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using clients.Models;

namespace clients.Context;

public partial class MyprojContext : DbContext
{
    public MyprojContext()
    {
    }

    public MyprojContext(DbContextOptions<MyprojContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientFile> ClientFiles { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=myproj;Username=postgres;password=18b22M02a");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("clients_pk");

            entity.ToTable("clients", "clients");

            entity.Property(e => e.IdClient)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("id_client");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.DateReg).HasColumnName("date_reg");
            entity.Property(e => e.IdGender).HasColumnName("id_gender");
            entity.Property(e => e.Mail)
                .HasColumnType("character varying")
                .HasColumnName("mail");
            entity.Property(e => e.NameClient)
                .HasColumnType("character varying")
                .HasColumnName("name_client");
            entity.Property(e => e.OtchestvoCl)
                .HasColumnType("character varying")
                .HasColumnName("otchestvo_cl");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Photo)
                .HasColumnType("character varying")
                .HasColumnName("photo");
            entity.Property(e => e.SurnameCl)
                .HasColumnType("character varying")
                .HasColumnName("surname_cl");

            entity.HasOne(d => d.IdGenderNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdGender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clients_gender_fk");

            entity.HasMany(d => d.IdTags).WithMany(p => p.IdClients)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("IdTag")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("client_tag_tags_fk"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("client_tag_clients_fk"),
                    j =>
                    {
                        j.HasKey("IdClient", "IdTag").HasName("client_tag_pk");
                        j.ToTable("client_tag", "clients");
                        j.IndexerProperty<int>("IdClient").HasColumnName("id_client");
                        j.IndexerProperty<int>("IdTag").HasColumnName("id_tag");
                    });
        });

        modelBuilder.Entity<ClientFile>(entity =>
        {
            entity.HasKey(e => e.IdClientFile).HasName("client_file_pk");

            entity.ToTable("client_file", "clients");

            entity.Property(e => e.IdClientFile)
                .ValueGeneratedNever()
                .HasColumnName("id_client_file");
            entity.Property(e => e.Filename)
                .HasColumnType("character varying")
                .HasColumnName("filename");
            entity.Property(e => e.IdClient).HasColumnName("id_client");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientFiles)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_file_clients_fk");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.IdGender).HasName("gender_pk");

            entity.ToTable("gender", "clients");

            entity.Property(e => e.IdGender)
                .ValueGeneratedNever()
                .HasColumnName("id_gender");
            entity.Property(e => e.NameGender)
                .HasColumnType("character varying")
                .HasColumnName("name_gender");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.IdTag).HasName("tags_pk");

            entity.ToTable("tags", "clients");

            entity.Property(e => e.IdTag)
                .ValueGeneratedNever()
                .HasColumnName("id_tag");
            entity.Property(e => e.ColorTag)
                .HasColumnType("character varying")
                .HasColumnName("color_tag");
            entity.Property(e => e.NameTag)
                .HasColumnType("character varying")
                .HasColumnName("name_tag");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.IdClientVisit).HasName("visits_pk");

            entity.ToTable("visits", "clients");

            entity.HasIndex(e => e.IdClient, "fki_visits_fk1");

            entity.Property(e => e.IdClientVisit)
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("id_client_visit");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.TimedateVisit)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timedate_visit");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Visits)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("visits_fk1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
