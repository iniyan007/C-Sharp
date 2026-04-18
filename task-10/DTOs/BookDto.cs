using System.ComponentModel.DataAnnotations;

namespace BooksApi.DTOs;

// ─────────────────────────────────────────────────────────────────────────────
// WHY DTOs (Data Transfer Objects)?
//
// We NEVER expose our database entity (Book) directly to the API caller.
// Reasons:
//   1. Security — hide internal fields (IsDeleted, audit fields)
//   2. Flexibility — API shape can evolve independently of the DB schema
//   3. Validation — validate input without polluting the entity
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>
/// Used when a CLIENT sends data to CREATE a new book.
/// Data annotations provide automatic validation — no manual if-checks needed!
/// </summary>
public class CreateBookDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required")]
    [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
    public string Author { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^(?:\d{10}|\d{13})$", ErrorMessage = "ISBN must be 10 or 13 digits")]
    public string ISBN { get; set; } = string.Empty;

    [Range(0.01, 9999.99, ErrorMessage = "Price must be between 0.01 and 9999.99")]
    public decimal Price { get; set; }

    [Range(1000, 2100, ErrorMessage = "Published year must be between 1000 and 2100")]
    public int PublishedYear { get; set; }

    [Required]
    public string Genre { get; set; } = string.Empty;
}

/// <summary>
/// Used when a CLIENT sends data to UPDATE an existing book.
/// All fields are optional — client only sends what they want to change.
/// </summary>
public class UpdateBookDto
{
    [StringLength(200, MinimumLength = 1)]
    public string? Title { get; set; }

    [StringLength(100)]
    public string? Author { get; set; }

    [Range(0.01, 9999.99)]
    public decimal? Price { get; set; }

    public int? PublishedYear { get; set; }

    public string? Genre { get; set; }
}

/// <summary>
/// Used when the SERVER sends data BACK to the client.
/// This controls exactly what the client can see.
/// </summary>
public class BookResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PublishedYear { get; set; }
    public string Genre { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    // Notice: IsDeleted is NOT here — clients don't need to know about it
}

/// <summary>
/// Wraps paginated results — a real-world pattern for list endpoints.
/// </summary>
public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
}