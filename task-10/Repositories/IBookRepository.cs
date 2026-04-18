using BooksApi.DTOs;
using BooksApi.Models;

namespace BooksApi.Repositories;

// ─────────────────────────────────────────────────────────────────────────────
// REPOSITORY PATTERN
//
// The Repository pattern abstracts data access behind an interface.
// Benefits:
//   1. Swap databases without changing business logic
//   2. Easy to mock in unit tests (just implement the interface)
//   3. Centralizes query logic — no scattered EF queries throughout the app
//
// IBookRepository = the CONTRACT (what operations exist)
// BookRepository  = the IMPLEMENTATION (how they work with EF Core)
// ─────────────────────────────────────────────────────────────────────────────

public interface IBookRepository
{
    Task<PagedResult<Book>> GetAllAsync(int page, int pageSize, string? genre, string? search);
    Task<Book?> GetByIdAsync(int id);
    Task<Book?> GetByISBNAsync(string isbn);
    Task<Book> AddAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<int> SaveChangesAsync();
}