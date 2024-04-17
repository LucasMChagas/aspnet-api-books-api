using BooksApi.DTO.Author;
using BooksApi.DTO.Book;
using BooksApi.Models;
using BooksApi.Services.Author;
using BooksApi.Services.Book;
using BooksApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookInterface _bookInterface;

        public BookController(IBookInterface bookInterface)
        {
            _bookInterface = bookInterface;
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseViewModel<List<BookModel>>>> ListBooks()
        {
            var books = await _bookInterface.ListBooks();
            return Ok(books);
        }

        [HttpGet("BuscarLivroPorId/{bookId}")]
        public async Task<ActionResult<ResponseViewModel<BookModel>>> GetBookById(int bookId)
        {
            var book = await _bookInterface.GetBookById(bookId);
            return Ok(book);
        }

        [HttpGet("BuscarLivrosPorAutor/{authorId}")]
        public async Task<ActionResult<ResponseViewModel<List<BookModel>>>> GetBooksByAuthorId(int authorId)
        {
            var books = await _bookInterface.GetBookByAuthorId(authorId);
            return Ok(books);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<ResponseViewModel<BookModel>>> CreateBook(BooksCreationDto booksCreationDto)
        {
            var book = await _bookInterface.CreateBook(booksCreationDto);
            return Ok(book);
        }

        [HttpPut("AtualizarLivro")]
        public async Task<ActionResult<ResponseViewModel<BookModel>>> UpdateBook(BookUpdateDto bookUpdateDto)
        {
            var book = await _bookInterface.UpdateBook(bookUpdateDto);
            return Ok(book);
        }

        [HttpDelete("DeletarLivro")]
        public async Task<ActionResult<ResponseViewModel<BookModel>>> DeleteBook(int bookId)
        {
            var book = await _bookInterface.DeleteBook(bookId);
            return Ok(book);
        }
    }
}
