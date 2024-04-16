using BooksApi.ViewModel;
using BooksApi.Models;
using BooksApi.DTO.Author;

namespace BooksApi.Services.Author
{
    public interface IAuthorInterface
    {
        Task<ResponseViewModel<List<AuthorModel>>> ListAuthors();
        Task<ResponseViewModel<AuthorModel>> GetAuthorById(int authorId);
        Task<ResponseViewModel<AuthorModel>> GetAuthorByBookId(int bookId);
        Task<ResponseViewModel<AuthorModel>> CreateAuthor(AuthorsCreationDto authorDto);
        Task<ResponseViewModel<AuthorModel>> UpdateAuthor(AuthorUpdateDto authorModel);
        Task<ResponseViewModel<AuthorModel>> DeleteAuthor(int authorId);

    }
}
