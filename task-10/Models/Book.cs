namespace BooksApi.Models;

/// <summary>
/// The Book entity — this is what gets stored in the database.
/// In EF Core, a class like this is called an "Entity".
/// Each property maps to a column in the Books table.
/// </summary>
public class Book
{
    /// <summary>
    /// EF Core convention: a property named "Id" becomes the Primary Key automatically.
    /// </summary>
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int PublishedYear { get; set; }

    public string Genre { get; set; } = string.Empty;

    /// <summary>
    /// Soft delete: instead of removing from DB, we mark it deleted.
    /// This is a common real-world pattern for data safety.
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}