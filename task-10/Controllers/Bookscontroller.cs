using Microsoft.AspNetCore.Mvc;
using BooksApi.DTOs;
using BooksApi.Services;

namespace BooksApi.Controllers;

// ─────────────────────────────────────────────────────────────────────────────
// CONTROLLER
//
// Controllers handle HTTP requests. They:
//   1. Parse incoming request data
//   2. Call the service layer
//   3. Return appropriate HTTP responses
//
// Controllers should be THIN — no business logic here, just HTTP orchestration.
//
// [ApiController]    → Enables automatic model validation, problem details
// [Route]            → Sets the base URL path for all actions in this controller
// ─────────────────────────────────────────────────────────────────────────────

[ApiController]
[Route("api/[controller]")]  // [controller] = "books" (from "BooksController")
[Produces("application/json")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookService bookService, ILogger<BooksController> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    // ─── GET /api/books ─────────────────────────────────────────────────────

    /// <summary>
    /// Get all books with optional filtering and pagination.
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Items per page (default: 10, max: 100)</param>
    /// <param name="genre">Filter by genre (optional)</param>
    /// <param name="search">Search by title or author (optional)</param>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<BookResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<BookResponseDto>>> GetBooks(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? genre = null,
        [FromQuery] string? search = null)
    {
        // [FromQuery] means values come from URL: /api/books?page=2&genre=Programming
        var result = await _bookService.GetBooksAsync(page, pageSize, genre, search);
        return Ok(result); // 200 OK
    }

    // ─── GET /api/books/5 ───────────────────────────────────────────────────

    /// <summary>
    /// Get a specific book by ID.
    /// </summary>
    [HttpGet("{id:int}")]   // {id:int} = route constraint: only matches integers
    [ProducesResponseType(typeof(BookResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookResponseDto>> GetBook(int id)
    {
        // [FromRoute] is implicit here — {id} in the route maps to the id parameter
        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
            return NotFound(new { Message = $"Book with ID {id} was not found." }); // 404

        return Ok(book); // 200
    }

    // ─── POST /api/books ────────────────────────────────────────────────────

    /// <summary>
    /// Create a new book.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(BookResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<BookResponseDto>> CreateBook([FromBody] CreateBookDto dto)
    {
        // [FromBody] → data comes from the JSON request body
        // [ApiController] automatically returns 400 if model validation fails
        // (the Data Annotations on CreateBookDto are checked before this runs)

        var created = await _bookService.CreateBookAsync(dto);

        // 201 Created + Location header pointing to the new resource
        // This is the REST standard for successful resource creation
        return CreatedAtAction(
            nameof(GetBook),         // The action to get this resource
            new { id = created.Id }, // Route values for that action
            created                  // The created resource in the body
        );
    }

    // ─── PUT /api/books/5 ───────────────────────────────────────────────────

    /// <summary>
    /// Update an existing book (partial update supported).
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(BookResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookResponseDto>> UpdateBook(int id, [FromBody] UpdateBookDto dto)
    {
        var updated = await _bookService.UpdateBookAsync(id, dto);

        if (updated == null)
            return NotFound(new { Message = $"Book with ID {id} was not found." });

        return Ok(updated);
    }

    // ─── DELETE /api/books/5 ────────────────────────────────────────────────

    /// <summary>
    /// Delete a book (soft delete — data is preserved in the database).
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await _bookService.DeleteBookAsync(id);

        if (!deleted)
            return NotFound(new { Message = $"Book with ID {id} was not found." });

        return NoContent(); // 204 — success, but no body to return
    }
}