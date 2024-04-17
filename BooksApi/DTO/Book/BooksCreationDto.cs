namespace BooksApi.DTO.Book
{
    public class BooksCreationDto
    {        
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
    }
}
