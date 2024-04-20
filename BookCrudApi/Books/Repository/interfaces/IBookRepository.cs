using BookCrudApi.Books.Model;
using BookCrudApi.Dto;

namespace BookCrudApi.Books.Repository.interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> GetByTitleAsync(string title);
        Task<Book> CreateBook(CreateBookRequest request);
        Task<Book> UpdateBook(int id, UpdateBookRequest request);
        Task<Book> DeleteBookById(int id);

    }
}
