using BooksApi.ViewModel;
using BooksApi.Models;

namespace BooksApi.Services.Author
{
    public interface IAuthorInterface
    {
        Task<ResponseViewModel<List<AuthorModel>>> ListAuthors();
        Task<ResponseViewModel<AuthorModel>> GetAuthorById(int authorId);
        Task<ResponseViewModel<AuthorModel>> GetAuthorByBookId(int bookId);

    }
}
