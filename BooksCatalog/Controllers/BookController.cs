using System.Collections.Generic;
using System.Linq;
using BooksCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using BooksCatalog.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BooksCatalog.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/books")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
            if (_bookService.GetAll() != null && _bookService.GetAll().Any()) return;
            _bookService.Add(new Book
            {
                OriginalTitle = "A Life on Our Planet: My Witness Statement and a Vision for the Future",
                Author = "David Attenborough",
                Isbn10 = 1529108276
            });
            _bookService.Add(new Book
            {
                OriginalTitle = "Life on Air",
                Author = "David Attenborough",
                Isbn10 = 1849900010
            });
            _bookService.Add(new Book
            {
                OriginalTitle = "An Improbable Life: The Autobiography",
                Author = "Trevor McDonald",
                Isbn10 = 1474614779
            });
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>All books</returns>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _bookService.GetAll();
        }

        /// <summary>
        /// Get book by id
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>Book</returns>
        [HttpGet("{id:int}")]
        public Book Get(int id)
        {
            return _bookService.Get(id);
        }

        /// <summary>
        /// Add new book to the Catalog
        /// </summary>
        /// <param name="book">Book</param>
        /// <returns>New Book</returns>
        /// <response code="201">Returns the newly created book</response>
        /// <response code="400">If the book is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _bookService.Add(book);
            return Ok(_bookService.Get(id));
        }

        /// <summary>
        /// Update book
        /// </summary>
        /// <param name="book">Book</param>
        /// <returns>Book</returns>
        /// <response code="200">The book was updated</response>
        /// <response code="400">If the book is null</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _bookService.Update(book.Id, book);
            return Ok(book);
        }

        /// <summary>
        /// Delete book by id
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>Deleted book</returns>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var book = _bookService.Get(id);
            if (book == null) return Ok(null);
            _bookService.Delete(id);
            return Ok(book);
        }
    }
}