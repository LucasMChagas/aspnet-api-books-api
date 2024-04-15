using BooksApi.Data;
using BooksApi.Models;
using BooksApi.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Services.Author
{
    public class AuthorService : IAuthorInterface
    {
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseViewModel<AuthorModel>> GetAuthorById(int authorId)
        {
            var response = new ResponseViewModel<AuthorModel>();
            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync(author => author.Id == authorId );

                if (author == null)
                {
                    response.Message = "Nenhum autor encontrado!";
                    response.Status = false;
                    return response;
                }

                response.Data = author;
                response.Message = "Autor encontrado!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseViewModel<AuthorModel>> GetAuthorByBookId(int bookId)        
        {
            var response = new ResponseViewModel<AuthorModel>();

            try
            {
                var book = await _context.Books.
                    Include(book => book.Author).
                    FirstOrDefaultAsync(book => book.Id == bookId);

                if (book == null)
                {
                    response.Message = "Nenhum resgistro localizado";
                    response.Status = false;
                    return response;
                }

                response.Message = "Autor localizado";
                response.Data = book.Author;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseViewModel<List<AuthorModel>>> ListAuthors()
        {
            var response = new ResponseViewModel<List<AuthorModel>>();
            try
            {
                var authors = await _context.Authors.ToListAsync();
                response.Data = authors;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
