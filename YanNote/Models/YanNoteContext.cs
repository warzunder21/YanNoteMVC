using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace YanNote.Models
{
    public partial class YanNoteContext : DbContext
    {
        public YanNoteContext()
        {
        }

        public YanNoteContext(DbContextOptions<YanNoteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<NoteTag> NoteTag { get; set; }
        public virtual DbSet<Rem> Rem { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YanNote;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Note");

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<NoteTag>(entity =>
            {
                entity.HasKey(e => new { e.NotesId, e.TagsId });

                entity.ToTable("NoteTag");

                entity.HasIndex(e => e.TagsId, "IX_NoteTag_TagsId");

                entity.HasOne(d => d.Notes)
                    .WithMany(p => p.NoteTags)
                    .HasForeignKey(d => d.NotesId);

                entity.HasOne(d => d.Tags)
                    .WithMany(p => p.NoteTags)
                    .HasForeignKey(d => d.TagsId);
            });

            modelBuilder.Entity<Rem>(entity =>
            {
                entity.ToTable("Rem");

                entity.HasIndex(e => e.NoteId, "IX_Rem_NoteId")
                    .IsUnique();

                entity.HasOne(d => d.Note)
                    .WithOne(p => p.Rem)
                    .HasForeignKey<Rem>(d => d.NoteId);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Name).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
