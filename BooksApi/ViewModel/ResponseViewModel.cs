namespace BooksApi.ViewModel
{
    public class ResponseViewModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
