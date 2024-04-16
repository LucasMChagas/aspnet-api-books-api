using BooksApi.DTO.Author;
using BooksApi.Models;
using BooksApi.Services.Author;
using BooksApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpPost("CriarAutor")]
        public async Task<ActionResult<ResponseViewModel<AuthorModel>>> CreateAuthor(AuthorsCreationDto authorsCreationDto)
        {
            var author = await _authorInterface.CreateAuthor(authorsCreationDto);
            return Ok(author);
        }

        [HttpPut("AtualizarAutor")]
        public async Task<ActionResult<ResponseViewModel<AuthorModel>>> UpdateAuthor(AuthorUpdateDto authorUpdateDto)
        {
            var author = await _authorInterface.UpdateAuthor(authorUpdateDto);
            return Ok(author);           
        }

        [HttpDelete("DeletarAutor")]
        public async Task<ActionResult<ResponseViewModel<AuthorModel>>> DeleteAuthor(int authorId)
        {
            var author = await _authorInterface.DeleteAuthor(authorId);
            return Ok(author);
        }
    }
}
