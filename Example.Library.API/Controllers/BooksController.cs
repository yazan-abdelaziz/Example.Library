using Example.Library.Business.BusinessLogic.Services;
using Example.Library.Contracts.Book;
using Lib.AspNetCore.ServerTiming;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Example.Library.API.Controllers
{
    [ApiController]
    [Route("/books")]
    public class BooksController : Controller
    {
        private readonly IServerTiming _serverTiming;
        private readonly IBookService _bookService;

        public BooksController(
            IServerTiming serverTiming,
            IBookService bookService)
        {
            _serverTiming = serverTiming;
            _bookService = bookService;
        }

        // <summary>
        /// Get a list of all Books.
        /// </summary>
        /// <remarks>Get a list of all Books.</remarks>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("", Name = "GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var resources = await _bookService.GetAllBooks();
            return Ok(resources);
        }

        // <summary>
        /// Get Book By Id.
        /// </summary>
        /// <param name="id">Id of the book</param>
        /// <remarks>Get Book.</remarks>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:min(1)}", Name = "GetBookById")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var resources = await _bookService.GetBookById(id);
            return Ok(resources);
        }

        // <summary>
        /// Create new book.
        /// </summary>
        /// <param name="model">Book model</param>
        /// <remarks>Craete Book.</remarks>
        /// <response code="400">The model is not valid.</response>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreateBookById")]
        public async Task<IActionResult> CreateBookById([FromBody] BookModel model)
        {
            var resources = await _bookService.CreateBook(model);
            return Ok(resources);
        }

        // <summary>
        /// Update existing book.
        /// </summary>
        /// <param name="model">Book model</param>
        /// <param name="Id">Book Id</param>
        /// <remarks>Update existing Book.</remarks>
        /// <response code="400">The model is not valid.</response>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}", Name = "UpdateBookById")]
        public async Task<IActionResult> UpdateBookById(int id, [FromBody] BookModel model)
        {
            var resources = await _bookService.UpdateBook(id, model);
            return Ok(resources);
        }

        // <summary>
        /// Delete existing book.
        /// </summary>
        /// <param name="Id">Book Id</param>
        /// <remarks>Delete existing Book.</remarks>
        /// <response code="400">Book is not found.</response>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}", Name = "DeleteBook")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}
