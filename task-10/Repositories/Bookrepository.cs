using Microsoft.EntityFrameworkCore;
using BooksApi.Data;
using BooksApi.DTOs;
using BooksApi.Models;

namespace BooksApi.Repositories;

/// <summary>
/// Concrete implementation of IBookRepository using EF Core.
/// All database queries live here — services never touch DbContext directly.
/// </summary>
public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    // DbContext is injected — we don't create it, ASP.NET DI provides it
    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Book>> GetAllAsync(int page, int pageSize, string? genre, string? search)
    {
        // Start with a base query — EF builds SQL lazily, nothing runs yet
        // Note: The global query filter (IsDeleted == false) is applied automatically
        IQueryable<Book> query = _context.Books.AsNoTracking(); // AsNoTracking = faster for read-only

        // Apply optional filters
        if (!string.IsNullOrWhiteSpace(genre))
            query = query.Where(b => b.Genre.ToLower() == genre.ToLower());

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(b =>
                b.Title.Contains(search) ||
                b.Author.Contains(search));

        // Count BEFORE pagination (for total pages calculation)
        var totalCount = await query.CountAsync();

        // Apply pagination: Skip past previous pages, take current page size
        var items = await query
            .OrderBy(b => b.Title)       // Always sort for consistent paging
            .Skip((page - 1) * pageSize) // e.g., page 2, size 10 → skip 10
            .Take(pageSize)
            .ToListAsync();              // NOW the SQL actually executes

        return new PagedResult<Book>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        // FindAsync is optimized — checks the EF change tracker cache first
        return await _context.Books.FindAsync(id);
    }

    public async Task<Book?> GetByISBNAsync(string isbn)
    {
        return await _context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.ISBN == isbn);
    }

    public async Task<Book> AddAsync(Book book)
    {
        // Add to the DbContext's "tracked" collection
        _context.Books.Add(book);
        // SaveChanges is called separately (in service) — Unit of Work pattern
        return book;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        // Tell EF this entity has been modified
        _context.Entry(book).State = EntityState.Modified;
        return book;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        // SOFT DELETE: don't actually remove, just mark it
        book.IsDeleted = true;
        book.UpdatedAt = DateTime.UtcNow;

        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Books.AnyAsync(b => b.Id == id);
    }

    public async Task<int> SaveChangesAsync()
    {
        // This is where EF Core actually generates and executes the SQL
        return await _context.SaveChangesAsync();
    }
}