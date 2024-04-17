using BooksApi.Data;
using BooksApi.DTO.Book;
using BooksApi.Models;
using BooksApi.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BooksApi.Services.Book
{
    public class BookService : IBookInterface
    {
        private readonly AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseViewModel<BookModel>> CreateBook(BooksCreationDto bookDto)
        {
            var response = new ResponseViewModel<BookModel>();

            try
            {
                var book = new BookModel()
                {
                    Title = bookDto.Title,
                    Author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == bookDto.AuthorId)                    
                };

                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                var listBook = await _context.Books.ToListAsync();
                response.Data = listBook[listBook.Count - 1];
                response.Message = "Livro criado com sucesso";
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

        public async Task<ResponseViewModel<BookModel>> DeleteBook(int bookId)
        {
            var response = new ResponseViewModel<BookModel>();

            try
            {
                var book = await _context.Books
                    .Include(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Id == bookId);

                if (book == null)
                {
                    response.Message = "Nenhum livro encontrado!";
                    response.Status = false;
                    return response;
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                response.Message = "Livro excluído com sucesso!";
                response.Status = true;
                response.Data = book;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseViewModel<List<BookModel>>> GetBookByAuthorId(int authorId)
        {
            var response = new ResponseViewModel<List<BookModel>>();

            try
            {
                var author = await _context.Authors.
                    Include(author => author.Books).
                    FirstOrDefaultAsync(author => author.Id == authorId);

                if (author == null)
                {
                    response.Message = "Nenhum resgistro localizado";
                    response.Status = false;
                    return response;
                }

                response.Message = "Livros localizados";
                response.Data = author.Books.ToList();
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

        public async Task<ResponseViewModel<BookModel>> GetBookById(int bookId)
        {
            var response = new ResponseViewModel<BookModel>();
            try
            {
                var book = await _context.Books
                    .Include(x => x.Author)
                    .FirstOrDefaultAsync(book => book.Id == bookId);

                if (book == null)
                {
                    response.Message = "Nenhum livro encontrado!";
                    response.Status = false;
                    return response;
                }

                response.Data = book;
                response.Message = "Livro encontrado!";
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

        public async Task<ResponseViewModel<List<BookModel>>> ListBooks()
        {
            var response = new ResponseViewModel<List<BookModel>>();
            try
            {
                var books = await _context.Books
                    .Include(x => x.Author)
                    .ToListAsync();

                response.Data = books;
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

        public async Task<ResponseViewModel<BookModel>> UpdateBook(BookUpdateDto bookModel)
        {
            var response = new ResponseViewModel<BookModel>();

            try
            {
                var book = await _context.Books
                    .Include(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Id == bookModel.Id);

                if (book == null)
                {
                    response.Message = "Nenhum livro localizado";
                    response.Status = false;
                    return response;
                }

                book.Title = bookModel.Title;                

                _context.Books.Update(book);
                await _context.SaveChangesAsync();

                response.Message = "Livro atualizado com sucesso!";
                response.Status = true;
                response.Data = book;
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
