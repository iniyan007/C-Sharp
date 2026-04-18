using Microsoft.EntityFrameworkCore;
using BooksApi.Models;

namespace BooksApi.Data;

// ─────────────────────────────────────────────────────────────────────────────
// WHAT IS DbContext?
//
// DbContext is EF Core's gateway to the database. Think of it as:
//   - A "session" with the database
//   - It tracks changes to your entities
//   - It translates C# LINQ queries into SQL
//   - Each DbSet<T> represents a table
// ─────────────────────────────────────────────────────────────────────────────

public class AppDbContext : DbContext
{
    // Constructor takes options (like connection string) injected via DI
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet<Book> = the "Books" table in the database
    public DbSet<Book> Books { get; set; }

    /// <summary>
    /// OnModelCreating lets us configure entities using the Fluent API —
    /// an alternative to data annotations, more powerful for complex configs.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(entity =>
        {
            // Configure table name explicitly (optional — EF would use "Books" by convention)
            entity.ToTable("Books");

            // Primary key (redundant here, EF finds "Id" automatically — shown for learning)
            entity.HasKey(b => b.Id);

            // Column configurations
            entity.Property(b => b.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(b => b.Author)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(b => b.ISBN)
                  .IsRequired()
                  .HasMaxLength(13);

            // Unique index on ISBN — no two books can have the same ISBN
            entity.HasIndex(b => b.ISBN)
                  .IsUnique();

            entity.Property(b => b.Price)
                  .HasColumnType("decimal(10,2)");

            // Global Query Filter: automatically excludes soft-deleted records
            // from ALL queries — you never see deleted books unless you explicitly ignore this filter
            entity.HasQueryFilter(b => !b.IsDeleted);
        });

        // Seed data — pre-populate the database with sample books
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "Clean Code",
                Author = "Robert C. Martin",
                ISBN = "9780132350884",
                Price = 35.99m,
                PublishedYear = 2008,
                Genre = "Programming",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 2,
                Title = "The Pragmatic Programmer",
                Author = "David Thomas",
                ISBN = "9780135957059",
                Price = 49.99m,
                PublishedYear = 2019,
                Genre = "Programming",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 3,
                Title = "Design Patterns",
                Author = "Gang of Four",
                ISBN = "9780201633610",
                Price = 54.99m,
                PublishedYear = 1994,
                Genre = "Software Architecture",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}