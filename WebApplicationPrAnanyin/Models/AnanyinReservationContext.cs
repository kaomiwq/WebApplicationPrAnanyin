using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationPrAnanyin.Models;

public partial class AnanyinReservationContext : DbContext
{
    public AnanyinReservationContext()
    {
    }

    public AnanyinReservationContext(DbContextOptions<AnanyinReservationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=176.62.176.149;Database=AnanyinReservation; Integrated Security=False; TrustServerCertificate=True;user id=sa; password=123mgok2025...; Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC2758907AE1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TableId).HasColumnName("TableID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Table).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Table");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_User");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tables__3214EC276FB92542");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Number).HasMaxLength(10);
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27B3B57498");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
