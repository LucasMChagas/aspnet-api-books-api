using BooksApi.DTO.Author;
using BooksApi.DTO.Book;
using BooksApi.Models;
using BooksApi.ViewModel;

namespace BooksApi.Services.Book
{
    public interface IBookInterface
    {
        Task<ResponseViewModel<List<BookModel>>> ListBooks();
        Task<ResponseViewModel<BookModel>> GetBookById(int bookId);
        Task<ResponseViewModel<List<BookModel>>> GetBookByAuthorId(int authorId);
        Task<ResponseViewModel<BookModel>> CreateBook(BooksCreationDto bookDto);
        Task<ResponseViewModel<BookModel>> UpdateBook(BookUpdateDto authorModel);
        Task<ResponseViewModel<BookModel>> DeleteBook(int bookId);
    }
}
