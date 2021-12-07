using Example.Library.Business.BusinessLogic.Services;
using Example.Library.Contracts.Author;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Example.Library.API.Controllers
{
    [ApiController]
    [Route("/books")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(
            IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // <summary>
        /// Get a list of all Authors.
        /// </summary>
        /// <remarks>Get a list of all Authors.</remarks>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("", Name = "GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var resources = await _authorService.GetAllAuthors();
            return Ok(resources);
        }

        // <summary>
        /// Get Author By Id.
        /// </summary>
        /// <param name="id">Id of the Author</param>
        /// <remarks>Get Author.</remarks>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:min(1)}", Name = "GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var resources = await _authorService.GetAuthorById(id);
            return Ok(resources);
        }

        // <summary>
        /// Create new Author.
        /// </summary>
        /// <param name="model">Author model</param>
        /// <remarks>Craete Book.</remarks>
        /// <response code="400">The model is not valid.</response>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreateAuthor")]
        public async Task<IActionResult> CreateBookById([FromBody] AuthorModel model)
        {
            var resources = await _authorService.CreateAuthor(model);
            return Ok(resources);
        }

        // <summary>
        /// Update existing author.
        /// </summary>
        /// <param name="model">Author model</param>
        /// <param name="Id">Author Id</param>
        /// <remarks>Update existing Author.</remarks>
        /// <response code="400">The model is not valid.</response>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}", Name = "UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorModel model)
        {
            var resources = await _authorService.UpdateAuthor(id, model);
            return Ok(resources);
        }

        // <summary>
        /// Delete existing author.
        /// </summary>
        /// <param name="Id">Author Id</param>
        /// <remarks>Delete existing Author.</remarks>
        /// <response code="400">Author is not found.</response>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}", Name = "DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthor(id);
            return NoContent();
        }
    }
}
