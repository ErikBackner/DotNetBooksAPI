using MongoDB.Driver;
using DotNetBooksAPI.Models;

namespace DotNetBooksAPI.Data
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public MongoDBService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
            _booksCollection = database.GetCollection<Book>("Books");
        }

        public async Task<List<Book>> GetAllBooks() => await _booksCollection.Find(_ => true).ToListAsync();
        public async Task<Book> GetBookById(string id) => await _booksCollection.Find(book => book.Id == id).FirstOrDefaultAsync();
        public async Task AddBook(Book book) => await _booksCollection.InsertOneAsync(book);
        public async Task UpdateBook(Book book) => await _booksCollection.ReplaceOneAsync(b => b.Id == book.Id, book);
        public async Task<bool> DeleteBook(string id) => (await _booksCollection.DeleteOneAsync(b => b.Id == id)).DeletedCount > 0;
    }
}







