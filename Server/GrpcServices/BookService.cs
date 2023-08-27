using Grpc.Core;
using Newtonsoft.Json;
using Server.Models;
using Server.Protos;

namespace Server.GrpcServices
{
    public class BookService : BookGrpcService.BookGrpcServiceBase
    {
        private readonly ILogger<BookService> _logger;

        public BookService(ILogger<BookService> logger)
        {
            _logger = logger;
        }

        public override async Task<BookList> GetBooks(Empty request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation($"Server: Start: GetBooks method called! {DateTime.UtcNow:hh.mm.ss.ffffff}");

                var books = new[]
                {
                new Book { Title = "Book 1", Author = "Author 1" },
                new Book { Title = "Book 2", Author = "Author 2" },
            };

                var bookList = new BookList();
                bookList.Books.AddRange(books);

                var response = await Task.FromResult(bookList);

                _logger.LogInformation($"Server: Finish: GetBooks method finished! {DateTime.UtcNow:hh.mm.ss.ffffff}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            };
        }

        public override async Task<BookListAsString> GetBooksAsString(Empty request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation($"Server: Start: GetBooksAsString method called! {DateTime.UtcNow:hh.mm.ss.ffffff}");

                var data = new List<BookModel>()
                {
                    new BookModel { Title = "Book 1", Author = "Author 1" },
                    new BookModel { Title = "Book 2", Author = "Author 2" },
                };

                var res = await Task.FromResult(data);

                var response = new BookListAsString();
                response.Data = JsonConvert.SerializeObject(data);

                _logger.LogInformation($"Server: Finish: GetBooksAsString method finished! {DateTime.UtcNow:hh.mm.ss.ffffff}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            };
        }
    }
}
