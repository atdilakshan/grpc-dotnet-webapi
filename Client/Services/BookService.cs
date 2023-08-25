using Client.Protos;
using Grpc.Net.Client;

namespace Client.Services
{
    public class BookService
    {
        private readonly ILogger<BookService> _logger;
        private readonly GrpcChannel _channel;
        private readonly BookGrpcService.BookGrpcServiceClient _client;

        public BookService(ILogger<BookService> logger)
        {
            this._logger = logger;
            _channel = GrpcChannel.ForAddress("https://localhost:7051");
            _client = new BookGrpcService.BookGrpcServiceClient(_channel);
        }

        public async Task<BookList> GetBooksAsync()
        {
            try
            {
                _logger.LogInformation($"Client: Start: GetBooks method called! {DateTime.UtcNow}");
                var request = new Empty();

                var response = await _client.GetBooksAsync(request);
                _logger.LogInformation($"Client: Finish: GetBooks method finished! {DateTime.UtcNow}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
