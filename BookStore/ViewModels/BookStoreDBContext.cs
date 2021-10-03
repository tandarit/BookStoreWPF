using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

#nullable disable

namespace BookStore.ViewModels
{
    public partial class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options)
        {
           
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<BookFormat> BookFormats { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Format> Formats { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=EPHUBUDW02FA;Initial Catalog=BookStoreDB;Integrated Security=True");
                //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["BookStoreDatabase"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.AuthorFirstName)
                    .IsRequired()
                    .HasMaxLength(155)
                    .IsUnicode(false);

                entity.Property(e => e.AuthorLastName)
                    .IsRequired()
                    .HasMaxLength(155)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("ntext");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK_Books_Publishers");
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_BookAuthors_Authors");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_BookAuthors_Books");
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasKey(e => e.BookCategoryId)
                    .HasName("PK__BookCate__BB9E06924C15C81D");

                entity.Property(e => e.BookCategoryId).HasColumnName("BookCategoryId");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookCategories)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_BookCategories_Books");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.BookCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_BookCategories_Categories");
            });

            modelBuilder.Entity<BookFormat>(entity =>
            {
                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookFormats)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_BookFormats_Books");

                entity.HasOne(d => d.Format)
                    .WithMany(p => p.BookFormats)
                    .HasForeignKey(d => d.FormatId)
                    .HasConstraintName("FK_BookFormats_Formats");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Format>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.FormatName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(e => e.PublisherName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
