using BookCrudApi.Books.Model;

namespace BookCrudApi.Books.Service.Interfaces
{
    public interface IBookQueryService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> GetByTitle(string title);
    }
}
