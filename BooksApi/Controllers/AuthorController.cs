using BooksApi.Models;
using BooksApi.Services.Author;
using BooksApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorInterface _authorInterface;
        public AuthorController(IAuthorInterface authorInterface)
        {
            _authorInterface = authorInterface;
        }

        [HttpGet("ListarAutores")]
        public async Task<ActionResult<ResponseViewModel<List<AuthorModel>>>> ListAuthors()
        {
            var authors = await _authorInterface.ListAuthors();
            return Ok(authors);
        }

        [HttpGet("BuscarAutorPorId/{authorId}")]
        public async Task<ActionResult<ResponseViewModel<AuthorModel>>> GetAuthorById(int authorId)
        {
            var author = await _authorInterface.GetAuthorById(authorId);
            return Ok(author);
        }

        [HttpGet("BuscarAutorPorLivroId/{bookId}")]
        public async Task<ActionResult<ResponseViewModel<AuthorModel>>> GetAuthorByBookId(int bookId)
        {
            var author = await _authorInterface.GetAuthorByBookId(bookId);
            return Ok(author);
        }
    }
}
