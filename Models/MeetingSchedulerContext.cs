using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MeetingScheduler.Models
{
  public partial class MeetingSchedulerContext : DbContext
  {
    public MeetingSchedulerContext()
    {
    }

    public MeetingSchedulerContext(DbContextOptions<MeetingSchedulerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Availabletimes> Availabletimes { get; set; }
    public virtual DbSet<Calendar> Calendar { get; set; }
    public virtual DbSet<Calendaraccess> Calendaraccess { get; set; }
    public virtual DbSet<Meetings> Meetings { get; set; }
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<Usersigninkeys> Usersigninkeys { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Availabletimes>(entity =>
      {
        entity.ToTable("availabletimes");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Calendarid).HasColumnName("calendarid");

        entity.Property(e => e.Editstamp)
                  .HasColumnName("editstamp")
                  .HasDefaultValueSql("now()");

        entity.Property(e => e.Endtime).HasColumnName("endtime");

        entity.Property(e => e.Starttime).HasColumnName("starttime");

        entity.HasOne(d => d.Calendar)
                  .WithMany(p => p.Availabletimes)
                  .HasForeignKey(d => d.Calendarid)
                  .HasConstraintName("availabletimes_calendarid_fkey");
      });

      modelBuilder.Entity<Calendar>(entity =>
      {
        entity.ToTable("calendar");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Description)
                  .HasColumnName("description")
                  .HasColumnType("character varying");

        entity.Property(e => e.Userid)
                  .IsRequired()
                  .HasColumnName("userid")
                  .HasColumnType("character varying");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Calendar)
                  .HasForeignKey(d => d.Userid)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("calendar_userid_fkey");
      });

      modelBuilder.Entity<Calendaraccess>(entity =>
      {
        entity.HasKey(e => new { e.Calendarid, e.Userid })
                  .HasName("calendaraccess_pkey");

        entity.ToTable("calendaraccess");

        entity.Property(e => e.Calendarid).HasColumnName("calendarid");

        entity.Property(e => e.Userid)
                  .HasColumnName("userid")
                  .HasColumnType("character varying");

        entity.Property(e => e.Expire).HasColumnName("expire");

        entity.Property(e => e.Meetingcount)
                  .HasColumnName("meetingcount")
                  .HasDefaultValueSql("'-10'::integer");

        entity.Property(e => e.Meetingminutelength)
                  .HasColumnName("meetingminutelength")
                  .HasDefaultValueSql("'-10'::integer");

        entity.HasOne(d => d.Calendar)
                  .WithMany(p => p.Calendaraccess)
                  .HasForeignKey(d => d.Calendarid)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("calendaraccess_calendarid_fkey");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Calendaraccess)
                  .HasForeignKey(d => d.Userid)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("calendaraccess_userid_fkey");
      });

      modelBuilder.Entity<Meetings>(entity =>
      {
        entity.ToTable("meetings");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Description)
                  .HasColumnName("description")
                  .HasColumnType("character varying");

        entity.Property(e => e.Endtime).HasColumnName("endtime");

        entity.Property(e => e.Requestuserid)
                  .IsRequired()
                  .HasColumnName("requestuserid")
                  .HasColumnType("character varying");

        entity.Property(e => e.Starttime).HasColumnName("starttime");

        entity.Property(e => e.Userid)
                  .IsRequired()
                  .HasColumnName("userid")
                  .HasColumnType("character varying");

        entity.HasOne(d => d.Requestuser)
                  .WithMany(p => p.MeetingsRequestuser)
                  .HasForeignKey(d => d.Requestuserid)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("meetings_requestuserid_fkey");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.MeetingsUser)
                  .HasForeignKey(d => d.Userid)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("meetings_userid_fkey");
      });

      modelBuilder.Entity<Users>(entity =>
      {
        entity.ToTable("users");

        entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .HasColumnType("character varying");

        entity.Property(e => e.Fullname)
                  .HasColumnName("fullname")
                  .HasColumnType("character varying");
      });

      modelBuilder.Entity<Usersigninkeys>(entity =>
      {
        entity.HasKey(e => new { e.Userid, e.Signinkey })
                  .HasName("usersigninkeys_pkey");

        entity.ToTable("usersigninkeys");

        entity.Property(e => e.Userid)
                  .HasColumnName("userid")
                  .HasColumnType("character varying");

        entity.Property(e => e.Signinkey)
                  .HasColumnName("signinkey")
                  .HasColumnType("character varying");

        entity.Property(e => e.Code)
                  .IsRequired()
                  .HasColumnName("code")
                  .HasColumnType("character varying");

        entity.Property(e => e.Expire)
                  .HasColumnName("expire")
                  .HasDefaultValueSql("(now() + '01:00:00'::interval)");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Usersigninkeys)
                  .HasForeignKey(d => d.Userid)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("usersigninkeys_userid_fkey");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
