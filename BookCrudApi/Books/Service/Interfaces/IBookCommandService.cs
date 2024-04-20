using BookCrudApi.Books.Model;
using BookCrudApi.Dto;

namespace BookCrudApi.Books.Service.Interfaces
{
    public interface IBookCommandService
    {
        Task<Book> CreateBook(CreateBookRequest request);
        Task<Book> UpdateBook(int id, UpdateBookRequest request);
        Task<Book> DeleteBookById(int id);
    }
}
