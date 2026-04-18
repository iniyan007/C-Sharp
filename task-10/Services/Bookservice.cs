using BooksApi.DTOs;
using BooksApi.Models;
using BooksApi.Repositories;
using Microsoft.Extensions.Logging;

namespace BooksApi.Services;

/// <summary>
/// Contains all business logic for book management.
/// This class has no knowledge of HTTP — it just works with data.
/// </summary>
public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly ILogger<BookService> _logger;

    // Both dependencies are injected — we never use "new BookRepository()"
    public BookService(IBookRepository repository, ILogger<BookService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResult<BookResponseDto>> GetBooksAsync(int page, int pageSize, string? genre, string? search)
    {
        _logger.LogInformation("Fetching books: page={Page}, size={PageSize}, genre={Genre}, search={Search}",
            page, pageSize, genre, search);

        // Clamp values to safe ranges — business rule
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var result = await _repository.GetAllAsync(page, pageSize, genre, search);

        // Map entity → DTO using our helper method
        return new PagedResult<BookResponseDto>
        {
            Items = result.Items.Select(MapToDto),
            TotalCount = result.TotalCount,
            Page = result.Page,
            PageSize = result.PageSize
        };
    }

    public async Task<BookResponseDto?> GetBookByIdAsync(int id)
    {
        _logger.LogInformation("Fetching book with ID {BookId}", id);

        var book = await _repository.GetByIdAsync(id);

        if (book == null)
        {
            _logger.LogWarning("Book with ID {BookId} not found", id);
            return null;
        }

        return MapToDto(book);
    }

    public async Task<BookResponseDto> CreateBookAsync(CreateBookDto dto)
    {
        _logger.LogInformation("Creating book with ISBN {ISBN}", dto.ISBN);

        // Business Rule: ISBN must be unique
        var existing = await _repository.GetByISBNAsync(dto.ISBN);
        if (existing != null)
        {
            // Custom exception — handled globally by our middleware
            throw new InvalidOperationException($"A book with ISBN '{dto.ISBN}' already exists.");
        }

        // Map DTO → Entity (never let the client set Id, CreatedAt, etc.)
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            ISBN = dto.ISBN,
            Price = dto.Price,
            PublishedYear = dto.PublishedYear,
            Genre = dto.Genre,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(book);
        await _repository.SaveChangesAsync(); // Commit the transaction

        _logger.LogInformation("Book created with ID {BookId}", book.Id);

        return MapToDto(book);
    }

    public async Task<BookResponseDto?> UpdateBookAsync(int id, UpdateBookDto dto)
    {
        _logger.LogInformation("Updating book with ID {BookId}", id);

        var book = await _repository.GetByIdAsync(id);
        if (book == null) return null;

        // Only update fields that were actually provided (partial update / PATCH-style)
        if (dto.Title != null)         book.Title = dto.Title;
        if (dto.Author != null)        book.Author = dto.Author;
        if (dto.Price.HasValue)        book.Price = dto.Price.Value;
        if (dto.PublishedYear.HasValue) book.PublishedYear = dto.PublishedYear.Value;
        if (dto.Genre != null)         book.Genre = dto.Genre;

        book.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(book);
        await _repository.SaveChangesAsync();

        return MapToDto(book);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        _logger.LogInformation("Deleting book with ID {BookId}", id);

        var deleted = await _repository.DeleteAsync(id);
        if (!deleted) return false;

        await _repository.SaveChangesAsync();
        return true;
    }

    // ─── Private Helpers ────────────────────────────────────────────────────

    /// <summary>
    /// Maps a Book entity to a BookResponseDto.
    /// In larger projects, you'd use AutoMapper library for this.
    /// </summary>
    private static BookResponseDto MapToDto(Book book) => new()
    {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        ISBN = book.ISBN,
        Price = book.Price,
        PublishedYear = book.PublishedYear,
        Genre = book.Genre,
        CreatedAt = book.CreatedAt,
        UpdatedAt = book.UpdatedAt
    };
}