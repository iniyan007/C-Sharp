using BooksApi.DTOs;

namespace BooksApi.Services;

// ─────────────────────────────────────────────────────────────────────────────
// SERVICE LAYER
//
// The Service layer contains BUSINESS LOGIC — the rules of your application.
// It sits between the Controller (HTTP) and Repository (DB).
//
// Separation of concerns:
//   Controller  → Handle HTTP (parse request, return response)
//   Service     → Business rules (validation, orchestration)
//   Repository  → Data access (DB queries)
//
// This makes each layer independently testable and replaceable.
// ─────────────────────────────────────────────────────────────────────────────

public interface IBookService
{
    Task<PagedResult<BookResponseDto>> GetBooksAsync(int page, int pageSize, string? genre, string? search);
    Task<BookResponseDto?> GetBookByIdAsync(int id);
    Task<BookResponseDto> CreateBookAsync(CreateBookDto dto);
    Task<BookResponseDto?> UpdateBookAsync(int id, UpdateBookDto dto);
    Task<bool> DeleteBookAsync(int id);
}