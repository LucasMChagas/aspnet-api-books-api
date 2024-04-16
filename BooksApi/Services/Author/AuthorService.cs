using BooksApi.Data;
using BooksApi.DTO.Author;
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
                response.Status = true;
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
                response.Status = true;
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
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseViewModel<AuthorModel>> CreateAuthor(AuthorsCreationDto authorDto)
        {
            var response = new ResponseViewModel<AuthorModel>();

            try
            {
                var author = new AuthorModel()
                {
                    Name = authorDto.Name,
                    LastName = authorDto.LastName,
                };

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                var listAuthor = await _context.Authors.ToListAsync();
                response.Data = listAuthor[listAuthor.Count - 1];
                response.Message = "Autor criado com sucesso";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseViewModel<AuthorModel>> UpdateAuthor(AuthorUpdateDto authorModel)
        {
            var response = new ResponseViewModel<AuthorModel>();

            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == authorModel.Id);

                if (author == null)
                {
                    response.Message = "Nenhum autor localizado";
                    response.Status = false;
                    return response;
                }

                author.Name = authorModel.Name;
                author.LastName = authorModel.LastName;

                _context.Authors.Update(author);
                await _context.SaveChangesAsync();

                response.Message = "Autor atualizado com sucesso!";
                response.Status = true;
                response.Data = author;
                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseViewModel<AuthorModel>> DeleteAuthor(int authorId)
        {
            var response = new ResponseViewModel<AuthorModel>();

            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == authorId);

                if (author == null)
                {
                    response.Message = "Nenhum autor localizado";
                    response.Status = false;
                    return response;
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                response.Message = "Autor excluído com sucesso!";
                response.Status = true;
                response.Data = author;
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
